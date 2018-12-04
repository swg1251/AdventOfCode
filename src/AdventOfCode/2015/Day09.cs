using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day09 : IDay
    {
		private Dictionary<string, Dictionary<string, int>> cities;

		public void GetInput()
		{
			cities = new Dictionary<string, Dictionary<string, int>>();

			var input = File.ReadAllLines("2015/input/day09.txt").Where(l => !string.IsNullOrEmpty(l));
			foreach (var line in input)
			{
				var lineParts = line.Split(' ');

				var cityName = lineParts[0];
				if (!cities.ContainsKey(cityName))
				{
					cities[cityName] = new Dictionary<string, int>();
				}

				var destinationCityName = lineParts[2];
				if (!cities.ContainsKey(destinationCityName))
				{
					cities[destinationCityName] = new Dictionary<string, int>();
				}

				var distance = Convert.ToInt32(lineParts[4]);
				cities[cityName][destinationCityName] = distance;
				cities[destinationCityName][cityName] = distance;
			}
		}

		// Breadth-first search
		public void Solve()
		{
			var shortestPath = Int32.MaxValue;
			var longestPath = Int32.MinValue;
			var states = new Queue<SearchState>();

			// Enqueue each city as a starting point
			foreach (var cityName in cities.Keys)
			{
				var state = new SearchState { CurrentCity = cityName, ExploredCities = new List<string>() };
				state.ExploredCities.Add(cityName);
				states.Enqueue(state);
			}

			while (states.Any())
			{
				var currentState = states.Dequeue();

				if (currentState.ExploredCities.Count == cities.Count)
				{
					if (currentState.TotalDistance < shortestPath)
					{
						shortestPath = currentState.TotalDistance;
					}
					if (currentState.TotalDistance > longestPath)
					{
						longestPath = currentState.TotalDistance;
					}
				}

				foreach (var destination in cities[currentState.CurrentCity])
				{
					if (!currentState.ExploredCities.Contains(destination.Key))
					{
						var childState = new SearchState
						{
							CurrentCity = destination.Key,
							ExploredCities = new List<string>(),
							TotalDistance = currentState.TotalDistance + destination.Value
						};
						childState.ExploredCities.AddRange(currentState.ExploredCities);
						childState.ExploredCities.Add(destination.Key);
						states.Enqueue(childState);
					}
				}
			}
			Console.WriteLine($"The shortest possible path (part one) is: {shortestPath}");
			Console.WriteLine($"The longest possible path (part two) is : {longestPath}");
		}

		internal class SearchState
		{
			public string CurrentCity { get; set; }

			public List<string> ExploredCities { get; set; }

			public int TotalDistance { get; set; }
		}
    }
}
