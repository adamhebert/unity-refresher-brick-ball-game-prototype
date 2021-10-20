using UnityEngine;

namespace GameUtilities
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 vector, float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var sin = Mathf.Sin(radians);
            var cos = Mathf.Cos(radians);

            // Allocation. Look into another way potentially. Not an issue for this game.
            return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
        }
    }
}
