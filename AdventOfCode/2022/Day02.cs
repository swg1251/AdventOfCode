using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
	public class Day02 : IDay
	{
		private List<string> guide;

		public void GetInput()
		{
			guide = InputHelper.GetStringsFromInput(2022, 2);
		}

		public void Solve()
		{
			var scorePartOne = 0;
			var scorePartTwo = 0;

			foreach (var line in guide)
			{
				var parts = line.Split(' ');
				var theirMove = parts[0];
				var myMove = parts[1];

				scorePartOne += GetScorePartOne(GetMoveType(theirMove), GetMoveType(myMove));
				scorePartTwo += GetScorePartTwo(GetMoveType(theirMove), myMove);
			}

			Console.WriteLine($"The score for part one is {scorePartOne}");
			Console.WriteLine($"The score for part two is {scorePartTwo}");
		}

		private int GetScorePartOne(MoveType theirMove, MoveType myMove)
		{
			if (theirMove == myMove)
			{
				return (int)myMove + 3;
			}
			if (theirMove == GetDefeatedMove(myMove))
			{
				return (int)myMove + 6;
			}
			return (int)myMove;
		}

		private int GetScorePartTwo(MoveType theirMove, string requiredResult)
		{
			return requiredResult switch
			{
				"X" => GetScorePartOne(theirMove, GetDefeatedMove(theirMove)),
				"Y" => GetScorePartOne(theirMove, theirMove),
				"Z" => GetScorePartOne(theirMove, GetDefeatingMove(theirMove)),
				_ => throw new Exception(),
			};
		}

		private enum MoveType
		{
			Rock = 1,
			Paper = 2,
			Scissors = 3
		}

		private MoveType GetDefeatedMove(MoveType move)
		{
			return move switch
			{
				MoveType.Paper => MoveType.Rock,
				MoveType.Rock => MoveType.Scissors,
				MoveType.Scissors => MoveType.Paper,
				_ => throw new Exception(),
			};
		}

		private MoveType GetDefeatingMove(MoveType move)
		{
			return move switch
			{
				MoveType.Paper => MoveType.Scissors,
				MoveType.Rock => MoveType.Paper,
				MoveType.Scissors => MoveType.Rock,
				_ => throw new Exception(),
			};
		}

		private MoveType GetMoveType(string move)
		{
			switch (move)
			{
				case "A":
				case "X":
					return MoveType.Rock;
				case "B":
				case "Y":
					return MoveType.Paper;
				case "C":
				case "Z":
					return MoveType.Scissors;
				default:
					throw new Exception();
			}
		}
	}
}
