using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2016
{
	public class Day20 : IDay
	{
		private List<(long min, long max)> ranges;

		public void GetInput()
		{
			ranges = new List<(long min, long max)>();
			var lines = InputHelper.GetStringsFromInput(2016, 20);
			foreach (var line in lines)
			{
				var lineParts = line.Split('-');
				ranges.Add((Convert.ToInt64(lineParts[0]), Convert.ToInt64(lineParts[1])));
			}
		}

		public void Solve()
		{
			var validIps = new List<long>();

			// since input contains a range starting at 0, assume total minimum will be a range's max + 1
			foreach (var candidate in ranges.Select(r => r.max + 1))
			{
				if (candidate <= uint.MaxValue && !ranges.Any(r => candidate >= r.min && candidate <= r.max))
				{
					validIps.Add(candidate); ;
				}	
			}
			Console.WriteLine($"The lowest valid IP (part one) is {validIps.Min()}");

			var totalValid = 0;

			// start at each previously identified IP and count up until it hits another blocked range
			for (int i = 0; i < validIps.Count; i++)
			{
				var candidate = validIps[i];

				while (candidate <= uint.MaxValue && !ranges.Any(r => candidate >= r.min && candidate <= r.max))
				{
					totalValid++;
					candidate++;
				}
			}
			Console.WriteLine($"The total number of valid IPs (part two) is {totalValid}");
		}
	}
}
