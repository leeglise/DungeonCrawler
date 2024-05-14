using DungeonCrawler.Code.ShopMVC.ShopFolder;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.GameplayMVC.Entities;

namespace DungeonCrawler.Code.ShopMVC
{
	public class ShopView : IShopView
	{
		private SpriteFont font;
		private Color penColor = Color.White;
		private int fontSize = Constants.fontSize;

		private Texture2D background;
		private Texture2D choosedItem;

		private Dictionary<Item, bool> items;
		private int currentItemIndex;
		private int itemsPrice;

		public void Initialize(GameRoot instance)
		{
			background = instance.Content.Load<Texture2D>("ShopArts/background");
			font = instance.Content.Load<SpriteFont>("Font");

			choosedItem = instance.Content.Load<Texture2D>("ShopArts/choosedItem");
		}

		public void UpdateParameters(Dictionary<Item, bool> items, int currentItemIndex, int itemsPrice)
		{
			this.items = items;
			this.currentItemIndex = currentItemIndex;
			this.itemsPrice = itemsPrice;
		}

		public void Draw(GameRoot instance, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
					background, new Vector2(0, 0), null,
					Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

			Texture2D texture;

			int i = 0;

			foreach (var item in items)
			{
				var x = (i + 1) * Constants.textureSize;
				var y = 3 * Constants.textureSize;
				if (item.Value)
				{
					texture = instance.Content.Load<Texture2D>("ShopArts/" + item.Key.ItemType);
					spriteBatch.DrawString(font, itemsPrice.ToString(),
						new Vector2(x + (Constants.textureSize - fontSize) / 2, y + Constants.textureSize),
						penColor,
						0,
						new Vector2(0, 0),
						Constants.textureScale,
						SpriteEffects.None,
						0
						);
				}
				else
				{
					texture = instance.Content.Load<Texture2D>("ShopArts/sold");
				}

				spriteBatch.Draw(
					texture, new Vector2(x, y), null,
					Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

				if (i == currentItemIndex)
				{
					spriteBatch.Draw(
						choosedItem, new Vector2(x, y), null,
						Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
				}
				i++;
			}
		}
	}
}
