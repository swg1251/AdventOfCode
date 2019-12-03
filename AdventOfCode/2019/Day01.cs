using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day01 : IDay
	{
		private IEnumerable<double> masses;

		public void GetInput()
		{
			masses = File.ReadAllLines("2019/input/day01.txt").Where(l => !string.IsNullOrEmpty(l)).Select(i => Convert.ToDouble(i));
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var totalFuel = masses.Select(m => Math.Floor(m / 3d) - 2).Sum();
			Console.WriteLine($"The total fuel requirement (part one) is: {totalFuel}");
		}

		private void PartTwo()
		{
			var totalFuel = 0d;
			foreach (var inputMass in masses)
			{
				var moduleFuel = 0d;
				var mass = inputMass;
				while (true)
				{
					var fuel = Math.Floor(mass / 3d) - 2;
					if (fuel <= 0)
					{
						break;
					}

					moduleFuel += fuel;
					mass = fuel;
				}
				totalFuel += moduleFuel;
			}

			Console.WriteLine($"The total fuel requirement account for fuel (part two) is: {totalFuel}");
		}
	}
}
