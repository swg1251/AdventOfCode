using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day05 : IDay
	{
		private List<(int x1, int y1, int x2, int y2)> linePoints;
		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 5);
			linePoints = new List<(int x1, int y1, int x2, int y2)>();
			foreach (var line in lines)
			{
				var lineStrPoints = line.Split(" -> ");
				var point1 = lineStrPoints[0].Split(',');
				var point2 = lineStrPoints[1].Split(',');
				linePoints.Add((Convert.ToInt32(point1[0]), Convert.ToInt32(point2[0]), Convert.ToInt32(point1[1]), Convert.ToInt32(point2[1])));
			}
		}

		public void Solve()
		{
			PlotLines(new Dictionary<(int x, int y), int>(), false);
			PlotLines(new Dictionary<(int x, int y), int>(), true);
		}

		private void PlotLines(Dictionary<(int x, int y), int> points, bool includeDiagonal)
		{
			foreach (var (x1, x2, y1, y2) in linePoints)
			{
				if (x1 == x2)
				{
					if (y1 < y2)
					{
						for (int i = y1; i <= y2; i++)
						{
							points[(x1, i)] = points.GetValueOrDefault((x1, i)) + 1;
						}
					}
					else if (y2 < y1)
					{
						for (int i = y2; i <= y1; i++)
						{
							points[(x1, i)] = points.GetValueOrDefault((x1, i)) + 1;
						}
					}
				}
				else if (y1 == y2)
				{
					if (x1 < x2)
					{
						for (int i = x1; i <= x2; i++)
						{
							points[(i, y1)] = points.GetValueOrDefault((i, y1)) + 1;
						}
					}
					else if (x2 < x1)
					{
						for (int i = x2; i <= x1; i++)
						{
							points[(i, y1)] = points.GetValueOrDefault((i, y1)) + 1;
						}
					}
				}
				else if (includeDiagonal)
				{
					var x = x1;
					var y = y1;
					if (x1 < x2 && y1 < y2)
					{
						while (x <= x2 && y <= y2)
						{
							points[(x, y)] = points.GetValueOrDefault((x, y)) + 1;
							x++;
							y++;
						}
					}
					else if (x1 < x2 && y2 < y1)
					{
						while (x <= x2 && y >= y2)
						{
							points[(x, y)] = points.GetValueOrDefault((x, y)) + 1;
							x++;
							y--;
						}
					}
					else if (x2 < x1 && y1 < y2)
					{
						while (x >= x2 && y <= y2)
						{
							points[(x, y)] = points.GetValueOrDefault((x, y)) + 1;
							x--;
							y++;
						}
					}
					else if (x2 < x1 && y2 < y1)
					{
						while (x >= x2 && y >= y2)
						{
							points[(x, y)] = points.GetValueOrDefault((x, y)) + 1;
							x--;
							y--;
						}
					}
				}
			}

			Console.WriteLine($"The number of points where 2 or more lines cross (part {(includeDiagonal ? "two" : "one")}) is {points.Count(p => p.Value > 1)}");
		}
	}
}
