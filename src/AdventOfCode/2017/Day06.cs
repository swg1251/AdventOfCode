using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day06 : IDay
    {
		private List<int> memoryBanks;

		public Day06()
		{
			memoryBanks = new List<int>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day06.txt").Where(l => !string.IsNullOrEmpty(l)).First();
			foreach (var value in input.Split('\t'))
			{
				memoryBanks.Add(Convert.ToInt32(value));
			}
		}

		public void Solve()
		{
			var redistributionCount = 0;
			var distributions = new List<List<int>>();
			var firstDuplicateIndex = 0;
			var loopSize = 0;

			while (true)
			{
				// we already found the first duplicate, now we have that distribution again
				if (firstDuplicateIndex > 0 && memoryBanks.SequenceEqual(distributions[firstDuplicateIndex]))
				{
					loopSize = redistributionCount - firstDuplicateIndex;
					break;
				}
				else if (firstDuplicateIndex == 0 && distributions.Any(d => d.SequenceEqual(memoryBanks)))
				{
					firstDuplicateIndex = redistributionCount;
				}

				distributions.Add(new List<int>(memoryBanks));

				var max = memoryBanks.Max();
				var i = memoryBanks.IndexOf(max);
				memoryBanks[i] = 0;
				i++;

				// loop around each bank to distribute the max value
				while (max > 0)
				{
					if (i >= memoryBanks.Count)
					{
						i = 0;
					}
					memoryBanks[i]++;
					max--;
					i++;
				}

				redistributionCount++;
			}

			Console.WriteLine($"The number of cycles required to see a duplicate (part one) is {firstDuplicateIndex}");
			Console.WriteLine($"The size of the loop (part two) is {loopSize}");
		}
    }
}
