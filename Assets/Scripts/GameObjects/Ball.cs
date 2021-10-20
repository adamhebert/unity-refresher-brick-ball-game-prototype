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
                    }
                });

        private void OnCollisionEnter2D(Collision2D collision) =>
            TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                type =>
                {
                    switch (type)
                    {
                        case TagUtils.GameObjectType.Paddle:
                            AddForce();
                            break;
                    }
                });


        private void OnCollisionExit2D(Collision2D collision) =>
            TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                type =>
                {
                    switch (type)
                    {
                        // A helper to make the ball go upward from the paddle. We never really want to hit it sideways.
                        case TagUtils.GameObjectType.Paddle:

                            const float halfCircleAngle = 180.0f;

                            var angleFromRight = Vector2.SignedAngle(this._RigidBody.velocity, Vector2.right);

                            if (angleFromRight < -halfCircleAngle + this._MaxAllowedAngleFromUp)
                            {
                                // Moving too straight sideways in the upper left quadrant.
                                this._RigidBody.velocity = this._RigidBody.velocity.Rotate(halfCircleAngle - this._MaxAllowedAngleFromUp + angleFromRight);
                            }
                            else if (angleFromRight > halfCircleAngle - this._MaxAllowedAngleFromUp)
                            {
                                // Moving too straight sideways in the lower left quadrant.
                                this._RigidBody.velocity = this._RigidBody.velocity.Rotate(angleFromRight - (halfCircleAngle - this._MaxAllowedAngleFromUp));
                            }
                            else if (angleFromRight < 0.0f && angleFromRight > -this._MaxAllowedAngleFromUp)
                            {
                                // Moving too straight sideways in the upper right quadrant.
                                this._RigidBody.velocity = this._RigidBody.velocity.Rotate(angleFromRight + this._MaxAllowedAngleFromUp);
                            }
                            else if (angleFromRight > 0.0f && angleFromRight < this._MaxAllowedAngleFromUp)
                            {
                                // Moving too straight sideways in the lower right quadrant.
                                this._RigidBody.velocity = this._RigidBody.velocity.Rotate(angleFromRight - this._MaxAllowedAngleFromUp);
                            }
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

        private void AddForce()
        {
            if (this._RigidBody.velocity.magnitude < _MaxMagnitude)
            {
                this._RigidBody.AddForce(this._RigidBody.transform.up * this._IncrementalForceOnPaddleHit);
            }
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
        [SerializeField] private float _MaxMagnitude = 1.0f;
        [SerializeField] private float _IncrementalForceOnPaddleHit = default;
        [SerializeField] private float _MaxAllowedAngleFromUp = default;
        [SerializeField] private Rigidbody2D _RigidBody = default;
        [SerializeField] private float _AfterDeathDelayBeforeTransition = 0.0f;
        #endregion
    }
}
