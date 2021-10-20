using UnityEngine;

namespace Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 vector, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);

            // Allocation. Look into another way potentially. Not an issue for this game.
            return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
        }
    }
}
