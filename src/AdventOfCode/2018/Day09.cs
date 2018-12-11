using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day09 : IDay
	{
		private int maxPlayers;
		private int maxMarble;

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day09.txt").Where(l => !string.IsNullOrEmpty(l)).First().Split(' ');

			maxPlayers = Convert.ToInt32(input[0]);
			maxMarble = Convert.ToInt32(input[6]);
		}

		public void Solve()
		{
			Console.WriteLine($"The winning elf's score (part one) is: {GetMaxScore(maxPlayers, maxMarble)}");
			Console.WriteLine($"The winning elf's score (part two) is: {GetMaxScore(maxPlayers, maxMarble * 100)}");
		}

		private long GetMaxScore(int playerCount, int lastMarble)
		{
			var players = new Dictionary<int, long>();
			for (int i = 1; i < maxPlayers + 1; i++)
			{
				players[i] = 0;
			}

			var currentPlayer = 1;
			var currentMarble = new Marble { Value = 0 };
			currentMarble.Next = currentMarble;
			currentMarble.Previous = currentMarble;

			for (int i = 1; i < lastMarble + 1; i++)
			{
				if (i % 23 == 0)
				{
					players[currentPlayer] += i;

					var ccw7 = currentMarble.Previous.Previous.Previous.Previous.Previous.Previous.Previous; // lol
					players[currentPlayer] += ccw7.Value;

					var ccw8 = ccw7.Previous;
					var ccw6 = ccw7.Next;

					ccw8.Next = ccw6;
					ccw6.Previous = ccw8;

					currentMarble = ccw6;
				}
				else
				{
					var cw1 = currentMarble.Next;
					var cw2 = currentMarble.Next.Next;

					currentMarble = new Marble
					{
						Value = i,
						Previous = cw1,
						Next = cw2
					};
					cw1.Next = currentMarble;
					cw2.Previous = currentMarble;
				}

				currentPlayer++;
				if (currentPlayer > playerCount)
				{
					currentPlayer = 1;
				}
			}

			return players.Max(p => p.Value);
		}

		internal class Marble
		{
			public int Value { get; set; }

			public Marble Next { get; set; }

			public Marble Previous { get; set; }
		}
	}
}
