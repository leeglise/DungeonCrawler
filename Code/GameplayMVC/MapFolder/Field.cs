using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.GameplayMVC.Entities;

namespace DungeonCrawler.Code.GameplayMVC.MapFolder
{
    public class Field
    {
        private IInteractable entity;

        private Vector2 coordinates;

        public EntityType EntityType { get { return entity.EntityType; } }
        public int Value { get { return entity.Value; } set { entity.Value = value; } }
        public Vector2 Coordinates { get { return coordinates; } }

        public Field() { }
        public Field(IInteractable entity, Vector2 coordinates)
        {
            this.entity = entity;
            this.coordinates = coordinates * Constants.textureSize;
        }

        public void Update(Player player)
        {
            entity.Interact(player);
            entity = player;
        }

        public void Empty()
        {
            entity = new Empty();
        }
    }
}
