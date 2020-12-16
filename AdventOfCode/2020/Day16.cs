using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day16 : IDay
	{
		private Dictionary<string, List<int>> fields;
		private List<List<int>> nearbyTickets;
		private List<int> myTicket;

		public void GetInput()
		{
			var lines = File.ReadAllLines("2020/input/day16.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();

			fields = new Dictionary<string, List<int>>();

			for (int i = 0; i < lines.Count; i++)
			{
				if (lines[i].StartsWith("your ticket"))
				{
					lines = lines.Skip(i + 1).ToList();
					break;
				}

				var lineParts = lines[i].Split(": ");
				var ranges = lineParts[1].Split(' ');

				var fieldName = lineParts[0];
				var min1 = Convert.ToInt32(ranges[0].Split('-')[0]);
				var max1 = Convert.ToInt32(ranges[0].Split('-')[1]);

				var min2 = Convert.ToInt32(ranges[2].Split('-')[0]);
				var max2 = Convert.ToInt32(ranges[2].Split('-')[1]);

				var range = new List<int>();
				for (int j = min1; j <= max1; j++)
				{
					range.Add(j);
				}
				for (int j = min2; j <= max2; j++)
				{
					range.Add(j);
				}
				fields.Add(fieldName, range);
			}

			myTicket = new List<int>();
			foreach (var value in lines.First().Split(',').Select(x => Convert.ToInt32(x)))
			{
				myTicket.Add(value);
			}
			lines = lines.Skip(2).ToList();

			nearbyTickets = new List<List<int>>();
			foreach (var line in lines)
			{
				var ticket = new List<int>();
				var values = line.Split(',').Select(x => Convert.ToInt32(x));
				foreach (var value in values)
				{
					ticket.Add(value);
				}
				nearbyTickets.Add(ticket);
			}
		}

		public void Solve()
		{
			var errors = nearbyTickets.Sum(t => t.Where(v => !fields.Values.Any(f => f.Contains(v))).Sum());
			Console.WriteLine($"The error rate (part one) is {errors}");

			// discard invalid tickets
			nearbyTickets = nearbyTickets.Where(t => !t.Any(v => !fields.Values.Any(f => f.Contains(v)))).ToList();

			var possibleFields = new Dictionary<string, List<int>>();
			var fieldsAndIndices = new Dictionary<string, int>();

			foreach (var field in fields.Keys)
			{
				var possibleIndices = new List<int>();
				for (int i = 0; i < nearbyTickets.First().Count; i++)
				{
					if (nearbyTickets.All(t => fields[field].Contains(t[i])))
					{
						possibleIndices.Add(i);
					}
				}
				possibleFields[field] = possibleIndices;
			}

			while (possibleFields.Any())
			{
				var solvedField = possibleFields.FirstOrDefault(f => f.Value.Count == 1);
				if (!solvedField.Equals(default(KeyValuePair<string, List<int>>)))
				{
					var index = solvedField.Value.First();
					fieldsAndIndices[solvedField.Key] = index;
					foreach (var field in possibleFields.Keys)
					{
						possibleFields[field].Remove(index);
					}
					possibleFields.Remove(solvedField.Key);
				}

				for (int i = 0; i < nearbyTickets.First().Count; i++)
				{
					foreach (var field in fields.Keys)
					{
						if (!nearbyTickets.All(t => fields[field].Contains(t[i])))
						{
							if (possibleFields.ContainsKey(field))
							{
								possibleFields[field].Remove(i);
							}
						}
					}
				}
			}

			var departureIndices = fieldsAndIndices.Where(f => f.Key.StartsWith("departure")).Select(kvp => kvp.Value);
			var partTwo = 1L;
			foreach (var index in departureIndices)
			{
				partTwo *= myTicket[index];
			}

			Console.WriteLine($"The product of departure values on my ticket (part two) is {partTwo}");
		}
	}
}
