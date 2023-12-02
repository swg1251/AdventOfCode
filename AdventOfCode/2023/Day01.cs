using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023
{
	public class Day01 : IDay
	{
		private List<string> calibrations;
		Dictionary<string, int> numbers;

		public void GetInput()
		{
			calibrations = InputHelper.GetStringsFromInput(2023, 1).ToList();
			numbers = new Dictionary<string, int>
			{
				{ "1", 1 },
				{ "2", 2 },
				{ "3", 3 },
				{ "4", 4 },
				{ "5", 5 },
				{ "6", 6 },
				{ "7", 7 },
				{ "8", 8 },
				{ "9", 9 },
				{ "one", 1 },
				{ "two", 2 },
				{ "three", 3 },
				{ "four", 4 },
				{ "five", 5 },
				{ "six", 6 },
				{ "seven", 7 },
				{ "eight", 8 },
				{ "nine", 9 }
			};
		}

		public void Solve()
		{
			GetValues("[0-9]{1}");
			GetValues(@"([0-9])|(one|two|three|four|five|six|seven|eight|nine)", true);
		}

		private void GetValues(string matchPattern, bool partTwo = false)
		{
			var regex = new Regex(matchPattern);
			var total = 0;

			foreach (var calibration in calibrations)
			{
				var matches = regex.Matches(calibration);
				if (matches.Count >= 1)
				{
					var lastDigit = matches.Last().Value;

					if (partTwo)
					{
						// go right to left for last digit in case of overlap, eg "3oneight" should give 38, not 31
						lastDigit = Regex.Match(calibration, matchPattern, RegexOptions.RightToLeft).Value;
					}

					var valueString =
						numbers[matches.First().Value].ToString() +
						numbers[lastDigit].ToString();
					total += Convert.ToInt32(valueString);
				}
				else
				{
					Console.WriteLine($"Couldn't find digits in input line {calibration}");
				}
			}

			Console.WriteLine($"The sum of calibration values (part {(partTwo ? "two" : "one")}) is: {total}");
		}
	}
}
