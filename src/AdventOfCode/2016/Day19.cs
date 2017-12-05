using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day19 : IDay
    {
		private List<int> elfPresentCounts;

		public Day19()
		{
			elfPresentCounts = new List<int>();
		}

		public void GetInput()
		{
			var input = Convert.ToInt32(File.ReadAllLines("2016/input/day19.txt").Where(l => !string.IsNullOrEmpty(l)).First());

			for (int i = 0; i < input; i++)
			{
				elfPresentCounts.Add(1);
			}
		}

		public void Solve()
		{
			Steal(false);
			Console.WriteLine($"The elf with all the presents (part one) is {elfPresentCounts.IndexOf(elfPresentCounts.Count) + 1}");

			elfPresentCounts.Clear();
			GetInput();

			Steal(true);
			Console.WriteLine($"The elf with all the presents (part two) is {elfPresentCounts.IndexOf(elfPresentCounts.Count) + 1}");
		}

		public void Steal(bool partTwo)
		{
			int i = 0;
			int removedCount = 0;

			while (removedCount < elfPresentCounts.Count - 1)
			{
				if (i >= elfPresentCounts.Count)
				{
					i = 0;
				}
				if (elfPresentCounts[i] < 1)
				{
					i++;
					continue;
				}

				var nextElf = partTwo ? GetNextIndexPartTwo(i, removedCount) : GetNextIndex(i);
				elfPresentCounts[i] += elfPresentCounts[nextElf];
				elfPresentCounts[nextElf] = 0;
				removedCount++;
				i++;
			}
		}

		private int GetNextIndex(int startIndex)
		{
			for (int i = startIndex + 1; i < elfPresentCounts.Count; i++)
			{
				if (elfPresentCounts[i] > 0)
				{
					return i;
				}
			}
			for (int i = 0; i < startIndex; i++)
			{
				if (elfPresentCounts[i] > 0)
				{
					return i;
				}
			}
			return startIndex;
		}

		private int GetNextIndexPartTwo(int startIndex, int removedCount)
		{
			var elvesToMove = (elfPresentCounts.Count - removedCount) / 2;
			var moved = 0;
			var i = startIndex;

			while (moved < elvesToMove)
			{
				if (i == elfPresentCounts.Count)
				{
					i = 0;
				}
				if (elfPresentCounts[i] == 0)
				{
					i++;
					continue;
				}

				i++;
				moved++;
			}

			return i - 1;
		}
    }
}
