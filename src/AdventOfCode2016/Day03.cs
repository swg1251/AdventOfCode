using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day03 : IDay
    {
		private List<string> triangleLines;
		private List<List<int>> triangles;
		private List<List<int>> triangles2;

		public Day03()
		{
			triangles = new List<List<int>>();
			triangles2 = new List<List<int>>();
		}

        public void GetInput()
        {
            triangleLines = File.ReadAllLines("input/day03.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        }

		public void Solve()
		{
			GetTriangles();
			var validTriangles = triangles.Count(t => IsValid(t));
			Console.WriteLine($"The number of valid triangles (part 1) is: {validTriangles}");

			GetTrianglesPart2();
			var validTriangles2 = triangles2.Count(t => IsValid(t));
			Console.WriteLine($"The number of valid triangles (part 2) is: {validTriangles2}");
		}

		private void GetTriangles()
		{
			foreach (var line in triangleLines)
			{
				var sides = line.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
				var triangle = new List<int> { Convert.ToInt32(sides[0]), Convert.ToInt32(sides[1]), Convert.ToInt32(sides[2]) };
				triangle.Sort();
				triangles.Add(triangle);
			}
		}

		private void GetTrianglesPart2()
		{
			for (int i = 0; i < triangleLines.Count; i += 3)
			{
				var sides = triangleLines[i].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
				var sides2 = triangleLines[i + 1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
				var sides3 = triangleLines[i + 2].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

				var triangle = new List<int> { Convert.ToInt32(sides[0]), Convert.ToInt32(sides2[0]), Convert.ToInt32(sides3[0]) };
				var triangle2 = new List<int> { Convert.ToInt32(sides[1]), Convert.ToInt32(sides2[1]), Convert.ToInt32(sides3[1]) };
				var triangle3 = new List<int> { Convert.ToInt32(sides[2]), Convert.ToInt32(sides2[2]), Convert.ToInt32(sides3[2]) };

				triangle.Sort();
				triangle2.Sort();
				triangle3.Sort();

				triangles2.Add(triangle);
				triangles2.Add(triangle2);
				triangles2.Add(triangle3);
			}
		}

		private bool IsValid(List<int> triangle)
		{
			return triangle[0] + triangle[1] > triangle[2]
				&& triangle[0] + triangle[2] > triangle[1]
				&& triangle[1] + triangle[2] > triangle[0];
		}
    }
}
