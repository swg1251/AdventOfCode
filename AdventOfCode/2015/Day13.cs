using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day13 : IDay
    {
		private Dictionary<string, Dictionary<string, int>> people;

		public void GetInput()
		{
			people = new Dictionary<string, Dictionary<string, int>>();
			foreach (var line in File.ReadAllLines("2015/input/day13.txt").Where(l => !string.IsNullOrEmpty(l)))
			{
				var lineParts = line.Split(' ');

				var person = lineParts[0];
				if (!people.ContainsKey(person))
				{
					people[person] = new Dictionary<string, int>();
				}

				var otherPerson = lineParts[10].Remove(lineParts[10].Length - 1);
				var happiness = Convert.ToInt32(lineParts[3]);
				if (lineParts[2] == "lose")
				{
					happiness *= -1;
				}
				people[person][otherPerson] = happiness;
			}
		}

		public void Solve()
		{
			var partOne = Search();
			Console.WriteLine($"The best seating arrangement (part one) yields {partOne} happiness");

			people.Add("Me", new Dictionary<string, int>());
			foreach (var person in people.Where(p => p.Key != "Me"))
			{
				people["Me"][person.Key] = 0;
				people[person.Key]["Me"] = 0;
			}

			var partTwo = Search();
			Console.WriteLine($"The best seating arrangement including \"Me\" (part two) yields {partTwo} happiness");
		}

		private int Search()
		{
			var maxHappiness = Int32.MinValue;
			var states = new Queue<SearchState>();
			states.Enqueue(new SearchState
			{
				CurrentPerson = people.First().Key,
				ExploredPeople = new List<string> { people.First().Key },
				Happiness = 0
			});

			while (states.Any())
			{
				var currentState = states.Dequeue();

				// Last person is next to the first person
				if (currentState.ExploredPeople.Count == people.Count)
				{
					currentState.Happiness += people[currentState.CurrentPerson][currentState.ExploredPeople.First()];
					currentState.Happiness += people[currentState.ExploredPeople.First()][currentState.CurrentPerson];

					if (currentState.Happiness > maxHappiness)
					{
						maxHappiness = currentState.Happiness;
					}
					continue;
				}

				foreach (var otherPerson in people[currentState.CurrentPerson])
				{
					if (!currentState.ExploredPeople.Contains(otherPerson.Key))
					{
						var childState = new SearchState
						{
							CurrentPerson = otherPerson.Key,
							ExploredPeople = new List<string>(),
							Happiness = currentState.Happiness
								+ people[currentState.CurrentPerson][otherPerson.Key]
								+ people[otherPerson.Key][currentState.CurrentPerson]
						};
						childState.ExploredPeople.AddRange(currentState.ExploredPeople);
						childState.ExploredPeople.Add(otherPerson.Key);
						states.Enqueue(childState);
					}
				}
			}
			return maxHappiness;
		}

		internal class SearchState
		{
			public string CurrentPerson { get; set; }

			public List<string> ExploredPeople { get; set; }

			public int Happiness { get; set; }
		}
    }
}
