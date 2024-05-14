using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    public abstract class Entity : IInteractable
    {
        protected EntityType entityType;

        protected int value = 0;

        public int Value { get { return value; } set { this.value = value; } }

        public EntityType EntityType => entityType;

        public Entity() { }

        public Entity(EntityType entityType)
        {
            this.entityType = entityType;
        }

        public Entity(EntityType entityType, int value) : this(entityType)
        {
            this.value = value;
        }

        public abstract void Interact(IInteractable entity);
    }
}
