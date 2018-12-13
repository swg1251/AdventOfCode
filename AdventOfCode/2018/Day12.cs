using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day12 : IDay
	{
		private IEnumerable<string> initialInput;
		private string initialState;

		public void GetInput()
		{
			initialInput = File.ReadAllLines("2018/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).Skip(2);
			initialState = File.ReadAllLines("2018/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).First().Replace("initial state: ", "");
		}

		public void Solve()
		{
			var partOne = GetTotal(GetGoodInputs(initialInput), GetInitialState(initialState), 20);
			Console.WriteLine($"The total number after 20 generations (part one) is: {partOne}");


			var partTwo = GetTotal(GetGoodInputs(initialInput), GetInitialState(initialState), 50000000000);
			Console.WriteLine($"The total number after 50 billion generations (part two) is: {partTwo}");
		}

		public List<int> GetGoodInputs(IEnumerable<string> input)
		{
			// create binary strings and convert to int
			var goodInputs = new List<string>();
			foreach (var line in input)
			{
				var lineParts = line.Split(' ');
				if (lineParts[2] == "#")
				{
					goodInputs.Add(lineParts[0].Replace('.', '0').Replace('#', '1'));
				}
			}

			var intInputs = new List<int>();
			foreach (var binary in goodInputs)
			{
				intInputs.Add(Convert.ToInt32(binary, 2));
			}
			return intInputs;
		}

		public List<long> GetInitialState(string initialState)
		{
			var plants = new List<long>();
			for (int i = 0; i < initialState.Length; i++)
			{
				if (initialState[i] == '#')
				{
					plants.Add(i);
				}
			}
			return plants;
		}

		public long GetTotal(IEnumerable<int> producers, List<long> plants, long generations)
		{
			var newPlants = new List<long>();

			var min = plants.Min() - 2;
			var max = plants.Max() + 2;

			for (long i = 1; i < generations + 1; i++)
			{
				if (i % 1000000 == 0)
				{
					Console.WriteLine(i);
				}

				newPlants = new List<long>();

				for (long j = min; j <= max; j++)
				{
					// convert binary array to int
					var ba = new BitArray(new bool[]
					{
						plants.Contains(j + 2),
						plants.Contains(j + 1),
						plants.Contains(j),
						plants.Contains(j - 1),
						plants.Contains(j - 2)
					});
					var iv = new int[1];
					ba.CopyTo(iv, 0);

					if (producers.Contains(iv[0]))
					{
						newPlants.Add(j);
					}
				}

				if (plants.Sum(p => p + 1) == newPlants.Sum())
				{
					// plants essentially the same, just shifting 1 to the right each time
					// for each current index, the final index would be index + (50bil - current gen)
					return newPlants.Sum(p => p + (generations - i));
				}

				plants = newPlants;

				min = plants.Min() - 2;
				max = plants.Max() + 2;
			}
			return plants.Sum();
		}
	}
}
