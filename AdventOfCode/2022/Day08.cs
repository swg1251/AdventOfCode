using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day08 : IDay
	{
		private List<List<int>> trees;

		public void GetInput()
		{
			trees = InputHelper.GetIntegerGridFromInput(2022, 8);
		}

		public void Solve()
		{
			var visibleTrees = 0;
			var scenicScores = new Dictionary<(int y, int x), int>();

			for (int y = 0; y < trees.Count; y++)
			{
				for (int x = 0; x < trees[y].Count; x++)
				{
					var visibleFromLeft = true;
					var leftScore = 0;
					for (int x2 = x - 1; x2 >= 0; x2--)
					{
						leftScore++;
						if (trees[y][x2] >= trees[y][x])
						{
							visibleFromLeft = false;
							break;
						}
					}
					var visibleFromRight = true;
					var rightScore = 0;
					for (int x2 = x + 1; x2 < trees[y].Count; x2++)
					{
						rightScore++;
						if (trees[y][x2] >= trees[y][x])
						{
							visibleFromRight = false;
							break;
						}
					}
					var visibleFromTop = true;
					var upScore = 0;
					for (int y2 = y - 1; y2 >= 0; y2--)
					{
						upScore++;
						if (trees[y2][x] >= trees[y][x])
						{
							visibleFromTop = false;
							break;
						}
					}
					var visibleFromBottom = true;
					var downScore = 0;
					for (int y2 = y + 1; y2 < trees.Count; y2++)
					{
						downScore++;
						if (trees[y2][x] >= trees[y][x])
						{
							visibleFromBottom = false;
							break;
						}
					}

					if (visibleFromLeft || visibleFromRight || visibleFromTop || visibleFromBottom)
					{
						visibleTrees++;
					}

					scenicScores[(y, x)] = leftScore * rightScore * upScore * downScore;
				}
			}

			Console.WriteLine($"The number of trees that can be seen from outside the grid (part one) is {visibleTrees}");
			Console.WriteLine($"The highest scenic score for any tree (part two) is {scenicScores.Max(t => t.Value)}");
		}
	}
}
