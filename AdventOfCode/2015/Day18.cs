using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day18 : IDay
	{
		private bool[][] initialState;

		public void GetInput()
		{
			initialState = new bool[100][];

			var input = File.ReadAllLines("2015/input/day18.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
			for (int i = 0; i < 100; i++)
			{
				var row = new bool[100];
				for (int j = 0; j < 100; j++)
				{
					row[j] = input[i][j] == '#';
				}
				initialState[i] = row;
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The number of lights on after 100 steps (part one) is {Do(initialState)}");
			Console.WriteLine($"The number of lights on after 100 steps (part two) is {Do(initialState, true)}");
		}

		private int Do(bool[][] startState, bool partTwo = false)
		{
			var currentLights = new bool[100][];

			for (int step = 0; step < 100; step++)
			{
				currentLights = new bool[100][];
				for (int i = 0; i < 100; i++)
				{
					currentLights[i] = new bool[100];
				}
				if (partTwo)
				{
					currentLights[0][0] = true;
					currentLights[0][99] = true;
					currentLights[99][0] = true;
					currentLights[99][99] = true;
				}

				for (int i = 0; i < 100; i++)
				{
					for (int j = 0; j < 100; j++)
					{
						var onNeighbors = 0;

						// up-left
						if (i > 0 && j > 0 && startState[i - 1][j - 1])
						{
							onNeighbors++;
						}
						// up
						if (i > 0 && startState[i - 1][j])
						{
							onNeighbors++;
						}
						// up-right
						if (i > 0 && j < 99 && startState[i - 1][j + 1])
						{
							onNeighbors++;
						}
						// left
						if (j > 0 && startState[i][j - 1])
						{
							onNeighbors++;
						}
						// right
						if (j < 99 && startState[i][j + 1])
						{
							onNeighbors++;
						}
						// down-left
						if (i < 99 && j > 0 && startState[i + 1][j - 1])
						{
							onNeighbors++;
						}
						// down
						if (i < 99 && startState[i + 1][j])
						{
							onNeighbors++;
						}
						// down-right
						if (i < 99 && j < 99 && startState[i + 1][j + 1])
						{
							onNeighbors++;
						}

						if (startState[i][j])
						{
							currentLights[i][j] = (onNeighbors == 2 || onNeighbors == 3);
						}
						else
						{
							currentLights[i][j] = onNeighbors == 3;
						}
					}
				}

				if (partTwo)
				{
					currentLights[0][0] = true;
					currentLights[0][99] = true;
					currentLights[99][0] = true;
					currentLights[99][99] = true;
				}
				startState = currentLights;
			}

			return startState.Sum((bool[] r) => r.Count(l => l));
		}
	}
}
