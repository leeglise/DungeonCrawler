using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.ShopMVC.ShopFolder;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code
{
	public class InterfaceView : IInterfaceView
	{

		private List<Texture2D> playerImages;

		private SpriteFont font;
		private Color penColor = Color.White;
		private int fontSize = Constants.fontSize;


		private List<int> playerParameters;
		private Item playerItem;

		private int level;

		public void Initialize(GameRoot instance)
		{
			font = instance.Content.Load<SpriteFont>("Font");

			playerImages = new List<Texture2D>
			{
				instance.Content.Load<Texture2D>("EntityArts/sword"),
				instance.Content.Load<Texture2D>("EntityArts/shield"),
				instance.Content.Load<Texture2D>("EntityArts/heart"),
				instance.Content.Load<Texture2D>("EntityArts/coin"),
				instance.Content.Load<Texture2D>("EntityArts/star")
			};
		}

		public void UpdatePlayer(Player player)
		{
			playerParameters = player.GetParameters();
			playerItem = player.Item;
		}

		public void UpdateParameters(Player player, int level)
		{
			UpdatePlayer(player);
			this.level = level;
		}

		public void Draw(GameRoot instance, SpriteBatch spriteBatch)
		{
			Texture2D texture;

			for (int i = 0; i < playerParameters.Count - 1; i++)
			{
				if ((i == 0 || i == 1) && playerParameters[i] == 0)
				{
					texture = instance.Content.Load<Texture2D>("EntityArts/empty");

					spriteBatch.Draw(
						texture,
						new Vector2(i * Constants.textureSize, Constants.interfaceStartPositionY),
						null,
						Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
				}
				else
				{
					texture = playerImages[i];

					spriteBatch.Draw(
						texture,
						new Vector2(i * Constants.textureSize, Constants.interfaceStartPositionY),
						null,
						Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

					spriteBatch.DrawString(font, playerParameters[i].ToString(),
						new Vector2(Constants.textureSize * i + Constants.textIndentationX,
						Constants.textStartPositionY - fontSize - Constants.textIndentationY),
						penColor,
						0,
						new Vector2(0, 0),
						Constants.textureScale,
						SpriteEffects.None,
						0
						);
				}
			}

			if (playerItem.ItemType == ItemType.empty)
			{
				texture = instance.Content.Load<Texture2D>("EntityArts/empty");
			}
			else
			{
				texture = instance.Content.Load<Texture2D>("ShopArts/" + playerItem.ItemType);
			}
			spriteBatch.Draw(
				texture,
				new Vector2(4 * Constants.textureSize, Constants.interfaceStartPositionY),
				null,
				Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);


			texture = playerImages[4];
			spriteBatch.Draw(
				texture,
				new Vector2(0, Constants.interfaceStartPositionY + Constants.textureSize),
				null,
				Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

			var score = playerParameters[4];
			var scoreText = (score / 1000).ToString() + (score / 100 % 10).ToString() + (score / 10 % 10).ToString() + (score % 10).ToString();

			spriteBatch.DrawString(font, scoreText,
						new Vector2(Constants.scoreTextureWidth, Constants.textStartPositionY),
						penColor,
						0,
						new Vector2(0, 0),
						Constants.textureScale,
						SpriteEffects.None,
						0
						);

			spriteBatch.DrawString(font, "LVL: " + level.ToString(),
						new Vector2(Constants.scoreTextureWidth + fontSize * 4, Constants.textStartPositionY),
						penColor,
						0,
						new Vector2(0, 0),
						Constants.textureScale,
						SpriteEffects.None,
						0
						);
		}
	}
}
