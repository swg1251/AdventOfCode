using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day09 : IDay
	{
		private List<List<int>> heightMap;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 9);

			heightMap = new List<List<int>>();
			foreach (var line in lines)
			{
				heightMap.Add(line.Select(c => Convert.ToInt32(c.ToString())).ToList());
			}
		}

		public void Solve()
		{
			var lowPoints = new List<(int y, int x)>();

			for (int i = 0; i < heightMap.Count; i++)
			for (int j = 0; j < heightMap[i].Count; j++)
			{
				if (IsLocalMinimum(i, j, heightMap))
				{
					lowPoints.Add((i, j));
				}
			}

			Console.WriteLine($"The sum of low point risk levels (part one) is {lowPoints.Sum(lp => heightMap[lp.y][lp.x] + 1)}");

			var basinSizes = new List<int>();
			foreach (var lowPoint in lowPoints)
			{
				var basinPoints = new HashSet<(int y, int x)> { lowPoint };

				var points = new Queue<(int y, int x)>();
				points.Enqueue(lowPoint);

				while (points.Any())
				{
					var (y, x) = points.Dequeue();

					var newPoints = GetAdjacentBasinPoints(y, x, heightMap);
					foreach (var newPoint in newPoints)
					{
						if (basinPoints.Add(newPoint))
						{
							points.Enqueue(newPoint);
						}
					}
				}

				basinSizes.Add(basinPoints.Count);
			}

			basinSizes.Sort();
			var topThree = basinSizes.TakeLast(3).ToList();
			var product = topThree[0] * topThree[1] * topThree[2];

			Console.WriteLine($"The product of the three largest basin sizes (part two) is {product}");
		}

		private bool IsLocalMinimum(int y, int x, List<List<int>> map)
		{
			var height = map.Count;
			var width = map.First().Count;
			var value = map[y][x];

			if (y - 1 >= 0 && map[y - 1][x] <= value)
			{
				return false;
			}
			if (y + 1 < height && map[y + 1][x] <= value)
			{
				return false;
			}
			if (x - 1 >= 0 && map[y][x - 1] <= value)
			{
				return false;
			}
			if (x + 1 < width && map[y][x + 1] <= value)
			{
				return false;
			}
			if (y - 1 >= 0 && x - 1 >= 0 && map[y - 1][x - 1] <= value)
			{
				return false;
			}
			if (y - 1 >= 0 && x + 1 < width && map[y - 1][x + 1] <= value)
			{
				return false;
			}
			if (y + 1 < height && x - 1 >= 0 && map[y + 1][x - 1] <= value)
			{
				return false;
			}
			if (y + 1 < height && x + 1 < width && map[y + 1][x + 1] <= value)
			{
				return false;
			}

			return true;
		}

		private List<(int y, int x)> GetAdjacentBasinPoints(int y, int x, List<List<int>> map)
		{
			var height = map.Count;
			var width = map.First().Count;
			var value = map[y][x];
			var points = new List<(int y, int x)>();

			if (y - 1 >= 0 && map[y - 1][x] > value && map[y - 1][x] != 9)
			{
				points.Add((y - 1, x));
			}
			if (y + 1 < height && map[y + 1][x] > value && map[y + 1][x] != 9)
			{
				points.Add((y + 1, x));
			}
			if (x - 1 >= 0 && map[y][x - 1] > value && map[y][x - 1] != 9)
			{
				points.Add((y, x - 1));
			}
			if (x + 1 < width && map[y][x + 1] > value && map[y][x + 1] != 9)
			{
				points.Add((y, x + 1));
			}

			return points;
		}
	}
}
