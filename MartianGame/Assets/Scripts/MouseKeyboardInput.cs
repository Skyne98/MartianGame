using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseKeyboardInput : MonoBehaviour, IInput
{
    Action<ArmDownGestureArgs> _downGesture;
    Action<ArmGrabbedGestureArgs> _grabbedGesture;
    Action<ArmReleasedGestureArgs> _releasedGesture;
    Action<ScaleGestureArgs> _scaleGesture;

    bool grabbed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScaleGesture(new ScaleGestureArgs(Input.mousePosition.y / Screen.height));
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            OnArmDownGesture(new ArmDownGestureArgs(Arm.Left));
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            OnArmDownGesture(new ArmDownGestureArgs(Arm.Right));
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnArmGrabbedGesture(new ArmGrabbedGestureArgs(Arm.Left, GetArmPosition(Arm.Left)));
            grabbed = true;
        }
        if (Input.GetMouseButtonUp(0) && grabbed)
        {
            OnArmReleasedGesture(new ArmReleasedGestureArgs(Arm.Left, GetArmPosition(Arm.Left)));
            grabbed = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnArmGrabbedGesture(new ArmGrabbedGestureArgs(Arm.Right, GetArmPosition(Arm.Right)));
            grabbed = true;
        }
        if (Input.GetMouseButtonUp(1) && grabbed)
        {
            OnArmReleasedGesture(new ArmReleasedGestureArgs(Arm.Right, GetArmPosition(Arm.Right)));
            grabbed = false;
        }
    }

    public Action<ArmDownGestureArgs> OnArmDownGesture
    {
        get
        {
            return _downGesture;
        }

        set
        {
            _downGesture = value;
        }
    }

    public Action<ArmGrabbedGestureArgs> OnArmGrabbedGesture
    {
        get
        {
            return _grabbedGesture;
        }

        set
        {
            _grabbedGesture = value;
        }
    }

    public Action<ArmReleasedGestureArgs> OnArmReleasedGesture
    {
        get
        {
            return _releasedGesture;
        }

        set
        {
            _releasedGesture = value;
        }
    }

    public Action<ScaleGestureArgs> OnScaleGesture
    {
        get
        {
            return _scaleGesture;
        }

        set
        {
            _scaleGesture = value;
        }
    }

    public Vector2 GetArmPosition(Arm arm)
    {
        return Input.mousePosition;
    }

    public bool GetArmPressed(Arm arm)
    {
        if (arm == Arm.Left)
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.GetMouseButton(1);
        }
    }
}
