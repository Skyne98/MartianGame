using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MartianGame.Scripts
{
    public class MouseKeyboardInput : MonoBehaviour, IInput
    {
        public Action<ArmDownGestureArgs> OnArmDownGesture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Action<ArmGrabbedGestureArgs> OnArmGrabbedGesture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Action<ArmReleasedGestureArgs> OnArmReleasedGesture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Action<ScaleGestureArgs> OnScaleGesture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
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
}
