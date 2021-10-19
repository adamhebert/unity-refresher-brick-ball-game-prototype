using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace GameObjects
{
    public sealed class Ball : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mGameLifetime = GameLifetime.Instance;

            Initialize();

            mAfterDeathDelay = new WaitForSeconds(_AfterDeathDelayBeforeTransition);
        }

        private void OnTriggerEnter2D(Collider2D collision) =>
            TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                type =>
                {
                    switch (type)
                    {
                        case TagUtils.GameObjectType.DeadBallArea:
                            mGameLifetime.AddDeath();

                            // TODO: What happens when the player runs out of lives? Game Over screen needs to be implemented or something. Maybe start game over for demo purposes.

                            // This is super goofy. The ball shouldn't control this...especially if there are special ball powerups in the game later, or multiple balls, etc.
                            // Just trying to get something going quickly for now.
                            if (mGameLifetime.GetRest() <= 0)
                            {
                                StartCoroutine(ChangeScene());
                            }
                            else
                            {
                                Initialize();
                            }
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

            this._RigidBody.position = _StartingPosition;
            this._RigidBody.velocity = Vector2.zero;
            this._RigidBody.AddForce(GetStartingDirection() * _StartingForceMultiplier);
        }

        private IEnumerator ChangeScene()
        {
            // TODO: Move this duty to some scene manager...some other thing should detect that a game over has occurred.
            yield return mAfterDeathDelay;
            yield return SceneManager.LoadSceneAsync("GameOverMenu");
            yield return SceneManager.UnloadSceneAsync(this.gameObject.scene);
            yield return Resources.UnloadUnusedAssets();
        }

        private GameLifetime mGameLifetime;
        private WaitForSeconds mAfterDeathDelay;

        #region InspectorMembers
        [SerializeField] private Vector2 _StartingPosition = default;
        [SerializeField] private float _StartingForceMultiplier = 1.0f;
        [SerializeField] private Rigidbody2D _RigidBody = default;
        [SerializeField] private float _AfterDeathDelayBeforeTransition = 0.0f;
        #endregion
    }
}
