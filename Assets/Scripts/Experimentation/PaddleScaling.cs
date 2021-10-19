using UnityEngine;

namespace Experimentation
{
    public sealed class PaddleScaling : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mScreenWidthPrev = Screen.width;
            mScreenHeightPrev = Screen.height;
            mReferenceScale = transform.localScale;

            OnScreenChange();
        }

        // Update is called once per frame
        private void Update()
        {
            // Consider making this some kind of event thing.
            if (mScreenWidthPrev != Screen.width || mScreenHeightPrev != Screen.height)
            {
                mScreenWidthPrev = Screen.width;
                mScreenHeightPrev = Screen.height;

                OnScreenChange();
            }
        }

        private void OnScreenChange()
        {
            var scaleModifier = ResolutionUtils.GetScaleModifier(Screen.width, Screen.height);

            transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y * scaleModifier, 1.0f);
        }

        private int mScreenWidthPrev;
        private int mScreenHeightPrev;
        private Vector3 mReferenceScale;
    }
}