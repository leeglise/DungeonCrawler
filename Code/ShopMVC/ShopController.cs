using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC
{
	public class ShopController : IShopController
	{
		public event EventHandler ItemBought = delegate { };
		public event EventHandler PressedEsc = delegate { };

		private KeyboardState previousState;

		public void HandleInput(KeyboardState keyboardState, IShopModel shopModel)
		{
			int newIndex = shopModel.Index;

			if (keyboardState.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))
			{
				PressedEsc.Invoke(this, new EventArgs());
			}

			if (keyboardState.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter))
			{
				ItemBought.Invoke(this, new EventArgs());
			}

			else if (keyboardState.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A) || (keyboardState.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left)))
			{
				newIndex -= 1;
			}
			else if (keyboardState.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D) || (keyboardState.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right)))
			{
				newIndex += 1;
			}

			if (newIndex != shopModel.Index && newIndex >= 0 && newIndex < shopModel.ItemsList.Count)
			{
				shopModel.MoveCursor(newIndex);
			}

			previousState = keyboardState;
		}

		public void UpdateState(KeyboardState keyboardState)
		{
			previousState = keyboardState;
		}
	}
}
