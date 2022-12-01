using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day01 : IDay
	{
		private List<List<int>> elves;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2022, 1, true);
			elves = new List<List<int>>();

			var elf = new List<int>();
			foreach (var line in lines)
			{
				if (string.IsNullOrEmpty(line))
				{
					elves.Add(elf);
					elf = new List<int>();
				}
				else
				{
					elf.Add(Convert.ToInt32(line));
				}
			}
			elves.Add(elf);
		}

		public void Solve()
		{
			Console.WriteLine($"The elf carrying the most calories (part one) has {elves.Max(e => e.Sum())}");

			var topThreeElves = elves.OrderByDescending(e => e.Sum()).Take(3);
			var topThreeTotal = topThreeElves.Sum(e => e.Sum());

			Console.WriteLine($"The amount of calories carried by the top three elves (part two) is {topThreeTotal}");
		}
	}
}
