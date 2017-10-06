using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day18 : IDay
    {
		private List<List<bool>> rows { get; set; }
		public void GetInput()
		{
			rows = new List<List<bool>>();
			var firstRow = new List<bool>();

			var input = File.ReadAllLines("input/day18.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
			foreach (var tile in input)
			{
				firstRow.Add(tile == '^');
			}
			rows.Add(firstRow);
		}

		public void Solve()
		{
			for (int i = 0; i < 399999; i++)
			{
				var nextRow = new List<bool>();
				for (int j = 0; j < rows[i].Count; j++)
				{
					var left = (j == 0) ? false : rows[i][j - 1];
					var center = rows[i][j];
					var right = (j == rows[i].Count - 1) ? false : rows[i][j + 1];

					var trap = (left && center && !right)
							|| (center && right && !left)
							|| (left && !center && !right)
							|| (!left && !center && right);
					nextRow.Add(trap);
				}
				rows.Add(nextRow);
			}

			Console.WriteLine($"The number of safe tiles in 40 rows (part one) is: {rows.Take(40).Sum(r => r.Count(t => !t))}");
			Console.WriteLine($"The number of safe tiles in 400000 rows (part two) is: {rows.Sum(r => r.Count(t => !t))}");
		}
    }
}
