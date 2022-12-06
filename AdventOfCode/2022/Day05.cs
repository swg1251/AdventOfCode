using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day05 : IDay
	{
		private List<Stack<char>> stacksPartOne;
		private List<Stack<char>> stacksPartTwo;
		private List<string> instructions;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2022, 5, true);
			var stackLabelsLine = lines.FindIndex(l => l.StartsWith(" 1"));
			instructions = lines.Skip(stackLabelsLine + 2).ToList();

			var stackCount = Convert.ToInt32(lines[stackLabelsLine].Split("   ").Last());
			stacksPartOne = new List<Stack<char>>();
			stacksPartTwo = new List<Stack<char>>();
			for (int i = 0; i < stackCount; i++)
			{
				stacksPartOne.Add(new Stack<char>());
				stacksPartTwo.Add(new Stack<char>());
			}

			for (int i = stackLabelsLine - 1; i >= 0; i--)
			{
				for (int j = 0; j < lines[i].Length; j += 4)
				{
					if (!string.IsNullOrWhiteSpace(lines[i].Substring(j, 3)))
					{
						stacksPartOne[j / 4].Push(lines[i][j + 1]);
						stacksPartTwo[j / 4].Push(lines[i][j + 1]);
					}
				}
			}
		}

		public void Solve()
		{
			foreach (var instruction in instructions)
			{
				var parts = instruction.Split(' ');
				var quantity = Convert.ToInt32(parts[1]);
				var sourceIndex = Convert.ToInt32(parts[3]) - 1;
				var destIndex = Convert.ToInt32(parts[5]) - 1;
				var partTwoStack = new Stack<char>();

				for (int i = 0; i < quantity; i++)
				{
					stacksPartOne[destIndex].Push(stacksPartOne[sourceIndex].Pop());
					partTwoStack.Push(stacksPartTwo[sourceIndex].Pop());
				}

				while (partTwoStack.Any())
				{
					stacksPartTwo[destIndex].Push(partTwoStack.Pop());
				}
			}

			var topCratesPartOne = "";
			var topCratesPartTwo = "";
			for (int i = 0; i < stacksPartOne.Count; i++)
			{
				topCratesPartOne += stacksPartOne[i].Peek();
				topCratesPartTwo += stacksPartTwo[i].Peek();
			}
			Console.WriteLine($"The crates on top of each stack (part one) are {topCratesPartOne}");
			Console.WriteLine($"The crates on top of each stack (part two) are {topCratesPartTwo}");
		}
	}
}
