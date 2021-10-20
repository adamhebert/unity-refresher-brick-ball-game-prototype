using System;
using UnityEngine;
using Prelude;

namespace GameObjects
{
    public sealed class Level : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            mBricks = FindObjectsOfType<Brick>(true);

            mBricks.ForEach(
                brick =>
                {
                    brick.BrickDestroyed += OnBrickDestroyed;
                });

            FindObjectsOfType<PaddleMovement>().ForEach(
                paddle =>
                {
                    paddle.BallHit += OnBallHitPaddle;
                });

            ResetData();
        }

        private void ResetData()
        {
            mBricks.ForEach(
                brick =>
                {
                    brick.gameObject.SetActive(true); // Maybe emit an event back to the bricks in the future rather than directly manipulating.
                });

            mBricksAlive = mBricks.Length;
        }

        // Make some kind of event system in the future. Need better interaction between game objects.
        private void OnBrickDestroyed(object sender, EventArgs e) => --mBricksAlive;
        private void OnBallHitPaddle(object sender, EventArgs e)
        {
            if (mBricksAlive <= 0)
            {
                ResetData();
            }
        }

        private int mBricksAlive;
        // Would rather this be populated via data somewhere. Have to do more thinking about where that can go in the future. For now, get the game going.
        private Brick[] mBricks;
    }
}
