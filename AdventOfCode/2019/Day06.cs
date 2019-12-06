using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2019
{
	public class Day06 : IDay
	{
		private List<Planet> planets;

		public void GetInput()
		{
			var input = File.ReadAllLines("2019/input/day06.txt").Where(l => !string.IsNullOrEmpty(l));

			planets = new List<Planet>();
			foreach (var line in input)
			{
				var lineParts = line.Split(')');

				var planet = planets.FirstOrDefault(p => p.Name == lineParts[0]);
				if (planet is null)
				{
					planet = new Planet(lineParts[0]);
					planets.Add(planet);
				}

				var orbiter = planets.FirstOrDefault(p => p.Name == lineParts[1]);
				if (orbiter is null)
				{
					orbiter = new Planet(lineParts[1]);
					planets.Add(orbiter);
				}

				planet.Orbiters.Add(orbiter);
				orbiter.Orbiting.Add(planet);
			}
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var orbits = new HashSet<string>();

			foreach (var com in planets)
			{
				var queue = new Queue<Planet>();
				queue.Enqueue(com);

				while (queue.Any())
				{
					var planet = queue.Dequeue();
					foreach (var orbiter in planet.Orbiters)
					{
						orbits.Add(orbiter.Name + planet.Name);
						orbits.Add(orbiter.Name + com.Name);
						queue.Enqueue(orbiter);
					}
				}
			}

			Console.WriteLine($"Number of total direct and indirect orbits (part one): {orbits.Count}");
		}

		private void PartTwo()
		{
			var start = planets.First(p => p.Name == "YOU").Orbiting.Single();
			var seen = new HashSet<string>();

			var queue = new Queue<(Planet planet, int distance)>();
			queue.Enqueue((start, 0));

			while (queue.Any())
			{
				var p = queue.Dequeue();
				var planet = p.planet;

				if (planet.Name == "SAN")
				{
					// Subtract 1 as this was distance to Santa, not to what he's orbiting
					Console.WriteLine($"Number of jumps to Santa (part two): {p.distance - 1}");
					return;
				}

				seen.Add(planet.Name);

				foreach (var orbiter in planet.Orbiters)
				{
					if (!seen.Contains(orbiter.Name))
					{
						queue.Enqueue((orbiter, p.distance + 1));
					}
				}
				foreach (var orbiting in planet.Orbiting)
				{
					if (!seen.Contains(orbiting.Name))
					{
						queue.Enqueue((orbiting, p.distance + 1));
					}
				}
			}
		}

		internal class Planet
		{
			public string Name { get; set; }

			public List<Planet> Orbiters { get; set; }

			public List<Planet> Orbiting { get; set; }

			public Planet(string name)
			{
				Name = name;
				Orbiters = new List<Planet>();
				Orbiting = new List<Planet>();
			}
		}
	}
}
