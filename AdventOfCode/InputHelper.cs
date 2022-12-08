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
			var lines = File.ReadAllLines(GetFileName(year, day)).Where(l => !string.IsNullOrEmpty(l));
			return lines.Select(l => Convert.ToInt32(l)).ToList();
		}

		public static List<List<int>> GetIntegerGridFromInput(int year, int day)
		{
			var lines = File.ReadAllLines(GetFileName(year, day)).Where(l => !string.IsNullOrEmpty(l));
			var grid = new List<List<int>>();
			foreach (var line in lines)
			{
				grid.Add(line.Select(x => Convert.ToInt32(x)).ToList());
			}
			return grid;
		}

		public static List<int> GetIntegersFromCommaSeparatedInput(int year, int day)
		{
			var line = File.ReadAllLines(GetFileName(year, day)).First();
			return line.Split(',').Select(l => Convert.ToInt32(l)).ToList();
		}

		public static List<long> GetLongIntegersFromInput(int year, int day)
		{
			var lines = File.ReadAllLines(GetFileName(year, day)).Where(l => !string.IsNullOrEmpty(l));
			return lines.Select(l => Convert.ToInt64(l)).ToList();
		}

		public static List<string> GetStringsFromInput(int year, int day, bool keepBlankLines = false)
		{
			var lines = File.ReadAllLines(GetFileName(year, day)).ToList();
			if (!keepBlankLines)
			{
				lines = lines.Where(l => !string.IsNullOrEmpty(l)).ToList();
			}
			return lines;
		}

		private static string GetFileName(int year, int day)
		{
			var dayStr = day > 9 ? $"day{day}" : $"day0{day}";
			return $"{year}/input/{dayStr}.txt";
		}
	}
}
