namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Enemy : Entity
    {
        public Enemy(EntityType entityType, int value) : base(entityType, value) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
