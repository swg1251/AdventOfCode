using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day14 : IDay
	{
		private List<string> lines;
		private Dictionary<long, long> valuesPartOne;
		private Dictionary<long, long> valuesPartTwo;

		public void GetInput()
		{
			lines = File.ReadAllLines("2020/input/day14.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
			valuesPartOne = new Dictionary<long, long>();
			valuesPartTwo = new Dictionary<long, long>();
		}

		public void Solve()
		{
			var mask = string.Empty;

			foreach (var line in lines)
			{
				var lineParts = line.Split(' ');

				if (lineParts[0] == "mask")
				{
					mask = lineParts[2];
					continue;
				}

				var index = Convert.ToInt64(lineParts[0].Split('[')[1].TrimEnd(']'));
				var value = Convert.ToInt64(lineParts[2]);

				valuesPartOne[index] = GetMaskedValue(mask, value);

				var partTwoMasks = GetPartTwoMasks(mask);
				foreach (var newMask in partTwoMasks)
				{
					var newIndex = GetMaskedValue(newMask, index);
					valuesPartTwo[newIndex] = value;
				}
			}

			Console.WriteLine($"The sum of all values (part one) is {valuesPartOne.Values.Sum()}");
			Console.WriteLine($"The sum of all values (part two) is {valuesPartTwo.Values.Sum()}");
		}

		private long GetMaskedValue(string mask, long value)
		{
			value |= Convert.ToInt64(mask.Replace('X', '0'), 2);
			value &= Convert.ToInt64(mask.Replace('X', '1'), 2);
			return value;
		}

		private List<string> GetPartTwoMasks(string mask)
		{
			var masks = new List<string>();
			var xCount = mask.Count(c => c == 'X');

			for (int i = 0; i < Math.Pow(2, xCount); i++)
			{
				var xBinary = Convert.ToString(i, 2).PadLeft(xCount, '0');
				var xIndex = 0;
				var newMask = string.Empty;

				for (int j = 0; j < mask.Length; j++)
				{
					if (mask[j] == '0')
					{
						newMask += 'X';
					}
					else if (mask[j] == '1')
					{
						newMask += '1';
					}
					else if (mask[j] == 'X')
					{
						newMask += xBinary[xIndex];
						xIndex++;
					}
				}

				masks.Add(newMask);
			}

			return masks;
		}
	}
}
