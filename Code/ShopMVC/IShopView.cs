using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ScreenMVC;
using DungeonCrawler.Code.ShopMVC.ShopFolder;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC
{
	public interface IShopView
	{
		void Initialize(GameRoot instance);
		void UpdateParameters(Dictionary<Item, bool> items, int currentItemIndex, int itemsPrice);
		void Draw(GameRoot instance, SpriteBatch spriteBatch);
	}
}
