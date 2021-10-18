using System;
using UnityEngine;

namespace Experimentation
{
    public sealed class Wall_Experiment : MonoBehaviour
    {
        public enum AxisAlignedWallSide
        {
            Left,
            Right,
            Top,
            Bottom,
        }

        // Start is called before the first frame update
        void Start()
        {
            mScreenWidthPrev = Screen.width;
            mScreenHeightPrev = Screen.height;
            mReferenceScale = transform.localScale;

            OnScreenChange();
        }

        // Update is called once per frame
        void Update()
        {
            // Consider making this some kind of event thing.
            if (mScreenWidthPrev != Screen.width || mScreenHeightPrev != Screen.height)
            {
                mScreenWidthPrev = Screen.width;
                mScreenHeightPrev = Screen.height;

                OnScreenChange();
            }
        }

        // Idea here is to make sure the wall boundaries are always at the end of the screen.
        private void OnScreenChange()
        {
            static float getHalfSize(float size) => size / 2.0f;

            static (float ScreenOffsetX, float WorldHalfWidth) getXScreenOffset(AxisAlignedWallSide wallSide, float spriteWidth)
            {
                var spriteHalfWidth = getHalfSize(spriteWidth);
                switch (wallSide)
                {
                    case AxisAlignedWallSide.Right: return (Screen.width, -spriteHalfWidth);
                    case AxisAlignedWallSide.Left: return (0.0f, spriteHalfWidth);
                    case AxisAlignedWallSide.Top:
                    case AxisAlignedWallSide.Bottom: return (Screen.width / 2, 0.0f);
                    default: throw new Exception($"Invalid {nameof(WallSide)}. {wallSide} is not a valid value.");
                };
            }

            static (float ScreenOffsetY, float WorldHalfHeight) getYScreenOffset(AxisAlignedWallSide wallSide, float spriteHeight)
            {
                var spriteHalfHeight = getHalfSize(spriteHeight);
                switch (wallSide)
                {
                    case AxisAlignedWallSide.Top: return (Screen.height, -spriteHalfHeight);
                    case AxisAlignedWallSide.Bottom: return (0.0f, spriteHalfHeight);
                    case AxisAlignedWallSide.Left:
                    case AxisAlignedWallSide.Right: return (Screen.height / 2, 0.0f);
                    default: throw new Exception($"Invalid {nameof(WallSide)}. {wallSide} is not a valid value.");
                }
            }

            // Modify scale first so the rest of the calculations can be based off new resolution forced scale.
            //var scaleModifier = ResolutionUtils.GetScaleModifier(Screen.width, Screen.height);
            //transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y * scaleModifier, 1.0f);
            //transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y, 1.0f);
            //switch (WallSide)
            //{
            //    case AxisAlignedWallSide.Left:
            //    case AxisAlignedWallSide.Right:
            //        transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y, 1.0f);
            //        break;
            //    case AxisAlignedWallSide.Top:
            //    case AxisAlignedWallSide.Bottom:
            //        transform.localScale = new Vector3(mReferenceScale.x, mReferenceScale.y * scaleModifier, 1.0f);
            //        break;
            //}

            var spriteRendererSize = this.GetComponent<SpriteRenderer>().bounds.size;

            var (screenOffsetX, worldHalfWidth) = getXScreenOffset(WallSide, spriteRendererSize.x);
            var (screenOffsetY, worldHalfHeight) = getYScreenOffset(WallSide, spriteRendererSize.y);
            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenOffsetX, screenOffsetY, Camera.main.nearClipPlane)) + new Vector3(worldHalfWidth, worldHalfHeight, 0.0f);

            this.transform.position = worldPoint;
        }

        // Exposed to Unity Editor.
        [SerializeField] private AxisAlignedWallSide WallSide = AxisAlignedWallSide.Left;

        private int mScreenWidthPrev;
        private int mScreenHeightPrev;
        private Vector3 mReferenceScale;
    }
}