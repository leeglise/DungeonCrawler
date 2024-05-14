namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Shield : Entity
    {
        public Shield(EntityType entityType, int value) : base(entityType, value) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
