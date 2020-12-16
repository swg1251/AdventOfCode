using System;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day04 : IDay
	{
		private const int inputMin = 264360;
		private const int inputMax = 746325;

		public void GetInput()
		{
			// this space intentionally left blank
		}

		public void Solve()
		{
			var validPasswordsPartOne = 0;
			var validPasswordsPartTwo = 0;
			for (int i = inputMin; i <= inputMax; i++)
			{
				if (IsValid(i.ToString()))
				{
					validPasswordsPartOne++;
				}
				if (IsValid(i.ToString(), true))
				{
					validPasswordsPartTwo++;
				}
			}

			Console.WriteLine($"The number of valid passwords (part one) is: {validPasswordsPartOne}");
			Console.WriteLine($"The number of valid passwords (part two) is: {validPasswordsPartTwo}");
		}

		private bool IsValid(string password, bool partTwo = false)
		{
			var orderedPassword = string.Concat(password.OrderBy(c => c));
			if (password != orderedPassword)
			{
				return false;
			}

			var hasAdjacentPair = false;
			for (int i = 0; i < password.Length - 1; i++)
			{
				if (password[i] == password[i + 1] && (!partTwo || password.Count(c => c == password[i]) == 2))
				{
					hasAdjacentPair = true;
					break;
				}
			}

			if (!hasAdjacentPair)
			{
				return false;
			}

			return true;
		}
	}
}
