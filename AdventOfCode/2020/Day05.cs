using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day05 : IDay
	{
		private IEnumerable<string> boardingPasses;

		public void GetInput()
		{
			boardingPasses = InputHelper.GetStringsFromInput(2020, 5);
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
			var rowBinary = boardingPass.Substring(0, 7).Replace('F', '0').Replace('B', '1');
			var colBinary = boardingPass.Substring(7).Replace('L', '0').Replace('R', '1');

			return new Seat(Convert.ToInt32(rowBinary, 2), Convert.ToInt32(colBinary, 2));
		}

		internal class Seat
		{
			public int Row { get; set; }
			public int Col { get; set; }
			public int SeatId => Row * 8 + Col;

			public Seat(int row, int col)
			{
				Row = row;
				Col = col;
			}
		}
	}
}
