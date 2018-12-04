using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day11 : IDay
    {
		private string password;

		public void GetInput()
		{
			password = File.ReadAllLines("2015/input/day11.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			while (!IsValid(password))
			{
				password = Increment(password);
			}
			Console.WriteLine($"The first valid password (part one) is: {password}");

			password = Increment(password);
			while (!IsValid(password))
			{
				password = Increment(password);
			}
			Console.WriteLine($"The first valid password (part one) is: {password}");
		}

		private string Increment(string pass)
		{
			var charPass = pass.ToCharArray();

			var carry = 1;
			var finished = false;

			while (!finished)
			{
				if (charPass[charPass.Length - carry] != 'z')
				{
					// Skip i, o, and l to save some time
					if (charPass[charPass.Length - carry] == 'i' ||
						charPass[charPass.Length - carry] == 'o' ||
						charPass[charPass.Length - carry] == 'l')
					{
						charPass[charPass.Length - carry]++;
					}

					charPass[charPass.Length - carry]++;
					finished = true;
				}
				else
				{
					charPass[charPass.Length - carry] = 'a';
					carry++;
				}
			}

			return new string(charPass);
		}

		private bool IsValid(string pass)
		{
			// Can't contain i, o, or l
			if (pass.Contains('i') || pass.Contains('o') || pass.Contains('l'))
			{
				return false;
			}

			// Must have increasing straight
			var hasStraight = false;
			for (int i = 0; i < pass.Length - 2; i++)
			{
				if (pass[i] + 1 == pass[i + 1] && pass[i] + 2 == pass[i + 2])
				{
					hasStraight = true;
					break;
				}
			}
			if (!hasStraight)
			{
				return false;
			}

			// Must have two non-overlapping pairs
			var doubleChars = new HashSet<char>();
			for (int i = 0; i < pass.Length - 1; i++)
			{
				if (pass[i] == pass[i + 1])
				{
					doubleChars.Add(pass[i]);
					i++;
				}
			}
			if (doubleChars.Count() < 2)
			{
				return false;
			}

			return true;
		}
    }
}
