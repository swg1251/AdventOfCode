using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day16 : IDay
    {
		private IEnumerable<string> instructions;
		private string programs = "abcdefghijklmnop";
		private List<string> seenPositions;

		public Day16()
		{
			seenPositions = new List<string>();
		}
		
		public void GetInput()
		{
			instructions = File.ReadAllLines("2017/input/day16.txt").Where(l => !string.IsNullOrEmpty(l)).First().Split(',');
		}

		public void Solve()
		{
			seenPositions.Add(programs);

			Dance();
			Console.WriteLine($"The position after one dance (part one) is {programs}");

			var i = 1;
			while (true)
			{
				Dance();
				i++;

				if (seenPositions.Contains(programs))
				{
					break;
				}

				seenPositions.Add(programs);
			}

			// we have enountered a known position, we know it cycles every i dances
			var cycleSize = i - seenPositions.IndexOf(programs);

			// do the dances after the final cycle has finished until we hit 1,000,000
			for (int j = 0; j < 1000000 % cycleSize; j++)
			{
				Dance();
			}

			Console.WriteLine($"The position after one BILLION dances (part two) is {programs}");
		}

		private void Dance()
		{
			foreach (var instruction in instructions)
			{
				// spin
				if (instruction[0] == 's')
				{
					var spinSize = Convert.ToInt32(instruction.Substring(1));
					var spinGroup = programs.Substring(programs.Length - spinSize);
					programs = programs.Remove(programs.Length - spinSize);
					programs = spinGroup + programs;
				}

				// exchange
				else if (instruction[0] == 'x')
				{
					var instructionParts = instruction.Substring(1).Split('/');
					var pos1 = Convert.ToInt32(instructionParts[0]);
					var pos2 = Convert.ToInt32(instructionParts[1]);
					programs = SwapPositions(programs, pos1, pos2);
				}

				// partner
				else
				{
					var char1 = instruction[1];
					var char2 = instruction[3];
					var pos1 = programs.IndexOf(char1);
					var pos2 = programs.IndexOf(char2);
					programs = SwapPositions(programs, pos1, pos2);
				}
			}
		}

		private string SwapPositions(string input, int pos1, int pos2)
		{
			var output = "";
			for (int i = 0; i < input.Length; i++)
			{
				if (i == pos1)
				{
					output += input[pos2];
				}
				else if (i == pos2)
				{
					output += input[pos1];
				}
				else
				{
					output += input[i];
				}
			}
			return output;
		}
    }
}
