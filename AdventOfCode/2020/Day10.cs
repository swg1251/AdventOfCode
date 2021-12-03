using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day10 : IDay
	{
		private List<int> adapters;

		public void GetInput()
		{
			adapters = InputHelper.GetIntegersFromInput(2020, 10);
		}

		public void Solve()
		{
			adapters.Add(0);
			adapters.Sort();
			adapters.Add(adapters.Max() + 3);

			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var oneDiffs = 0;
			var threeDiffs = 0;

			for (int i = 0; i < adapters.Count - 1; i++)
			{
				var diff = adapters[i + 1] - adapters[i];
				if (diff == 1)
				{
					oneDiffs++;
				}
				else if (diff == 3)
				{
					threeDiffs++;
				}
			}

			Console.WriteLine($"The product of number of one jumps and three jumps (part one) is {oneDiffs * threeDiffs}");
		}

		private void PartTwo()
		{
			var pathDict = new Dictionary<int, long>();
			foreach (var adapter in adapters)
			{
				// initialize with no paths to any adapter
				pathDict[adapter] = 0;
			}

			// one path to starting adapter (0)
			pathDict[0] = 1;

			foreach (var adapter in adapters)
			{
				for (int i = 1; i < 4; i++)
				{
					// current adapter minus 1/2/3 existing means there is another adapter that paths to this
					if (adapters.Contains(adapter - i))
					{
						// number of paths to this adapter += all paths to previous
						pathDict[adapter] += pathDict[adapter - i];
					}
				}
			}

			Console.WriteLine($"The total number of valid combinations (part two) is {pathDict[adapters.Max()]}");
		}
	}
}
