using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day04 : IDay
	{
		private List<(List<int> elf1, List<int> elf2)> pairs;

		public void GetInput()
		{
			pairs = new List<(List<int> elf1, List<int> elf2)>();
			foreach (var line in InputHelper.GetStringsFromInput(2022, 4))
			{
				var elves = line.Split(',');
				var elf1min = Convert.ToInt32(elves[0].Split('-')[0]);
				var elf1max = Convert.ToInt32(elves[0].Split('-')[1]);
				var elf2min = Convert.ToInt32(elves[1].Split('-')[0]);
				var elf2max = Convert.ToInt32(elves[1].Split('-')[1]);
				pairs.Add((
					Enumerable.Range(elf1min, (elf1max - elf1min) + 1).ToList(),
					Enumerable.Range(elf2min, (elf2max - elf2min) + 1).ToList()));
			}
		}

		public void Solve()
		{
			var partialOverlap = 0;
			var fullOverlap = 0;

			foreach (var (elf1, elf2) in pairs)
			{
				var overlap = elf1.Intersect(elf2);
				if (overlap.Any())
				{
					partialOverlap++;

					if (overlap.Count() == elf1.Count || overlap.Count() == elf2.Count)
					{
						fullOverlap++;
					}
				}
			}

			Console.WriteLine($"The number of pairs fully containing the other (part one) is {fullOverlap}");
			Console.WriteLine($"The number of pairs with overlapping ranges (part two) is {partialOverlap}");
		}
	}
}
