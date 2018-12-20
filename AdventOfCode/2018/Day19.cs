using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day19 : IDay
	{
		private List<string> input;

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
			input = File.ReadAllLines("2018/input/day19.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			var partOne = ProcessInstructions(input);
			Console.WriteLine($"After processing the instructions (part one) the value of reg 0 is: {partOne[0]}");

			var magicNumber = ProcessInstructions(input, true).Max();

			// Deconstructing the input shows that a certain number is calculated (10550400 + some number)
			// The main loop of the program will sum up all of the factors of this number, extremely inefficiently
			var partTwo = GetFactors(magicNumber).Sum();
			Console.WriteLine($"After processing the instructions (part two) the value of reg 0 is: {partTwo}");
		}

		public List<int> ProcessInstructions(List<string> instructions, bool partTwo = false)
		{
			var ipIndex = Convert.ToInt32(instructions[0].Split(' ')[1]);
			var ip = 0;
			instructions = instructions.Skip(1).ToList();

			var register = new List<int> { partTwo ? 1 : 0, 0, 0, 0, 0, 0 };

			while (ip < instructions.Count)
			{
				var instruction = instructions[ip].Split(' ').ToList();
				var op = instruction[0];
				var a = Convert.ToInt32(instruction[1]);
				var b = Convert.ToInt32(instruction[2]);
				var c = Convert.ToInt32(instruction[3]);

				register[ipIndex] = ip;

				if (op == "setr")
				{
					register[c] = register[a];
				}
				else if (op == "seti")
				{
					register[c] = a;
				}

				else
				{
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
					}
					else
					{
						var cop = compareOps.FirstOrDefault(o => o.Method.Name == op.Remove(2));
						if (cop != null)
						{
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
					}
				}

				if (partTwo && ip == instructions.Count - 1)
				{
					// the "magic number" that is the target to find factors of is determined at the end of the first loop
					return register;
				}

				ip = register[ipIndex] + 1;
			}

			return register;
		}

		private IEnumerable<int> GetFactors(int n)
		{
			var factors = new HashSet<int>();
			for (int i = 1; i <= Math.Sqrt(n); i++)
			{
				if (n % i == 0)
				{
					factors.Add(i);
					factors.Add(n / i);
				}
			}
			return factors;
		}
	}
}

/*
Deconstruction of my input
I worked from the inside (bottom section) out (up)

r3 is ip

(0)
addi 3 16 3		go to 17

(1)
seti 1 6 1		r1 = 1
seti 1 9 5		r5 = 1
mulr 1 5 2		r2 = r1* r5
eqrr 2 4 2		r2 = r2 == r4
addr 2 3 3		r3 += r2 (skip 1 if r2 == r4)
addi 3 1 3		skip 1
addr 1 0 0		r0 += r1 (skipped unless r2 == r4)
addi 5 1 5		r5++ (always incrementing?)				(r5 = 2 (first loop))
gtrr 5 4 2		r2 = r5 > r4
addr 3 2 3		skip 2
seti 2 4 3		go to 3
addi 1 1 1		r1++
gtrr 1 4 2		r2 = r1 > r4
addr 2 3 3		r3 (ip) += r2 (skips next step if r2 = 1, which happens when r5 > r4)
seti 1 0 3		go to 2
mulr 3 3 3		r3 = r3 * r3 (if this happens we are done!)

	// this ends up being a sum of all factors of the sum of the numbers calculated below
	for (i = r1; i <= r4; i++)
		for (j = r5; j <= r4; j++)
			// i * j is a factor of r4
			if (i * j) == r4
				// add the factor
				r0 += j

(17)
addi 4 2 4		r4 += 2
mulr 4 4 4		r4 = r4 * r4
mulr 3 4 4		r4 = r3 * r4 (r3 = 19)
muli 4 11 4		r4 = r4 * 11
addi 2 5 2		r2 += 5
mulr 2 3 2		r2 *= r3
addi 2 1 2		r2++
addr 4 2 4		r4 = r4 + r2
addr 3 0 3		r3 = r3 + r0
seti 0 3 3		go to 1

	in the first loop:
		r4 = 2
		r4 *= 2
		r4 *= 19
		r4 *= 11

		(r2 is 0 at this point)
		r2 +=5
		r2 *= 22
		r2++

		r4 += r2

		// r4 is now 947
		if r0 > 0 skip ahead else go to 1


(27)
setr 3 6 2		r2 = r3 (r3 = 27 here)
mulr 2 3 2		r2 *= r3 (r2 = 28)
addr 3 2 2		r2 += r3 (r3 = 29)
mulr 3 2 2		r2 *= r3 (r3 = 30)
muli 2 14 2		r2 *= 14
mulr 2 3 2		r2 *= r3 (r3 = 32)
addr 4 2 4		r4 += r2
seti 0 8 0		r0 = 0
seti 0 8 3		go to 1

		r2 = 27
		r2 *= 28
		r2 += 29
		r2 *= 30
		r2 *= 14
		r2 *= 32
		// r2 now equals 10550400
		// r4 is 947
		r4 += r2
		// r4 is 10551347, my "magic number"
		r0 = 0
		go to start

*/
