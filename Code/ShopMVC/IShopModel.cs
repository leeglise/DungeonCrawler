using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ShopMVC.ShopFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC
{
	public interface IShopModel
	{
		event EventHandler<ShopEventArgs> Updated;

		int Index { get; }
		Dictionary<Item, bool> ItemsList { get; }
		void MoveCursor(int newIndex);
		void UpdateShop();
		void BuyItem();
		void Initialize(Player player);
	}

	public class ShopEventArgs : EventArgs
	{
		public Dictionary<Item, bool> Items { get; set; }
		public int CurrentItemIndex { get; set; }
		public int ItemsPrice { get; set; }
		public Player Player { get; set; }
	}
}
