using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class DebugScript : MonoBehaviour
{
    MouseKeyboardInput _mouseKeyboardInput;
    bool dragging = false;

    // Use this for initialization
    void Start() {
        _mouseKeyboardInput = GetComponent<MouseKeyboardInput>();
        _mouseKeyboardInput.OnArmGrabbedGesture += OnDragStart;
        _mouseKeyboardInput.OnArmReleasedGesture += OnDrawStop;
        _mouseKeyboardInput.OnArmDownGesture += OnDown;
        _mouseKeyboardInput.OnScaleGesture += OnScale;
    }

    private void OnScale(ScaleGestureArgs obj)
    {
        Debug.Log("Scale: " + obj.Value);
    }

    private void OnDown(ArmDownGestureArgs obj)
    {
        Debug.Log("Arm " + obj.Arm.ToString() + " is down");
    }

    private void OnDrawStop(ArmReleasedGestureArgs obj)
    {
        dragging = false;
        Debug.Log("Dragging stopped. Arm: " + obj.Arm.ToString());
    }

    private void OnDragStart(ArmGrabbedGestureArgs obj)
    {
        dragging = true;
        Debug.Log("Dragging started. Arm: " + obj.Arm.ToString());
    }

    // Update is called once per frame
    void Update() {
        if (dragging)
        {
            Debug.Log("Dragging");
        }
    }
}
