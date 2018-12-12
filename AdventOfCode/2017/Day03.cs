using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day03 : IDay
    {
		private int input;
		private List<Position> positions;
		private bool partTwo;

		public Day03()
		{
			positions = new List<Position>();
			partTwo = false;
		}

		public void GetInput()
		{
			input = Convert.ToInt32(File.ReadAllLines("2017/input/day03.txt").Where(l => !string.IsNullOrEmpty(l)).First());
		}

		public void Solve()
		{
			Spiral();
			var goalPosition = positions.First(p => p.Value == input);
			Console.WriteLine($"Amount of steps required (part one) is {Math.Abs(goalPosition.X) + Math.Abs(goalPosition.Y)}");

			partTwo = true;
			positions = new List<Position>();

			Spiral();
			goalPosition = positions.First(p => p.Value > input);
			Console.WriteLine($"First value beyond input (part two) is {goalPosition.Value}");
		}

		private void Spiral()
		{
			var steps = 1;
			var value = 1;
			var x = 0;
			var y = 0;
			positions.Add(new Position(value, x, y));

			while (value <= input)
			{
				// right
				for (int i = 0; i < steps; i++)
				{
					x++;
					value = partTwo ? CalculatePositionValue(x, y) : value + 1;
					positions.Add(new Position(value, x, y));
				}

				// up
				for (int i = 0; i < steps; i++)
				{
					y++;
					value = partTwo ? CalculatePositionValue(x, y) : value + 1;
					positions.Add(new Position(value, x, y));
				}

				// increment step count
				steps++;

				// left
				for (int i = 0; i < steps; i++)
				{
					x--;
					value = partTwo ? CalculatePositionValue(x, y) : value + 1;
					positions.Add(new Position(value, x, y));
				}

				// down
				for (int i = 0; i < steps; i++)
				{
					y--;
					value = partTwo ? CalculatePositionValue(x, y) : value + 1;
					positions.Add(new Position(value, x, y));
				}

				// increment step count
				steps++;
			}
		}

		private int CalculatePositionValue(int x, int y)
		{
			var adjacentPositions = positions.Where(p =>
				(p.X != x || p.Y != y) &&
				Math.Abs(p.X - x) <= 1 &&
				Math.Abs(p.Y - y) <= 1);

			return adjacentPositions.Sum(p => p.Value);
		}

		internal class Position
		{
			public Position(int value, int x, int y)
			{
				Value = value;
				X = x;
				Y = y;
			}

			public int Value { get; set; }
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}
