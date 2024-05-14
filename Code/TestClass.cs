using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonCrawler.Code
{
	class TestClass
	{
		static public void EnemiesGenerator()
		{
			Random rnd = new();

			int currentSum = 0;

			int idealSum;
			int idealLvl = 0;

			int maxLvl = 0;
			int minLvl = 0;

			double scale = 1;

			int enemyCount = 10;

			List<int> enemiesList = new List<int>();

			for (int i = 1; i <= 20; i++)
			{
				currentSum = 0;
				enemiesList.Clear();
				enemyCount = rnd.Next(10, 14);
				scale = 1 + (double)(i - 1) % 4 * 0.125;
				for (int j = 0; j < enemyCount; j++)
				{
					if (i % 4 == 0)
						maxLvl = 3 + i / 4 - 1;

					else
						maxLvl = 3 + i / 4;

					minLvl = maxLvl - 4 <= 0 ? 1 : maxLvl - 4;

					idealLvl = (maxLvl + minLvl) / 2;

					if ((maxLvl - minLvl + 1) % 2 == 0)
						idealLvl += j % 2;

					idealSum = (int)(idealLvl * j * scale);
					var s = idealLvl * j;

					if (currentSum > idealSum)
						maxLvl = idealLvl;
					else if (currentSum < idealSum)
						minLvl = idealLvl;

					var enemy = rnd.Next(minLvl, maxLvl + 1);

					enemiesList.Add(enemy);
					currentSum += enemy;
				}
				if (i % 4 == 0)
					maxLvl = 3 + i / 4 - 1;
				else
					maxLvl = 3 + i / 4;

				minLvl = maxLvl - 4 <= 0 ? 1 : maxLvl - 4;


				Debug.WriteLine("Уровень: " + i + " Максимальная сила: " + maxLvl + " Минимальная сила: "
					+ minLvl + " Суммарная сила: " + currentSum + " Средняя сила: " + idealLvl);
				Debug.WriteLine("Количество врагов: " + enemiesList.Count + " Список врагов: ");
				foreach (var e in enemiesList)
					Debug.Write(e + " ");
				Debug.WriteLine("");

			}

			Debug.WriteLine("\n\n\n\n");

			var currentMinLvl = 0;
			var currentMaxLvl = 0;

			for (int i = 1; i <= 20; i++)
			{
				currentSum = 0;
				enemiesList.Clear();

				if (i % 4 == 0)
					maxLvl = 3 + i / 4 - 1;
				else
					maxLvl = 3 + i / 4;

				minLvl = maxLvl - 4 <= 0 ? 1 : maxLvl - 4;

				enemyCount = rnd.Next(10, 14);

				scale = 1 + (double)(i - 1) % 4 * 0.125;

				for (int j = 0; j < enemyCount; j++)
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

					enemiesList.Add(enemy);
					currentSum += enemy;
				}

				Debug.WriteLine("Уровень: " + i + " Максимальная сила: " + maxLvl + " Минимальная сила: "
					+ minLvl + " Суммарная сила: " + currentSum + " Средняя сила: " + idealLvl);
				Debug.WriteLine("Количество врагов: " + enemiesList.Count + " Список врагов: ");
				foreach (var e in enemiesList)
					Debug.Write(e + " ");
				Debug.WriteLine("");
			}

			double u = 0;
			double o = Math.Sqrt(0.2);
			double penis;
			for (int i = 1; i <= 20; i++)
			{
				var minX = 13 + i * 2 + rnd.Next(0, 5);
				var maxX = 20 + i * 2 + rnd.Next(0, 10);
				var x = rnd.Next(minX, maxX + 1);
				penis = (int)(10 * 1 / (o * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -0.5 * Math.Pow(x - u, 2)));
				Debug.WriteLine(penis);
			}
		}
	}
}
