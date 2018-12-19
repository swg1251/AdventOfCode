using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day16 : IDay
	{
		private List<string> inputPartOne;

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day16.txt").ToList();
			var splitIndex = 0;
			for (int i = 0; i < input.Count - 2; i++)
			{
				if (string.IsNullOrWhiteSpace(input[i]) && string.IsNullOrWhiteSpace(input[i + 1]) && string.IsNullOrWhiteSpace(input[i + 2]))
				{
					splitIndex = i;
					break;
				}
			}
			inputPartOne = input.Take(splitIndex).Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			var matchingScenarios = 0;
			for (int i = 0; i < inputPartOne.Count; i+= 3)
			{
				var scenario = new List<string> { inputPartOne[i], inputPartOne[i + 1], inputPartOne[i + 2] };
				if (GetMatchingMethodCount(scenario) >= 3)
				{
					matchingScenarios++;
				}
			}
			Console.WriteLine(matchingScenarios);
		}

		public int GetMatchingMethodCount(List<string> scenario)
		{
			var before = scenario[0].Split('[')[1].TrimEnd(']').Split(", ").Select(x => Convert.ToInt32(x)).ToList();
			var instruction = scenario[1].Split(' ').Select(x => Convert.ToInt32(x)).ToList();
			var after = scenario[2].Split('[')[1].TrimEnd(']').Split(", ").Select(x => Convert.ToInt32(x)).ToList();

			int add(int a, int b) => a + b;
			int mul(int a, int b) => a * b;
			int ban(int a, int b) => a & b;
			int bor(int a, int b) => a | b;
			var actionOps = new List<Func<int, int, int>> { add, mul, ban, bor };

			int gt(int a, int b) => a > b ? 1 : 0;
			int eq(int a, int b) => a == b ? 1 : 0;
			var compareOps = new List<Func<int, int, int>> { gt, eq };

			var matchingOps = 0;
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
					matchingOps++;
				}

				// immediate
				b = instruction[2];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps++;
				}
			}

			// seti
			result[c] = instruction[1];
			if (ListsEqual(result, after))
			{
				matchingOps++;
			}

			// setr
			result[c] = before[instruction[1]];
			if (ListsEqual(result, after))
			{
				matchingOps++;
			}

			foreach (var op in compareOps)
			{
				// immediate/register
				var a = instruction[1];
				var b = before[instruction[2]];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps++;
				}

				// register/immediate
				a = before[instruction[1]];
				b = instruction[2];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps++;
				}

				// register/register
				a = before[instruction[1]];
				b = before[instruction[2]];
				result[c] = op(a, b);
				if (ListsEqual(result, after))
				{
					matchingOps++;
				}
			}

			return matchingOps;
		}

		private bool ListsEqual(List<int> a, List<int> b)
		{
			return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
		}

		public List<int> banr(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = registers[instruction[1]] & registers[instruction[2]];
			return registers;
		}

		public List<int> bani(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = registers[instruction[1]] & instruction[2];
			return registers;
		}

		public List<int> borr(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = registers[instruction[1]] | registers[instruction[2]];
			return registers;
		}

		public List<int> bori(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = registers[instruction[1]] | instruction[2];
			return registers;
		}

		public List<int> setr(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = registers[instruction[1]];
			return registers;
		}

		public List<int> seti(List<int> registers, List<int> instruction)
		{
			registers[instruction[3]] = instruction[1];
			return registers;
		}
	}
}
