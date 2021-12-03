using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
	public class Day08 : IDay
	{
		private List<string> originalInstructions;

		public void GetInput()
		{
			originalInstructions = InputHelper.GetStringsFromInput(2020, 8);
		}

		public void Solve()
		{
			Console.WriteLine(Run(originalInstructions, false));

			for (int i = 0; i < originalInstructions.Count; i++)
			{
				var op = originalInstructions[i].Substring(0, 3);
				if (op == "acc")
				{
					continue;
				}

				var newInstructions = new List<string>(originalInstructions);
				if (op == "nop")
				{
					newInstructions[i] = newInstructions[i].Replace("nop", "jmp");
				}
				else
				{
					newInstructions[i] = newInstructions[i].Replace("jmp", "nop");
				}

				var acc = Run(newInstructions, true);
				if (acc != -1)
				{
					Console.WriteLine(acc);
					return;
				}
			}

			
		}

		private int Run(List<string> instructions, bool partTwo)
		{
			var acc = 0;
			var executedInstructions = new HashSet<int>();

			for (int i = 0; i < instructions.Count; i++)
			{
				if (!executedInstructions.Add(i))
				{
					return partTwo ? -1 : acc;
				}

				var instParts = instructions[i].Split(' ');

				var op = instParts[0];
				if (op == "nop")
				{
					continue;
				}

				var val = Convert.ToInt32(instParts[1].Substring(1));
				val *= instParts[1][0] == '+' ? 1 : -1;

				if (op == "jmp")
				{
					i += val - 1;
				}
				if (op == "acc")
				{
					acc += val;
				}
			}

			return partTwo ? acc : -1;
		}
	}
}
