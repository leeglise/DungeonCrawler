namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Portal : Entity
    {
        public Portal(EntityType entityType) : base(entityType) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
