using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.GameplayMVC.Entities;

namespace DungeonCrawler.Code.GameplayMVC
{
    public interface IGameplayVIew
    {
        void Initialize(GameRoot instance);

        void UpdateParameters(List<List<Field>> map, Player player, List<Vector2> ViableMapFields);

        void Draw(GameRoot instance, SpriteBatch spriteBatch);
    }
}
