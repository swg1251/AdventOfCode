using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day05 : IDay
	{
		private IEnumerable<string> boardingPasses;

		public void GetInput()
		{
			boardingPasses = File.ReadAllLines("2020/input/day05.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			var seats = new List<Seat>();
			foreach (var boardingPass in boardingPasses)
			{
				seats.Add(GetSeat(boardingPass));
			}

			Console.WriteLine($"The highest seat ID (part one) is {seats.Max(s => s.SeatId)}");

			var firstRow = seats.Min(s => s.Row);
			var lastRow = seats.Max(s => s.Row);
			for (int i = firstRow; i < lastRow; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (seats.Count(s => s.Row == i && s.Col == j) == 0)
					{
						Console.WriteLine($"My seat ID (part two) is {i * 8 + j}");
						return;
					}
				}
			}
		}

		private Seat GetSeat(string boardingPass)
		{
			var rows = new List<int>();
			for (int i = 0; i < 128; i++)
			{
				rows.Add(i);
			}

			var cols = new List<int>();
			for (int i = 0; i < 8; i++)
			{
				cols.Add(i);
			}

			foreach (var c in boardingPass)
			{
				if (c == 'F')
				{
					rows = rows.Take(rows.Count / 2).ToList();
				}
				else if (c == 'B')
				{
					rows = rows.TakeLast(rows.Count / 2).ToList();
				}
				else if (c == 'L')
				{
					cols = cols.Take(cols.Count / 2).ToList();
				}
				else if (c == 'R')
				{
					cols = cols.TakeLast(cols.Count / 2).ToList();
				}
			}

			return new Seat(rows[0], cols[0]);
		}

		internal class Seat
		{
			public int Row;
			public int Col;
			public int SeatId => Row * 8 + Col;

			public Seat(int row, int col)
			{
				Row = row;
				Col = col;
			}
		}
	}
}
