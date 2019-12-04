using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day03 : IDay
	{
		private List<string> wire1Directions;
		private List<string> wire2Directions;

		public void GetInput()
		{
			var lines = File.ReadAllLines("2019/input/day03.txt").Where(l => !string.IsNullOrEmpty(l));
			wire1Directions = lines.First().Split(',').ToList();
			wire2Directions = lines.Last().Split(',').ToList();
		}

		public void Solve()
		{
			var intersections = GetIntersections();
			var minDistance = intersections.Min(i => ManhattanDistance(i.Key));
			Console.WriteLine($"The distance to closest intersection is (part one): {minDistance}");

			var minimalDelay = intersections.Min(i => i.Value.steps);
			Console.WriteLine($"The minimal signal delay (part two) is: {minimalDelay}");
		}

		private IEnumerable<KeyValuePair<(int x, int y), (int count, int steps)>> GetIntersections()
		{
			var locations = new Dictionary<(int x, int y), (int count, int steps)>();
			Map(locations, wire1Directions);
			Map(locations, wire2Directions);

			return locations.Where(l => l.Value.count > 1);
		}

		private void Map(Dictionary<(int x, int y), (int count, int steps)> locations, List<string> directions)
		{
			var x = 0;
			var y = 0;
			var steps = 0;
			var currentWireLocations = new HashSet<(int x, int y)>();

			for (int i = 0; i < directions.Count; i++)
			{
				var instruction = directions[i];
				var direction = instruction[0];
				var length = Convert.ToInt32(instruction.Substring(1));

				for (int j = 0; j < length; j++)
				{
					switch (direction)
					{
						case 'U':
							y++;
							break;
						case 'D':
							y--;
							break;
						case 'L':
							x--;
							break;
						case 'R':
							x++;
							break;
					}
					steps++;

					if (!locations.ContainsKey((x, y)))
					{
						locations[(x, y)] = (0, 0);
					}

					if (currentWireLocations.Add((x, y)))
					{
						var location = locations[(x, y)];
						location.count++;
						location.steps += steps;
						locations[(x, y)] = location;
					}
				}
			}
		}

		private int ManhattanDistance((int x, int y) location)
		{
			return Math.Abs(location.x) + Math.Abs(location.y);
		}
	}
}
