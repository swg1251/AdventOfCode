﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day22 : IDay
	{
		private Queue<int> playerOneCardsInput;
		private Queue<int> playerTwoCardsInput;
		private Dictionary<(int, int), int> knownScores;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 22);
			playerOneCardsInput = new Queue<int>();
			playerTwoCardsInput = new Queue<int>();
			knownScores = new Dictionary<(int, int), int>();
			var playerTwo = false;

			foreach (var line in lines.Skip(1))
			{
				if (line.StartsWith("Player 2"))
				{
					playerTwo = true;
					continue;
				}

				if (playerTwo)
				{
					playerTwoCardsInput.Enqueue(Convert.ToInt32(line));
				}
				else
				{
					playerOneCardsInput.Enqueue(Convert.ToInt32(line));
				}
			}
		}

		public void Solve()
		{
			var partOne = GetWinningScore(new Queue<int>(playerOneCardsInput), new Queue<int>(playerTwoCardsInput), false);
			Console.WriteLine($"The winning score (part one) is {Math.Abs(partOne)}");

			knownScores.Clear();

			var partTwo = GetWinningScore(new Queue<int>(playerOneCardsInput), new Queue<int>(playerTwoCardsInput), true);
			Console.WriteLine($"The winning score (part two) is {Math.Abs(partTwo)}");
		}

		// returns positive score if player one wins, negative if player two wins
		private int GetWinningScore(Queue<int> playerOneCards, Queue<int> playerTwoCards, bool partTwo)
		{
			var playerOneHashCode = GetSequenceHashCode(playerOneCards);
			var playerTwoHashCode = GetSequenceHashCode(playerTwoCards);
			if (knownScores.TryGetValue((playerOneHashCode, playerTwoHashCode), out int knownScore))
			{
				return knownScore;
			}

			var seenGames = new HashSet<(int, int)>();
			var infiniteRecursionDetected = false;

			while (playerOneCards.Any() && playerTwoCards.Any())
			{
				if (!seenGames.Add((GetSequenceHashCode(playerOneCards), GetSequenceHashCode(playerTwoCards))))
				{
					infiniteRecursionDetected = true;
					break;
				}

				var playerOneCard = playerOneCards.Dequeue();
				var playerTwoCard = playerTwoCards.Dequeue();
				var playerOneWins = playerOneCard > playerTwoCard;

				if (partTwo && playerOneCards.Count >= playerOneCard && playerTwoCards.Count >= playerTwoCard)
				{
					var newDeckOne = new Queue<int>(playerOneCards.Take(playerOneCard));
					var newDeckTwo = new Queue<int>(playerTwoCards.Take(playerTwoCard));
					var subScore = GetWinningScore(newDeckOne, newDeckTwo, true);
					playerOneWins = subScore > 0;
				}

				if (playerOneWins)
				{
					playerOneCards.Enqueue(playerOneCard);
					playerOneCards.Enqueue(playerTwoCard);
				}
				else
				{
					playerTwoCards.Enqueue(playerTwoCard);
					playerTwoCards.Enqueue(playerOneCard);
				}
			}

			var winningCards = (playerOneCards.Any() || infiniteRecursionDetected)
				? playerOneCards.Reverse()
				: playerTwoCards.Reverse();

			var score = 0;
			var i = 1;
			foreach (var card in winningCards)
			{
				score += i * card;
				i++;
			}

			if (!playerOneCards.Any())
			{
				score *= -1;
			}

			knownScores[(playerOneHashCode, playerTwoHashCode)] = score;
			return score;
		}

		public static int GetSequenceHashCode<T>(IEnumerable<T> sequence)
		{
			const int seed = 487;
			const int modifier = 31;

			return sequence.Aggregate(seed, (current, item) =>
				(current * modifier) + item.GetHashCode());
		}
	}
}
