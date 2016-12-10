using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IInput
    {
        Vector2 GetArmPosition(Arm arm);
        bool GetArmPressed(Arm arm); 

        Action<ScaleGestureArgs> OnScaleGesture { get; set; }
        Action<ArmDownGestureArgs> OnArmDownGesture { get; set; }
        Action<ArmGrabbedGestureArgs> OnArmGrabbedGesture { get; set; }
        Action<ArmReleasedGestureArgs> OnArmReleasedGesture { get; set; }
    }

    public class ScaleGestureArgs
    {
        public float Value; //0 - Arms together 1 - Arms are fully stretched 
    }

    public class ArmDownGestureArgs
    {
        public Arm Arm;
    }

    public class ArmGrabbedGestureArgs
    {
        public Arm Arm;
        public Vector2 Position;
    }

    public class ArmReleasedGestureArgs
    {
        public Arm Arm;
        public Vector2 Position;
    }

    public enum Arm
    {
        Left,
        Right
    }
}
