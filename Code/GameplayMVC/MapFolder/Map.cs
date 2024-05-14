using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using DungeonCrawler.Code.GameplayMVC;
using DungeonCrawler.Code.GameplayMVC.Entities;

namespace DungeonCrawler.Code.GameplayMVC.MapFolder
{
    public class Map
    {
        private List<List<Field>> map;

        private LevelEntities levelEntities;

        private int level;

        public int Level { get { return level; } }

        public Map()
        {
            map = new List<List<Field>>
            {
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList()
            };

            levelEntities = new LevelEntities();

            level = 0;
        }

        public void GenerateMap(Player player)
        {
            level += 1;

            levelEntities.Update(level);

            var rnd = new Random();

            var distribution = levelEntities.GetDistribution();

            IInteractable entity;

            for (int i = 0; i < GameplayConstants.mapSize; i++)
            {
                for (int j = 0; j < GameplayConstants.mapSize; j++)
                {
                    if (i == 0 && j == 2)
                    {
                        if (level == GameplayConstants.maxLevel)
                            entity = new Enemy(EntityType.Boss, levelEntities.GetBossPower());
                        else
                            entity = new Portal(EntityType.Portal);

                        map[i][j] = new Field(entity, new Vector2(j, i));
                    }

                    else if (i == 4 && j == 2)
                        map[i][j] = new Field(player, new Vector2(j, i));

                    else
                    {
                        var index = rnd.Next(distribution.Count);
                        var fieldNumber = int.Parse(distribution[index].ToString());

                        var currentEntity = (EntityType)fieldNumber;

                        int value;

                        switch (currentEntity)
                        {
                            case EntityType.Enemy:
                                value = levelEntities.GetEnemy();
                                entity = new Enemy(currentEntity, value);
                                break;

                            case EntityType.Sword:
                                value = levelEntities.GetSword();
                                entity = new Sword(currentEntity, value);
                                break;

                            case EntityType.Shield:
                                value = levelEntities.GetShield();
                                entity = new Shield(currentEntity, value);
                                break;

                            case EntityType.Coin:
                                value = levelEntities.GetCoin();
                                entity = new Coin(currentEntity, value);
                                break;

                            case EntityType.Potion:
                                value = levelEntities.GetPotion();
                                entity = new Potion(currentEntity, value);
                                break;

                            case EntityType.Shopman:
                                entity = new Shopman(currentEntity);
                                break;

                            default:
                                entity = new Empty();
                                break;
                        }

                        map[i][j] = new Field(entity, new Vector2(j, i));
                        distribution.Remove(distribution[index]);
                    }
                }
            }
        }

        public void NewLevel(Player player)
        {
            GenerateMap(player);
        }

        public void RestartLevel(Player player)
        {
            level -= 1;
            GenerateMap(player);
        }

        public void Restart(Player player)
        {
            level = 0;

            map.Clear();

            map = new List<List<Field>>
            {
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList(),
                Enumerable.Repeat(new Field(), 5).ToList()
            };

            GenerateMap(player);
        }

        public void Interact(Vector2 coordinates, Player player)
        {
            map[(int)coordinates.Y][(int)coordinates.X].Update(player);
        }

        public EntityType Update(Player player)
        {
            var previousX = (int)player.PreviousPosition.X;
            var previousY = (int)player.PreviousPosition.Y;

            map[previousY][previousX].Empty();

            var x = (int)player.Position.X;
            var y = (int)player.Position.Y;

            var currentFieldType = map[y][x].EntityType;

            map[y][x].Update(player);

            return currentFieldType;
        }

        public void DamageEnemies(int value)
        {
            for (int i = 0; i < GameplayConstants.mapSize; i++)
            {
                for (int j = 0; j < GameplayConstants.mapSize; j++)
                {
                    if (map[i][j].EntityType == EntityType.Enemy)
                    {
                        var newValue = map[i][j].Value - value;
                        if (newValue <= 0)
                        {
                            map[i][j] = new Field(new Empty(), new Vector2(j, i));
                        }
                        else
                        {
                            map[i][j].Value = newValue;
                        }
                    }
                }
            }
        }

        public bool IsBounds(Vector2 coordinates)
        {
            var x = (int)coordinates.X;
            var y = (int)coordinates.Y;

            if (x >= GameplayConstants.mapSize || y >= GameplayConstants.mapSize || x <= -1 || y <= -1 || map[y][x].EntityType == EntityType.Empty)
                return true;

            return false;
        }

        public Field GetField(Vector2 coordinates)
        {
            return map[(int)coordinates.Y][(int)coordinates.X];
        }

        public List<List<Field>> GetMap()
        {
            return map;
        }
    }
}

/*
0 0 0 0 0
0 0 0 0 0
0 0 0 0 0
0 0 0 0 0
0 0 0 0 0
 */
