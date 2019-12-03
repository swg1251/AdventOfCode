using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day02 : IDay
	{
		private List<int> initialState;

		public void GetInput()
		{
			initialState = File.ReadAllLines("2019/input/day02.txt").First().Split(',').Select(x => Convert.ToInt32(x)).ToList();
		}

		public void Solve()
		{
			Console.WriteLine($"The value at pos 0 after running (part one) is: {RunProgram(12, 2)}");

			for (int noun = 0; noun < 100; noun++)
			{
				for (int verb = 0; verb < 100; verb++)
				{
					if (RunProgram(noun, verb) == 19690720)
					{
						var result = 100 * noun + verb;
						Console.WriteLine($"After reaching 19690720, 100 * noun ({noun}) + verb ({verb}) (part two) is: {result}");
						return;
					}
				}
			}
		}

		private int RunProgram(int noun, int verb)
		{
			GetInput();
			initialState[1] = noun;
			initialState[2] = verb;

			var i = 0;
			while (true)
			{
				Func<int, int, int> op;

				switch (initialState[i])
				{
					case 1:
						op = Add;
						break;
					case 2:
						op = Mul;
						break;
					case 99:
						return initialState[0];
					default:
						throw new InvalidOperationException($"Invalid opcode encountered: {initialState[i]}");
				}

				var val1 = initialState[i + 1];
				var val2 = initialState[i + 2];
				var target = initialState[i + 3];

				initialState[target] = op(initialState[val1], initialState[val2]);

				i += 4;
			}
		}

		private int Add(int a, int b) => a + b;

		private int Mul(int a, int b) => a * b;
	}
}
