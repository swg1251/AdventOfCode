using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day10 : IDay
    {
		private int position;
		private int skipSize;

		private List<int> lengthsPartOne;
		private List<int> hashPartOne;

		private List<int> lengthsPartTwo;
		private List<int> hashPartTwo;

		public Day10()
		{
			lengthsPartOne = new List<int>();
			hashPartOne = new List<int>();

			lengthsPartTwo = new List<int>();
			hashPartTwo = new List<int>();

			for (int i = 0; i < 256; i++)
			{
				hashPartOne.Add(i);
				hashPartTwo.Add(i);
			}
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day10.txt").Where(l => !string.IsNullOrEmpty(l)).First();
			foreach (var length in input.Split(','))
			{
				lengthsPartOne.Add(Convert.ToInt32(length));
			}

			foreach (var c in input)
			{
				lengthsPartTwo.Add(c);
			}
			lengthsPartTwo.Add(17);
			lengthsPartTwo.Add(31);
			lengthsPartTwo.Add(73);
			lengthsPartTwo.Add(47);
			lengthsPartTwo.Add(23);
		}

		public void Solve()
		{
			KnotHash(lengthsPartOne, hashPartOne);
			Console.WriteLine($"The product of the first two hash values (part one) is {hashPartOne[0] * hashPartOne[1]}");

			// reset position/skip size before part two
			position = 0;
			skipSize = 0;

			// KnotHash 64 rounds, not resetting position/skip size
			for (int i = 0; i < 64; i++)
			{
				KnotHash(lengthsPartTwo, hashPartTwo);
			}

			// condense hash via XOR'ing each 16 element sublist
			var denseHash = new List<int>();
			for (int i = 0; i < 16; i++)
			{
				var result = hashPartTwo[i * 16];
				for (int j = 1; j < 16; j++)
				{
					result ^= hashPartTwo[(i * 16) + j];
				}
				denseHash.Add(result);
			}

			// get hexadecimal
			var hashHexadecimal = "";
			for (int i = 0; i < 16; i++)
			{
				hashHexadecimal += Convert.ToString(denseHash[i], 16);
			}

			Console.WriteLine($"The hexadecimal hash string (part two) is {hashHexadecimal}");
		}

		public void KnotHash(List<int> lengths, List<int> hash)
		{
			foreach (var length in lengths)
			{
				// reverse the length
				// - build a list of the length
				var subList = new List<int>();
				for (int i = 0; i < length; i++)
				{
					subList.Add(hash[Loop255(i + position)]);
				}

				// - reverse it
				subList.Reverse();

				// - replace original list elements with reversed list elements
				for (int i = 0; i < length; i++)
				{
					hash[Loop255(i + position)] = subList[i];
				}

				// move forward length + skip size
				position = Loop255(position + length + skipSize);

				// increment skip size
				skipSize++;
			}
		}

		private int Loop255(int value)
		{
			while (value > 255)
			{
				value -= 256;
			}
			return value;
		}
    }
}
