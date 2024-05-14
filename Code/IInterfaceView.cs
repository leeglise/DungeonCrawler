using DungeonCrawler.Code.GameplayMVC.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code
{
	public interface IInterfaceView
	{
		void Initialize(GameRoot instance);
		void UpdatePlayer(Player player);
		void UpdateParameters(Player player, int level);
		void Draw(GameRoot instance, SpriteBatch spriteBatch);
	}
}
