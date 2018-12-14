using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day11 : IDay
	{
		private int serial;
		public Dictionary<(int x, int y, int size), int> Boxes;

		public void GetInput()
		{
			serial = Convert.ToInt32(File.ReadAllLines("2018/input/day11.txt").Where(l => !string.IsNullOrEmpty(l)).First());

			// save previous boxes in a dictionary to save time
			Boxes = new Dictionary<(int x, int y, int size), int>();
		}

		public void Solve()
		{
			var partOne = PartOne(serial);
			Console.WriteLine($"The largest 3x3 square power (part one) is at: {partOne.x},{partOne.y}");

			var partTwo = PartTwo(serial);
			Console.WriteLine($"The identifier of the best square (part two) is: {partTwo.x},{partTwo.y},{partTwo.size}");
		}

		public int GetPowerLevel(int x, int y, int serialNumber)
		{
			var power = (x + 10) * y;
			power += serialNumber;
			power *= (x + 10);
			power = (power % 1000) / 100;
			power -= 5;
			return power;
		}

		public (int x, int y, int powerLevel) PartOne(int serialNumber)
		{
			var grid = GetGrid(serialNumber);
			var ans = GetMaxBox(grid, 3);
			return (ans.x, ans.y, ans.powerLevel);
		}

		public (int x, int y, int powerLevel, int size) PartTwo(int serialNumber)
		{
			var grid = GetGrid(serialNumber);
			var highestPower = 0;
			var finalAnswer = (x: 0, y: 0, powerLevel: 0, size: 0);
			for (int i = 1; i < 101; i++)
			{
				var sol = GetMaxBox(grid, i);
				if (sol.powerLevel > highestPower)
				{
					highestPower = sol.powerLevel;
					finalAnswer = sol;
				}
			}
			return finalAnswer;
		}

		private List<List<int>> GetGrid(int serialNumber)
		{
			var grid = new List<List<int>>();

			for (int y = 1; y < 301; y++)
			{
				var row = new List<int>();
				for (int x = 1; x < 301; x++)
				{
					var power = GetPowerLevel(x, y, serialNumber);
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
					if (Boxes.TryGetValue((x, y, boxSize - 1), out total))
					{
						for (int j = 0; j < boxSize - 1; j++)
						{
							total += grid[y + j][x + (boxSize - 1)];
						}
						for (int i = 0; i < boxSize - 1; i++)
						{
							total += grid[y + boxSize - 1][x + i];
						}
						total += grid[y + boxSize - 1][x + boxSize - 1];
					}
					else
					{
						for (int j = 0; j < boxSize; j++)
						{
							for (int i = 0; i < boxSize; i++)
							{
								total += grid[y + j][x + i];
							}
						}
					}

					Boxes[(x, y, boxSize)] = total;

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
