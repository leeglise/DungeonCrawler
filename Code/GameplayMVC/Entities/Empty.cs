using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Empty : IInteractable
    {
        public EntityType EntityType => EntityType.Empty;

        public int Value { get { return 0; } set {} }

        public Empty() { }

        public void Interact(IInteractable entity) { }
    }
}
