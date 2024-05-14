/*using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Code.Entities;

namespace DungeonCrawler.Code
{
	public class Shop
	{
		private SpriteFont font;
		private int fontSize = Constants.fontSize;
		private Color PenColor = Color.White;

		private int distance = Constants.textureWidth;
		private int textPositionX = (Constants.textureWidth - Constants.fontSize * 2) / 2;
		private int textPositionY = Constants.textStartPositionY + Constants.textHeightIndentation - Constants.fontSize * 2;

		private int currentItemIndex;
		private Texture2D choosedItemImage;
		private Texture2D soldImage;

		private Dictionary<Entity, bool> items;

		public static readonly int itemsPrice = 10;

		public Shop(GameRoot instance)
		{
			font = instance.Content.Load<SpriteFont>("Font");

			choosedItemImage = instance.Content.Load<Texture2D>("Arts/choosedItem");
			soldImage = instance.Content.Load<Texture2D>("Arts/sold");

			items = new Dictionary<Entity, bool>();
		}

		public void NewItems(GameRoot instance)
		{
			items.Clear();
			items.Add(new Sword(instance, ObjectType.Sword, 8), false);
			items.Add(new Shield(instance, ObjectType.Shield, 8), false);
			items.Add(new Potion(instance, ObjectType.Potion, 4), false);

			currentItemIndex = 2;
		}

		public void Move(int index)
		{
			currentItemIndex += index;

			if (currentItemIndex > 3)
				currentItemIndex = 3;

			else if (currentItemIndex < 1)
				currentItemIndex = 1;
		}

		public Entity Buy(Player player)
		{
			items[items.ElementAt(currentItemIndex - 1).Key] = true;
			return items.ElementAt(currentItemIndex - 1).Key;
		}

		public bool IsSold()
		{
			return items[items.ElementAt(currentItemIndex - 1).Key];
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			*//*int i = 1;

			foreach (var item in items)
			{
				var xCoordinate = i * Constants.textureWidth;
				var yCoordinate = 3 * Constants.textureHeight;

				if (!item.Value)
				{
					spriteBatch.Draw(item.Key.Texture, new Vector2(xCoordinate, yCoordinate), null, Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0f);

					spriteBatch.DrawString(font, item.Key.Value.ToString(), new Vector2(xCoordinate + fontSize, yCoordinate + distance - fontSize), PenColor, //особенно это переделать, убрать -75
								0, new Vector2(0, 0), Constants.fontScale, SpriteEffects.None, 0);

					spriteBatch.DrawString(font, itemsPrice.ToString(), new Vector2(xCoordinate + 58, yCoordinate + Constants.textureHeight + 30), PenColor,
							0, new Vector2(0, 0), Constants.fontScale, SpriteEffects.None, 0);
				}

				else
				{
					spriteBatch.Draw(soldImage, new Vector2(xCoordinate, yCoordinate), null, Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0f);
				}

				if (i == currentItemIndex)
					spriteBatch.Draw(choosedItemImage, new Vector2(xCoordinate, yCoordinate), null, Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0f);

				i++;
			}*//*
		}
	}
}
*/