using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.GameplayMVC.Entities;

namespace DungeonCrawler.Code.GameplayMVC
{
    public interface IGameplayController
    {
        event EventHandler PlayerMoved;
        event EventHandler ItemUsed;
        event EventHandler PressedEsc;

        void HandleInput(Map map, Player player, KeyboardState keyboardState);

        void UpdateState(KeyboardState keyboardState);
    }

	public class GameplayControlsEventArgs : EventArgs
	{
		public KeyboardState KeyboardState { get; set; }

		public Player Player { get; set; }

		public Map Map { get; set; }
	}
}