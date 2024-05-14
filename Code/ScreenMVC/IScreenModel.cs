using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using static DungeonCrawler.Code.GameplayMVC.IGameplayModel;

namespace DungeonCrawler.Code.ScreenMVC
{
    public interface IScreenModel
    {
        int ButtonsCount { get; }
        int CurrentButtonIndex { get; }
        List<(int, ButtonType)> CurrentButtons { get; }

        event EventHandler<ScreenEventArgs> Updated;
        event EventHandler<ScreenEventArgs> ButtonPressed;

        void Initialize();
        void Update(int newIndex);
        void UpdateScreenModel(GameState gameState);
        void PressButton();
    }

    public class ScreenEventArgs : EventArgs
    {
        public List<(int, ButtonType)> CurrentButtons { get; set; }
        public (int, ButtonType) ChoosedButton { get; set; }
        public GameState GameState { get; set; }
        public ButtonType PressedButton { get; set; }
    }
}
