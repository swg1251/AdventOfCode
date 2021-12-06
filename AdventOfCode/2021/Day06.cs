using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day06 : IDay
	{
		private List<int> fish;

		public void GetInput()
		{
			fish = InputHelper.GetStringsFromInput(2021, 6)
				.First()
				.Split(',')
				.Select(x => Convert.ToInt32(x))
				.ToList();
		}

		public void Solve()
		{
			var fishDict = new Dictionary<int, long>();
			foreach (var f in fish)
			{
				fishDict[f] = fishDict.GetValueOrDefault(f) + 1;
			}

			for (int day = 0; day < 256; day++)
			{
				var newDict = new Dictionary<int, long>();

				for (int i = 7; i >= 0; i--)
				{
					newDict[i] = fishDict.GetValueOrDefault(i + 1);
				}

				newDict[6] += fishDict.GetValueOrDefault(0);
				newDict[8] = fishDict.GetValueOrDefault(0);

				fishDict = newDict;

				if (day == 79)
				{
					Console.WriteLine($"After 80 days (part one) there are {fishDict.Values.Sum()} fish");
				}
			}

			Console.WriteLine($"After 256 days (part two) there are {fishDict.Values.Sum()} fish");
		}
	}
}
