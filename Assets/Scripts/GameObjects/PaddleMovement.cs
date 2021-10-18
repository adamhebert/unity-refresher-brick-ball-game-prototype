using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameObjects
{
    public sealed class PaddleMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            mCurrentVelocity = mMovementVelocity;
            mCurrentDirection = Vector2.zero;
            mHorizontalWorldEnd = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - mWallPixelScale - mWallCollisionBuffer, Screen.height, 0.0f)).x - this.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
        }

        // Update is called once per frame
        void Update()
        {
            static bool isMoving(MovementType movementType) =>
                mMovementToKeyCodesTempMappingSystem.GetValueIfPresent(movementType).Match(keyCodes => keyCodes.Any(Input.GetKey), () => false);

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
        }

        private void FixedUpdate()
        {
            // Tried playing around with the paddle being dynamic. Need to deeper dive into the physics system in Unity...collision was wonky from what I'd expect. So I switched back to Kinematic. Will control the paddle myself.
            //if (this.mCurrentDirection.sqrMagnitude >= 0.0f)
            //{
            //    this.mRigidBody.AddForce(this.mCurrentDirection * this.mCurrentVelocity);
            //}


            // Assumption is a normalized vector here for our purposes. But scaling the vector may be desired. Will have to see where game options take me.
            // Consider making some other kind of system to move things based on cameras. This feels dangerous if left to each script/component...if we ever have more cameras.
            if (mCurrentDirection.sqrMagnitude > 0.0f)
            {
                this.transform.Translate(mCurrentDirection * Time.fixedDeltaTime * mCurrentVelocity, Camera.main.transform);
            }
        }

        // Exposed to Unity Editor.
        [SerializeField] private float mMovementVelocity = 1.0f;
        [SerializeField] private float mWallPixelScale;

        private float mCurrentVelocity;
        private Vector2 mCurrentDirection;
        private float mHorizontalWorldEnd;

        private static readonly float mWallCollisionBuffer = 4.0f;

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
    }
}
