using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
	public class Day03 : IDay
	{
		private List<List<bool>> map;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 3);

			map = new List<List<bool>>();
			foreach (var line in lines)
			{
				var row = new List<bool>();
				foreach (var c in line)
				{
					row.Add(c == '#');
				}
				map.Add(row);
			}
		}

		public void Solve()
		{
			var partOne = GetTreeCount(3, 1);
			Console.WriteLine($"The number of trees encountered on slope right 3 down 1 (part one) is {partOne}");

			var product = 1L;
			product *= (GetTreeCount(1, 1));
			product *= partOne;
			product *= (GetTreeCount(5, 1));
			product *= (GetTreeCount(7, 1));
			product *= (GetTreeCount(1, 2));

			Console.WriteLine($"The product of the number of trees on all slopes (part two) is {product}");
		}

		private int GetTreeCount(int right, int down)
		{
			var width = map[0].Count;

			var trees = 0;

			var j = 0;
			for (int i = 0; i < map.Count; i+= down)
			{
				if (map[i][j])
				{
					trees++;
				}
				j += right;

				if (j >= width)
				{
					j -= width;
				}
			}

			return trees;
		}
	}
}
