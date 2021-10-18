using UnityEngine;
using Utilities;

public sealed class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
                });
        }
    }

    private BrickState mBrickState = BrickState.Normal;

    private enum BrickState
    {
        Normal,
        Breaking,
        Dead
    }
}
