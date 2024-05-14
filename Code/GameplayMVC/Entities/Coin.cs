namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class Coin : Entity
    {
        public Coin(EntityType entityType, int value) : base(entityType, value) { }

        public override void Interact(IInteractable entity)
        {
            entity.Interact(this);
        }
    }
}
