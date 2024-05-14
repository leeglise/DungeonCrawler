using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using DungeonCrawler.Code.GameplayMVC.MapFolder;
using DungeonCrawler.Code.GameplayMVC.Entities;
using DungeonCrawler.Code.ShopMVC.ShopFolder;

namespace DungeonCrawler.Code.GameplayMVC
{
    public class GameplayModel : IGameplayModel
    {
        private Player player;
        private Map map;

        private GameState gameState;

        public Player Player { get { return player; } }
        public Map Map { get { return map; } }

        public event EventHandler<GameplayEventArgs> Updated = delegate { };
		public event EventHandler<GameplayEventArgs> GameStateChanged = delegate { };

		private List<Vector2> viableMapFields;

		public List<Vector2> ViableFields => viableMapFields;

		public void Initialize()
        {
            player = new Player(EntityType.Player);
            player.PlayerDied += GameOver;
            player.PlayerWon += GameWon;

            map = new Map();
            map.GenerateMap(player);

            viableMapFields = new List<Vector2>();

            gameState = GameState.PlayingLevel;
        }

        public void Update()
        {
            var currentFieldType = map.Update(player);

            if (currentFieldType == EntityType.Portal)
            {
                map.NewLevel(player);
            }

            if (currentFieldType == EntityType.Shopman)
            {
                GameStateChanged.Invoke(this, new GameplayEventArgs { GameState = GameState.InShop });
            }

            UpdateViableMapFields();
            Updated.Invoke(this, new GameplayEventArgs { Map = map.GetMap(), Player = player, ViableMapFields = viableMapFields, Level = map.Level });
        }

        public void UseItem()
        {
            var usedItem = player.UseItem();
            switch (usedItem.ItemType)
            {
                case ItemType.bomb:
                    map.DamageEnemies(usedItem.Value);
                    UpdateViableMapFields();
                    break;

                case ItemType.healthUp:
                    player.HealthUp(usedItem.Value);
                    break;

                case ItemType.rewind:
                    player.SetStartPosition();
                    map.RestartLevel(player);
					UpdateViableMapFields();
					break;

                case ItemType.skip:
                    player.SetStartPosition();
                    map.NewLevel(player);
                    UpdateViableMapFields();
                    break;

                case ItemType.empoweredShield:
                    player.MakeInvulnerable();
                    break;

                case ItemType.dice:
                    player.RandomStats();
                    break;
            }

			Updated.Invoke(this, new GameplayEventArgs { Map = map.GetMap(), Player = player, ViableMapFields = viableMapFields, Level = map.Level });
		}

		public void UpdateViableMapFields()
		{
			viableMapFields.Clear();
			var directions = new Vector2[]
			{
				new Vector2(0, -1) + player.Position,
				new Vector2(0, 1) + player.Position,
				new Vector2(-1, 0) + player.Position,
				new Vector2(1, 0) + player.Position,
			};
			foreach (var direction in directions)
			{
				if (!map.IsBounds(direction))
				{
					viableMapFields.Add(direction);
				}
			}

            if (viableMapFields.Count == 0)
            {
                GameOver(this, new EventArgs());
            }
		}

		public void Restart()
        {
            player.SetDefaultParameters();
            map.Restart(player);
            UpdateViableMapFields();
			Updated.Invoke(this, new GameplayEventArgs { Map = map.GetMap(), Player = player, ViableMapFields = viableMapFields, Level = map.Level });
		}

        public void GameOver(object sender, EventArgs e)
        {
            gameState = GameState.GameOver;
            GameStateChanged.Invoke(this, new GameplayEventArgs { GameState = gameState });
        }

        public void GameWon(object sender, EventArgs e)
        {
            gameState = GameState.GameWon;
			GameStateChanged.Invoke(this, new GameplayEventArgs { GameState = gameState });
		}
    }
}
