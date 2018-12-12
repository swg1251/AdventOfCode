using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day16 : IDay
	{
		private Dictionary<int, Dictionary<string, int>> sues;
		private Dictionary<string, int> trueSue;

		public void GetInput()
		{
			var input = File.ReadAllLines("2015/input/day16.txt").Where(l => !string.IsNullOrEmpty(l));

			trueSue = new Dictionary<string, int>();
			trueSue["children"] = 3;
			trueSue["cats"] = 7;
			trueSue["samoyeds"] = 2;
			trueSue["pomeranians"] = 3;
			trueSue["akitas"] = 0;
			trueSue["vizslas"] = 0;
			trueSue["goldfish"] = 5;
			trueSue["trees"] = 3;
			trueSue["cars"] = 2;
			trueSue["perfumes"] = 1;

			sues = new Dictionary<int, Dictionary<string, int>>();
			foreach (var line in input)
			{
				var lineParts = line.Split(' ');
				var sueNumber = Convert.ToInt32(lineParts[1].TrimEnd(':'));
				sues[sueNumber] = new Dictionary<string, int>();
				sues[sueNumber][lineParts[2].TrimEnd(':')] = Convert.ToInt32(lineParts[3].TrimEnd(','));
				sues[sueNumber][lineParts[4].TrimEnd(':')] = Convert.ToInt32(lineParts[5].TrimEnd(','));
				sues[sueNumber][lineParts[6].TrimEnd(':')] = Convert.ToInt32(lineParts[7].TrimEnd(','));
			}
		}

		public void Solve()
		{
			SueSearch(false);
			SueSearch(true);
		}

		private void SueSearch(bool partTwo)
		{
			bool equal(int a, int b) => a == b;
			bool lessThan(int a, int b) => a < b;
			bool greaterThan(int a, int b) => a > b;

			foreach (var sue in sues)
			{
				var isTrueSue = true;
				foreach (var key in trueSue.Keys)
				{
					Func<int, int, bool> op = equal;
					if (partTwo)
					{
						if (key == "cats" || key == "trees") op = greaterThan;
						else if (key == "pomeranians" || key == "goldfish") op = lessThan;
					}

					if (sue.Value.ContainsKey(key) && !op(sue.Value[key], trueSue[key]))
					{
						isTrueSue = false;
						break;
					}
				}
				if (isTrueSue)
				{
					Console.WriteLine($"The true Sue (part {(partTwo ? "two" : "one")}) is: {sue.Key}");
					break;
				}
			}
		}
	}
}
