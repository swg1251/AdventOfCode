using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day07 : IDay
	{
		private List<int> crabs;

		public void GetInput()
		{
			crabs = InputHelper.GetIntegersFromCommaSeparatedInput(2021, 7);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var fuelUsages = new Dictionary<int, int>();
			for (int i = crabs.Min(); i <= crabs.Max(); i++)
			{
				fuelUsages[i] = crabs.Sum(c => Math.Abs(c - i));
			}

			Console.WriteLine($"The least amount of fuel that can be used (part one) is {fuelUsages.Values.Min()}");
		}

		private void PartTwo()
		{
			var fuelUsages = new Dictionary<int, int>();
			for (int i = crabs.Min(); i <= crabs.Max(); i++)
			{
				var fuel = 0;

				foreach (var crab in crabs)
				{
					var distance = Math.Abs(crab - i);
					var stepFuel = 1;

					for (int steps = 0; steps < distance; steps++)
					{
						fuel += stepFuel;
						stepFuel++;
					}
				}

				fuelUsages[i] = fuel;
			}

			Console.WriteLine($"The least amount of fuel that can be used (part two) is {fuelUsages.Values.Min()}");
		}
	}
}
