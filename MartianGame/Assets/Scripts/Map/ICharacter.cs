using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map
{
    public interface ICharacter
    {
        void MoveForward();
        void Rotate(RotationSide rotationSide);

        int GetX();
        int GetY();
        Action<MoveFailedArgs> OnMoveFailed { get; }
        Action OnMoveFinished { get; }
        Action<RotationFailedArgs> OnRotationFailed { get; }
        Action OnRotationFinished { get; }
    }

    public class RotationFailedArgs
    {
        RotationSide _rotationSide;
        RotationFailType _rotationFailSide;

        public RotationFailedArgs(RotationSide rotationSide, RotationFailType rotationFailType)
        {
            _rotationSide = rotationSide;
            _rotationFailSide = rotationFailType;
        }
    }

    public class MoveFailedArgs
    {
        public int XFrom, YFrom, XTo, YTo;
        public MoveFailType FailType;

        public MoveFailedArgs(int xFrom, int yFrom, int xTo, int yTo, MoveFailType failType)
        {
            XFrom = xFrom;
            YFrom = yFrom;
            XTo = xTo;
            YTo = yTo;
            FailType = failType;
        }
    }

    public enum MoveFailType
    {
        ObstacleAhead,
        AlreadyMoving,
        AlreadyRotating
    }
    public enum RotationFailType
    {
        AlreadyRotating,
        AlreadyMoving
    }
    public enum RotationSide
    {
        Left,
        Right
    }
}
