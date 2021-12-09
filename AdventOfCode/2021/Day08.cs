using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day08 : IDay
	{
		private List<string> lines;

		public void GetInput()
		{
			lines = InputHelper.GetStringsFromInput(2021, 8);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var onesFoursSevensEights = 0;

			foreach (var line in lines)
			{
				var lineParts = line.Split(" | ");
				var outputs = lineParts[1].Split(' ');
				onesFoursSevensEights += outputs.Count(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7);
			}

			Console.WriteLine($"The number of 1/4/7/8 in the outputs (part one) is {onesFoursSevensEights}");
		}

		private void PartTwo()
		{
			var total = 0;

			foreach (var line in lines)
			{
				var lineParts = line.Split(" | ");

				var inputs = lineParts[0].Split(' ');
				var outputs = lineParts[1].Split(' ');
				var numbers = new Dictionary<int, string>();

				// 1 is unique, has 2 segments
				numbers[1] = inputs.Single(s => s.Length == 2);

				// 4 is unique, has 4 segments
				numbers[4] = inputs.Single(s => s.Length == 4);

				// 7 is unique, has 3 segments
				numbers[7] = inputs.Single(s => s.Length == 3);

				// 8 is unique, has 7 segments
				numbers[8] = inputs.Single(s => s.Length == 7);

				// 0 has 6 segments - 2 shared with 1, 3 shared with 4, 3 shared with 7, 6 shared with 8
				numbers[0] = inputs.Single(s => s.Length == 6 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 6);

				// 2 has 5 segments - 1 shared with 1, 2 shared with 4, 2 shared with 7, 5 shared with 8
				numbers[2] = inputs.Single(s => s.Length == 5 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 1 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 5);

				// 3 has 5 segments - 2 shared with 1, 3 shared with 4, 3 shared with 7, 5 shared with 8
				numbers[3] = inputs.Single(s => s.Length == 5 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 5);

				// 5 has 5 segments - 1 shared with 1, 3 shared with 4, 2 shared with 7, 5 shared with 8
				numbers[5] = inputs.Single(s => s.Length == 5 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 1 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 5);

				// 6 has 6 segments - 1 shared with 1, 3 shared with 4, 2 shared with 7, 6 shared with 8
				numbers[6] = inputs.Single(s => s.Length == 6 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 1 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 6);

				// 9 has 6 segments - 2 shared with 1, 4 shared with 4, 3 shared with 7, 6 shared with 8
				numbers[9] = inputs.Single(s => s.Length == 6 &&
					GetNumberOfSharedLetters(s, numbers[1]) == 2 &&
					GetNumberOfSharedLetters(s, numbers[4]) == 4 &&
					GetNumberOfSharedLetters(s, numbers[7]) == 3 &&
					GetNumberOfSharedLetters(s, numbers[8]) == 6);

				var outputStr = string.Empty;
				foreach (var o in outputs)
				{
					outputStr += numbers.Single(n => n.Value.Length == o.Length &&
						GetNumberOfSharedLetters(o, n.Value) == o.Length).Key.ToString();
				}
				total += Convert.ToInt32(outputStr);
			}
			Console.WriteLine($"The total of all outputs (part two) is {total}");
		}

		private int GetNumberOfSharedLetters(string string1, string string2)
		{
			return string1.Count(c => string2.Contains(c));
		}
	}
}
