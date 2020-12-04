using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day04 : IDay
	{
		private List<Dictionary<string, string>> passports;

		public void GetInput()
		{
			passports = new List<Dictionary<string, string>>();

			var lines = File.ReadAllLines("2020/input/day4.txt");

			var passport = new Dictionary<string, string>();

			foreach (var line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					passports.Add(passport);
					passport = new Dictionary<string, string>();
					continue;
				}

				var lineParts = line.Split(' ');
				foreach (var part in lineParts)
				{
					var kvp = part.Split(':');
					passport.Add(kvp[0], kvp[1]);
				}
			}

			// make sure the last one gets added
			passports.Add(passport);
		}

		public void Solve()
		{
			var validPartOne = 0;
			var validPartTwo = 0;

			foreach (var passport in passports)
			{
				if (passport.ContainsKey("byr") &&
					passport.ContainsKey("iyr") &&
					passport.ContainsKey("eyr") &&
					passport.ContainsKey("hgt") &&
					passport.ContainsKey("hcl") &&
					passport.ContainsKey("ecl") &&
					passport.ContainsKey("pid"))
				{
					validPartOne++;
				}
				else
				{
					continue;
				}

				if (ValidYear(passport["byr"], 1920, 2002) &&
					ValidYear(passport["iyr"], 2010, 2020) &&
					ValidYear(passport["eyr"], 2020, 2030) &&
					ValidHeight(passport["hgt"]) &&
					ValidHairColor(passport["hcl"]) &&
					ValidEyeColor(passport["ecl"]) &&
					ValidPassportId(passport["pid"]))
				{
					validPartTwo++;
				}
			}

			Console.WriteLine($"The number of valid passports (part one) is {validPartOne}");
			Console.WriteLine($"The number of valid passports (part two) is {validPartTwo}");
		}
		
		private bool ValidYear(string yearStr, int min, int max)
		{
			if (yearStr.Length == 4)
			{
				var year = Convert.ToInt32(yearStr);
				return year >= min && year <= max;
			}

			return false;
		}

		private bool ValidHeight(string height)
		{
			if (height.Length <= 2)
			{
				return false;
			}

			var unit = height.Substring(height.Length - 2);
			var measure = Convert.ToInt32(height.Substring(0, height.Length - 2));

			if (unit == "cm")
			{
				return measure >= 150 && measure <= 193;
			}
			if (unit == "in")
			{
				return measure >= 59 && measure <= 76;
			}

			return false;
		}

		private bool ValidHairColor(string color)
		{
			var validChars = "0123456789abcdef";
			if (color.Length == 7)
			{
				return color[0] == '#' && color.Substring(1).All(c => validChars.Contains(c));
			}

			return false;
		}

		private bool ValidEyeColor(string color)
		{
			return color == "amb" ||
				color == "blu" ||
				color == "brn" ||
				color == "gry" ||
				color == "grn" ||
				color == "hzl" ||
				color == "oth";
		}

		private bool ValidPassportId(string id)
		{
			return id.Length == 9 && int.TryParse(id, out int x);
		}
	}
}
