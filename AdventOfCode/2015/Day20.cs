using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day20 : IDay
    {
		private int input;

		public void GetInput()
		{
			input = Convert.ToInt32(File.ReadAllLines("2015/input/day20.txt").Where(l => !string.IsNullOrEmpty(l)).First());
		}

		public void Solve()
		{
			var partOne = GetHouse(input);
			Console.WriteLine($"The first house to get at least {input} presents (part one) is: {partOne}");

			var partTwo = GetHouse(input, true);
			Console.WriteLine($"The first house to get at least {input} presents (part two) is: {partTwo}");
		}

		public int GetHouse(int presentTarget, bool partTwo = false)
		{
			var houseNumber = 2;
			while (GetPresentCount(houseNumber, partTwo) <= presentTarget)
			{
				// only consider even numbers, they have more factors
				houseNumber+= 2;
			}
			return houseNumber;
		}

		public int GetPresentCount(int houseNumber, bool partTwo = false)
		{
			// the elves who deliver to a given house are those whose numbers are a factor of that house number
			var deliverers = GetFactors(houseNumber);

			if (partTwo)
			{
				// if elfNumber / houseNumber > 50, that elf has already delivered 50 presents and won't deliver to this house
				deliverers = deliverers.Where(d => houseNumber / d <= 50);
			}

			// the number of presents is the sum of the factors (elves' numbers) times the number each elf delivers
			return deliverers.Sum() * (partTwo ? 11 : 10);
		}

		private IEnumerable<int> GetFactors(int n)
		{
			var factors = new HashSet<int>();
			for (int i = 1; i <= Math.Sqrt(n); i++)
			{
				if (n % i == 0)
				{
					factors.Add(i);
					factors.Add(n / i);
				}
			}
			return factors;
		}
	}
}
