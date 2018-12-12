using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day03 : IDay
    {
		private string instructions;
		private List<Position> positions;

		public Day03()
		{
			positions = new List<Position>();
		}

		public void GetInput()
		{
			instructions = File.ReadAllLines("2015/input/day03.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			MoveSanta();
			positions.Clear();
			MoveSantaAndRoboSanta();
		}

		private void MoveSanta()
		{
			positions.Add(new Position(0, 0));

			var x = 0;
			var y = 0;

			foreach (var instruction in instructions)
			{
				if (instruction == '^')
				{
					y++;
				}
				else if (instruction == 'v')
				{
					y--;
				}
				else if (instruction == '<')
				{
					x--;
				}
				else if (instruction == '>')
				{
					x++;
				}

				if (!positions.Any(p => p.X == x && p.Y == y))
				{
					positions.Add(new Position(x, y));
				}
			}

			Console.WriteLine($"The number of houses Santa visits (part one) is {positions.Count}");
		}

		private void MoveSantaAndRoboSanta()
		{
			positions.Add(new Position(0, 0));

			var santaX = 0;
			var santaY = 0;
			var roboX = 0;
			var roboY = 0;

			for (int i = 0; i < instructions.Length - 1; i += 2)
			{
				if (instructions[i] == '^')
				{
					santaY++;
				}
				else if (instructions[i] == 'v')
				{
					santaY--;
				}
				else if (instructions[i] == '<')
				{
					santaX--;
				}
				else if (instructions[i] == '>')
				{
					santaX++;
				}

				if (!positions.Any(p => p.X == santaX && p.Y == santaY))
				{
					positions.Add(new Position(santaX, santaY));
				}

				if (instructions[i + 1] == '^')
				{
					roboY++;
				}
				else if (instructions[i + 1] == 'v')
				{
					roboY--;
				}
				else if (instructions[i + 1] == '<')
				{
					roboX--;
				}
				else if (instructions[i + 1] == '>')
				{
					roboX++;
				}

				if (!positions.Any(p => p.X == roboX && p.Y == roboY))
				{
					positions.Add(new Position(roboX, roboY));
				}
			}

			Console.WriteLine($"The number of houses Santa and Robo Santa visit (part two) is {positions.Count}");
		}

		internal class Position
		{
			public int X { get; set; }
			public int Y { get; set; }

			public Position(int x, int y)
			{
				X = x;
				Y = y;
			}
		}
    }
}
