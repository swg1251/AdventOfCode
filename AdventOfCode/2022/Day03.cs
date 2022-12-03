using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day03 : IDay
	{
		private const string lowercase = "_abcdefghijklmnopqrstuvwxyz";
		private const string uppercase = "_ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private List<string> sacks;

		public void GetInput()
		{
			sacks = InputHelper.GetStringsFromInput(2022, 3);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		public void PartOne()
		{
			var totalPriority = 0;
			foreach (var sack in sacks)
			{
				var firstCompartment = sack.Substring(0, sack.Length / 2);
				var secondCompartment = sack.Substring(sack.Length / 2);
				var sharedItem = firstCompartment.Intersect(secondCompartment).Single();
				totalPriority += GetPriority(sharedItem);
			}
			Console.WriteLine($"The sum of the item priorities (part one) is {totalPriority}");
		}

		public void PartTwo()
		{
			var totalPriority = 0;
			for (int i = 0; i + 2 < sacks.Count; i += 3)
			{
				var sharedItem = sacks[i].Intersect(sacks[i + 1]).Intersect(sacks[i + 2]).Single();
				totalPriority += GetPriority(sharedItem);
			}
			Console.WriteLine($"The sum of the group item priorities (part two) is {totalPriority}");
		}

		private int GetPriority(char item)
		{
			var priority = lowercase.IndexOf(item);
			if (priority == -1)
			{
				priority = uppercase.IndexOf(item) + 26;
			}
			return priority;
		}
	}
}
