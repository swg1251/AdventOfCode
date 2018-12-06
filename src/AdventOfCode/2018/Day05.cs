using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018
{
	public class Day05 : IDay
	{
		private string originalString;

		public void GetInput()
		{
			originalString = File.ReadAllLines("2018/input/day05.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			var partOne = GetReactedLength(originalString);
			Console.WriteLine($"The reacted polymer length is (part one): {partOne}");

			var shortestLength = int.MaxValue;
			foreach (var c in "abcdefghijklmnopqrstuvqwxyz")
			{
				var s = originalString.Replace(c.ToString(), "").Replace(char.ToUpperInvariant(c).ToString(), "");
				var length = GetReactedLength(s);
				if (length < shortestLength)
				{
					shortestLength = length;
				}
			}
			Console.WriteLine($"The shortest reacted polymer length is (part two): {shortestLength}");
		}

		// Slow, takes ~5 seconds each time
		private int GetReactedLength(string polymer)
		{
			var removed = true;

			while (removed)
			{
				removed = false;

				for (int i = 0; i < polymer.Length - 1; i++)
				{
					var c1 = polymer[i];
					var c2 = polymer[i + 1];

					// Difference between lowercase/uppercase ASCII character codes is 32
					if (Math.Abs(c1 - c2) == 32)
					{
						polymer = polymer.Remove(i, 2);
						removed = true;
						i--;
						break;
					}
				}
			}

			return polymer.Length;
		}
	}
}
