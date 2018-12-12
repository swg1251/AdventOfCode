using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2017
{
    public class Day09 : IDay
    {
		private string input;
		private int score;
		private int garbageCharacterCount;

		public void GetInput()
		{
			input = File.ReadAllLines("2017/input/day09.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			RemoveGarbage();
			GetScore();
			Console.WriteLine($"The score of the input (part one) is {score}");
			Console.WriteLine($"The number of non-cancelled charcters contained in garbage is (part two) {garbageCharacterCount}");
		}

		private void GetScore()
		{
			var level = 0;

			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '{')
				{
					level++;
				}
				else if (input[i] == '}')
				{
					score += level;
					level--;
				}
			}
		}

		private void RemoveGarbage()
		{
			var inputGarbageless = input;

			for (int i = 0; i < input.Length; i++)
			{
				// start of some garbage
				if (input[i] == '<')
				{
					var garbageStartIndex = i;
					i++;

					// garbage ends at the first non-cancelled '>'
					while (input[i] != '>')
					{
						// cancel/skip the next character after '!'
						if (input[i] == '!')
						{
							i++;
						}

						i++;
					}

					var garbageEndIndex = i;
					var garbage = input.Substring(garbageStartIndex, garbageEndIndex - garbageStartIndex + 1);

					// count the non-cancelled garbage characters for part two
					for (int j = 1; j < garbage.Length - 1; j++)
					{
						if (garbage[j] != '!')
						{
							garbageCharacterCount++;
						}
						else
						{
							j++;
						}
					}

					// remove the garbage, but only the first instance (this garbage could be contained in another garbage)
					var regex = new Regex(garbage);
					inputGarbageless = regex.Replace(inputGarbageless, "", 1);
				}
			}

			input = inputGarbageless;
		}
    }
}
