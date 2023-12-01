using System;
using System.Collections.Generic;
using System.Linq;
using PQ = Collections.Generic;

namespace AdventOfCode.Year2021
{
	public class Day15 : IDay
	{
		private List<List<int>> risksPartOne;
		private List<List<int>> risksPartTwo;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 15);

			risksPartOne = new List<List<int>>();
			foreach (var line in lines)
			{
				risksPartOne.Add(line.Select(x => Convert.ToInt32(x.ToString())).ToList());
			}

			risksPartTwo = new List<List<int>>();
			for (int i = 0; i < 5; i++)
			{
				foreach (var line in lines)
				{
					var row = new List<int>();
					for (int j = 0; j < 5; j++)
					{
						row.AddRange(line.Select(x => Convert.ToInt32(x.ToString()) + i + j));
					}
					for (int x = 0; x < row.Count; x++)
					{
						if (row[x] > 9)
						{
							row[x] = (row[x] % 10) + 1;
						}
					}
					risksPartTwo.Add(row);
				}
			}
		}

		public void Solve()
		{
			Dijkstra(risksPartOne);
			Dijkstra(risksPartTwo, true);
		}

		private void Dijkstra(List<List<int>> risks, bool partTwo = false)
		{
			var height = risks.Count;
			var width = risks.First().Count;
			var visited = new HashSet<(int y, int x)> { (0, 0) };

			var priorityQueue = new PQ.BinaryHeap<int, (int y, int x)>();
			priorityQueue.Enqueue(0, (0, 0));

			while (!priorityQueue.IsEmpty)
			{
				var current = priorityQueue.Dequeue();
				var totalRisk = current.Key;
				var (y, x) = current.Value;

				if (y == height - 1 && x == width - 1)
				{
					Console.WriteLine($"The lowest possible risk (part {(partTwo ? "two" : "one")}) is {totalRisk}");
					break;
				}
				
				foreach (var (y2, x2) in GetNeighbors(y, x, height, width))
				{
					if (visited.Add((y2, x2)))
					{
						var risk = risks[y2][x2];
						priorityQueue.Enqueue(totalRisk + risk, (y2, x2));
					}
				}
			}
		}

		private IEnumerable<(int y, int x)> GetNeighbors(int y, int x, int height, int width)
		{
			return new List<(int y, int x)> { (y, x - 1), (y, x + 1), (y - 1, x), (y + 1, x) }
				.Where(p => p.y >= 0 && p.y < height && p.x >= 0 && p.x < width);
		}
	}
}
