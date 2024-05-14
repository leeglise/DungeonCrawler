using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ScreenMVC
{
    public class ScreenController : IScreenController
    {
        public event EventHandler ButtonPressed;

        KeyboardState previousState;

        public void HandleInput(KeyboardState keyboardState, IScreenModel screenModel)
        {
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter))
            {
                ButtonPressed.Invoke(this, new EventArgs());
                return; 
            }

            var newIndex = screenModel.CurrentButtonIndex;

            if (keyboardState.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W) || (keyboardState.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up)))
            {
                newIndex -= 1;
            }
            else if (keyboardState.IsKeyDown(Keys.S) && !previousState.IsKeyDown(Keys.S) || (keyboardState.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down)))
            {
                newIndex += 1;
            }

            if (newIndex >= 0 && newIndex < screenModel.ButtonsCount && newIndex != screenModel.CurrentButtonIndex)
            {
                screenModel.Update(newIndex);
            }
        }

        public void UpdateState(KeyboardState keyboardState)
        {
            previousState = keyboardState;
        }
    }
}
