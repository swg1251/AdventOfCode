using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day05 : IDay
    {
		private List<int> instructions;

		public Day05()
		{
			instructions = new List<int>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day05.txt").Where(l => !string.IsNullOrEmpty(l));

			foreach (var instruction in input)
			{
				instructions.Add(Convert.ToInt32(instruction));
			}
		}

		public void Solve()
		{
			ProcessInstructions(false);

			instructions.Clear();
			GetInput();

			ProcessInstructions(true);
		}

		private void ProcessInstructions(bool partTwo)
		{
			var i = 0;
			var steps = 0;

			while (i < instructions.Count)
			{
				var newIndex = i + instructions[i];
				steps++;
				instructions[i] += (partTwo && instructions[i] > 2) ? -1 : 1;
				i = newIndex;
			}

			Console.WriteLine($"Reached the exit (part {(partTwo ? "two" : "one")}) in {steps} steps");
		}
    }
}
