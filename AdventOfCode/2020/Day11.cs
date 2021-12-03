using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day11 : IDay
	{
		private List<List<SeatState>> seats;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 11);

			seats = new List<List<SeatState>>();

			foreach (var line in lines)
			{
				var row = new List<SeatState>();
				foreach (var c in line)
				{
					if (c == 'L')
					{
						row.Add(SeatState.Empty);
					}
					else if (c == '.')
					{
						row.Add(SeatState.Floor);
					}
				}
				seats.Add(row);
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The number of occupied seats (part one) is {Run()}");
			Console.WriteLine($"The number of occupied seats (part two) is {Run(true)}");
		}

		private int Run(bool partTwo = false)
		{
			var currentState = new List<List<SeatState>>(seats);

			while (true)
			{
				var newState = new List<List<SeatState>>();

				for (int y = 0; y < currentState.Count; y++)
				{
					var newRow = new List<SeatState>();

					for (int x = 0; x < currentState[y].Count; x++)
					{
						if (currentState[y][x] == SeatState.Floor)
						{
							newRow.Add(SeatState.Floor);
							continue;
						}

						var adjacentSeats = 0;
						if (partTwo)
						{
							adjacentSeats = GetAdjacentSeatCountPartTwo(currentState, y, x);
						}
						else
						{
							adjacentSeats = GetAdjacentSeatCountPartOne(currentState, y, x);
						}

						var adjacentThreshold = partTwo ? 5 : 4;
						if (currentState[y][x] == SeatState.Empty && adjacentSeats == 0)
						{
							newRow.Add(SeatState.Occupied);
						}
						else if (currentState[y][x] == SeatState.Occupied && adjacentSeats >= adjacentThreshold)
						{
							newRow.Add(SeatState.Empty);
						}
						else
						{
							newRow.Add(currentState[y][x]);
						}
					}

					newState.Add(newRow);
				}

				if (AreEqual(currentState, newState))
				{
					return currentState.Sum(r => r.Count(s => s == SeatState.Occupied));
				}

				currentState = new List<List<SeatState>>(newState);
			}
		}

		private int GetAdjacentSeatCountPartOne(List<List<SeatState>> currentState, int y, int x)
		{
			var adjacentSeats = 0;
			var moves = new List<(int dy, int dx)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

			foreach (var (dy, dx) in moves)
			{
				if (y + dy >= 0 && y + dy < currentState.Count &&
					x + dx >= 0 && x + dx < currentState[y].Count &&
					currentState[y + dy][x + dx] == SeatState.Occupied)
				{
					adjacentSeats++;
				}
			}

			return adjacentSeats;
		}

		private int GetAdjacentSeatCountPartTwo(List<List<SeatState>> currentState, int y, int x)
		{
			var moves = new List<(int dy, int dx)> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

			return moves.Count(m => OccupiedSeatInDirection(m.dy, m.dx, currentState, y, x));
		}

		private bool OccupiedSeatInDirection(int dy, int dx, List<List<SeatState>> state, int y, int x)
		{
			y += dy;
			x += dx;

			while (y >= 0 && y < state.Count && x >= 0 && x < state[y].Count && state[y][x] != SeatState.Empty)
			{
				if (state[y][x] == SeatState.Occupied)
				{
					return true;
				}
				y += dy;
				x += dx;
			}

			return false;
		}

		private bool AreEqual(List<List<SeatState>> oldState, List<List<SeatState>> newState)
		{
			for (int i = 0; i < oldState.Count; i++)
			{
				for (int j = 0; j < oldState[i].Count; j++)
				{
					if (oldState[i][j] != newState[i][j])
					{
						return false;
					}
				}
			}

			return true;
		}

		internal enum SeatState
		{
			Floor,
			Empty,
			Occupied
		}
	}
}
