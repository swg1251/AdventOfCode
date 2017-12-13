using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day13 : IDay
    {
		private List<Layer> initialLayers;

		public Day13()
		{
			initialLayers = new List<Layer>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day13.txt").Where(l => !string.IsNullOrEmpty(l));
			foreach (var inputParts in input.Select(l => l.Split(' ')))
			{
				var depth = Convert.ToInt32(inputParts[0].Replace(":", ""));
				var range = Convert.ToInt32(inputParts[1]);
				initialLayers.Add(new Layer(depth, range));
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The severity of the whole trip (part one) is {GetSeverity(initialLayers, false)}");

			var i = 0;
			while (true)
			{
				// update initial positions once before each attempt to simulate the delay
				foreach (var layer in initialLayers)
				{
					layer.UpdatePosition();
				}
				i++;

				if (GetSeverity(initialLayers, true) == 0)
				{
					break;
				}
			}

			Console.WriteLine($"The fewest number of picoseconds to delay for a safe trip (part two) is {i}");
		}

		private int GetSeverity(List<Layer> initialLayers, bool partTwo)
		{
			var layers = new List<Layer>(initialLayers);

			var severity = 0;
			for (int i = 0; i <= layers.Max(l => l.Depth); i++)
			{
				var currentLayer = layers.Find(l => l.Depth == i);
				if (currentLayer != null && currentLayer.Position == 0)
				{
					// if encountered during part two, break immediately
					if (partTwo)
					{
						return Int32.MaxValue;
					}

					severity += (currentLayer.Depth * currentLayer.Range);
				}

				foreach (var layer in layers)
				{
					layer.UpdatePosition();
				}
			}
			return severity;
		}

		internal class Layer
		{
			public int Depth { get; set; }
			public int Range { get; set; }
			public int Position { get; set; }

			private bool isAscending { get; set; }

			public Layer(int depth, int range)
			{
				Depth = depth;
				Range = range;
			}

			public void UpdatePosition()
			{
				if (isAscending)
				{
					if (Position > 0)
					{
						Position--;
						return;
					}
					else
					{
						Position++;
						isAscending = false;
						return;
					}
				}
				else
				{
					if (Position < Range - 1)
					{
						Position++;
						return;
					}
					else
					{
						Position--;
						isAscending = true;
					}
				}
			}
		}
    }
}
