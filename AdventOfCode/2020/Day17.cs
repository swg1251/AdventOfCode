using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day17 : IDay
	{
		private Dictionary<(int z, int y, int x), bool> cubes;
		private Dictionary<(int z, int y, int x, int w), bool> cubesPartTwo;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 17);
			cubes = new Dictionary<(int z, int y, int x), bool>();
			cubesPartTwo = new Dictionary<(int z, int y, int x, int w), bool>();

			for (int y = 0; y < lines.Count; y++)
			{
				for (int x = 0; x < lines[y].Length; x++)
				{
					var active = false;
					if (lines[y][x] == '#')
					{
						active = true;
					}
					cubes.Add((0, y, x), active);
					cubesPartTwo.Add((0, y, x, 0), active);
				}
			}
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		public void PartOne()
		{
			for (int cycles = 0; cycles < 6; cycles++)
			{
				// Print(cubes, cycles);

				var newCubes = new Dictionary<(int z, int y, int x), bool>();

				var minZ = cubes.Where(c => c.Value == true).Min(k => k.Key.z) - 1;
				var maxZ = cubes.Where(c => c.Value == true).Max(k => k.Key.z) + 1;
				var minY = cubes.Where(c => c.Value == true).Min(k => k.Key.y) - 1;
				var maxY = cubes.Where(c => c.Value == true).Max(k => k.Key.y) + 1;
				var minX = cubes.Where(c => c.Value == true).Min(k => k.Key.x) - 1;
				var maxX = cubes.Where(c => c.Value == true).Max(k => k.Key.x) + 1;

				for (int z = minZ; z <= maxZ; z++)
				for (int y = minY; y <= maxY; y++)
				for (int x = minZ; x <= maxX; x++)
				{
					var activeNeighbors = 0;
					cubes.TryGetValue((z, y, x), out bool active);

					for (int k = -1; k <= 1; k++)
					for (int j = -1; j <= 1; j++)
					for (int i = -1; i <= 1; i++)
					{
						if (k == 0 && j == 0 && i == 0)
						{
							continue;
						}

						if (cubes.TryGetValue((z + k, y + j, x + i), out bool neighborActive) && neighborActive)
						{
							activeNeighbors++;
						}
					}

					if (active)
					{
							newCubes[(z, y, x)] = (activeNeighbors == 2 || activeNeighbors == 3);
					}
					else
					{
							newCubes[(z, y, x)] = activeNeighbors == 3;
					}
				}

				cubes = new Dictionary<(int z, int y, int x), bool>(newCubes);
			}

			Console.WriteLine($"The number of active cubes after six cycles (part one) is {cubes.Count(c => c.Value == true)}");
		}

		public void PartTwo()
		{
			for (int cycles = 0; cycles < 6; cycles++)
			{
				var newCubes = new Dictionary<(int z, int y, int x, int w), bool>();

				var minZ = cubesPartTwo.Where(c => c.Value == true).Min(k => k.Key.z) - 1;
				var maxZ = cubesPartTwo.Where(c => c.Value == true).Max(k => k.Key.z) + 1;
				var minY = cubesPartTwo.Where(c => c.Value == true).Min(k => k.Key.y) - 1;
				var maxY = cubesPartTwo.Where(c => c.Value == true).Max(k => k.Key.y) + 1;
				var minX = cubesPartTwo.Where(c => c.Value == true).Min(k => k.Key.x) - 1;
				var maxX = cubesPartTwo.Where(c => c.Value == true).Max(k => k.Key.x) + 1;
				var minW = cubesPartTwo.Where(c => c.Value == true).Min(k => k.Key.w) - 1;
				var maxW = cubesPartTwo.Where(c => c.Value == true).Max(k => k.Key.w) + 1;

				for (int z = minZ; z <= maxZ; z++)
				for (int y = minY; y <= maxY; y++)
				for (int x = minZ; x <= maxX; x++)
				for (int w = minW; w <= maxW; w++)
				{
					var activeNeighbors = 0;
					cubesPartTwo.TryGetValue((z, y, x, w), out bool active);

					for (int k = -1; k <= 1; k++)
					for (int j = -1; j <= 1; j++)
					for (int i = -1; i <= 1; i++)
					for (int p = -1; p <= 1; p++)
					{
						if (k == 0 && j == 0 && i == 0 && p == 0)
						{
							continue;
						}

						if (cubesPartTwo.TryGetValue((z + k, y + j, x + i, w + p), out bool neighborActive) && neighborActive)
						{
							activeNeighbors++;
						}
					}

					if (active)
					{
						newCubes[(z, y, x, w)] = (activeNeighbors == 2 || activeNeighbors == 3);
					}
					else
					{
						newCubes[(z, y, x, w)] = activeNeighbors == 3;
					}
				}

				cubesPartTwo = new Dictionary<(int z, int y, int x, int w), bool>(newCubes);
			}

			Console.WriteLine($"The number of active cubes after six cycles (part two) is {cubesPartTwo.Count(c => c.Value == true)}");
		}

		private void Print(Dictionary<(int z, int y, int x), bool> state, int cycles)
		{
			Console.WriteLine($"After {cycles} cycles:\n");

			var minZ = state.Where(c => c.Value == true).Min(k => k.Key.z);
			var maxZ = state.Where(c => c.Value == true).Max(k => k.Key.z);
			var minY = state.Where(c => c.Value == true).Min(k => k.Key.y);
			var maxY = state.Where(c => c.Value == true).Max(k => k.Key.y);
			var minX = state.Where(c => c.Value == true).Min(k => k.Key.x);
			var maxX = state.Where(c => c.Value == true).Max(k => k.Key.x);

			for (int z = minZ; z <= maxZ; z++)
			{
				Console.WriteLine($"z={z}");
				for (int y = minY; y <= maxY; y++)
				{
					var line = string.Empty;
					for (int x = minX; x <= maxX; x++)
					{
						var c = '.';
						if (state.TryGetValue((z, y, x), out bool active) && active)
						{
							c = '#';
						}
						line += c;
					}
					Console.WriteLine(line);
				}
				Console.WriteLine();
			}

			Console.WriteLine();
		}

		/*
		internal class Cube
		{
			public int Z { get; set; }

			public int Y { get; set; }

			public int X { get; set; }

			public bool IsActive { get; set; }
		}
		*/
	}
}
