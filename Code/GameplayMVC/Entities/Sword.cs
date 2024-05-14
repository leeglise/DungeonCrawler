namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Sword : Entity
    {
        public Sword(EntityType entityType, int value) : base(entityType, value) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
