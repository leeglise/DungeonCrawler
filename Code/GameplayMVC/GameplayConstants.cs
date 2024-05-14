using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.GameplayMVC
{
    sealed class GameplayConstants
    {
        public static readonly int mapSize = 5;

        public static readonly int maxLevel = 20;

        public static readonly int bossScale = 20;

        public static readonly double diceScale = 1.5;

		public static readonly Vector2 startPosition = new Vector2(2, 4);

        public static readonly double levelDifficultyScale = 0.125;
    }
}
