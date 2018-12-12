using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day05 : IDay
	{
		private string originalString;

		public void GetInput()
		{
			originalString = File.ReadAllLines("2018/input/day05.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			var partOne = GetReactedLength(originalString);
			Console.WriteLine($"The reacted polymer length is (part one): {partOne}");

			var shortestLength = int.MaxValue;
			foreach (var c in "abcdefghijklmnopqrstuvqwxyz")
			{
				var s = originalString.Replace(c.ToString(), "").Replace(char.ToUpperInvariant(c).ToString(), "");
				var length = GetReactedLength(s);
				if (length < shortestLength)
				{
					shortestLength = length;
				}
			}
			Console.WriteLine($"The shortest reacted polymer length is (part two): {shortestLength}");
		}

		// Shoutouts to tapdudy for stack idea
		private int GetReactedLength(string polymer)
		{
			var stack = new Stack<char>();
			foreach (var polyChar in polymer)
			{
				// Difference between lowercase/uppercase ASCII character codes is 32
				if (stack.Any() && Math.Abs(polyChar - stack.Peek()) == 32)
				{
					stack.Pop();
				}
				else
				{
					stack.Push(polyChar);
				}
			}
			return stack.Count;
		}
	}
}
