using DungeonCrawler.Code.GameplayMVC;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Code
{
	public static class Constants
	{
		public static readonly int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		public static readonly int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

		public static readonly int mapSize = 5;

		//высота окна = множитель * высоту экрана, ширина окна = размер текстур * размер карты

		public static readonly float sizeScale = 1200 / 1440f; //устанавливает множитель разрешения на 0.8333 для высоты экрана

		public static readonly int height = (int)(screenHeight * sizeScale);

		public static readonly float textureScale = height / 1440f;

		public static readonly int textureDefaultSize = 216;
		public static readonly int textureSize = (int)(textureDefaultSize * textureScale);

		public static readonly int width = textureSize * mapSize;

		//размер текстуры звезды, нужно для отступов 
		public static readonly int scoreTextureWidth = (int)(40 * textureScale);
		public static readonly int scoreTextureHeight = (int)(50 * textureScale);

		//отступы для интерфейса
		public static readonly int textStartPositionX = 0;
		public static readonly int textStartPositionY = (int)(textureSize * (mapSize + 1) + 50 * textureScale);
		public static readonly int interfaceStartPositionX = 0;
		public static readonly int interfaceStartPositionY = (int)(textureSize * mapSize + 50 * textureScale);

		public static readonly int fontDefaultSize = 50;
		public static readonly int fontSize = (int)(fontDefaultSize * textureScale);

		//отсутпы для текста
		public static readonly int textIndentationX = (int)(30 * textureScale); 
		public static readonly int textIndentationY = (int)(30 * textureScale); 
	}
}
