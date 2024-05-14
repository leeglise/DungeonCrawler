using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ShopMVC.ShopFolder;

namespace DungeonCrawler.Code.GameplayMVC
{
    public class GameplayController : IGameplayController
    {
		public event EventHandler PlayerMoved = delegate { };
		public event EventHandler ItemUsed = delegate { };
        public event EventHandler PressedEsc = delegate { };
        

		private KeyboardState previousState;

        public void HandleInput(Map map, Player player, KeyboardState keyboardState)
        {
            var newCoordinates = player.Position;

            if (keyboardState.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape))
            {
                PressedEsc.Invoke(this, new EventArgs());
                return;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && player.Item.ItemType != ItemType.empty)
            {
                ItemUsed.Invoke(this, new EventArgs());
                return;
            }

            if ((keyboardState.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W)) || (keyboardState.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up)))
            {
                newCoordinates += new Vector2(0, -1);
            }

            else if (keyboardState.IsKeyDown(Keys.S) && !previousState.IsKeyDown(Keys.S) || (keyboardState.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down)))
            {
                newCoordinates += new Vector2(0, 1);
            }

            else if (keyboardState.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A) || (keyboardState.IsKeyDown(Keys.Left) && !previousState.IsKeyDown(Keys.Left)))
            {
                newCoordinates += new Vector2(-1, 0);
            }

            else if (keyboardState.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D) || (keyboardState.IsKeyDown(Keys.Right) && !previousState.IsKeyDown(Keys.Right)))
            {
                newCoordinates += new Vector2(1, 0);
            }

            if (newCoordinates != player.Position && !map.IsBounds(newCoordinates))
            {
                player.Move(newCoordinates);
                PlayerMoved.Invoke(this, new EventArgs());
            }
        }

        public void UpdateState(KeyboardState keyboardState)
        {
            previousState = keyboardState;
        }
    }
}
