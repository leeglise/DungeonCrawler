using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ShopMVC.ShopFolder
{
	 public abstract class Item
	{
		private ItemType itemType;
		private int value;

		public ItemType ItemType => itemType;
		public int Value => value;

		public Item(ItemType itemType) 
		{
			this.itemType = itemType;
		}

		public Item(ItemType itemType, int value) : this(itemType)
		{
			this.value = value;
		}
	}
}
