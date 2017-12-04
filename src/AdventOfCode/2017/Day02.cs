using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day02 : IDay
    {
		private List<List<int>> spreadsheet;
		private int checksum;

		public Day02()
		{
			spreadsheet = new List<List<int>>();
			checksum = 0;
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day02.txt").Where(l => !string.IsNullOrEmpty(l));

			foreach (var line in input)
			{
				// convert each number of the tab-delimited puzzle input to an int
				spreadsheet.Add(line.Split('\t').Select(n => Convert.ToInt32(n)).ToList());
			}
		}

		public void Solve()
		{
			PartOne();
			Console.WriteLine($"The checksum (part one) is {checksum}");

			checksum = 0;
			PartTwo();
			Console.WriteLine($"The checksum (part two) is {checksum}");
		}

		private void PartOne()
		{
			foreach (var line in spreadsheet)
			{
				checksum += line.Max() - line.Min();
			}
		}

		private void PartTwo()
		{
			foreach (var line in spreadsheet)
			{
				var found = false;
				for (int i = 0; i < line.Count(); i++)
				{
					if (found)
					{
						break;
					}

					for (int j = 0; j < line.Count(); j++)
					{
						if (i != j && line[i] % line[j] == 0)
						{
							checksum += line[i] / line[j];
							found = true;
							break;
						}
					}
				}
			}
		}
    }
}
