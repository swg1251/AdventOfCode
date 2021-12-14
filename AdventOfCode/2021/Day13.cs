using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day13 : IDay
	{
		private HashSet<(int x, int y)> dots;
		private List<(char axis, int foldIndex)> instructions;
		
		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 13, true);
			dots = new HashSet<(int x, int y)>();
			instructions = new List<(char axis, int foldIndex)>();

			var i = -1;
			while (++i >= 0)
			{
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					break;
				}

				var lineParts = lines[i].Split(',');
				dots.Add((Convert.ToInt32(lineParts[0]), Convert.ToInt32(lineParts[1])));
			}

			while (++i < lines.Count)
			{
				var instructionParts = lines[i].Split(' ')[2].Split('=');
				instructions.Add((instructionParts[0][0], Convert.ToInt32(instructionParts[1])));
			}
		}

		public void Solve()
		{
			var firstInstruction = true;
			foreach (var (axis, foldIndex) in instructions)
			{
				if (axis == 'x')
				{
					var dotsToFold = dots.Where(d => d.x > foldIndex).ToList();
					foreach (var (x, y) in dotsToFold)
					{
						var newX = x - ((x - foldIndex) * 2);
						dots.Add((newX, y));
						dots.Remove((x, y));
					}
				}
				else if (axis == 'y')
				{
					var dotsToFold = dots.Where(d => d.y > foldIndex).ToList();
					foreach (var (x, y) in dotsToFold)
					{
						var newY = y - ((y - foldIndex) * 2);
						dots.Add((x, newY));
						dots.Remove((x, y));
					}
				}

				if (firstInstruction)
				{
					Console.WriteLine($"After one instruction the number of dots is {dots.Count}");
					firstInstruction = false;
				}
			}

			Console.WriteLine("Part two solution:\n");
			for (int y = dots.Min(d => d.y); y <= dots.Max(d => d.y); y++)
			{
				var row = string.Empty;
				for (int x = dots.Min(d => d.x); x <= dots.Max(d => d.x); x++)
				{
					var c = "  ";
					if (dots.Any(d => d.x == x && d.y == y))
					{
						c = "XX";
					}
					row += c;
				}
				Console.WriteLine(row);
			}
		}
	}
}
