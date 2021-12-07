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

		public static List<int> GetIntegersFromCommaSeparatedInput(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			var line = File.ReadAllLines($"{year}/input/{dayStr}.txt").First();
			return line.Split(',').Select(l => Convert.ToInt32(l)).ToList();
		}

		public static List<long> GetLongIntegersFromInput(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			var lines = File.ReadAllLines($"{year}/input/{dayStr}.txt").Where(l => !string.IsNullOrEmpty(l));
			return lines.Select(l => Convert.ToInt64(l)).ToList();
		}

		public static List<string> GetStringsFromInput(int year, int day, bool keepBlankLines = false)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			var lines = File.ReadAllLines($"{year}/input/{dayStr}.txt").ToList();
			if (!keepBlankLines)
			{
				lines = lines.Where(l => !string.IsNullOrEmpty(l)).ToList();
			}
			return lines;
		}
	}
}
