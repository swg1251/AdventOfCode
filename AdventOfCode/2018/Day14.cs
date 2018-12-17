using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day14 : IDay
	{
		private int input;

		public void GetInput()
		{
			input = Convert.ToInt32(File.ReadAllLines("2018/input/day14.txt").Where(l => !string.IsNullOrEmpty(l)).First());
		}

		public void Solve()
		{
			var answer = GetScore(input);
			Console.WriteLine($"The scores of the next ten recipes after {input} (part one) is: {answer.partOne}");
			Console.WriteLine($"{input} first appears after (part two) {answer.partTwo} recipes");
		}

		public (string partOne, int partTwo) GetScore(int recipeCount)
		{
			var scores = new List<int> { 3, 7 };
			var elf1 = 0;
			var elf2 = 1;
			var partTwo = -1;

			while (true)
			{
				for (int i = 0; i < 50000000; i++)
				{
					var score = scores[elf1] + scores[elf2];
					foreach (var c in score.ToString())
					{
						scores.Add(Convert.ToInt32(c.ToString()));
					}

					elf1 = (1 + elf1 + scores[elf1]) % scores.Count;
					elf2 = (1 + elf2 + scores[elf2]) % scores.Count;
				}

				// check if we have part two answer yet
				// only do this every 5 million, it's very expensive
				partTwo = string.Join("", scores).IndexOf(recipeCount.ToString());
				if (partTwo != -1)
				{
					break;
				}
			}

			var finalScore = "";
			for (int i = recipeCount; i < recipeCount + 10; i++)
			{
				finalScore += scores[i].ToString();
			}

			var scoreString = string.Join("", scores);
			return (finalScore, partTwo);
		}
	}
}
