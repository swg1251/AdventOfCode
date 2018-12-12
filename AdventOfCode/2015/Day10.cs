using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day10 : IDay
    {
		private string lookSay;

		public void GetInput()
		{
			lookSay = File.ReadAllLines("2015/input/day10.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			for (int iterations = 0; iterations < 50; iterations++)
			{
				// StringBuilder was orders of magnitude faster than .ToString()ing everything
				var sb = new StringBuilder();

				for (int i = 0; i < lookSay.Length - 1; i++)
				{
					var value = lookSay[i];
					var streak = 1;
					while (lookSay[i] == lookSay[i + 1])
					{
						streak++;
						i++;
					}

					sb.Append(streak);
					sb.Append(value);
				}

				if (lookSay[lookSay.Length - 2] != lookSay[lookSay.Length - 1])
				{
					sb.Append('1');
					sb.Append(lookSay[lookSay.Length - 1]);
				}

				lookSay = sb.ToString();

				if (iterations == 39)
				{
					Console.WriteLine($"After 40 iterations, the length (part one) is: {lookSay.Length}");
				}
				else if (iterations == 49)
				{
					Console.WriteLine($"After 50 iterations, the length (part two) is: {lookSay.Length}");
				}
			}
		}
    }
}
