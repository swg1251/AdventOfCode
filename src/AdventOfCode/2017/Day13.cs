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

			initialLayers.Clear();
			GetInput();

			Console.WriteLine("Beginning part two - warning: slow...");

			var i = 0;
			while (true)
			{
				var layersCopy = new List<Layer>();
				
				// update initial positions once before each attempt to simulate the delay
				foreach (var layer in initialLayers)
				{
					layer.UpdatePosition();
					layersCopy.Add(new Layer(layer.Depth, layer.Range, layer.Position, layer.IsAscending));
				}
				i++;
				if (i % 1000000 == 0)
				{
					Console.WriteLine($"Current delay: {i} picoseconds...");
				}

				if (GetSeverity(layersCopy, true) == 0)
				{
					break;
				}
			}

			Console.WriteLine($"The fewest number of picoseconds to delay for a safe trip (part two) is {i}");
		}

		private int GetSeverity(List<Layer> layers, bool partTwo)
		{
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

			public bool IsAscending { get; set; }

			public Layer(int depth, int range, int position = 0, bool isAscending = false)
			{
				Depth = depth;
				Range = range;
				Position = position;
				IsAscending = isAscending;
			}

			public void UpdatePosition()
			{
				if (IsAscending)
				{
					if (Position > 0)
					{
						Position--;
						return;
					}
					else
					{
						Position++;
						IsAscending = false;
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
						IsAscending = true;
					}
				}
			}
		}
    }
}
