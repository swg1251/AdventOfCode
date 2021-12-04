using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day04 : IDay
	{
		private List<int> draws;
		private List<(int index, List<List<int>> board)> boardTuples;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 4, true);

			draws = lines.First().Split(',').Select(x => Convert.ToInt32(x)).ToList();
			boardTuples = new List<(int index, List<List<int>> board)>();

			var i = -1;
			var board = new List<List<int>>();

			foreach (var line in lines.Skip(2))
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					boardTuples.Add((i++, board));
					board = new List<List<int>>();
					continue;
				}

				var row = line.Split(' ')
					.Where(x => !string.IsNullOrWhiteSpace(x))
					.Select(x => Convert.ToInt32(x))
					.ToList();
				board.Add(row);
			}
			boardTuples.Add((i++, board));
		}

		public void Solve()
		{
			var winningScores = new List<int>();
			var winningBoards = new HashSet<int>();
			IEnumerable<int> drawn = null;

			for (int i = 5; i <= draws.Count; i++)
			{
				drawn = draws.Take(i);

				foreach (var (index, board) in boardTuples.Where(bt => !winningBoards.Contains(bt.index)))
				{
					foreach (var row in board)
					{
						if (row.All(x => drawn.Contains(x)))
						{
							winningScores.Add(GetBoardScore(board, drawn));
							winningBoards.Add(index);
							break;
						}
					}

					for (int column = 0; column < board.First().Count; column++)
					{
						if (board.All(r => drawn.Contains(r[column])))
						{
							winningScores.Add(GetBoardScore(board, drawn));
							winningBoards.Add(index);
							break;
						}
					}
				}
			}

			Console.WriteLine($"The score of the first winning board (part one) is {winningScores.First()}");
			Console.WriteLine($"The score of the final winning board (part two) is {winningScores.Last()}");
		}

		private int GetBoardScore(List<List<int>> board, IEnumerable<int> drawn)
		{
			return board.Sum(r => r.Where(x => !drawn.Contains(x)).Sum()) * drawn.Last();
		}
	}
}
