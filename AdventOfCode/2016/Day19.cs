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

			for (int i = 1; i <= input; i++)
			{
				elves.Add(new Elf { ElfNumber = i });
			}

			elves[0].NextElf = elves[1];
			for (int i = 1; i < elves.Count - 1; i++)
			{
				elves[i].NextElf = elves[i + 1];
				elves[i].PreviousElf = elves[i - 1];
			}
			elves[0].PreviousElf = elves[elves.Count - 1];
			elves[elves.Count - 1].NextElf = elves[0];
			elves[elves.Count - 1].PreviousElf = elves[elves.Count - 2];
		}

		public void Solve()
		{
			PartOne();

			elves.Clear();
			GetInput();

			PartTwo();
		}

		private void PartOne()
		{
			var elf = elves[0];

			while (elf.NextElf.ElfNumber != elf.ElfNumber)
			{
				elf.NextElf = elf.NextElf.NextElf;
				elf = elf.NextElf;
			}

			Console.WriteLine($"The elf with all the presents (part one) is {elf.ElfNumber}");
		}

		private void PartTwo()
		{
			var elfCount = elves.Count;
			var elf = elves[0];
			var oppositeElf = elves[elfCount / 2];

			while (elf.NextElf.ElfNumber != elf.ElfNumber)
			{
				// "remove" elf across the circle
				oppositeElf.PreviousElf.NextElf = oppositeElf.NextElf;
				oppositeElf.NextElf.PreviousElf = oppositeElf.PreviousElf;

				// set the new opposite elf - when the number remaining is odd, the next "opposite" elf will be two ahead
				oppositeElf = (elfCount % 2 == 1) ? oppositeElf.NextElf.NextElf : oppositeElf.NextElf;
				elfCount--;

				elf = elf.NextElf;
			}

			Console.WriteLine($"The elf with all the presents (part two) is {elf.ElfNumber}");
		}

		internal class Elf
		{
			public int ElfNumber { get; set; }
			public Elf NextElf { get; set; }
			public Elf PreviousElf { get; set; }
		}
    }
}
