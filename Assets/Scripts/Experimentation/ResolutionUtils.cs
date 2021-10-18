using UnityEngine;

namespace Experimentation
{
    public static class ResolutionUtils
    {
        private static readonly Vector2 TargetResolution = new Vector2(1920.0f, 1080.0f);
        private static readonly float TargetResolutionMatch = 0.50f;

        // Obviously need a better system than this to handle resolution changes, but it gets the ball rolling.
        public static float GetScaleModifier(int screenWidth, int screenHeight) =>
            Mathf.Pow(screenWidth / TargetResolution.x, 1.0f - TargetResolutionMatch) * Mathf.Pow(screenHeight / TargetResolution.y, TargetResolutionMatch);
    }
}
