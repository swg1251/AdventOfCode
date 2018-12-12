using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day08 : IDay
    {
		private IEnumerable<string> strings;
		private int totalLength;

		public void GetInput()
		{
			strings = File.ReadAllLines("2015/input/day08.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			totalLength = strings.Sum(s => s.Length);
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var valueLength = 0;

			foreach (var s in strings)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] == '"')
					{
						continue;
					}

					valueLength++;

					if (s[i] == '\\')
					{
						if (s[i + 1] == 'x')
						{
							i += 2;
						}
						i++;
					}
				}
			}

			Console.WriteLine($"The difference in string literals and string value length (part one) is: {totalLength - valueLength}");
		}

		private void PartTwo()
		{
			var newTotalLength = 0;

			foreach (var s in strings)
			{
				var newString = "\"";

				foreach (var c in s)
				{
					if (c == '\"')
					{
						newString += '\\';
						newString += '\"';
					}
					else if (c == '\\')
					{
						newString += '\\';
						newString += '\\';
					}
					else
					{
						newString += c;
					}
				}

				newString += "\"";

				newTotalLength += newString.Length;
			}

			Console.WriteLine($"The difference between the re-encoded length and original length (part two) is: {newTotalLength - totalLength}");
		}
    }
}
