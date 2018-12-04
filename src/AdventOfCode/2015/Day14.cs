using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day14 : IDay
    {
		private const int raceTime = 2503;
		private List<Reindeer> reindeer;

		public void GetInput()
		{
			reindeer = new List<Reindeer>();
			foreach (var line in File.ReadAllLines("2015/input/day14.txt").Where(l => !string.IsNullOrEmpty(l)))
			{
				var lineParts = line.Split(' ');
				var velocity = Convert.ToInt32(lineParts[3]);
				var flyTime = Convert.ToInt32(lineParts[6]);
				var restTime = Convert.ToInt32(lineParts[13]);

				reindeer.Add(new Reindeer
				{
					Velocity = velocity,
					FlyTime = flyTime,
					RestTime = restTime,
					Position = new List<int>(),
					Points = 0
				});
			}
		}

		public void Solve()
		{
			foreach (var deer in reindeer)
			{
				var flyTimeRemaining = deer.FlyTime;
				var restTimeRemaining = deer.RestTime;
				var distanceTraveled = 0;

				var i = 0;
				while (i < raceTime)
				{
					while (flyTimeRemaining > 0)
					{
						distanceTraveled += deer.Velocity;
						flyTimeRemaining--;
						i++;
						deer.Position.Add(distanceTraveled);
					}
					flyTimeRemaining = deer.FlyTime;

					while (restTimeRemaining > 0)
					{
						restTimeRemaining--;
						i++;
						deer.Position.Add(distanceTraveled);
					}
					restTimeRemaining = deer.RestTime;
				}
			}

			var maxDistance = reindeer.Max(r => r.Position[raceTime - 1]);
			Console.WriteLine($"The winning reindeer (part one) traveled {maxDistance} km");

			for (int i = 0; i < raceTime; i++)
			{
				var leadingPosition = reindeer.Max(r => r.Position[i]);
				var leadingDeer = reindeer.First(r => r.Position[i] == leadingPosition);
				leadingDeer.Points++;
			}

			var maxPoints = reindeer.Max(r => r.Points);
			Console.WriteLine($"The winning reindeer (part two) earned {maxPoints} points");
		}

		internal class Reindeer
		{
			public int Velocity { get; set; }

			public int FlyTime { get; set; }

			public int RestTime { get; set; }

			public List<int> Position { get; set; }

			public int Points { get; set; }
		}
	}
}
