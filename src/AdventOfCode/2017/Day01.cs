using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day01 : IDay
    {
		private string captcha;
		private int sum;

		public Day01()
		{
			sum = 0;
		}

		public void GetInput()
		{
			captcha = File.ReadAllLines("2017/input/day01.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			PartOne();
			Console.WriteLine($"The captcha solution (part one) is {sum}");

			sum = 0;
			PartTwo();
			Console.WriteLine($"The captcha solution (part two) is {sum}");
		}

		private void PartOne()
		{
			for (int i = 0; i < captcha.Length - 1; i++)
			{
				// if the digit matches the next, add it to the sum
				if (captcha[i] == captcha[i + 1])
				{
					sum += (int)Char.GetNumericValue(captcha[i]);
				}
			}

			// check if the final digit matches the first
			if (captcha[0] == captcha[captcha.Length - 1])
			{
				sum += (int)Char.GetNumericValue(captcha[0]);
			}
		}

		private void PartTwo()
		{
			var halfLength = captcha.Length / 2;

			for (int i = 0; i < halfLength; i++)
			{
				// if the digit matches the one halfway around the list, add it to the sum
				if (captcha[i] == captcha[i + halfLength])
				{
					sum += (int)Char.GetNumericValue(captcha[i]);
				}
			}

			// any matches from the first half will match again as the list loops back around
			// rather than looping through again, just double the current sum
			sum *= 2;
		}
    }
}
