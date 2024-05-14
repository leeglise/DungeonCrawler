using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using DungeonCrawler.Code.GameplayMVC;

namespace DungeonCrawler.Code.GameplayMVC.Entities
{
    class LevelEntities
    {
        private string[] distributions = File.ReadAllLines("Levels/distribution.txt");
        private int distributionsCount;
        List<char> distribution;

        private List<int> enemiesPowersList;
        private int enemiesCount;
        private int bossPower;

        private List<int> swordsPowersList;
        private int swordsCount;

        private List<int> shieldsPowersList;
        private int shieldsCount;

        private List<int> coinsValuesList;
        private int coinsCount;

        private List<int> potionsValuesList;
        private int potionsCount;

        private int level = 0;

        private Random rnd = new Random();

        private double scaleModificator = GameplayConstants.levelDifficultyScale;

        private double scale;

        public LevelEntities()
        {
            enemiesPowersList = new List<int>();
            swordsPowersList = new List<int>();
            shieldsPowersList = new List<int>();
            coinsValuesList = new List<int>();
            potionsValuesList = new List<int>();

            distributionsCount = distributions.Length;
        }

        public void Update(int level)
        {
            this.level = level;

            scale = 1 + (double)(level - 1) % 4 * scaleModificator;

            enemiesCount = 0;
            enemiesPowersList.Clear();

            swordsCount = 0;
            swordsPowersList.Clear();

            shieldsCount = 0;
            shieldsPowersList.Clear();

            coinsCount = 0;
            coinsValuesList.Clear();

            potionsCount = 0;
            potionsValuesList.Clear();

            var rnd = new Random();

            var distributionNumber = rnd.Next(distributionsCount);
            distribution = distributions[distributionNumber].ToList();

            foreach (var symbol in distribution)
            {
                switch (symbol)
                {
                    case '1':
                        enemiesCount++;
                        break;
                    case '2':
                        swordsCount++;
                        break;
                    case '3':
                        shieldsCount++;
                        break;
                    case '4':
                        coinsCount++;
                        break;
                    case '5':
                        potionsCount++;
                        break;
                }
            }

            GenerateEnemies();
            GenerateSwords();
            GenerateShields();
            GenerateCoins();
            GeneratePotions();
        }

        public void GenerateEnemies()
        {
            int currentSum = 0;

            int idealSum;
            int idealLvl = 0;

            int minLvl;
            int maxLvl;

            if (level % 4 == 0)
                maxLvl = 3 + level / 4 - 1;
            else
                maxLvl = 3 + level / 4;

            minLvl = maxLvl - 4 <= 0 ? 1 : maxLvl - 4;

            var currentMinLvl = minLvl;
            var currentMaxLvl = maxLvl;

            for (int j = 0; j < enemiesCount; j++)
            {
                idealLvl = (maxLvl + minLvl) / 2;

                if ((maxLvl - minLvl + 1) % 2 == 0)
                    idealLvl += j % 2;

                idealSum = (int)(idealLvl * j * scale);

                if (currentSum > idealSum && currentMaxLvl > idealLvl)
                {
                    currentMaxLvl--;
                    currentMinLvl = minLvl;
                }

                else if (currentSum < idealSum && currentMinLvl < idealLvl)
                {
                    currentMinLvl++;
                    currentMaxLvl = maxLvl;
                }

                else if (currentSum == idealSum)
                {
                    currentMaxLvl = maxLvl;
                    currentMinLvl = minLvl;
                }

                var enemy = rnd.Next(currentMinLvl, currentMaxLvl + 1);

                enemiesPowersList.Add(enemy);
                currentSum += enemy;
            }

            bossPower = maxLvl + GameplayConstants.maxLevel / GameplayConstants.bossScale;

            /*Debug.WriteLine("Уровень: " + level + " Максимальная сила: " + maxLvl + " Минимальная сила: "
                + minLvl + " Средняя сила: " + idealLvl + " Суммарная сила: " + currentSum);

            Debug.WriteLine("Количество врагов: " + enemiesPowersList.Count + " Список врагов: ");
            foreach (var e in enemiesPowersList)
                Debug.Write(e + " ");

            Debug.WriteLine("");*/
        }

        public void GenerateSwords()
        {
            int minPower = level / 20 + 1;
            int maxPower = minPower + 1 + (level - 1) / 4;

            int currentMinPower = minPower;
            int currentMaxPower = maxPower;
            int currentSum = 0;

            int idealPower = (maxPower + minPower) / 2 + 1;
            int idealSum;

            for (int i = 0; i < swordsCount; i++)
            {
                idealSum = (int)(idealPower * i * scale);

                if (currentSum > idealSum && currentMaxPower > idealPower)
                {
                    currentMaxPower--;
                    currentMinPower = minPower;
                }

                else if (currentSum < idealSum && currentMinPower < idealPower)
                {
                    currentMinPower++;
                    currentMaxPower = maxPower;
                }

                else if (currentSum == idealSum)
                {
                    currentMaxPower = maxPower;
                    currentMinPower = minPower;
                }

                var sword = rnd.Next(currentMinPower, currentMaxPower + 1);

                swordsPowersList.Add(sword);
                currentSum += sword;
            }


            /*Debug.WriteLine("Уровень: " + level + " Максимальная сила меча: " + maxPower + " Минимальная сила меча: "
                + minPower + " Средняя сила: " + idealPower + " Суммарная сила: " + currentSum);

            Debug.WriteLine("Количество мечей: " + swordsPowersList.Count + " Список мечей: ");
            foreach (var e in swordsPowersList)
                Debug.Write(e + " ");

            Debug.WriteLine("");*/

        }

        public void GenerateShields()
        {
            int minPower = level / 20 + 1;
			int maxPower = minPower + 1 + (level - 1) / 4;

            int currentMinPower = minPower;
            int currentMaxPower = maxPower;
            int currentSum = 0;

            int idealPower = (maxPower + minPower) / 2 + 1;
            int idealSum;

            for (int i = 0; i < shieldsCount; i++)
            {
                idealSum = (int)(idealPower * i * scale);

                if (currentSum > idealSum && currentMaxPower > idealPower)
                {
                    currentMaxPower--;
                    currentMinPower = minPower;
                }

                else if (currentSum < idealSum && currentMinPower < idealPower)
                {
                    currentMinPower++;
                    currentMaxPower = maxPower;
                }

                else if (currentSum == idealSum)
                {
                    currentMaxPower = maxPower;
                    currentMinPower = minPower;
                }

                var shield = rnd.Next(currentMinPower, currentMaxPower + 1);

                shieldsPowersList.Add(shield);
                currentSum += shield;
            }

            /*Debug.WriteLine("Уровень: " + level + " Максимальная сила щита: " + maxPower + " Минимальная сила щита: "
                + minPower + " Средняя сила: " + idealPower + " Суммарная сила: " + currentSum);

            Debug.WriteLine("Количество щитов: " + shieldsPowersList.Count + " Список щитов: ");
            foreach (var e in shieldsPowersList)
                Debug.Write(e + " ");

            Debug.WriteLine("");*/
        }

        public void GenerateCoins()
        {
            for (int i = 0; i < coinsCount; i++)
            {
                coinsValuesList.Add(rnd.Next(1, 4));
            }

            /*Debug.WriteLine("Количество монет: " + coinsValuesList.Count + " Список монет: ");

            foreach (var e in coinsValuesList)
                Debug.Write(e + " ");

            Debug.WriteLine("");*/
        }

        public void GeneratePotions()
        {
            for (int i = 0; i < potionsCount; i++)
            {
                potionsValuesList.Add(rnd.Next(1, 5));
            }

            /*Debug.WriteLine("Количество хилок: " + potionsValuesList.Count + " Список хилок: ");

            foreach (var e in potionsValuesList)
                Debug.Write(e + " ");

            Debug.WriteLine("");*/
        }

        public int GetEnemy()
        {
            var enemy = enemiesPowersList.First();
            enemiesPowersList.Remove(enemy);

            return enemy;
        }

        public int GetSword()
        {
            var sword = swordsPowersList.First();
            swordsPowersList.Remove(sword);

            return sword;
        }

        public int GetShield()
        {
            var shield = shieldsPowersList.First();
            shieldsPowersList.Remove(shield);

            return shield;
        }

        public int GetCoin()
        {
            var coin = coinsValuesList.First();
            coinsValuesList.Remove(coin);

            return coin;
        }

        public int GetPotion()
        {
            var potion = potionsValuesList.First();
            potionsValuesList.Remove(potion);

            return potion;
        }

        public int GetBossPower()
        {
            return bossPower;
        }

        public List<char> GetDistribution()
        {
            return distribution;
        }
    }
}
