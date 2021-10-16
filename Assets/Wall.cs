using System;
using UnityEngine;

public sealed class Wall : MonoBehaviour
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

        var spriteRendererSize = this.GetComponent<SpriteRenderer>().bounds.size;

        var (screenOffsetX, worldHalfWidth) = getXScreenOffset(WallSide, spriteRendererSize.x);
        var (screenOffsetY, worldHalfHeight) = getYScreenOffset(WallSide, spriteRendererSize.y);
        var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenOffsetX, screenOffsetY, Camera.main.nearClipPlane)) + new Vector3(worldHalfWidth, worldHalfHeight, 0.0f);

        this.transform.position = worldPoint;
    }

    public AxisAlignedWallSide WallSide;

    private int mScreenWidthPrev;
    private int mScreenHeightPrev;
}
