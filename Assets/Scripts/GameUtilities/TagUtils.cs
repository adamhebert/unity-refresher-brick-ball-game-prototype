using System.Collections.Generic;
using Prelude;

namespace GameUtilities
{
    public static class TagUtils
    {
        public enum GameObjectType
        {
            Ball,
            Brick,
            Paddle,
            Wall,
            DeadBallArea,
        }

        public static Option<GameObjectType> GetGameObjectTypeFromTag(string tag) => mTagToGameObjectType.GetValueIfPresent(tag);

        // Could just parse directly into the enum, but I like not needing to change code to match user input. Translation layer allows for easy pivots to be made.
        private static Dictionary<string, GameObjectType> mTagToGameObjectType =
            new Dictionary<string, GameObjectType>
            {
                { "Ball", GameObjectType.Ball },
                { "Brick", GameObjectType.Brick },
                { "Paddle", GameObjectType.Paddle },
                { "Wall", GameObjectType.Wall },
                { "DeadBallArea", GameObjectType.DeadBallArea },
            };
    }
}
