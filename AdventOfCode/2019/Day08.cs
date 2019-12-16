using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day08 : IDay
	{
		private List<List<List<int>>> layers;
		private const int width = 25;
		private const int height = 6;

		public void GetInput()
		{
			var input = File.ReadAllLines("2019/input/day08.txt").First();

			layers = new List<List<List<int>>>();
			for (int i = 0; i < input.Length; i += width * height)
			{
				var layer = new List<List<int>>();
				for (int y = 0; y < height; y++)
				{
					var offset = y * width;
					var row = input.Substring(i + offset, width);
					layer.Add(row.Select(c => Convert.ToInt32(c.ToString())).ToList());
				}
				layers.Add(layer);
			}
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		public void PartOne()
		{
			var fewestZeros = layers.Min(l => CountZeros(l));
			var layer = layers.First(l => CountZeros(l) == fewestZeros);

			var ones = layer.Sum(r => r.Count(x => x == 1));
			var twos = layer.Sum(r => r.Count(x => x == 2));

			Console.WriteLine($"The ones * zeros in the least zero-y layer (part one) is: {ones * twos}");
		}

		public void PartTwo()
		{
			var message = new List<List<int>>();
			for (int y = 0; y < height; y++)
			{
				var row = new List<int>();
				for (int x = 0; x < width; x++)
				{
					row.Add(2);
				}
				message.Add(row);
			}

			foreach (var layer in layers)
			{
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						if (message[y][x] != 2)
						{
							continue;
						}
						message[y][x] = layer[y][x];
					}
				}
			}

			Console.WriteLine();

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					var pixel = message[y][x];
					var c = '?';
					if (pixel == 0)
					{
						c = ' ';

					}
					else if (pixel == 1)
					{
						c = 'X';
					}
					Console.Write(c);
				}
				Console.Write("\n");
			}
		}

		private int CountZeros(List<List<int>> layer)
		{
			return layer.Sum(r => r.Count(x => x == 0));
		}
	}
}
