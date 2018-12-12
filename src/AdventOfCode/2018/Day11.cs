using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day11 : IDay
	{
		private int serial;

		public void GetInput()
		{
			serial = Convert.ToInt32(File.ReadAllLines("2018/input/day11.txt").Where(l => !string.IsNullOrEmpty(l)).First());
		}

		public void Solve()
		{
			var grid = GetGrid(serial);
			var partOne = GetMaxBox(grid, 3);
			Console.WriteLine($"The largest 3x3 square power (part one) is at: {partOne.x},{partOne.y}");

			var highestPower = 0;
			var partTwo = (x: 0, y: 0, powerLevel: 0, size: 0);
			for (int i = 1; i < 101; i++)
			{
				var sol = GetMaxBox(grid, i);
				if (sol.powerLevel > highestPower)
				{
					highestPower = sol.powerLevel;
					partTwo = sol;
				}
			}
			Console.WriteLine($"The identifier of the best square (part two) is: {partTwo.x},{partTwo.y},{partTwo.size}");
		}

		private List<List<int>> GetGrid(int serialNumber)
		{
			var grid = new List<List<int>>();

			for (int y = 1; y < 301; y++)
			{
				var row = new List<int>();
				for (int x = 1; x < 301; x++)
				{
					var power = (x + 10) * y;
					power += serialNumber;
					power *= (x + 10);
					power = (power % 1000) / 100;
					power -= 5;

					row.Add(power);
				}
				grid.Add(row);
			}

			return grid;
		}

		private (int x, int y, int powerLevel, int size) GetMaxBox(List<List<int>> grid, int boxSize)
		{
			var maxX = 0;
			var maxY = 0;
			var maxTotal = 0;

			for (int y = 0; y < 301 - boxSize; y++)
			{
				for (int x = 0; x < 301 - boxSize; x++)
				{
					var total = 0;
					for (int j = 0; j < boxSize; j++)
					{
						for (int i = 0; i < boxSize; i++)
						{
							total += grid[y + j][x + i];
						}
					}

					if (total > maxTotal)
					{
						maxTotal = total;
						maxX = x + 1;
						maxY = y + 1;
					}
				}
			}
			return (maxX, maxY, maxTotal, boxSize);
		}
	}
}
