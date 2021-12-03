using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
	public class Day02 : IDay
	{
		private List<string> instructions;

		public void GetInput()
		{
			instructions = InputHelper.GetStringsFromInput(2021, 2);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var horizontal = 0;
			var depth = 0;

			foreach (var instruction in instructions)
			{
				var instructionParts = instruction.Split(' ');
				var direction = instructionParts[0];
				var distance = Convert.ToInt32(instructionParts[1]);

				switch (direction)
				{
					case "forward":
						horizontal += distance;
						break;
					case "down":
						depth += distance;
						break;
					case "up":
						depth -= distance;
						break;
				}
			}

			Console.WriteLine($"The product of the horizontal position and depth (part one) is {horizontal * depth}");
		}

		private void PartTwo()
		{
			var horizontal = 0;
			var depth = 0;
			var aim = 0;

			foreach (var instruction in instructions)
			{
				var instructionParts = instruction.Split(' ');
				var direction = instructionParts[0];
				var distance = Convert.ToInt32(instructionParts[1]);

				switch (direction)
				{
					case "forward":
						horizontal += distance;
						depth += aim * distance;
						break;
					case "down":
						aim += distance;
						break;
					case "up":
						aim -= distance;
						break;
				}
			}

			Console.WriteLine($"The product of the horizontal position and depth (part two) is {horizontal * depth}");
		}
	}
}
