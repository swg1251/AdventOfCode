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
				Startup();
			}

			switch (day)
			{
				case 1:
					var day01 = new Day01.Day01();
					day01.Go();
					break;
				default:
					Console.WriteLine("Not yet implemented.");
					break;
			}

			Startup();
		}
    }
}
