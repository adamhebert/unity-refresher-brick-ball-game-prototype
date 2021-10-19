using UnityEngine;
using Utilities;

namespace GameObjects
{
    public sealed class Brick : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mBrickState = BrickState.Normal;
            mGameLifetime = FindObjectOfType<GameLifetime>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // We only care about collision when it's in the basic normal state.
            if (mBrickState == BrickState.Normal)
            {
                // TODO: Start some kind of break animation.
                // TODO: Notify the score manager...maybe the ball should do that...or a third party reconciler...
                TagUtils.GetGameObjectTypeFromTag(collision.gameObject.tag).ForEach(
                    _ =>
                    {
                        mBrickState = BrickState.Breaking;
                        this.gameObject.SetActive(false);
                        mGameLifetime.AddScore(_PointValue);
                    });
            }
        }


        private BrickState mBrickState = BrickState.Normal;
        // This feels really wrong. Look into better ways of referencing things.
        private GameLifetime mGameLifetime;

        private enum BrickState
        {
            Normal,
            Breaking,
            Dead
        }

        #region InspectorMembers
        [SerializeField] private int _PointValue = 1;
        #endregion
    }
}
