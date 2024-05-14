using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler.Code.ScreenMVC
{
    public class ScreenView : IScreenView
    {
        private List<(int, ButtonType)> currentButtons;
        private (int, ButtonType) choosedButton;

        private Texture2D gameOver;
        private Texture2D gameWon;
        private Texture2D pause;
        private Texture2D mainMenu;
        private Texture2D controls;

        private Texture2D currentBackground;

        public void Initialize(GameRoot instance)
        {
            gameOver = instance.Content.Load<Texture2D>("ScreenArts/gameover");
            gameWon = instance.Content.Load<Texture2D>("ScreenArts/gamewon");
            pause = instance.Content.Load<Texture2D>("ScreenArts/pause");
            mainMenu = instance.Content.Load<Texture2D>("ScreenArts/mainmenu");
            controls = instance.Content.Load<Texture2D>("ScreenArts/controlsBackground");
        }

        public void UpdateParameters(List<(int, ButtonType)> currentButtons, (int, ButtonType) choosedButton, GameState gameState)
        {
            this.currentButtons = currentButtons;
            this.choosedButton = choosedButton;
            switch (gameState)
            {
                case GameState.MainMenu:
                    currentBackground = mainMenu;
                    break;
                case GameState.GameOver:
                    currentBackground = gameOver;
                    break;
                case GameState.GameWon:
                    currentBackground = gameWon;
                    break;
                case GameState.Controls:
                    currentBackground = controls;
                    break;
                default:
                    currentBackground = pause;
                    break;
            }
        }

        public void Draw(GameRoot instance, SpriteBatch spriteBatch)
        {
			spriteBatch.Draw(
					currentBackground, new Vector2(0, 0), null,
					Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);

			float coordinateY;
            Texture2D texture;

            for (int i = 0; i < currentButtons.Count; i++)
            {
                texture = instance.Content.Load<Texture2D>("ScreenArts/" + currentButtons[i].Item2);
                coordinateY = currentButtons[i].Item1 * ScreenConstants.buttonHeight;
				spriteBatch.Draw(
                    texture,
                    new Vector2(ScreenConstants.buttonStartX, ScreenConstants.buttonStartY + coordinateY), 
                    null, Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
			}

            texture = instance.Content.Load<Texture2D>("ScreenArts/choosed" + choosedButton.Item2);
            coordinateY = choosedButton.Item1 * ScreenConstants.buttonHeight;

			spriteBatch.Draw(
					texture,
					new Vector2(ScreenConstants.buttonStartX, ScreenConstants.buttonStartY + coordinateY),
					null, Color.White, 0, new Vector2(0, 0), Constants.textureScale, SpriteEffects.None, 0);
        }
    }
}
