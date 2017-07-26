using System;

namespace AdventOfCode2016
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2016");
            Console.WriteLine("Which day would you like to run?");
            var dayInput = Console.ReadLine();
            Console.WriteLine();

            int day;
            if (!int.TryParse(dayInput, out day) || day < 1 || day > 25)
            {
                Console.WriteLine("Invalid day. Please enter a value between 1-25.");
            }
            else
            {
                var dayString = day < 10 ? $"0{day}" : day.ToString();
                var dayType = Type.GetType($"AdventOfCode2016.Day{dayString}");
                var dayInstance = (IDay)Activator.CreateInstance(dayType ?? typeof(DayNotImplemented));

                dayInstance.GetInput();
                dayInstance.Solve();
                Console.WriteLine();
            }
            Main(null);
        }
    }
}
