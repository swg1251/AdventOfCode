using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day11 : IDay
    {
		private int maxSteps;
		private double x;
		private double y;
		private Position start;
		private Position farthestPosition;

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day11.txt").Where(l => !string.IsNullOrEmpty(l)).First();
			
			foreach (var direction in input.Split(','))
			{
				if (direction == "n")
				{
					y++;
				}
				else if (direction == "ne")
				{
					x++;
					y += .5;
				}
				else if (direction == "se")
				{
					x++;
					y -= .5;
				}
				else if (direction == "s")
				{
					y--;
				}
				else if (direction == "sw")
				{
					x--;
					y -= .5;
				}
				else if (direction == "nw")
				{
					x--;
					y += .5;
				}

				var currentSteps = CalculateSteps(new Position(x, y));
				if (currentSteps > maxSteps)
				{
					maxSteps = currentSteps;
				}
			}

			start = new Position(x, y);
		}

		public void Solve()
		{
			Console.WriteLine($"The fewest number of steps to reach the child process (part one) is {CalculateSteps(start)}");
			Console.WriteLine($"The fewest number of steps to reach the child process from the farthest point (part two) is {maxSteps}");
		}

		private int CalculateSteps(Position position)
		{
			var steps = 0;

			while (!position.IsGoal())
			{
				position = position.GetNextPosition();
				steps++;
			}
			return steps;
		}

		internal class Position
		{
			public double X { get; set; }
			public double Y { get; set; }

			public Position(double x, double y)
			{
				X = x;
				Y = y;
			}

			public bool IsGoal()
			{
				return X == 0 && Y == 0;
			}

			public Position GetNextPosition()
			{
				var neighbors = new List<Position>();

				// north
				neighbors.Add(new Position(X, Y + 1));

				// northeast
				neighbors.Add(new Position(X + 1, Y + .5));

				// southeast
				neighbors.Add(new Position(X + 1, Y - .5));

				// south
				neighbors.Add(new Position(X, Y - 1));

				// southwest
				neighbors.Add(new Position(X - 1, Y - .5));

				// northwest
				neighbors.Add(new Position(X - 1, Y + .5));

				return neighbors.OrderBy(p => p.Distance()).First();
			}

			public double Distance()
			{
				return Distance(X, Y);
			}

			public static double Distance(double x, double y)
			{
				return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
			}
		}
    }
}
