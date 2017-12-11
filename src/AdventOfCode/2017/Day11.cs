using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day11 : IDay
    {
		private Hexagon hexagon;

		public void GetInput()
		{
			var i = 0;
			hexagon = new Hexagon { Id = i };

			var input = File.ReadAllLines("2017/input/day11.txt").Where(l => !string.IsNullOrEmpty(l)).First();
			
			foreach (var direction in input.Split(','))
			{
				i++;

				if (direction == "n")
				{
					if (hexagon.North == null)
					{
						hexagon.North = new Hexagon { Id = i, South = hexagon };
					}
					hexagon = hexagon.North;
				}
				else if (direction == "ne")
				{
					if (hexagon.NorthEast == null)
					{
						hexagon.NorthEast = new Hexagon { Id = i, SouthWest = hexagon };
					}
					hexagon = hexagon.NorthEast;
				}
				else if (direction == "se")
				{
					if (hexagon.SouthEast == null)
					{
						hexagon.SouthEast = new Hexagon { Id = i, NorthWest = hexagon };
					}
					hexagon = hexagon.SouthEast;
				}
				else if (direction == "s")
				{
					if (hexagon.South == null)
					{
						hexagon.South = new Hexagon { Id = i, North = hexagon };
					}
					hexagon = hexagon.South;
				}
				else if (direction == "sw")
				{
					if (hexagon.SouthWest == null)
					{
						hexagon.SouthWest = new Hexagon { Id = i, NorthEast = hexagon };
					}
					hexagon = hexagon.SouthWest;
				}
				else if (direction == "nw")
				{
					if (hexagon.NorthWest == null)
					{
						hexagon.NorthWest = new Hexagon { Id = i, SouthEast = hexagon };
					}
					hexagon = hexagon.NorthWest;
				}
			}
		}

		public void Solve()
		{
			var queue = new Queue<Hexagon>();
			queue.Enqueue(hexagon);

			while (queue.Any())
			{
				var currentHexagon = queue.Dequeue();

				if (currentHexagon.Id == 0)
				{
					Console.WriteLine("reached the goal");
					return;
				}

				foreach(var neighbor in currentHexagon.GetNeighbors())
				{

				}
			}
		}

		internal class Hexagon
		{
			public int Id { get; set; }
			public Hexagon North { get; set; }
			public Hexagon NorthEast { get; set; }
			public Hexagon SouthEast { get; set; }
			public Hexagon South { get; set; }
			public Hexagon SouthWest { get; set; }
			public Hexagon NorthWest { get; set; }

			public List<int> Explored { get; set; }

			public IEnumerable<Hexagon> GetNeighbors()
			{

			}

			private Hexagon TryGetNeighborWithPath()
			{

			}
		}
    }
}
