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

        // Idea here is to make sure the wall boundaries are always at the end of the screen.
        private void OnScreenChange()
        {
            static float getHalfSize(float size) => size / 2.0f;

            static (float ScreenOffsetX, float WorldHalfWidth) getXScreenOffset(AxisAlignedWallSide wallSide, float spriteWidth) =>
                wallSide switch
                {
                    AxisAlignedWallSide.Right => (Screen.width, -getHalfSize(spriteWidth)),
                    AxisAlignedWallSide.Left => (0.0f, getHalfSize(spriteWidth)),
                    var side when side == AxisAlignedWallSide.Top && side == AxisAlignedWallSide.Bottom => (Screen.width / 2, 0.0f),
                    //AxisAlignedWallSide.Top or AxisAlignedWallSide.Bottom => (Screen.width / 2, 0.0f), // 'or' doesn't seem to work with Unity.
                    _ => throw new Exception($"Invalid {nameof(_WallSide)}. {wallSide} is not a valid value.")
                };

            static (float ScreenOffsetY, float WorldHalfHeight) getYScreenOffset(AxisAlignedWallSide wallSide, float spriteHeight) =>
                wallSide switch
                {
                    AxisAlignedWallSide.Top => (Screen.height, -getHalfSize(spriteHeight)),
                    AxisAlignedWallSide.Bottom => (0.0f, getHalfSize(spriteHeight)),
                    var side when side == AxisAlignedWallSide.Left || side == AxisAlignedWallSide.Right => (Screen.height / 2, 0.0f),
                    //AxisAlignedWallSide.Left or AxisAlignedWallSide.Right => (Screen.height / 2, 0.0f), // 'or' doesn't seem to work with Unity.
                    _ => throw new Exception($"Invalid {nameof(_WallSide)}. {wallSide} is not a valid value.")
                };

            // Modify scale first so the rest of the calculations can be based off new resolution forced scale.
            var scaleModifier = ResolutionUtils.GetScaleModifier(Screen.width, Screen.height);
            transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y * scaleModifier, 1.0f);
            transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y, 1.0f);
            switch (_WallSide)
            {
                case AxisAlignedWallSide.Left:
                case AxisAlignedWallSide.Right:
                    transform.localScale = new Vector3(mReferenceScale.x * scaleModifier, mReferenceScale.y, 1.0f);
                    break;
                case AxisAlignedWallSide.Top:
                case AxisAlignedWallSide.Bottom:
                    transform.localScale = new Vector3(mReferenceScale.x, mReferenceScale.y * scaleModifier, 1.0f);
                    break;
            }

            var spriteRendererSize = this.GetComponent<SpriteRenderer>().bounds.size;

            var (screenOffsetX, worldHalfWidth) = getXScreenOffset(_WallSide, spriteRendererSize.x);
            var (screenOffsetY, worldHalfHeight) = getYScreenOffset(_WallSide, spriteRendererSize.y);
            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenOffsetX, screenOffsetY, Camera.main.nearClipPlane)) + new Vector3(worldHalfWidth, worldHalfHeight, 0.0f);

            this.transform.position = worldPoint;
        }

        private int mScreenWidthPrev;
        private int mScreenHeightPrev;
        private Vector3 mReferenceScale;

        #region UnityInspectorMembers
        [SerializeField] private AxisAlignedWallSide _WallSide = AxisAlignedWallSide.Left;
        #endregion
    }
}