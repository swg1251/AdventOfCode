using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day14 : IDay
    {
		private string key;
		private List<List<bool>> binaryHashes;

		public Day14()
		{
			binaryHashes = new List<List<bool>>();
		}

		public void GetInput()
		{
			key = File.ReadAllLines("2017/input/day14.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			for (int i = 0; i < 128; i++)
			{
				var hexHash = KnotHash($"{key}-{i}");
				var binaryHash = HexToBinary(hexHash);
				binaryHashes.Add(binaryHash);
			}
			
			Console.WriteLine($"The number of used squares (part one) is {binaryHashes.Sum(bh => bh.Count(c => c))}");
		}

		private string KnotHash(string input)
		{
			var position = 0;
			var skipSize = 0;

			var lengths = new List<int>();
			foreach (var c in input)
			{
				lengths.Add(c);
			}
			lengths.Add(17);
			lengths.Add(31);
			lengths.Add(73);
			lengths.Add(47);
			lengths.Add(23);

			var hash = new List<int>();
			for (int i = 0; i < 256; i++)
			{
				hash.Add(i);
			}

			// KnotHash 64 rounds, not resetting position/skip size
			for (int iterations = 0; iterations < 64; iterations++)
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

			// condense hash via XOR'ing each 16 element sublist
			var denseHash = new List<int>();
			for (int i = 0; i < 16; i++)
			{
				var result = hash[i * 16];
				for (int j = 1; j < 16; j++)
				{
					result ^= hash[(i * 16) + j];
				}
				denseHash.Add(result);
			}

			// get hexadecimal
			var hashHexadecimal = "";
			for (int i = 0; i < 16; i++)
			{
				hashHexadecimal += Convert.ToString(denseHash[i], 16);
			}

			return hashHexadecimal;
		}

		private List<bool> HexToBinary(string hexString)
		{
			var binaryString = "";
			
			foreach (var c in hexString)
			{
				binaryString += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
			}

			var binary = new List<bool>();
			foreach (var c in binaryString)
			{
				binary.Add(c == '1');
			}

			return binary;
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
