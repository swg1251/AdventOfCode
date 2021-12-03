using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class Program
    {
		private const int minYear = 2015;
		private const int maxYear = 2021;
		private static int currentYear = 2021;

        public static void Main(string[] args)
        {
            Console.WriteLine($"Welcome to Advent of Code! The year is currently set to {currentYear}.");
            Console.WriteLine($"Enter a day 1-25, or enter a year to change to that year. (type q to quit)");

            var input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                return;
            }
			else if (input.ToLower() == "profile")
			{
				Console.WriteLine();
				RunProfiler();
			}
            Console.WriteLine();

			if (!int.TryParse(input, out int day) || day < 1 || (day > 25 && (day < minYear || day > maxYear)))
			{
				Console.WriteLine($"Invalid day/year. Please enter a value from 1-25 or {minYear}-{maxYear}.");
			}
			else if (day <= maxYear && day >= minYear)
			{
				currentYear = day;
			}
			else
			{
				var dayString = day < 10 ? $"0{day}" : day.ToString();
				var dayType = Type.GetType($"AdventOfCode.Year{currentYear}.Day{dayString}");
				var dayInstance = (IDay)Activator.CreateInstance(dayType ?? typeof(DayNotImplemented));

				dayInstance.GetInput();
				dayInstance.Solve();
				Console.WriteLine();
			}

			Main(null);
        }

		private static void RunProfiler()
		{
			var runTimes = new Dictionary<(int year, string day), long>();

			for (int year = minYear; year <= maxYear; year++)
			{
				for (int day = 1; day <= 25; day++)
				{
					var dayString = day < 10 ? $"0{day}" : day.ToString();
					var dayType = Type.GetType($"AdventOfCode.Year{year}.Day{dayString}");
					var dayInstance = (IDay)Activator.CreateInstance(dayType ?? typeof(DayNotImplemented));
					if (dayInstance is DayNotImplemented)
					{
						continue;
					}

					Console.WriteLine($"{year} day {dayString} starting");

					var watch = Stopwatch.StartNew();
					dayInstance.GetInput();
					dayInstance.Solve();
					watch.Stop();

					Console.WriteLine($"{year} day {dayString} finished in {watch.ElapsedMilliseconds} ms");

					runTimes.Add((year, dayString), watch.ElapsedMilliseconds);

					Console.WriteLine();
				}
			}

			foreach (var day in runTimes.OrderByDescending(d => d.Value))
			{
				Console.WriteLine($"{day.Key.year}\t{day.Key.day}\t{day.Value} ms");
			}
			var totalTime = TimeSpan.FromMilliseconds(runTimes.Sum(r => r.Value));
			Console.WriteLine($"\nAll days completed in {totalTime.ToString(@"hh\:mm\:ss")}\n");

			Main(null);
		}
    }
}
