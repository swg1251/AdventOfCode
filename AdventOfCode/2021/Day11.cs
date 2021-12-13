using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day11 : IDay
	{
		private List<List<int>> octopuses;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 11);
			octopuses = new List<List<int>>();

			foreach (var line in lines)
			{
				var row = line.Select(c => Convert.ToInt32(c.ToString())).ToList();
				octopuses.Add(row);
			}
		}

		public void Solve()
		{
			var totalFlashed = 0;
			var synchronized = false;
			int i = 0;

			while (!synchronized)
			{
				for (int y = 0; y < octopuses.Count; y++)
				{
					for (int x = 0; x < octopuses[y].Count; x++)
					{
						octopuses[y][x]++;
					}
				}

				var flashed = new HashSet<(int y, int x)>();
				var newFlashed = true;
				while (newFlashed)
				{
					newFlashed = false;

					for (int y = 0; y < octopuses.Count; y++)
					{
						for (int x = 0; x < octopuses[y].Count; x++)
						{
							if (octopuses[y][x] > 9 && flashed.Add((y, x)))
							{
								newFlashed = true;
								foreach (var (y2, x2) in GetAdjacentIndices(y, x))
								{
									octopuses[y2][x2]++;
								}
							}
						}
					}
				}

				for (int y = 0; y < octopuses.Count; y++)
				{
					for (int x = 0; x < octopuses[y].Count; x++)
					{
						if (octopuses[y][x] > 9)
						{
							octopuses[y][x] = 0;
						}
					}
				}

				totalFlashed += flashed.Count;
				synchronized = flashed.Count == octopuses.Sum(r => r.Count);
				i++;

				if (i == 100)
				{
					Console.WriteLine($"The total number of flashes after 100 steps (part one) is {totalFlashed}");
				}
			}

			Console.WriteLine($"The number of steps until flashes synchronize (part two) is {i}");
		}

		private List<(int y, int x)> GetAdjacentIndices(int y, int x)
		{
			var indices = new List<(int y, int x)>();
			var height = octopuses.Count;
			var width = octopuses.First().Count;

			if (y > 0)
			{
				indices.Add((y - 1, x));
			}
			if (y < height - 1)
			{
				indices.Add((y + 1, x));
			}
			if (x > 0)
			{
				indices.Add((y, x - 1));
			}
			if (x < width - 1)
			{
				indices.Add((y, x + 1));
			}
			if (y > 0 && x > 0)
			{
				indices.Add((y - 1, x - 1));
			}
			if (y > 0 && x < width - 1)
			{
				indices.Add((y - 1, x + 1));
			}
			if (y < height - 1 && x > 0)
			{
				indices.Add((y + 1, x - 1));
			}
			if (y < height - 1 && x < width - 1)
			{
				indices.Add((y + 1, x + 1));
			}

			return indices;
		}
	}
}
