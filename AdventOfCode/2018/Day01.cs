using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
    public class Day01 : IDay
    {
		private IEnumerable<string> frequencies;

		public void GetInput()
		{
			frequencies = File.ReadAllLines("2018/input/day01.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var frequency = 0;

			foreach (var freq in frequencies)
			{
				var value = Convert.ToInt32(freq.Substring(1));

				if (freq[0] == '+')
				{
					frequency += value;
				}
				else
				{
					frequency -= value;
				}
			}
			Console.WriteLine($"Resulting frequency (part one) is: {frequency}");
		}

		private void PartTwo()
		{
			var seen = new HashSet<int>();
			var frequency = 0;
			seen.Add(frequency);

			var setIsUnique = true;

			while (setIsUnique)
			{
				foreach (var freq in frequencies)
				{
					var value = Convert.ToInt32(freq.Substring(1));

					if (freq[0] == '+')
					{
						frequency += value;
					}
					else
					{
						frequency -= value;
					}

					if (!seen.Add(frequency))
					{
						setIsUnique = false;
						break;
					}
				}
			}
			Console.WriteLine($"The first duplicate frequency (part two) is: {frequency}");
		}
    }
}
