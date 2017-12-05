using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day19 : IDay
    {
		private List<Elf> elves;

		public Day19()
		{
			elves = new List<Elf>();
		}

		public void GetInput()
		{
			var input = Convert.ToInt32(File.ReadAllLines("2016/input/day19.txt").Where(l => !string.IsNullOrEmpty(l)).First());

			for (int i = 0; i < input; i++)
			{
				elves.Add(new Elf { ElfNumber = i + 1, HasPresents = true });
			}
		}

		public void Solve()
		{
			Steal(false);
			Console.WriteLine($"The elf with all the presents (part one) is {elves.First(e => e.HasPresents).ElfNumber}");

			elves.Clear();
			GetInput();

			Steal(true);
			Console.WriteLine($"The elf with all the presents (part two) is {elves.First(e => e.HasPresents).ElfNumber}");
		}

		public void Steal(bool partTwo)
		{
			int i = 0;
			int removedCount = 0;

			while (removedCount < elves.Count - 1)
			{
				if (i >= elves.Count)
				{
					i = 0;
				}
				if (!elves[i].HasPresents)
				{
					i++;
					continue;
				}

				var nextElf = partTwo ? GetNextIndexPartTwo(i, removedCount) : GetNextIndex(i);
				elves[nextElf].HasPresents = false;
				removedCount++;
				i++;
			}
		}

		private int GetNextIndex(int startIndex)
		{
			for (int i = startIndex + 1; i < elves.Count; i++)
			{
				if (elves[i].HasPresents)
				{
					return i;
				}
			}
			for (int i = 0; i < startIndex; i++)
			{
				if (elves[i].HasPresents)
				{
					return i;
				}
			}
			return startIndex;
		}

		private int GetNextIndexPartTwo(int startIndex, int removedCount)
		{
			var elvesToMove = (elves.Count - removedCount) / 2;
			var moved = 0;
			var i = startIndex;

			while (moved < elvesToMove)
			{
				if (i == elves.Count)
				{
					i = 0;
				}
				if (!elves[i].HasPresents)
				{
					i++;
					continue;
				}

				i++;
				moved++;
			}

			return i - 1;
		}

		internal class Elf
		{
			public int ElfNumber { get; set; }
			public bool HasPresents { get; set; }
		}
    }
}
