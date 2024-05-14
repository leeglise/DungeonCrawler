using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ScreenMVC
{
    public class ScreenModel : IScreenModel
    {
        public event EventHandler<ScreenEventArgs> Updated;
		public event EventHandler<ScreenEventArgs> ButtonPressed;

		private GameState gameState;
        private int currentButtonIndex;
        private List<(int, ButtonType)> currentButtons;

        public int CurrentButtonIndex => currentButtonIndex;
        public List<(int, ButtonType)> CurrentButtons => currentButtons;
        public int ButtonsCount => currentButtons.Count;

        public void Initialize()
        {
            gameState = GameState.MainMenu;
            currentButtons = new List<(int, ButtonType)>();
            SetMainMenuButtons();
        }

        public void UpdateScreenModel(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Pause:
                    SetPauseButtons();
                    this.gameState = GameState.Pause;
                    break;
                case GameState.MainMenu:
                    SetMainMenuButtons();
                    this.gameState = GameState.MainMenu;
                    break;
                case GameState.GameWon:
                    SetEndGameButtons();
                    this.gameState = GameState.GameWon;
                    break;
                case GameState.GameOver:
                    SetEndGameButtons();
                    this.gameState = GameState.GameOver;
                    break;
                case GameState.Controls:
                    SetControlsButton();
                    this.gameState = GameState.Controls;
                    break;
            }

			Updated.Invoke(this, new ScreenEventArgs { ChoosedButton = currentButtons[currentButtonIndex], CurrentButtons = currentButtons, GameState = gameState });
		}

        public void Update(int newIndex)
        {
            currentButtonIndex = newIndex;
            Updated.Invoke(this, new ScreenEventArgs { ChoosedButton = currentButtons[currentButtonIndex], CurrentButtons = currentButtons, GameState = gameState });
        }

        public void PressButton()
        {
            var pressedButton = currentButtons[currentButtonIndex].Item2;
            ButtonPressed.Invoke(this, new ScreenEventArgs { PressedButton = pressedButton });
        }

        public void SetMainMenuButtons()
        {
            currentButtonIndex = 0;
            currentButtons.Clear();

            currentButtons = new List<(int, ButtonType)>
            {
                (0, ButtonType.start), 
                (1, ButtonType.controls), 
                (2, ButtonType.exit)
            };
        }

        public void SetPauseButtons()
        {
            currentButtonIndex = 0;
			currentButtons.Clear();

			currentButtons = new List<(int, ButtonType)>
			{
				(0, ButtonType.resume),
				(1, ButtonType.restart),
				(2, ButtonType.exit)
			};
		}

        public void SetControlsButton()
        {
            currentButtonIndex = 0;
            currentButtons.Clear();

            currentButtons = new List<(int, ButtonType)>
            {
                (2, ButtonType.exit)
            };
        }

        public void SetEndGameButtons()
        {
            currentButtonIndex = 0;
			currentButtons.Clear();

			currentButtons = new List<(int, ButtonType)>
			{
				(0, ButtonType.restart),
				(1, ButtonType.exit)
			};
		}
	}
}
