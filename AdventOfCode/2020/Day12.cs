using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day12 : IDay
	{
		private List<string> directions;

		public void GetInput()
		{
			directions = File.ReadAllLines("2020/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var ship = new Ship { Direction = Direction.East };

			foreach (var move in directions)
			{
				var dir = move[0];
				var value = Convert.ToInt32(move.Substring(1));

				if (dir == 'N' || dir == 'E' || dir == 'S' || dir == 'W')
				{
					ship.Move(value, GetDirection(dir));
				}
				else if (dir == 'R' || dir == 'L')
				{
					ship.Turn(dir, value);
				}
				else if (dir == 'F')
				{
					ship.Move(value, ship.Direction);
				}
			}

			var distance = Math.Abs(ship.X) + Math.Abs(ship.Y);
			Console.WriteLine($"The distance from the starting point (part one) is {distance}");
		}

		private void PartTwo()
		{
			var ship = new Ship { Direction = Direction.East };
			var waypoint = new Ship { X = 10, Y = 1 };

			foreach (var move in directions)
			{
				var dir = move[0];
				var value = Convert.ToInt32(move.Substring(1));

				if (dir == 'N' || dir == 'E' || dir == 'S' || dir == 'W')
				{
					waypoint.Move(value, GetDirection(dir));
				}
				else if (dir == 'R' || dir == 'L')
				{
					waypoint.TurnWaypoint(dir, value);
				}
				else if (dir == 'F')
				{
					var dirX = waypoint.X < 0 ? Direction.West : Direction.East;
					var dirY = waypoint.Y < 0 ? Direction.South : Direction.North;

					for (int i = 0; i < value; i++)
					{
						ship.Move(Math.Abs(waypoint.X), dirX);
						ship.Move(Math.Abs(waypoint.Y), dirY);
					}
				}
			}

			var distance = Math.Abs(ship.X) + Math.Abs(ship.Y);
			Console.WriteLine($"The distance from the starting point (part two) is {distance}");
		}

		private Direction GetDirection(char c)
		{
			switch (c)
			{
				case 'N':
					return Direction.North;
				case 'E':
					return Direction.East;
				case 'S':
					return Direction.South;
				case 'W':
					return Direction.West;
				default:
					throw new Exception($"Bad direction: {c}");
			}
		}

		internal class Ship
		{
			public int X { get; set; }

			public int Y { get; set; }

			public Direction Direction { get; set; }

			public void Move(int value, Direction direction)
			{
				switch (direction)
				{
					case Direction.North:
						Y += value;
						break;
					case Direction.East:
						X += value;
						break;
					case Direction.South:
						Y -= value;
						break;
					case Direction.West:
						X -= value;
						break;
				}
			}

			public void Turn(char turnDir, int value)
			{
				for (int i = 0; i < value; i += 90)
				{
					switch (Direction)
					{
						case Direction.North:
							Direction = turnDir == 'L' ? Direction.West : Direction.East;
							break;
						case Direction.East:
							Direction = turnDir == 'L' ? Direction.North : Direction.South;
							break;
						case Direction.South:
							Direction = turnDir == 'L' ? Direction.East : Direction.West;
							break;
						case Direction.West:
							Direction = turnDir == 'L' ? Direction.South : Direction.North;
							break;
					}
				}
			}

			public void TurnWaypoint(char turnDir, int value)
			{
				for (int i = 0; i < value; i += 90)
				{
					var origX = X;
					var origY = Y;

					if (turnDir == 'L')
					{
						X = -origY;
						Y = origX;
					}
					else if (turnDir == 'R')
					{
						X = origY;
						Y = -origX;
					}
				}
			}

		}

		internal enum Direction
		{
			North,
			East,
			South,
			West
		}
	}
}
