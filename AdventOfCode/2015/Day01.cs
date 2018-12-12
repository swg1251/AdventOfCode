using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day01 : IDay
    {
		private string instructions;

		public void GetInput()
		{
			instructions = File.ReadAllLines("2015/input/day01.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			Console.WriteLine($"The instrucions take Santa (part one) to floor {instructions.Count(c => c == '(') - instructions.Count(c => c == ')')}");

			var i = 0;
			var position = 0;

			while (position > -1)
			{
				position += (instructions[i] == '(') ? 1 : -1;
				i++;
			}

			Console.WriteLine($"The position of the instruction causing Sants to enter the basement (part two) is {i}");
		}
    }
}
