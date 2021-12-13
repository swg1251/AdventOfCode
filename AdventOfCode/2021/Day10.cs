using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2021
{
	public class Day10 : IDay
	{
		private List<string> lines;

		public void GetInput()
		{
			lines = InputHelper.GetStringsFromInput(2021, 10);
		}

		public void Solve()
		{
			var invalids = new Dictionary<char, int>();
			var regex = new Regex(@"(\(\)|\[\]|\{\}|<>)");

			for (int i = 0; i < lines.Count; i++)
			{
				while (regex.IsMatch(lines[i]))
				{
					lines[i] = lines[i].Replace("()", "");
					lines[i] = lines[i].Replace("[]", "");
					lines[i] = lines[i].Replace("{}", "");
					lines[i] = lines[i].Replace("<>", "");
				}

				var invalid = lines[i].FirstOrDefault(c => ")]}>".Contains(c));
				if (invalid != default(char))
				{
					invalids[invalid] = invalids.GetValueOrDefault(invalid) + 1;
				}
			}

			var invalidScore =
				(invalids.GetValueOrDefault(')') * 3) +
				(invalids.GetValueOrDefault(']') * 57) +
				(invalids.GetValueOrDefault('}') * 1197) +
				(invalids.GetValueOrDefault('>') * 25137);

			Console.WriteLine($"The total score for corrupted lines (part one) is {invalidScore}");

			lines = lines.Where(l => !")]}>".Any(c => l.Contains(c))).ToList();

			var values = new Dictionary<char, int>();
			values['('] = 1;
			values['['] = 2;
			values['{'] = 3;
			values['<'] = 4;

			var scores = new List<long>();
			foreach (var line in lines)
			{
				var score = 0L;
				for (int i = line.Length - 1; i >= 0; i--)
				{
					score *= 5;
					score += values[line[i]];
				}
				scores.Add(score);
			}
			scores.Sort();

			Console.WriteLine($"The middle score of incomplete lines (part two) is {scores[scores.Count / 2]}");
		}
	}
}
