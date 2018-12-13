using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day12 : IDay
	{
		public IEnumerable<string> GoodInputs;
		public Dictionary<int, bool> Plants;

		public void GetInput()
		{
			// ???

		}

		public void Solve()
		{
			var inputs = File.ReadAllLines("2018/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).Skip(2);
			var initialState = File.ReadAllLines("2018/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).First().Replace("initial state: ", "");

			Console.WriteLine(PartOne(GetGoodInputs(inputs), GetInitialState(initialState)));
		}

		public List<string> GetGoodInputs(IEnumerable<string> input)
		{
			var goodInputs = new List<string>();
			foreach (var line in input)
			{
				var lineParts = line.Split(' ');
				if (lineParts[2] == "#")
				{
					goodInputs.Add(lineParts[0]);
				}
			}
			return goodInputs;
		}

		public Dictionary<int, bool> GetInitialState(string initialState)
		{
			var plants = new Dictionary<int, bool>();
			for (int i = -200; i < 200; i++)
			{
				plants[i] = false;
			}
			for (int i = 0; i < initialState.Length; i++)
			{
				plants[i] = initialState[i] == '#';
			}
			return plants;
		}

		public int PartOne(IEnumerable<string> producers, Dictionary<int, bool> plants)
		{
			var currentPlants = plants;
			var newPlants = new Dictionary<int, bool>();
			for (int i = -200; i < 200; i++)
			{
				newPlants[i] = false;
			}

			for (int i = 1; i < 21; i++)
			{
				newPlants = new Dictionary<int, bool>();
				for (int j = -200; j < 200; j++)
				{
					newPlants[j] = false;
				}

				for (int j = -198; j < 198; j++)
				{
					var pattern = string.Empty;
					pattern += currentPlants[j - 2] ? "#" : ".";
					pattern += currentPlants[j - 1] ? "#" : ".";
					pattern += currentPlants[j] ? "#" : ".";
					pattern += currentPlants[j + 1] ? "#" : ".";
					pattern += currentPlants[j + 2] ? "#" : ".";

					newPlants[j] = producers.Contains(pattern);
				}

				currentPlants = newPlants;
			}
			return currentPlants.Where(p => p.Value).Sum(p => p.Key);
		}

		public int PartTwo(IEnumerable<string> producers, Dictionary<int, bool> plants)
		{
			var currentPlants = plants;
			var newPlants = new Dictionary<int, bool>();
			for (int i = -200; i < 200; i++)
			{
				newPlants[i] = false;
			}

			for (long i = 1; i < 50000000000; i++)
			{
				newPlants = new Dictionary<int, bool>();
				for (int j = -200; j < 200; j++)
				{
					newPlants[j] = false;
				}

				for (int j = -198; j < 198; j++)
				{
					var pattern = string.Empty;
					pattern += currentPlants[j - 2] ? "#" : ".";
					pattern += currentPlants[j - 1] ? "#" : ".";
					pattern += currentPlants[j] ? "#" : ".";
					pattern += currentPlants[j + 1] ? "#" : ".";
					pattern += currentPlants[j + 2] ? "#" : ".";

					newPlants[j] = producers.Contains(pattern);
				}

				currentPlants = newPlants;
			}
			return currentPlants.Where(p => p.Value).Sum(p => p.Key);
		}
	}
}
