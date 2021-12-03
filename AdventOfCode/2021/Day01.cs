using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day01 : IDay
	{
		private List<int> depths;

		public void GetInput()
		{
			var lines = File.ReadAllLines("2021/input/day01.txt").Where(l => !string.IsNullOrEmpty(l));
			depths = lines.Select(l => Convert.ToInt32(l)).ToList();
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var increases = -1;
			var current = 0;

			foreach (var depth in depths)
			{
				var next = depth;

				if (next > current)
				{
					increases++;
				}

				current = next;
			}

			Console.WriteLine($"The number of depth increases (part one) is {increases}");
		}

		private void PartTwo()
		{
			var increases = -1;
			var current = 0;

			for (int i = 0; i < depths.Count - 2; i++)
			{
				var next = depths[i] + depths[i + 1] + depths[i + 2];

				if (next > current)
				{
					increases++;
				}

				current = next;
			}

			Console.WriteLine($"The number of depth increases (part two) is {increases}");
		}
	}
}
