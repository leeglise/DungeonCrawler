namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Potion : Entity
    {
        public Potion(EntityType entityType, int value) : base(entityType, value) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
