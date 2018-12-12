using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day16 : IDay
    {
		private List<bool> bitString { get; set; }

		public void GetInput()
		{
			bitString = new List<bool>();
			var inputString = File.ReadAllLines("2016/input/day16.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
			foreach (var bit in inputString)
			{
				bitString.Add(bit == '1');
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The checksum (part one) is: {GetChecksum(272)}");
			GetInput();
			Console.WriteLine($"The checksum (part two) is: {GetChecksum(35651584)}");
		}

		private string GetChecksum(int length)
		{
			while (bitString.Count < length)
			{
				var oppositeBitString = new List<bool>();
				for (int i = bitString.Count - 1; i > -1; i--)
				{
					oppositeBitString.Add(!bitString[i]);
				}

				bitString.Add(false);
				bitString = bitString.Concat(oppositeBitString).ToList();
			}
			bitString = bitString.Take(length).ToList();

			while (bitString.Count % 2 == 0)
			{
				var checksum = new List<bool>();
				for (int i = 0; i < bitString.Count - 1; i += 2)
				{
					checksum.Add(bitString[i] == bitString[i + 1]);
				}
				bitString = checksum;
			}

			var checksumString = "";
			foreach (var bit in bitString)
			{
				checksumString += bit ? "1" : "0";
			}

			return checksumString;
		}
    }
}
