using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2017
{
    public class Day04 : IDay
    {
		private List<List<string>> passPhrases;

		public Day04()
		{
			passPhrases = new List<List<string>>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day04.txt").Where(l => !string.IsNullOrEmpty(l));

			foreach (var line in input)
			{
				passPhrases.Add(line.Split(' ').ToList());
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The number of valid passphrases (part one) is {passPhrases.Count(p => IsValid(p))}");
			Console.WriteLine($"The number of valid passphrases (part two) is {passPhrases.Count(p => IsValidPartTwo(p))}");
		}

		public bool IsValid(List<string> passPhrase)
		{
			return passPhrase.Count == passPhrase.Distinct().Count();
		}

		public bool IsValidPartTwo(List<string> passPhrase)
		{
			passPhrase = passPhrase.Select(p => string.Concat(p.OrderBy(c => c))).ToList();
			return passPhrase.Count == passPhrase.Distinct().Count();
		}
    }
}
