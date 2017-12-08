using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day02 : IDay
    {
		List<List<int>> presentDimensions;

		public Day02()
		{
			presentDimensions = new List<List<int>>();
		}

		public void GetInput()
		{
			var lines = File.ReadAllLines("2015/input/day02.txt").Where(l => !string.IsNullOrEmpty(l));
			foreach (var line in lines)
			{
				var dimensions = line.Split('x').Select(d => Convert.ToInt32(d)).OrderBy(d => d);
				presentDimensions.Add(dimensions.ToList());
			}
		}

		public void Solve()
		{
			var paperNeeded = presentDimensions.Sum(d => (2 * d[0] * d[1]) + (2 * d[1] * d[2]) + (2 * d[0] * d[2]) + (d[0] * d[1]));
			Console.WriteLine($"The number of square feet of paper required (part one) is {paperNeeded}");

			var ribbonNeeded = presentDimensions.Sum(d => (2 * d[0]) + (2 * d[1]) + (d[0] * d[1] * d[2]));
			Console.WriteLine($"The number of feet of ribbon required (part two) is {ribbonNeeded}");
		}
    }
}
