using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Character : MonoBehaviour, ICharacter
    {
        Tile _currentTile;
        public float MoveSpeed = 1f;
        float _currentMove = 0f;

        int _currentRotation = 0; // 0 - UP | 1 - RIGHT | 2 - DOWN | 3 - LEFT
        float _currentRotating = 0f;
        int _rotationGoal = -1;
        TileGameObjectsController _tileGameObjectsController;
        Animator _animator;

        Tile _goalTile;

        Action<MoveFailedArgs> _onMoveFailed;
        Action _onMoveFinished;
        Action<RotationFailedArgs> _onRotationFailed;
        Action _onRotationFinished;

        //DEBUG
        public bool Move = false;
        public bool Turn = false;

        public void Initialize(Tile currentTile, TileGameObjectsController tileGameObjectsController)
        {
            _tileGameObjectsController = tileGameObjectsController;
            _currentTile = currentTile;
            transform.position = new Vector3(_currentTile.X * Tile.TileSize, _currentTile.Y * Tile.TileSize, -0.05f);
            _animator = GetComponent<Animator>();
        }

        public Action<MoveFailedArgs> OnMoveFailed
        {
            get
            {
                return _onMoveFailed;
            }
        }

        public Action OnMoveFinished
        {
            get
            {
                return _onMoveFinished;
            }
        }

        public Action<RotationFailedArgs> OnRotationFailed
        {
            get
            {
                return _onRotationFailed;
            }
        }

        public Action OnRotationFinished
        {
            get
            {
                return _onRotationFinished;
            }
        }

        public int GetX()
        {
            return _currentTile.X;
        }

        public int GetY()
        {
            return _currentTile.Y;
        }

        Tile GetTileAhead()
        {
            if (_currentRotation == 0)
            {
                return _tileGameObjectsController.GetTileAt(_currentTile.X, _currentTile.Y + 1);
            }
            if (_currentRotation == 1)
            {
                return _tileGameObjectsController.GetTileAt(_currentTile.X + 1, _currentTile.Y);
            }
            if (_currentRotation == 2)
            {
                return _tileGameObjectsController.GetTileAt(_currentTile.X, _currentTile.Y - 1);
            }
            if (_currentRotation == 3)
            {
                return _tileGameObjectsController.GetTileAt(_currentTile.X - 1, _currentTile.Y);
            }

            return null;
        }

        void Update()
        {
            DoMove();
            DoRotate();

            if (Move)
            {
                Move = false;
                MoveForward();
            }

            if (Turn)
            {
                Turn = false;
                Rotate(RotationSide.Left);
            }
        }

        void DoRotate()
        {
            if (_rotationGoal != -1)
            {
                _animator.SetBool("IsMoving", true);
                _currentRotating += Time.deltaTime;

                if (_currentRotating > MoveSpeed)
                {
                    _currentRotation = _rotationGoal;
                    _rotationGoal = -1;

                    if (OnRotationFinished != null)
                    {
                        OnRotationFinished();
                    }

                    _animator.SetBool("IsMoving", false);
                    transform.rotation = Quaternion.Euler(0, 0, -90 * _currentRotation);
                }
                else
                {
                    Quaternion currentRotation = Quaternion.Euler(0, 0, -90 * _currentRotation);
                    Quaternion goalRotation = Quaternion.Euler(0, 0, -90 * _rotationGoal);

                    transform.rotation = Quaternion.LerpUnclamped(currentRotation, goalRotation, (_currentRotating / MoveSpeed));
                }
            }
        }

        void DoMove()
        {
            if (_goalTile != null)
            {
                _animator.SetBool("IsMoving", true);
                _currentMove += Time.deltaTime;

                if (_currentMove > MoveSpeed)
                {
                    _currentTile = _goalTile;
                    _goalTile = null;

                    if (OnMoveFinished != null)
                    {
                        OnMoveFinished();
                    }

                    _animator.SetBool("IsMoving", false);
                    transform.position = new Vector3(_currentTile.X * Tile.TileSize, _currentTile.Y * Tile.TileSize, -0.05f);
                }
                else
                {

                    Vector3 currentVector = new Vector3(_currentTile.X * Tile.TileSize, _currentTile.Y * Tile.TileSize, -0.05f);
                    Vector3 goalVector = new Vector3(_goalTile.X * Tile.TileSize, _goalTile.Y * Tile.TileSize, -0.05f);

                    transform.position = currentVector + (goalVector - currentVector) * (_currentMove / MoveSpeed);
                }
            }

        }

        public void MoveForward()
        {
            if (_rotationGoal == -1)
            {
                Tile aheadTile = GetTileAhead();

                if (aheadTile != null)
                {
                    if (_goalTile == null)
                    {
                        if (aheadTile.Type != TileType.Rock)
                        {
                            _goalTile = aheadTile;
                            _currentMove = 0f;
                        }
                        else
                        {
                            if (OnMoveFailed != null)
                            {
                                OnMoveFailed(new MoveFailedArgs(_currentTile.X, _currentTile.Y, aheadTile.X, aheadTile.Y, MoveFailType.ObstacleAhead));
                            }
                        }
                    }
                    else
                    {
                        if (OnMoveFailed != null)
                        {
                            OnMoveFailed(new MoveFailedArgs(_currentTile.X, _currentTile.Y, aheadTile.X, aheadTile.Y, MoveFailType.AlreadyMoving));
                        }
                    }
                }
                else
                {
                    if (OnMoveFailed != null)
                    {
                        OnMoveFailed(new MoveFailedArgs(_currentTile.X, _currentTile.Y, _currentTile.X, _currentTile.Y, MoveFailType.ObstacleAhead));
                    }
                }
            }
            else
            {
                if (OnMoveFailed != null)
                {
                    OnMoveFailed(new MoveFailedArgs(_currentTile.X, _currentTile.Y, _currentTile.X, _currentTile.Y, MoveFailType.AlreadyRotating));
                }
            }
        }

        public void Rotate(RotationSide rotationSide)
        {
            if (_goalTile == null)
            {
                if (_rotationGoal == -1)
                {
                    if (rotationSide == RotationSide.Left)
                    {
                        _rotationGoal = _currentRotation - 1;
                    }

                    if (rotationSide == RotationSide.Right)
                    {
                        _rotationGoal = _currentRotation + 1;
                    }

                    if (_rotationGoal < 0)
                    {
                        _rotationGoal = 3;
                    }

                    if (_rotationGoal > 3)
                    {
                        _rotationGoal = 0;
                    }

                    _currentRotating = 0;
                }
                else
                {
                    if (OnRotationFailed != null)
                    {
                        OnRotationFailed(new RotationFailedArgs(rotationSide, RotationFailType.AlreadyRotating));
                    }
                }
            }
            else
            {
                if (OnRotationFailed != null)
                {
                    OnRotationFailed(new RotationFailedArgs(rotationSide, RotationFailType.AlreadyMoving));
                }
            }
        }
    }
}
