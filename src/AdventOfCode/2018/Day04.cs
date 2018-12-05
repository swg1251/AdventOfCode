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
				logs.Add(GetLog(l));
			}
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

			var maxSleepMinutes = guards.Max(g => g.Value.Values.Sum());
			var sleepiestGuard = guards.First(g => g.Value.Values.Sum() == maxSleepMinutes).Key;

			var sleepiestMinuteValue = guards[sleepiestGuard].Values.Max();
			var sleepiestMinuteKey = guards[sleepiestGuard].First(m => m.Value == sleepiestMinuteValue).Key;

			Console.WriteLine($"The sleepiest guard ID * his sleepiest minute (part one) is: {sleepiestGuard * sleepiestMinuteKey}");

			var sleepiestMinuteGuard = 0;
			var sleepiestMinuteGuardMinute = 0;
			var sleepiestMinuteTotal = 0;

			foreach (var g in guards)
			{
				foreach (var t in g.Value)
				{
					if (t.Value > sleepiestMinuteTotal)
					{
						sleepiestMinuteTotal = t.Value;
						sleepiestMinuteGuard = g.Key;
						sleepiestMinuteGuardMinute = t.Key;
					}
				}
			}

			Console.WriteLine($"The most minute-consistent sleepy guard * his minute (part two) is: {sleepiestMinuteGuard * sleepiestMinuteGuardMinute}");
		}

		private (DateTime dateTime, List<string> s) GetLog(string s)
		{
			var parts = s.Split(']');
			var datePart = parts[0].Replace("[", "");
			return (Convert.ToDateTime(datePart), parts[1].Trim().Split(' ').ToList());
		}
	}
}
