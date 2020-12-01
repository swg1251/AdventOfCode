using System;

namespace AdventOfCode
{
    public class Program
    {
		private const int minYear = 2015;
		private const int maxYear = 2020;
		private static int currentYear = 2020;

        public static void Main(string[] args)
        {
            Console.WriteLine($"Welcome to Advent of Code {currentYear}.");
            Console.WriteLine($"Enter a day 1-25, or enter a year to change to that year. (type q to quit)");

            var input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                return;
            }
            Console.WriteLine();

            int date;
            if (!int.TryParse(input, out date) || date < 1 || (date > 25 && (date < minYear || date > maxYear)))
            {
                Console.WriteLine($"Invalid day/year. Please enter a value from 1-25 or {minYear}-{maxYear}.");
            }
			else if (date <= maxYear && date >= minYear)
			{
				currentYear = date;
				Main(null);
				return;
			}
            else
            {
                var dayString = date < 10 ? $"0{date}" : date.ToString();
                var dayType = Type.GetType($"AdventOfCode.Year{currentYear}.Day{dayString}");
                var dayInstance = (IDay)Activator.CreateInstance(dayType ?? typeof(DayNotImplemented));

                dayInstance.GetInput();
                dayInstance.Solve();
                Console.WriteLine();
            }
            Main(null);
        }
    }
}
