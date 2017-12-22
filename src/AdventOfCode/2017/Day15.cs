using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day15 : IDay
    {
		private const long FACTOR_A = 16807;
		private const long FACTOR_B = 48271;
		private long startA;
		private long startB;
		private int matchCount;

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day15.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
			startA = Convert.ToInt64(input[0].Split(' ')[4]);
			startB = Convert.ToInt64(input[1].Split(' ')[4]);
		}

		public void Solve()
		{
			var valueA = startA;
			var valueB = startB;

			for (int i = 0; i < 40000000; i++)
			{
				valueA = ((valueA * FACTOR_A) % Int32.MaxValue);
				valueB = ((valueB * FACTOR_B) % Int32.MaxValue);

				if (Lowest16BitsEqual(valueA, valueB))
				{
					matchCount++;
				}
			}
			Console.WriteLine($"The number of matches (part one) is {matchCount}");

			valueA = startA;
			valueB = startB;
			matchCount = 0;

			for (int i = 0; i < 5000000; i++)
			{
				do
				{
					valueA = ((valueA * FACTOR_A) % Int32.MaxValue);
				}
				while (valueA % 4 != 0);

				do
				{
					valueB = ((valueB * FACTOR_B) % Int32.MaxValue);
				}
				while (valueB % 8 != 0);

				if (Lowest16BitsEqual(valueA, valueB))
				{
					matchCount++;
				}
			}
			Console.WriteLine($"The number of matches (part two) is {matchCount}");
		}

		private bool Lowest16BitsEqual(long valueA, long valueB)
		{
			valueA = (valueA << 48) >> 48;
			valueB = (valueB << 48) >> 48;

			return valueA == valueB;
		}
    }
}
