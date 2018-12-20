using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day16 : IDay
	{
		private List<string> inputPartOne;
		private List<string> inputPartTwo;

		private static int add(int a, int b) => a + b;
		private static int mul(int a, int b) => a * b;
		private static int ban(int a, int b) => a & b;
		private static int bor(int a, int b) => a | b;
		readonly List<Func<int, int, int>> actionOps = new List<Func<int, int, int>> { add, mul, ban, bor };

		private static int gt(int a, int b) => a > b ? 1 : 0;
		private static int eq(int a, int b) => a == b ? 1 : 0;
		readonly List<Func<int, int, int>> compareOps = new List<Func<int, int, int>> { gt, eq };

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day16.txt").ToList();
			var splitIndex = 0;
			for (int i = 0; i < input.Count; i++)
			{
				if (string.IsNullOrWhiteSpace(input[i]) && string.IsNullOrWhiteSpace(input[i + 1]) && string.IsNullOrWhiteSpace(input[i + 2]))
				{
					splitIndex = i;
					break;
				}
			}
			inputPartOne = input.Take(splitIndex).Where(l => !string.IsNullOrEmpty(l)).ToList();
			inputPartTwo = input.Skip(splitIndex).Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			var scenarios = ProcessScenarios(inputPartOne);
			var partOne = scenarios.Count(s => s.validOps.Count >= 3);
			Console.WriteLine($"The number of scenarios with >=3 possible ops (part one) is: {partOne}");

			var operations = ResolveOps(scenarios);
			var result = ProcessInstructions(inputPartTwo, operations);
			Console.WriteLine($"After processing all instructions (part two), register 0 has value: {result[0]}");
		}

		public List<(int opCode, List<string> validOps)> ProcessScenarios(List<string> inputScenarios)
		{
			var scenarios = new List<(int opCode, List<string> validOps)>();
			for (int i = 0; i < inputScenarios.Count; i += 3)
			{
				var scenario = new List<string> { inputScenarios[i], inputScenarios[i + 1], inputScenarios[i + 2] };
				scenarios.Add(GetPossibleOperations(scenario));
			}
			return scenarios;
		}

		private List<int> ProcessInstructions(List<string> instructions, Dictionary<int, string> operations)
		{
			var register = new List<int> { 0, 0, 0, 0 };

			foreach (var instructionString in instructions)
			{
				var instruction = instructionString.Split(' ').Select(i => Convert.ToInt32(i)).ToList();
				var op = operations[instruction[0]];
				var a = instruction[1];
				var b = instruction[2];
				var c = instruction[3];

				if (op == "setr")
				{
					register[c] = register[a];
					continue;
				}
				else if (op == "seti")
				{
					register[c] = a;
					continue;
				}

				var aop = actionOps.FirstOrDefault(o => o.Method.Name == op.Remove(3));
				if (aop != null)
				{
					if (op[3] == 'r')
					{
						register[c] = aop(register[a], register[b]);
					}
					else
					{
						register[c] = aop(register[a], b);
					}
					continue;
				}

				var cop = compareOps.FirstOrDefault(o => o.Method.Name == op.Remove(2));
				if (op[2] == 'i' && op[3] == 'r')
				{
					register[c] = cop(a, register[b]);
				}
				else if (op[2] == 'r' && op[3] == 'i')
				{
					register[c] = cop(register[a], b);
				}
				else
				{
					register[c] = cop(register[a], register[b]);
				}
			}

			return register;
		}

		private Dictionary<int, string> ResolveOps(List<(int opCode, List<string> validOps)> scenarios)
		{
			var allOps = new List<string>
			{
				"addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori",
				"setr", "seti",
				"gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr"
			};
			var operations = new Dictionary<int, string>();

			while (operations.Count < 16)
			{
				for (int i = 0; i < 16; i++)
				{
					var opCodeOps = scenarios.Where(s => s.opCode == i);
					var possibleOps = new List<string>();

					foreach (var op in allOps)
					{
						if (opCodeOps.All(s => s.validOps.Contains(op)))
						{
							possibleOps.Add(op);
						}
					}
					if (possibleOps.Count == 1)
					{
						operations[i] = possibleOps.First();
						allOps.Remove(possibleOps.First());
					}
				}
			}

			return operations;
		}

		private (int opCode, List<string> validOps) GetPossibleOperations(List<string> scenario)
		{
			var before = scenario[0].Split('[')[1].TrimEnd(']').Split(", ").Select(x => Convert.ToInt32(x)).ToList();
			var instruction = scenario[1].Split(' ').Select(x => Convert.ToInt32(x)).ToList();
			var after = scenario[2].Split('[')[1].TrimEnd(']').Split(", ").Select(x => Convert.ToInt32(x)).ToList();

			var matchingOps = new List<string>();
			var result = new List<int>();
			result.AddRange(before);
			var c = instruction[3];
			foreach (var op in actionOps)
			{
				var a = before[instruction[1]];

				// register
				var b = before[instruction[2]];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps.Add(op.Method.Name + "r");
				}

				// immediate
				b = instruction[2];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps.Add(op.Method.Name + "i");
				}
			}

			// setr
			result[c] = before[instruction[1]];
			if (ListsEqual(result, after))
			{
				matchingOps.Add("setr");
			}

			// seti
			result[c] = instruction[1];
			if (ListsEqual(result, after))
			{
				matchingOps.Add("seti");
			}

			foreach (var op in compareOps)
			{
				// immediate/register
				var a = instruction[1];
				var b = before[instruction[2]];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps.Add(op.Method.Name + "ir");
				}

				// register/immediate
				a = before[instruction[1]];
				b = instruction[2];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps.Add(op.Method.Name + "ri");
				}

				// register/register
				a = before[instruction[1]];
				b = before[instruction[2]];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps.Add(op.Method.Name + "rr");
				}
			}

			return (instruction[0],  matchingOps);
		}

		private bool ListsEqual(List<int> a, List<int> b)
		{
			return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
		}
	}
}
