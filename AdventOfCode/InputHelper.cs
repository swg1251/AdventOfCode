using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	public class InputHelper
	{
		public static List<int> GetIntegersFromInput(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			var lines = File.ReadAllLines($"{year}/input/{dayStr}.txt").Where(l => !string.IsNullOrEmpty(l));
			return lines.Select(l => Convert.ToInt32(l)).ToList();
		}

		public static List<long> GetLongIntegersFromInput(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			var lines = File.ReadAllLines($"{year}/input/{dayStr}.txt").Where(l => !string.IsNullOrEmpty(l));
			return lines.Select(l => Convert.ToInt64(l)).ToList();
		}

		public static List<string> GetStringsFromInput(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			return File.ReadAllLines($"{year}/input/{dayStr}.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}
	}
}
