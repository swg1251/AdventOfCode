using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day17 : IDay
	{
		private List<string> input;
		private int inputMinY;
		private int inputMaxY;

		public void GetInput()
		{
			input = File.ReadAllLines("2018/input/day17.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{

			var grid = GetClay(input);
			var answer = Flow(grid);

			Console.WriteLine($"The number of tiles the water can reach (part one) is: {answer.running + answer.standing}");
			Console.WriteLine($"The number of tiles of standing water (part two) is: {answer.standing}");
		}

		public Dictionary<int, Dictionary<int, Material>> GetClay(IEnumerable<string> input)
		{
			var clayCoords = new List<(int minX, int maxX, int minY, int maxY)>();
			foreach (var line in input)
			{
				var lineParts = line.Split(", ");
				var leftParts = lineParts[0].Split('=');
				var rightParts = lineParts[1].Split('=');

				int lineMinX, lineMaxX, lineMinY, lineMaxY = 0;
				if (leftParts[0] == "x")
				{
					lineMinX = Convert.ToInt32(leftParts[1]);
					lineMaxX = Convert.ToInt32(leftParts[1]);

					lineMinY = Convert.ToInt32(rightParts[1].Split("..")[0]);
					lineMaxY = Convert.ToInt32(rightParts[1].Split("..")[1]);
				}
				else
				{
					lineMinY = Convert.ToInt32(leftParts[1]);
					lineMaxY = Convert.ToInt32(leftParts[1]);

					lineMinX = Convert.ToInt32(rightParts[1].Split("..")[0]);
					lineMaxX = Convert.ToInt32(rightParts[1].Split("..")[1]);
				}
				clayCoords.Add((lineMinX, lineMaxX, lineMinY, lineMaxY));
			}

			var minX = clayCoords.Min(c => c.minX);
			var maxX = clayCoords.Max(c => c.maxX);
			inputMinY = clayCoords.Min(c => c.minY);
			inputMaxY = clayCoords.Max(c => c.maxY);

			var grid = new Dictionary<int, Dictionary<int, Material>>();
			for (int y = 0; y <= inputMaxY + 2; y++)
			{
				var row = new Dictionary<int, Material>();
				for (int x = minX - 2; x <= maxX + 2; x++)
				{
					var clay = clayCoords.Any(c =>
						x >= c.minX &&
						x <= c.maxX &&
						y >= c.minY &&
						y <= c.maxY
					);

					row[x] = clay ? Material.Clay : Material.Sand;
				}
				grid[y] = row;
			}

			grid[0][500] = Material.RunningWater;
			return grid;
		}

		public (int running, int standing) Flow(Dictionary<int, Dictionary<int, Material>> grid)
		{
			var minY = grid.Keys.Min();
			var maxY = grid.Keys.Max();
			var minX = grid.Values.Min(r => r.Keys.Min());
			var maxX = grid.Values.Max(r => r.Keys.Max());

			var startWater = (0, 0);
			var endWater = (0, 0);

			while (true)
			{
				startWater = endWater;

				for (int y = 0; y <= maxY; y++)
				{
					for (int x = minX; x <= maxX; x++)
					{
						if (grid[y][x] == Material.RunningWater)
						{
							// expand running water downwards
							var y2 = y + 1;
							Dictionary<int, Material> down;
							while (grid.TryGetValue(y2, out down) && down[x] == Material.Sand)
							{
								grid[y2][x] = Material.RunningWater;
								y2++;
							}

							// if below is settled
							Dictionary<int, Material> below;
							if (grid.TryGetValue(y + 1, out below) &&
								(below[x] == Material.Clay || below[x] == Material.StandingWater))
							{
								var settled = true;

								Material downLeft;
								if (grid[y].TryGetValue(x - 1, out downLeft))
								{
									// left is sand - expand left
									if (downLeft == Material.Sand || downLeft == Material.RunningWater)
									{
										var x2 = x - 1;
										Material downLeftTwo;
										while (grid[y].TryGetValue(x2, out downLeftTwo) && (downLeftTwo == Material.Sand || downLeftTwo == Material.RunningWater))
										{
											// if left-down is already running, move on
											if (grid[y + 1][x2 + 1] == Material.RunningWater)
											{
												settled = false;
												break;
											}	
											grid[y][x2] = Material.RunningWater;

											// sand below - not settled
											if (grid.ContainsKey(y + 1) && (grid[y + 1][x2] == Material.Sand))
											{
												settled = false;
												break;
											}
											x2--;
										}
									}
								}
								Material downRight;
								if (grid[y].TryGetValue(x + 1, out downRight))
								{
									// right is sand - expand right
									if (downRight == Material.Sand || downRight == Material.RunningWater)
									{
										var x2 = x + 1;
										Material downRightTwo;
										while (grid[y].TryGetValue(x2, out downRightTwo) && (downRightTwo == Material.Sand || downRightTwo == Material.RunningWater))
										{
											// if right-down is already running, move on
											if (grid[y + 1][x2 - 1] == Material.RunningWater)
											{
												settled = false;
												break;

											}
											grid[y][x2] = Material.RunningWater;

											// sand below - not settled
											if (grid.ContainsKey(y + 1) && (grid[y + 1][x2] == Material.Sand))
											{
												settled = false;
												break;
											}
											x2++;
										}
									}
								}
								// we are settled if expanding left AND right hit a wall
								if (settled)
								{
									grid[y][x] = Material.StandingWater;
								}
							}
						}
					}
				}
				endWater = CountWater(grid);

				// if nothing changed, we're done
				if (endWater.Equals(startWater))
				{
					return endWater;
				}
			}
		}

		public (int running, int standing) CountWater(Dictionary<int, Dictionary<int, Material>> grid)
		{
			var runningWater = 0;
			var standingWater = 0;

			for (int y = inputMinY; y <= inputMaxY; y++)
			{
				foreach (var x in grid[y].Values)
				{
					if (x == Material.RunningWater)
					{
						runningWater++;
					}
					else if (x == Material.StandingWater)
					{
						standingWater++;
					}
				}
			}
			return (runningWater, standingWater);
		}

		// used for debugging
		public void PrintGrid(Dictionary<int, Dictionary<int, Material>> grid)
		{
			var minY = grid.Keys.Min();
			var maxY = grid.Keys.Max();
			var minX = grid.Values.Min(r => r.Keys.Min());
			var maxX = grid.Values.Max(r => r.Keys.Max());

			for (int y = 0; y <= maxY; y++)
			{
				var row = "";
				for (int x = minX; x <= maxX; x++)
				{
					var symbol = "";
					switch (grid[y][x])
					{
						case Material.Sand:
							symbol = ".";
							break;
						case Material.RunningWater:
							symbol = "|";
							break;
						case Material.StandingWater:
							symbol = "~";
							break;
						case Material.Clay:
							symbol = "#";
							break;
					}
					row += symbol;
				}
				Console.WriteLine(row);
			}
			Console.ReadLine();
		}

		public enum Material
		{
			Sand,
			Clay,
			RunningWater,
			StandingWater
		}
	}
}
