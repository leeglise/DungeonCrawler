using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.ShopMVC.ShopFolder;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    public class Player : Entity
    {
        #region Fields
        private int maxHealth = 3;
        private int currentHealth;
        private int swordPower;
        private int shieldPower;
        private int coinsCount;
        private int score;

        private bool invulnerable;

		private Item item;

		protected Vector2 position;
        protected Vector2 previousPosition;
        #endregion

        public event EventHandler PlayerDied = delegate { };
        public event EventHandler PlayerWon = delegate { };

        #region Properties
        public Item Item { get { return item; } }
        public Vector2 Position { get { return position; } }
        public Vector2 PreviousPosition { get { return previousPosition; } }
        public int Health { get { return currentHealth; } }
        public int SwordPower { get { return swordPower; } }
        public int ShieldPower { get { return shieldPower; } }
        public int CoinsCount { get { return coinsCount; } }
        public int Score { get { return score; } }
        public bool Invulnerable { get { return invulnerable; } }
		#endregion

		#region Methods
		public Player(EntityType entityType) : base(entityType)
        {
            SetStartPosition();

            SetDefaultParameters();
            item = new EmptyItem(ItemType.empty);
        }

        public override void Interact(IInteractable entity)
        {
            var value = entity.Value;

            var entityType = entity.EntityType;
            switch (entityType)
            {
                case EntityType.Enemy:
                    Fight(value, entity);
                    break;

                case EntityType.Boss:
                    Fight(value, entity);
                    break;

                case EntityType.Sword:
                    swordPower = value;
                    break;

                case EntityType.Shield:
                    shieldPower = value;
                    break;

                case EntityType.Coin:
                    coinsCount += value;
                    break;

                case EntityType.Potion:
                    currentHealth += value;
                    currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
                    break;

                case EntityType.Portal:
                    SetStartPosition();
                    break;
            }
        }

        public void Fight(int value, IInteractable enemy)
        {
            var enemyType = enemy.EntityType;

            if (invulnerable)
            {
                value = 0;
                invulnerable = false;
                score += enemy.Value;
            }

            else
            {
				var temp = swordPower;
				swordPower -= value;
				value -= temp;

				swordPower = swordPower < 0 ? 0 : swordPower;
				value = value < 0 ? 0 : value;

				temp = shieldPower;
				shieldPower -= value;
				value -= temp;

				shieldPower = shieldPower < 0 ? 0 : shieldPower;
				value = value < 0 ? 0 : value;

				temp = currentHealth;
				currentHealth -= value;
				value -= temp;
			}

            if (value <= 0 && currentHealth > 0)
            {
				score += enemy.Value;
				if (enemyType == EntityType.Boss)
                    PlayerWon.Invoke(this, new EventArgs());
            }

            else if (currentHealth <= 0)
            {
               PlayerDied.Invoke(this, new EventArgs());
			}
        }

        public Item UseItem()
        {
            var usedItem = item;
            item = new EmptyItem(ItemType.empty);
            return usedItem;
        }

        public void HealthUp(int value)
        {
            maxHealth += value;
            currentHealth += value;
        }

        public void MakeInvulnerable()
        {
            invulnerable = true;
        }

        public void RandomStats()
        {
            var rnd = new Random();

            maxHealth = rnd.Next(1, maxHealth + 5);
            currentHealth = maxHealth;

            swordPower = rnd.Next(1, (int)(GameplayConstants.maxLevel / GameplayConstants.diceScale));
            shieldPower = rnd.Next(1, (int)(GameplayConstants.maxLevel / GameplayConstants.diceScale));
        }

        public void BuyItem(Item item, int price)
        {
            coinsCount -= price;
            this.item = item;
        }

        public void Move(Vector2 newPosition)
        {
            previousPosition = position;
            position = newPosition;
        }
        public void SetStartPosition()
        {
            position = GameplayConstants.startPosition;
            previousPosition = new Vector2(-1, -1);
        }

        public void SetDefaultParameters()
        {
            maxHealth = 3;
            currentHealth = maxHealth;
            swordPower = 0;
            shieldPower = 0;
            coinsCount = 0;
            score = 0;

            invulnerable = false;

            SetStartPosition();
        }

        public List<int> GetParameters()
        {
            return new List<int> { swordPower, shieldPower, currentHealth, coinsCount, score };
        }
        #endregion
    }
}
