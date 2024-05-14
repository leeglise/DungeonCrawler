using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using System.ComponentModel.Design;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ShopMVC.ShopFolder;

namespace DungeonCrawler.Code.GameplayMVC
{
    public class GameplayView : IGameplayVIew
    {
        private Texture2D background;
        private Texture2D fieldFrame;
        private Texture2D invulnerableImage;

        private SpriteFont font;
        private Color penColor = Color.White;
        private int fontSize = Constants.fontSize;

        private bool playerInvulnerable;

		private List<List<Field>> map;
		private List<Vector2> viableFields;

        public void Initialize(GameRoot instance)
        {
            font = instance.Content.Load<SpriteFont>("Font");

            background = instance.Content.Load<Texture2D>("EntityArts/background");
            fieldFrame = instance.Content.Load<Texture2D>("EntityArts/fieldFrame");
            invulnerableImage = instance.Content.Load<Texture2D>("EntityArts/invulnerable");
        }

        public void UpdateParameters(List<List<Field>> map, Player player, List<Vector2> viableFields)
        {
            this.map = map;
            this.viableFields = viableFields;

            playerInvulnerable = player.Invulnerable;
        }

        public void Draw(GameRoot instance, SpriteBatch spriteBatch)
        {
			spriteBatch.Draw(
					background, new Vector2(0, 0), null,
					Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

			Texture2D texture;
			int value;

			foreach (var line in map)
			{
				foreach (var field in line)
				{
					texture = instance.Content.Load<Texture2D>("EntityArts/" + field.EntityType);
					//var texture = instance.Content.Load<Texture2D>("Arts/" + field.ContentType + (field.Value > 0 ? field.Value.ToString() : ""));
					spriteBatch.Draw(
						texture,
						new Vector2(field.Coordinates.X, field.Coordinates.Y),
						null,
						Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

					if (field.EntityType == EntityType.Player)
					{
						if (playerInvulnerable)
						{
							spriteBatch.Draw(
								invulnerableImage,
								new Vector2(field.Coordinates.X, field.Coordinates.Y),
								null,
								Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
						}
					}
					else
					{
						value = field.Value;
						if (value > 0)
						{
							spriteBatch.DrawString(font, value.ToString(),
								new Vector2(
									field.Coordinates.X + Constants.textIndentationX,
									field.Coordinates.Y + Constants.textureSize - fontSize - Constants.textIndentationY),
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
			}

			foreach (var field in viableFields)
			{
				spriteBatch.Draw(
						fieldFrame,
						field * Constants.textureSize,
						null,
						Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
			}
		}
    }
}
