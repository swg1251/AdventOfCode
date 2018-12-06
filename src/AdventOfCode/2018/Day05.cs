using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
			foreach (var c in originalString.Select(c => char.ToLowerInvariant(c)).Distinct())
			{
				Console.WriteLine(c);
				var length = GetReactedLength(originalString, c);
				if (length < shortestLength)
				{
					shortestLength = length;
				}
			}
			Console.WriteLine($"The shortest reacted polymer length is (part two): {shortestLength}");
		}

		private int GetReactedLength(string polymer, char removeChar = '\0')
		{
			if (removeChar != '\0')
			{
				polymer = polymer.Replace(removeChar.ToString(), "");
				polymer = polymer.Replace(char.ToUpperInvariant(removeChar).ToString(), "");
			}

			var removed = true;

			while (removed)
			{
				removed = false;

				for (int i = 0; i < polymer.Length - 1; i++)
				{
					if (polymer[i] != polymer[i + 1] &&
						char.ToLowerInvariant(polymer[i]) == char.ToLowerInvariant(polymer[i + 1]))
					{
						polymer = polymer.Remove(i, 2);
						removed = true;
						i = 0;
						break;
					}
				}
			}

			return polymer.Length;
		}
	}
}
