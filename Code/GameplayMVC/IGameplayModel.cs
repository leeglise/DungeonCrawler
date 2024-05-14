using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.GameplayMVC.MapFolder;

namespace DungeonCrawler.Code.GameplayMVC
{
    public interface IGameplayModel
    {
        event EventHandler<GameplayEventArgs> Updated;
        event EventHandler<GameplayEventArgs> GameStateChanged;

        Player Player { get; }

        Map Map { get; }

        List<Vector2> ViableFields { get; }

        void Initialize();
        void Update();
        void UseItem();
        void Restart();
        public enum Direction
        {
            forward,
            backward,
            left,
            right
        }
    }

	public class GameplayEventArgs : EventArgs
	{
		public List<List<Field>> Map { get; set; }
		public Player Player { get; set; }
		public List<Vector2> ViableMapFields { get; set; }
		public GameState GameState { get; set; }
        public int Level { get; set; }
	}
}
