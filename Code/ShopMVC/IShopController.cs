using DungeonCrawler.Code.ScreenMVC;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC
{
	public interface IShopController
	{
		event EventHandler ItemBought;
		event EventHandler PressedEsc;

		void HandleInput(KeyboardState keyboardState, IShopModel shopModel);
		void UpdateState(KeyboardState keyboardState);
	}

	public class ShopControlsEventArgs : EventArgs
	{
		public KeyboardState KeyboardState { get; set; }

		public IShopModel ShopModel { get; set; }
	}
}
