using System;

namespace AdventOfCode2016
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Startup();
        }

		public static void Startup()
		{
			Console.WriteLine("Welcome to Advent of Code 2016");
			Console.WriteLine("Which day would you like to run?");
			var dayInput = Console.ReadLine();

			int day;
			if (!int.TryParse(dayInput, out day) || day < 1 || day > 25)
			{
				Console.WriteLine("Invalid day. Please enter a value between 1-25.");
			}
			else
			{
				switch (day)
				{
					case 1:
						var day01 = new Day01.Day01();
						day01.Go();
						break;
					case 2:
						var day02 = new Day02.Day02();
						day02.Go();
						break;
					case 3:
						var day03 = new Day03.Day03();
						day03.Go();
						break;
					case 4:
						var day04 = new Day04.Day04();
						day04.Go();
						break;
					case 5:
						var day05 = new Day05.Day05();
						day05.Go();
						break;
					default:
						Console.WriteLine("Not yet implemented.");
						break;
				}
			}
			Startup();
		}
    }
}
