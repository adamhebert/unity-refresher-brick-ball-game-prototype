using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Prelude;
using GameUtilities;

namespace GameObjects
{
    public sealed class PaddleMovement : MonoBehaviour
    {
        public event EventHandler BallHit;

        // Start is called before the first frame update
        private void Start()
        {
            mCurrentVelocity = _MovementVelocity;
            mCurrentDirection = Vector2.zero;
            mHorizontalWorldEnd = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - _WallPixelScale - _WallCollisionBuffer, Screen.height, 0.0f)).x - this.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
            mDummyEventArgs = new EventArgs { };
        }

        private void FixedUpdate()
        {
            // Tried playing around with the paddle being dynamic. Need to deeper dive into the physics system in Unity...collision was wonky from what I'd expect. So I switched back to Kinematic. Will control the paddle myself.
            //if (this.mCurrentDirection.sqrMagnitude >= 0.0f)
            //{
            //    this.mRigidBody.AddForce(this.mCurrentDirection * this.mCurrentVelocity);
            //}

            static bool isMoving(MovementType movementType) =>
                !GameLifetime.Instance.GameIsOver() && mMovementToKeyCodesTempMappingSystem.GetValueIfPresent(movementType).Match(keyCodes => keyCodes.Any(Input.GetKey), () => false);

            // TODO: Build an input system of some kind.
            // TODO: Allow the player to map whatever keys that want to specific things.
            if (this.transform.position.x > -mHorizontalWorldEnd && isMoving(MovementType.Left)) // Left always wins here...
            {
                // Move the paddle left.
                mCurrentDirection = Vector2.left;

            }
            else if (this.transform.position.x < mHorizontalWorldEnd && isMoving(MovementType.Right))
            {
                // Move the paddle right.
                mCurrentDirection = Vector2.right;
            }
            else
            {
                mCurrentDirection = Vector2.zero;
            }

            // Assumption is a normalized vector here for our purposes. But scaling the vector may be desired. Will have to see where game options take me.
            // Consider making some other kind of system to move things based on cameras. This feels dangerous if left to each script/component...if we ever have more cameras.
            if (mCurrentDirection.sqrMagnitude > 0.0f)
            {
                this.transform.Translate(mCurrentDirection * Time.fixedDeltaTime * mCurrentVelocity, Camera.main.transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) =>
            TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                type =>
                {
                    switch (type)
                    {
                        case TagUtils.GameObjectType.Ball:
                            BallHit(this, mDummyEventArgs);
                            break;
                    }
                });

        private float mCurrentVelocity;
        private Vector2 mCurrentDirection;
        private float mHorizontalWorldEnd;
        private EventArgs mDummyEventArgs; // Avoid in game allocations.

        private enum MovementType
        {
            Left,
            Right,
        }

        private static readonly Dictionary<MovementType, KeyCode[]> mMovementToKeyCodesTempMappingSystem =
            new Dictionary<MovementType, KeyCode[]>
            {
                { MovementType.Left, new KeyCode[] { KeyCode.LeftArrow, KeyCode.A } },
                { MovementType.Right, new KeyCode[] { KeyCode.RightArrow, KeyCode.D } }
            };

        #region InspectorMembers
        [SerializeField] private float _MovementVelocity = 1.0f;
        [SerializeField] private float _WallPixelScale = default;
        [SerializeField] private float _WallCollisionBuffer = default;
        #endregion
    }
}
