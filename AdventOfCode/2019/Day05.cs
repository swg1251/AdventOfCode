using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day05 : IDay
	{
		private List<int> state;

		public void GetInput()
		{
			state = File.ReadAllLines("2019/input/day05.txt").First().Split(',').Select(x => Convert.ToInt32(x)).ToList();
		}

		public void Solve()
		{
			RunProgram(1);
			GetInput();
			RunProgram(5);
		}

		private void RunProgram(int insertValue)
		{
			var i = 0;
			while (true)
			{
				var opcode = state[i];
				var strOp = opcode.ToString();
				var op = Convert.ToInt32(strOp.Last().ToString());
				int a, b;

				switch (op)
				{
					case 1:
					case 2:

						a = IsImmediate(strOp, 1) ? state[i + 1] : state[state[i + 1]];
						b = IsImmediate(strOp, 2) ? state[i + 2] : state[state[i + 2]];
						state[state[i + 3]] = AddOrMul(a, b, op);

						i += 4;
						break;

					case 3:

						state[state[i + 1]] = insertValue;
						i += 2;
						break;

					case 4:

						var output = IsImmediate(strOp, 1) ? state[i + 1] : state[state[i + 1]];
						if (output > 0)
						{
							Console.WriteLine(output);
						}
						i += 2;
						break;

					case 5:
					case 6:

						a = IsImmediate(strOp, 1) ? state[i + 1] : state[state[i + 1]];
						b = IsImmediate(strOp, 2) ? state[i + 2] : state[state[i + 2]];

						if (ShouldJump(op, a))
						{
							i = b;
						}
						else
						{
							i += 3;
						}

						break;

					case 7:
					case 8:
						a = IsImmediate(strOp, 1) ? state[i + 1] : state[state[i + 1]];
						b = IsImmediate(strOp, 2) ? state[i + 2] : state[state[i + 2]];

						if (EvalLessOrEqual(op, a, b))
						{
							state[state[i + 3]] = 1;
						}
						else
						{
							state[state[i + 3]] = 0;
						}

						i += 4;
						break;

					case 9:
						return;
					default:
						throw new InvalidOperationException($"Invalid opcode encountered: {state[i]}");
				}
			}
		}

		private int AddOrMul(int a, int b, int op)
		{
			return op == 1 ? a + b : a * b;
		}

		private bool IsImmediate(string opcode, int paramNumber)
		{
			return opcode.Length - (paramNumber + 2) >= 0 && opcode[opcode.Length - (paramNumber + 2)] == '1';
		}

		private bool ShouldJump(int op, int value)
		{
			return op == 5 ? value != 0 : value == 0;
		}

		private bool EvalLessOrEqual(int op, int a, int b)
		{
			return op == 7 ? a < b : a == b;
		}
	}
}
