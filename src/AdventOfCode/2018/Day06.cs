using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day06 : IDay
	{
		private List<Coord> coordinates;
		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day06.txt").Where(l => !string.IsNullOrEmpty(l));
			coordinates = new List<Coord>();
			foreach (var lineParts in input.Select(i => i.Split(' ')))
			{
				var x = Convert.ToInt32(lineParts[0].TrimEnd(','));
				var y = Convert.ToInt32(lineParts[1]);
				coordinates.Add(new Coord { X = x, Y = y, Count = 0, Bad = false });
			}
		}

		public void Solve()
		{
			var safeZones = 0;

			// use a big grid and assume any that touch the edge would be infinite
			for (int i = 0; i < 500; i++)
			{
				for (int j = 0; j < 500; j++)
				{
					// to reduce GetDistance Calls calculate all now
					var distances = new List<int>();
					foreach (var coord in coordinates)
					{
						distances.Add(GetDistance(coord, i, j));
					}

					if (distances.Sum() < 10000)
					{
						safeZones++;
					}

					var closestDistance = distances.Min();

					if (distances.Count(d => d == closestDistance) > 1)
					{
						continue;
					}
					var closestDistanceIndex = distances.IndexOf(closestDistance);
					var closest = coordinates[closestDistanceIndex];

					if (i == 0 || i == 499 || j == 0 || j == 499)
					{
						// if it touches the edge assume it is infinite
						closest.Bad = true;
					}
					else
					{
						closest.Count = closest.Count + 1;
					}
				}
			}

			Console.WriteLine($"The biggest non-infinite area (part one) is {coordinates.Where(c => !c.Bad).Max(c => c.Count)}");
			Console.WriteLine($"The number of spaces within 10000 of all zones (part two) is {safeZones}");
		}
		
		internal class Coord
		{
			public int X;

			public int Y;

			public int Count;

			public bool Bad;
		}

		private int GetDistance(Coord c, int x, int y)
		{
			return Math.Abs(c.X - x) + Math.Abs(c.Y - y);
		}
	}
}
