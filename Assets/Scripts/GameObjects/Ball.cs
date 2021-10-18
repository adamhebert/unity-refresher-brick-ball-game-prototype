using UnityEngine;
using Utilities;

namespace GameObjects
{
    public sealed class Ball : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            mGameLifetime = FindObjectOfType<GameLifetime>();

            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collision) =>
            TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                type =>
                {
                    switch (type)
                    {
                        case TagUtils.GameObjectType.DeadBallArea:
                            mGameLifetime.AddDeath();
                            Initialize();

                            // TODO: What happens when the player runs out of lives? Game Over screen needs to be implemented or something. Maybe start game over for demo purposes.
                            break;

                        case TagUtils.GameObjectType.Ball:
                        case TagUtils.GameObjectType.Brick:
                        case TagUtils.GameObjectType.Paddle:
                        case TagUtils.GameObjectType.Wall:
                        default:
                            break;
                    }
                });

        private void Initialize()
        {
            static Vector2 GetStartingDirection()
            {
                var x = Random.Range(-0.5f, 0.5f);
                var y = -1.0f;

                return new Vector2(x, y).normalized;
            }

            this.mRigidBody.position = mStartingPosition;
            this.mRigidBody.velocity = Vector2.zero;
            this.mRigidBody.AddForce(GetStartingDirection() * mStartingForceMultiplier);
        }

        // Exposed to Unity Editor.
        [SerializeField] private Vector2 mStartingPosition;
        [SerializeField] private float mStartingForceMultiplier = 1.0f;
        [SerializeField] private Rigidbody2D mRigidBody;

        private GameLifetime mGameLifetime;
    }
}
