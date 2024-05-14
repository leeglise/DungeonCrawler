using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ShopMVC.ShopFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC
{
	public class ShopModel : IShopModel
	{
		public event EventHandler<ShopEventArgs> Updated = delegate { };

		private int bombScale = 6;

		private int currentItemIndex;
		private List<ItemType> allItems;
		Dictionary<Item, bool> itemsList;
		private int price = 15;

		private Player player;

		public int Index => currentItemIndex;
		public Dictionary<Item, bool> ItemsList => itemsList;

		public void Initialize(Player player)
		{
			this.player = player;
			itemsList = new Dictionary<Item, bool>();

			allItems = Enum.GetValues(typeof(ItemType)).Cast<ItemType>().ToList();
		}

		public void BuyItem() 
		{
			if (player.CoinsCount >= price)
			{
				var item = itemsList.ElementAt(currentItemIndex).Key;
				player.BuyItem(item, price);

				itemsList[item] = false;

				Updated.Invoke(this, new ShopEventArgs { Items = itemsList, CurrentItemIndex = currentItemIndex, ItemsPrice = price, Player = player });
			}
		}

		public void MoveCursor(int newIndex)
		{
			currentItemIndex = newIndex;
			Updated.Invoke(this, new ShopEventArgs { Items = itemsList, CurrentItemIndex = currentItemIndex, ItemsPrice = price, Player = player });
		}

		public void UpdateShop()
		{
			itemsList.Clear();
			currentItemIndex = 1;

			var rnd = new Random();
			int itemsCount = allItems.Count;

			var newItems = Enumerable.Range(1, itemsCount - 1).OrderBy(g => rnd.NextDouble()).ToList().Take(3);

			foreach (var item in newItems)
			{
				Item currentItem;
				var currentItemType = (ItemType)item;

				switch (currentItemType)
				{
					case ItemType.bomb:
						currentItem = new Bomb(ItemType.bomb, GameplayConstants.maxLevel / bombScale);
						break;

					case ItemType.healthUp:
						currentItem = new HealthUp(ItemType.healthUp, 2);
						break;

					case ItemType.rewind:
						currentItem = new Rewind(ItemType.rewind);
						break;

					case ItemType.skip:
						currentItem = new Skip(ItemType.skip);
						break;

					case ItemType.empoweredShield:
						currentItem = new EmpoweredShield(ItemType.empoweredShield);
						break;

					case ItemType.dice:
						currentItem = new Dice(ItemType.dice);
						break;

					default:
						currentItem = new EmptyItem(ItemType.empty);
						break;
				}
				itemsList.Add(currentItem, true);
			}

			Updated.Invoke(this, new ShopEventArgs { Items = itemsList, CurrentItemIndex = currentItemIndex, ItemsPrice = price, Player = player });
		}
	}
}
