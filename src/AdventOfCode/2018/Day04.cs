using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day04 : IDay
	{
		private List<(DateTime dateTime, List<string> s)> logs;

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day04.txt").Where(l => !string.IsNullOrEmpty(l));

			logs = new List<(DateTime dateTime, List<string> s)>();
			foreach (var l in input)
			{
				var parts = l.Split(']');
				var datePart = parts[0].Replace("[", "");
				logs.Add((Convert.ToDateTime(datePart), parts[1].Trim().Split(' ').ToList()));
			}

			// sort each log chronologically
			logs = logs.AsEnumerable().OrderBy(l => l.dateTime).ToList();
		}

		public void Solve()
		{
			var guards = new Dictionary<int, Dictionary<int, int>>();

			var currentGuard = Convert.ToInt32(logs[0].s[1].Substring(1));
			var asleepMinute = 0;
			var awakeMinute = 0;

			for (int i = 1; i < logs.Count; i++)
			{
				// guard change
				if (logs[i].s[0] == "Guard")
				{
					currentGuard = Convert.ToInt32(logs[i].s[1].Substring(1));
					continue;
				}

				// falling asleep
				else if (logs[i].s[0] == "falls")
				{
					asleepMinute = logs[i].dateTime.Minute;
				}

				// waking up
				else if (logs[i].s[0] == "wakes")
				{
					awakeMinute = logs[i].dateTime.Minute;
					
					// add all asleep minutes
					if (!guards.ContainsKey(currentGuard))
					{
						guards[currentGuard] = new Dictionary<int, int>();
					}
					for (int m = asleepMinute; m < awakeMinute; m++)
					{
						if (!guards[currentGuard].ContainsKey(m))
						{
							guards[currentGuard][m] = 0;
						}
						guards[currentGuard][m]++;
					}
				}
			}

			// get the guard with the total most asleep minutes, then their most asleep minute
			var sleepiestGuard = guards.Aggregate((l, r) => l.Value.Values.Sum() > r.Value.Values.Sum() ? l : r).Key;
			var sleepiestMinute = guards[sleepiestGuard].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
			Console.WriteLine($"The sleepiest guard ID * his sleepiest minute (part one) is: {sleepiestGuard * sleepiestMinute}");

			// now get the guard who was asleep most in a single minute, then their sleepiest minute
			sleepiestGuard = guards.Aggregate((l, r) => l.Value.Values.Max() > r.Value.Values.Max() ? l : r).Key;
			sleepiestMinute = guards[sleepiestGuard].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
			Console.WriteLine($"The most minute-consistent sleepy guard * his minute (part two) is: {sleepiestGuard * sleepiestMinute}");
		}
	}
}
