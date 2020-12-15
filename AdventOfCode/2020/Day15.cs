using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day15 : IDay
	{
		private Dictionary<int, (int last, int beforeLast)> numbers;

		public void GetInput()
		{
			var startingNumbers = File.ReadAllLines("2020/input/day15.txt")
				.Where(l => !string.IsNullOrEmpty(l))
				.First()
				.Split(',')
				.Select(n => Convert.ToInt32(n));

			numbers = new Dictionary<int, (int last, int beforeLast)>();

			var i = 1;
			foreach (var number in startingNumbers)
			{
				numbers[number] = (i, 0);
				i++;
			}
		}

		public void Solve()
		{
			var i = numbers.Count + 1;
			var lastSpoken = numbers.Last().Key;
			var partOneAnswer = 0;

			while (i <= 30000000)
			{
				// consider last spoken, get times it was said before
				var (last, beforeLast) = numbers[lastSpoken];

				// first time
				if (beforeLast == 0)
				{
					// speak 0
					if (numbers.TryGetValue(0, out (int last, int beforeLast) zeroTurns))
					{
						zeroTurns.beforeLast = zeroTurns.last;
						zeroTurns.last = i;
						numbers[0] = zeroTurns;
					}
					else
					{
						numbers[0] = (i, 0);
					}

					lastSpoken = 0;
				}
				// has been spoken multiple times
				else
				{
					var valToSpeak = last - beforeLast;
					if (numbers.TryGetValue(valToSpeak, out (int last, int beforeLast) valToSpeakTurns))
					{
						valToSpeakTurns.beforeLast = valToSpeakTurns.last;
						valToSpeakTurns.last = i;
						numbers[valToSpeak] = valToSpeakTurns;
					}
					else
					{
						numbers[valToSpeak] = (i, 0);
					}

					lastSpoken = valToSpeak;
				}

				if (i == 2020)
				{
					partOneAnswer = lastSpoken;
				}

				i++;
			}

			Console.WriteLine($"The 2020th number spoken (part one) is {partOneAnswer}");
			Console.WriteLine($"The 30000000th number spoken (part two) is {lastSpoken}");
		}
	}
}
