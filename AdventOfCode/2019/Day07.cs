using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day07 : IDay
	{
		private List<int> program;

		public void GetInput()
		{
			program = File.ReadAllLines("2019/input/day07.txt").First().Split(',').Select(x => Convert.ToInt32(x)).ToList();
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var maxValue = 0;

			foreach (var (a, b, c, d, e) in GetPermutations(0, 5))
			{
				var ampA = new Amplifier(program, a, 0);
				RunProgram(ampA);

				var ampB = new Amplifier(program, b, ampA.Output);
				RunProgram(ampB);

				var ampC = new Amplifier(program, c, ampB.Output);
				RunProgram(ampC);

				var ampD = new Amplifier(program, d, ampC.Output);
				RunProgram(ampD);

				var ampE = new Amplifier(program, e, ampD.Output);
				RunProgram(ampE);

				if (ampE.Output > maxValue)
				{
					maxValue = ampE.Output;
				}
			}

			Console.WriteLine($"Max signal (part one): {maxValue}");
		}

		private void PartTwo()
		{
			var maxValue = 0;

			foreach (var (a, b, c, d, e) in GetPermutations(5, 9))
			{
				var ampA = new Amplifier(program, a, 0);
				RunProgram(ampA);

				var ampB = new Amplifier(program, b, ampA.Output);
				RunProgram(ampB);

				var ampC = new Amplifier(program, c, ampB.Output);
				RunProgram(ampC);

				var ampD = new Amplifier(program, d, ampC.Output);
				RunProgram(ampD);

				var ampE = new Amplifier(program, e, ampD.Output);
				RunProgram(ampE);

				while (true)
				{
					ampA.Input = ampE.Output;
					RunProgram(ampA);

					ampB.Input = ampA.Output;
					RunProgram(ampB);

					ampC.Input = ampB.Output;
					RunProgram(ampC);

					ampD.Input = ampC.Output;
					RunProgram(ampD);

					ampE.Input = ampD.Output;
					if (RunProgram(ampE))
					{
						break;
					}
				}

				if (ampE.Output > maxValue)
				{
					maxValue = ampE.Output;
				}
			}

			Console.WriteLine($"Max signal (part two): {maxValue}");
		}

		private bool RunProgram(Amplifier amp)
		{
			while (true)
			{
				var opcode = amp.Prog[amp.I];
				var strOp = opcode.ToString();
				var op = Convert.ToInt32(strOp.Last().ToString());
				int a, b;

				switch (op)
				{
					case 1:
					case 2:

						a = IsImmediate(strOp, 1) ? amp.Prog[amp.I + 1] : amp.Prog[amp.Prog[amp.I + 1]];
						b = IsImmediate(strOp, 2) ? amp.Prog[amp.I + 2] : amp.Prog[amp.Prog[amp.I + 2]];
						amp.Prog[amp.Prog[amp.I + 3]] = AddOrMul(a, b, op);

						amp.I += 4;
						break;

					case 3:

						var insertValue = amp.PhaseUsed ? amp.Input : amp.Phase;
						amp.Prog[amp.Prog[amp.I + 1]] = insertValue;
						amp.PhaseUsed = true;
						amp.I += 2;
						break;

					case 4:

						var output = IsImmediate(strOp, 1) ? amp.Prog[amp.I + 1] : amp.Prog[amp.Prog[amp.I + 1]];
						if (output > 0)
						{
							amp.Output = output;
						}

						amp.I += 2;
						return false;

					case 5:
					case 6:

						a = IsImmediate(strOp, 1) ? amp.Prog[amp.I + 1] : amp.Prog[amp.Prog[amp.I + 1]];
						b = IsImmediate(strOp, 2) ? amp.Prog[amp.I + 2] : amp.Prog[amp.Prog[amp.I + 2]];

						if (ShouldJump(op, a))
						{
							amp.I = b;
						}
						else
						{
							amp.I += 3;
						}

						break;

					case 7:
					case 8:
						a = IsImmediate(strOp, 1) ? amp.Prog[amp.I + 1] : amp.Prog[amp.Prog[amp.I + 1]];
						b = IsImmediate(strOp, 2) ? amp.Prog[amp.I + 2] : amp.Prog[amp.Prog[amp.I + 2]];

						if (EvalLessOrEqual(op, a, b))
						{
							amp.Prog[amp.Prog[amp.I + 3]] = 1;
						}
						else
						{
							amp.Prog[amp.Prog[amp.I + 3]] = 0;
						}

						amp.I += 4;
						break;

					case 9:
						return true;
					default:
						throw new InvalidOperationException($"Invalid opcode encountered: {amp.Prog[amp.I]}");
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

		private class Amplifier
		{
			public List<int> Prog { get; set; }

			public int Phase { get; set; }

			public int Input { get; set; }

			public bool PhaseUsed { get; set; }

			public int I { get; set; }

			public int Output { get; set; }

			public Amplifier(List<int> instructions, int phase, int input)
			{
				Prog = new List<int>();
				foreach (var instruction in instructions)
				{
					Prog.Add(instruction);
				}

				Phase = phase;
				Input = input;
			}
		}

		private IEnumerable<(int a, int b, int c, int d, int e)> GetPermutations(int min, int max)
		{
			for (int a = min; a <= max; a++)
			{
				for (int b = min; b <= max; b++)
				{
					if (a == b)
					{
						continue;
					}

					for (int c = min; c <= max; c++)
					{
						if (a == c || b == c)
						{
							continue;
						}

						for (int d = min; d <= max; d++)
						{
							if (a == d || b == d || c == d)
							{
								continue;
							}

							for (int e = min; e <= max; e++)
							{
								if (a == e || b == e || c == e || d == e)
								{
									continue;
								}

								yield return (a, b, c, d, e);
							}
						}
					}
				}
			}
		}
	}
}
