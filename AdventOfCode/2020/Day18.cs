using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020
{
	public class Day18 : IDay
	{
		private List<string> lines;

		public void GetInput()
		{
			lines = InputHelper.GetStringsFromInput(2020, 18);
		}

		public void Solve()
		{
			var regex = new Regex(@"\(([0-9]+ [+*] )+[0-9]+\)");

			var sumPartOne = 0L;
			var sumPartTwo = 0L;

			foreach (var line in lines)
			{
				var linePartOne = line;
				var linePartTwo = line;

				while (regex.IsMatch(linePartOne))
				{
					var match = regex.Match(linePartOne);

					var expr = match.Value.TrimStart('(').TrimEnd(')');
					var result = EvaluateGroup(expr);

					linePartOne = linePartOne.Replace(match.Value, result.ToString());
				}
				sumPartOne += EvaluateGroup(linePartOne);

				while (regex.IsMatch(linePartTwo))
				{
					var match = regex.Match(linePartTwo);

					var expr = match.Value.TrimStart('(').TrimEnd(')');
					var result = EvaluateGroupPartTwo(expr);

					linePartTwo = linePartTwo.Replace(match.Value, result.ToString());
				}
				sumPartTwo += EvaluateGroupPartTwo(linePartTwo);
			}

			Console.WriteLine($"The sum (part one) is {sumPartOne}");
			Console.WriteLine($"The sum (part two) is {sumPartTwo}");
		}

		private long EvaluateGroup(string group)
		{
			var lineParts = group.Split(' ').ToList();
			if (lineParts.Count == 1)
			{
				return Convert.ToInt64(lineParts[0]);
			}

			var val = Convert.ToInt64(lineParts[0]);

			for (int i = 0; i < lineParts.Count - 2; i += 2)
			{
				var op = lineParts[i + 1];
				var nextVal = Convert.ToInt64(lineParts[i + 2]);
				if (op == "+")
				{
					val += nextVal;
				}
				else
				{
					val *= nextVal;
				}
			}

			return val;
		}

		private long EvaluateGroupPartTwo(string group)
		{
			var regex = new Regex(@"[0-9]+ \+ [0-9]+");

			while (regex.IsMatch(group))
			{
				var match = regex.Match(group);

				var expr = match.Value.Split(' ');
				var val1 = Convert.ToInt64(expr[0]);
				var val2 = Convert.ToInt64(expr[2]);
				var result = val1 + val2;

				group = group.Replace(match.Value, result.ToString());
			}

			return EvaluateGroup(group);
		}
	}
}
