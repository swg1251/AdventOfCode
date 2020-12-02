using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day02 : IDay
	{
		private List<string> lines;
		public void GetInput()
		{
			lines = File.ReadAllLines("2020/input/day02.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			var validPartOne = 0;
			var validPartTwo = 0;

			foreach (var line in lines)
			{
				var lineParts = line.Split(' ');
				var counts = lineParts[0].Split('-');

				var min = Convert.ToInt32(counts[0]);
				var max = Convert.ToInt32(counts[1]);

				var pos1 = min - 1;
				var pos2 = max - 1;

				var checkCar = lineParts[1].TrimEnd(':');

				var pass = lineParts[2];

				// part one - valid count of char
				var charCount = pass.Count(c => c.ToString() == checkCar);
				if (charCount >= min && charCount <= max)
				{
					validPartOne++;
				}

				// part two - exactly one at specified index
				var charsAtIndex = 0;
				if (pass[pos1].ToString() == checkCar)
				{
					charsAtIndex++;
				}
				if (pass[pos2].ToString() == checkCar)
				{
					charsAtIndex++;
				}
				if (charsAtIndex == 1)
				{
					validPartTwo++;
				}
			}
			Console.WriteLine($"The number of valid passwords (part one) is {validPartOne}");
			Console.WriteLine($"The number of valid passwords (part two) is {validPartTwo}");
		}
	}
}
