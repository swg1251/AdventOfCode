using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day11 : IDay
    {
		private double x;
		private double y;
		private Position start;

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
			}

			start = new Position(x, y);
		}

		public void Solve()
		{
			var queue = new Queue<Position>();
			queue.Enqueue(start);

			while (queue.Any())
			{
				var currentPosition = queue.Dequeue();

				if (currentPosition.IsGoal())
				{
					Console.WriteLine($"The smallest number of steps to reach the goal (part one) is {currentPosition.Path.Count() - 1}");
					return;
				}

				foreach (var neighbor in currentPosition.GetNeighboringPositions())
				{
					if (neighbor.Distance() <= currentPosition.Distance())
					{
						queue.Enqueue(neighbor);
					}
				}
			}
		}

		internal class Position
		{
			public double X { get; set; }
			public double Y { get; set; }

			public IEnumerable<Position> Path { get; set; }

			public Position(double x, double y)
			{
				X = x;
				Y = y;
				Path = new List<Position>() { this };
			}

			public Position(double x, double y, IEnumerable<Position> path)
			{
				X = x;
				Y = y;
				Path = new List<Position>();
				foreach (var position in path)
				{
					Path.Append(position);
				}
				Path.Append(this);
			}

			public bool IsGoal()
			{
				return X == 0 && Y == 0;
			}

			public IEnumerable<Position> GetNeighboringPositions()
			{
				var neighbors = new List<Position>();

				// north
				if (!Path.Any(p => p.X == X && p.Y == Y + 1))
				{
					neighbors.Add(new Position(X, Y + 1, Path));
				}

				// northeast
				if (!Path.Any(p => p.X == X + 1 && p.Y == Y + .5))
				{
					neighbors.Add(new Position(X + 1, Y + .5, Path));
				}

				// southeast
				if (!Path.Any(p => p.X == X + 1 && p.Y == Y - .5))
				{
					neighbors.Add(new Position(X + 1, Y - .5, Path));
				}

				// south
				if (!Path.Any(p => p.X == X && p.Y == Y - 1))
				{
					neighbors.Add(new Position(X, Y - 1, Path));
				}

				// southwest
				if (!Path.Any(p => p.X == X - 1 && p.Y == Y - .5))
				{
					neighbors.Add(new Position(X - 1, Y - .5, Path));
				}

				// northwest
				if (!Path.Any(p => p.X == X - 1 && p.Y == Y + .5))
				{
					neighbors.Add(new Position(X - 1, Y + .5, Path));
				}

				return neighbors;
			}

			public double Distance()
			{
				return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
			}
		}
    }
}
