using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day10 : IDay
	{
		private List<Point> points;

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day10.txt").Where(l => !string.IsNullOrEmpty(l));

			points = new List<Point>();
			foreach (var lineParts in input.Select(l => l.Split('>')))
			{
				var positionPart = lineParts[0];
				var velocityPart = lineParts[1];

				var positionInner = positionPart.Split('<')[1].Split(", ");
				var posX = Convert.ToInt32(positionInner[0]);
				var posY = Convert.ToInt32(positionInner[1]);

				var velocityInner = velocityPart.Split('<')[1].Split(", ");
				var velX = Convert.ToInt32(velocityInner[0]);
				var velY = Convert.ToInt32(velocityInner[1]);

				points.Add(new Point
				{
					X = posX,
					Y = posY,
					Dx = velX,
					Dy = velY
				});
			}

		}

		public void Solve()
		{
			// toyed around with different sizes until only a few printed, solution ended up being 10 points tall
			var letterHeight = 10;
			var time = 0;

			while (GetYRange() >= letterHeight)
			{
				foreach (var point in points)
				{
					point.X += point.Dx;
					point.Y += point.Dy;
				}
				time++;
			}
			Console.WriteLine($"Part one answer:\n");
			Print();
			Console.WriteLine($"That message took {time} seconds to appear (part two)");
		}

		private void Print()
		{
			for (int y = points.Min(p => p.Y); y <= points.Max(p => p.Y); y++)
			{
				var line = "";
				for (int x = points.Min(p => p.X); x <= points.Max(p => p.X); x++)
				{
					var point = points.FirstOrDefault(p => p.X == x && p.Y == y);
					line += (point == null) ? " " : "X";
				}
				Console.WriteLine(line);
			}
			Console.WriteLine();
		}

		private int GetYRange()
		{
			return points.Max(p => p.Y) - points.Min(p => p.Y);
		}

		internal class Point
		{
			public int X { get; set; }

			public int Y { get; set; }

			public int Dx { get; set; }

			public int Dy { get; set; }
		}
	}
}
