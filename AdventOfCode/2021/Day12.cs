using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day12 : IDay
	{
		private Dictionary<string, List<string>> connections;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2021, 12);
			connections = new Dictionary<string, List<string>>();

			foreach (var line in lines)
			{
				var lineParts = line.Split('-');

				if (!connections.ContainsKey(lineParts[0]))
				{
					connections[lineParts[0]] = new List<string>();
				}
				if (!connections.ContainsKey(lineParts[1]))
				{
					connections[lineParts[1]] = new List<string>();
				}
				connections[lineParts[0]].Add(lineParts[1]);
				connections[lineParts[1]].Add(lineParts[0]);
			}
		}

		public void Solve()
		{
			FindPaths(false);
			FindPaths(true);
		}

		public void FindPaths(bool partTwo)
		{
			var state = new State { Current = "start" };
			var states = new Queue<State>();
			states.Enqueue(state);
			var paths = 0;

			while (states.Any())
			{
				state = states.Dequeue();
				if (state.Current == "end")
				{
					paths++;
					continue;
				}

				foreach (var cave in connections[state.Current])
				{
					var canVisit = true;
					if (partTwo && IsLower(cave))
					{
						if (cave == "start" || (state.Visited.Contains(cave) && state.SmallCaveVisitedTwice))
						{
							canVisit = false;
						}
					}
					else
					{
						canVisit = !IsLower(cave) || !state.Visited.Contains(cave);
					}

					if (canVisit)
					{
						var smallCaveVisitedTwice = state.SmallCaveVisitedTwice;
						if (IsLower(cave) && state.Visited.Contains(cave))
						{
							smallCaveVisitedTwice = true;
						}

						var visited = new List<string>(state.Visited)
						{
							state.Current
						};

						states.Enqueue(new State
						{
							Current = cave,
							Visited = visited,
							SmallCaveVisitedTwice = smallCaveVisitedTwice
						});
					}
				}
			}

			Console.WriteLine($"The number of paths (part {(partTwo ? "two" : "one" )}) is {paths}");
		}

		internal class State
		{
			public string Current;

			public List<string> Visited = new List<string>();

			public bool SmallCaveVisitedTwice { get; set; } = false;
		}

		private static bool IsLower(string s)
		{
			return s.All(c => "abcdefghijklmnopqrstuvwxuz".Contains(c));
		}
	}
}
