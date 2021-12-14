using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day14 : IDay
	{
		private string start;
		private Dictionary<string, string> insertions;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 14);
			start = lines[0];

			insertions = new Dictionary<string, string>();
			foreach (var line in lines.Skip(1))
			{
				var lineParts = line.Split(" -> ");
				insertions[lineParts[0]] = lineParts[1];
			}
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		// slow, actually builds the strings - fine for 10 steps (part one) but not 40 (part two)
		private void PartOne()
		{
			var polymer = start;
			for (int steps = 0; steps < 10; steps++)
			{
				var newPolymer = string.Empty;
				for (int i = 0; i < polymer.Length - 1; i++)
				{
					var pair = "" + polymer[i] + polymer[i + 1];
					var insertion = insertions[pair];
					newPolymer += polymer[i] + insertions[pair];
				}
				newPolymer += polymer.Last();
				polymer = newPolymer;
			}

			var letters = new Dictionary<char, int>();
			foreach (var c in polymer)
			{
				letters[c] = letters.GetValueOrDefault(c) + 1;
			}
			var max = letters.Max(kvp => kvp.Value);
			var min = letters.Min(kvp => kvp.Value);
			Console.WriteLine($"The max - min after 10 steps (part one) is {max - min}");
		}

		// fast, counts number of pairs instead
		private void PartTwo()
		{
			var pairs = new Dictionary<string, long>();
			for (int i = 0; i < start.Length - 1; i++)
			{
				var pair = "" + start[i] + start[i + 1];
				pairs[pair] = pairs.GetValueOrDefault(pair) + 1;
			}

			for (int i = 0; i < 40; i++)
			{
				var newPairs = new Dictionary<string, long>();
				foreach (var (pair, count) in pairs)
				{
					var newPair1 = "" + pair[0] + insertions[pair];
					var newPair2 = "" + insertions[pair] + pair[1];

					newPairs[newPair1] = newPairs.GetValueOrDefault(newPair1) + count;
					newPairs[newPair2] = newPairs.GetValueOrDefault(newPair2) + count;
				}
				pairs = newPairs;
			}

			var letters = new Dictionary<char, long>();
			foreach (var (pair, count) in pairs)
			{
				var c = pair[0];
				letters[c] = letters.GetValueOrDefault(c) + count;
			}
			letters[start.Last()] += 1;

			var max = letters.Values.Max();
			var min = letters.Values.Min();

			Console.WriteLine($"The max - min after 40 steps (part two) is {max - min}");
		}
	}
}
