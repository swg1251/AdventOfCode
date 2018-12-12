using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day17 : IDay
	{
		private List<int> containers;

		// Since we only need to know the number of good combos and how many containers each uses,
		// this will be a list of how many containers were used for each good combo
		private List<int> goodContainerComboCounts;

		public void GetInput()
		{
			goodContainerComboCounts = new List<int>();
			containers = File.ReadAllLines("2015/input/day17.txt").Where(l => !string.IsNullOrEmpty(l))
				.Select(l => Convert.ToInt32(l)).ToList();
		}

		public void Solve()
		{
			var containerCount = containers.Count;

			// Generate a range of from 0 to the 2^n number of combinations
			var binaryRange = Enumerable.Range(0, (int)Math.Pow(2, containerCount));
			foreach (var bs in binaryRange.Select(b => Convert.ToString(b, 2)))
			{
				// use a binary string where each bit represents the container at that index being used/not used
				var binary = bs.PadLeft(containerCount, '0');

				var total = 0;
				for (int i = 0; i < containerCount; i++)
				{
					if (binary[i] == '1')
					{
						total += containers[i];
					}
				}

				if (total == 150)
				{
					goodContainerComboCounts.Add(binary.Count(c => c == '1'));
				}
			}

			Console.WriteLine($"Combinations of 150 liters (part one) is: {goodContainerComboCounts.Count}");

			var optimalContainerCount = goodContainerComboCounts.Min();
			var optimalCount = goodContainerComboCounts.Count(c => c == optimalContainerCount);
			Console.WriteLine($"Combinations of 150 liters using least containers possible (part two) is: {optimalCount}");
		}
	}
}
