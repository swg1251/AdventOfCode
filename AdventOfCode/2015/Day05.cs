using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day05 : IDay
    {
		private IEnumerable<string> strings;

		public void GetInput()
		{
			strings = File.ReadAllLines("2015/input/day05.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			Console.WriteLine($"The number of nice strings (part one) is {strings.Count(s => IsNicePartOne(s))}");
			Console.WriteLine($"The number of nice strings (part two) is {strings.Count(s => IsNicePartTwo(s))}");
		}

		private bool IsNicePartOne(string s)
		{
			if (s.Count(c => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') < 3)
			{
				return false;
			}

			for (int i = 0; i < s.Length; i++)
			{
				if (i == s.Length - 1)
				{
					return false;
				}

				if (s[i] == s[i + 1])
				{
					break;
				}
			}

			if (s.Contains("ab") || s.Contains("cd") || s.Contains("pq") || s.Contains("xy"))
			{
				return false;
			}

			return true;
		}

		private bool IsNicePartTwo(string s)
		{
			var pairs = new List<string>();
			for (int i = 0; i < s.Length - 1; i++)
			{
				pairs.Add(s[i].ToString() + s[i + 1].ToString());

				if (i < s.Length - 2 && s[i] == s[i + 1] && s[i] == s[i + 2])
				{
					i++;
				}
			}
			if (pairs.Distinct().Count() == pairs.Count())
			{
				return false;
			}

			for (int i = 0; i < s.Length; i++)
			{
				if (i == s.Length - 2)
				{
					return false;
				}

				if (s[i] == s[i + 2])
				{
					break;
				}
			}

			return true;
		}
    }
}
