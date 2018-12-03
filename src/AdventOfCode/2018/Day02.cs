using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
    public class Day02 : IDay
    {
		private IEnumerable<string> boxes;

		public void GetInput()
		{
			boxes = File.ReadAllLines("2018/input/day02.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var twosCount = 0;
			var threesCount = 0;

			foreach (var box in boxes)
			{
				var hasTwo = false;
				var hasThree = false;

				foreach (var c in box)
				{
					if (box.Count(x => x == c) == 2)
					{
						hasTwo = true;
					}
					if (box.Count(x => x == c) == 3)
					{
						hasThree = true;
					}
				}

				if (hasTwo)
				{
					twosCount++;
				}
				if (hasThree)
				{
					threesCount++;
				}
			}

			Console.WriteLine($"The checksum for the box IDs (part one) is: {twosCount * threesCount}");
		}

		private void PartTwo()
		{
			var boxIdLength = boxes.First().Length;
			var correctBoxOverlap = string.Empty;

			for (int i = 0; i < boxIdLength; i++)
			{
				var newBoxes = boxes.Select(s => s.Remove(i, 1));
				
				foreach (var box in newBoxes)
				{
					if (newBoxes.Count(b => b == box) == 2)
					{
						correctBoxOverlap = box;
						break;
					}
				}

				if (correctBoxOverlap != string.Empty)
				{
					break;
				}
			}

			Console.WriteLine($"The letters in common between the correct box IDs are: {correctBoxOverlap}");
		}
    }
}
