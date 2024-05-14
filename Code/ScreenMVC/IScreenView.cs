using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code.ScreenMVC
{
    public interface IScreenView
    {
        void Initialize(GameRoot instance);

        void UpdateParameters(List<(int, ButtonType)> currentButtons, (int, ButtonType) choosedButton, GameState gameState);

        void Draw(GameRoot instance, SpriteBatch spriteBatch);
    }
}
