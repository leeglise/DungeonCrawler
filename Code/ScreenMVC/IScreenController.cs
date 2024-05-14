using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ScreenMVC
{
    public interface IScreenController
    {
        event EventHandler ButtonPressed;
        void HandleInput(KeyboardState keyboardState, IScreenModel screenModel);
        void UpdateState(KeyboardState keyboardState);
    }

    public class ScreenControlsEventArgs : EventArgs
    {
        public KeyboardState KeyboardState { get; set; }

        public IScreenModel ScreenModel { get; set; }
    }
}
