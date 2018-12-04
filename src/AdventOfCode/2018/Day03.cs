using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
    public class Day03 : IDay
    {
		private IEnumerable<string> claims;
		private List<(int id, int x, int y, int distX, int distY)> splitClaims;
		private int[,] fabric;

		public void GetInput()
		{
			fabric = new int[1000, 1000];
			splitClaims = new List<(int id, int x, int y, int distX, int distY)>();
			claims = File.ReadAllLines("2018/input/day03.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			foreach (var claim in claims)
			{
				splitClaims.Add(SplitClaim(claim));
			}

			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			foreach (var c in splitClaims)
			{
				for (int i = c.x; i < c.x + c.distX; i++)
				{
					for (int j = c.y; j < c.y + c.distY; j++)
					{
						fabric[i, j]++;
					}
				}
			}

			var twosCount = 0;
			for (int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 1000; j++)
				{
					if (fabric[i, j] > 1)
					{
						twosCount++;
					}
				}
			}

			Console.WriteLine($"The number of spaces with two or more claims (part one) is: {twosCount}");
		}

		private void PartTwo()
		{
			foreach (var c in splitClaims)
			{
				var overlap = false;

				for (int i = c.x; i < c.x + c.distX; i++)
				{
					for (int j = c.y; j < c.y + c.distY; j++)
					{
						if (fabric[i, j] > 1)
						{
							overlap = true;
							break;
						}
					}

					if (overlap)
					{
						break;
					}
				}

				if (!overlap)
				{
					Console.WriteLine($"The claim with no overlap (part two) is: {c.id}");
					return;
				}
			}
		}

		private (int id, int x, int y, int distX, int distY) SplitClaim(string claim)
		{
			var instructionParts = claim.Split(' ');
			var coords = instructionParts[2];
			var coordParts = coords.Remove(coords.Length - 1).Split(',');
			var distParts = instructionParts[3].Split('x');

			var id = Convert.ToInt32(instructionParts[0].Substring(1));
			var x = Convert.ToInt32(coordParts[0]);
			var y = Convert.ToInt32(coordParts[1]);
			var distX = Convert.ToInt32(distParts[0]);
			var distY = Convert.ToInt32(distParts[1]);

			return (id, x, y, distX, distY);
		}
    }
}
