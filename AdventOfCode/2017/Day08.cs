using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day08 : IDay
    {
		IEnumerable<string> input;
		Dictionary<string, int> registers;

		public Day08()
		{
			registers = new Dictionary<string, int>();
		}

		public void GetInput()
		{
			input = File.ReadAllLines("2017/input/day08.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			var maxValueAllTime = 0;

			foreach (var instruction in input.Select(l => l.Split(' ')))
			{
				var register = instruction[0];
				var multiplier = instruction[1] == "inc" ? 1 : -1;
				var incrementValue = Convert.ToInt32(instruction[2]);
				var checkRegister = instruction[4];
				var equalityOperator = instruction[5];
				var equalityValue = Convert.ToInt32(instruction[6]);

				if (!registers.ContainsKey(register))
				{
					registers[register] = 0;
				}
				if (!registers.ContainsKey(checkRegister))
				{
					registers[checkRegister] = 0;
				}

				if (equalityOperator == "<" && registers[checkRegister] < equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}
				else if (equalityOperator == "<=" && registers[checkRegister] <= equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}
				else if (equalityOperator == "==" && registers[checkRegister] == equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}
				else if (equalityOperator == "!=" && registers[checkRegister] != equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}
				else if (equalityOperator == ">" && registers[checkRegister] > equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}
				else if (equalityOperator == ">=" && registers[checkRegister] >= equalityValue)
				{
					registers[register] = registers[register] + (multiplier * incrementValue);
				}

				if (registers[register] > maxValueAllTime)
				{
					maxValueAllTime = registers[register];
				}
			}

			Console.WriteLine($"The highest value in any register (part one) is {registers.Values.Max()}");
			Console.WriteLine($"The highest value ever held in any register (part two) is {maxValueAllTime}");
		}
    }
}
