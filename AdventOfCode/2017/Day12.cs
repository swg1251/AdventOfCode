using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day12 : IDay
    {
		private List<Program> programs;
		private Dictionary<int, IEnumerable<Program>> groups;

		public Day12()
		{
			programs = new List<Program>();
			groups = new Dictionary<int, IEnumerable<Program>>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day12.txt").Where(l => !string.IsNullOrEmpty(l));

			foreach (var inputParts in input.Select(l => l.Split(' ')))
			{
				var leftProgramId = Convert.ToInt32(inputParts[0]);
				var rightProgramIds = new List<int>();

				for (int i = 2; i < inputParts.Length; i++)
				{
					rightProgramIds.Add(Convert.ToInt32(inputParts[i].Replace(",", "")));
				}

				var leftProgram = programs.Find(p => p.Id == leftProgramId);
				if (leftProgram == null)
				{
					leftProgram = new Program(leftProgramId);
					programs.Add(leftProgram);
				}

				foreach (var id in rightProgramIds)
				{
					if (id == leftProgram.Id)
					{
						continue;
					}

					var rightProgram = programs.Find(p => p.Id == id);
					if (rightProgram == null)
					{
						rightProgram = new Program(id);
						programs.Add(rightProgram);
					}

					leftProgram.ConnectedPrograms.Add(rightProgram);
					rightProgram.ConnectedPrograms.Add(leftProgram);
				}
			}
		}

		public void Solve()
		{
			// loop until all programs have been grouped
			while (programs.Any())
			{
				// start a new group with the first ungrouped program ID as its key
				var groopRoot = programs[0].Id;
				var foundNewConnection = false;
				do
				{
					foundNewConnection = false;

					foreach (var program in programs)
					{
						if (program.Id == groopRoot || program.ConnectedPrograms.Any(cp => cp.Id == groopRoot || cp.CanReachGroupRoot))
						{
							if (!program.CanReachGroupRoot)
							{
								foundNewConnection = true;
							}
							program.CanReachGroupRoot = true;
						}
					}
				}
				while (foundNewConnection); // stop searching when we are no longer finding new connnections to the group

				// add group to dictionary and remove the grouped programs from the collection
				groups[groopRoot] = new List<Program>(programs.Where(p => p.CanReachGroupRoot));
				programs.RemoveAll(p => p.CanReachGroupRoot);
			}

			Console.WriteLine($"The number of programs that can reach program zero (part one) is {groups[0].Count()}");
			Console.WriteLine($"The total number of program groups (part two) is {groups.Count}");
		}

		internal class Program
		{
			public int Id { get; set; }
			public List<Program> ConnectedPrograms { get; set; }
			public bool CanReachGroupRoot { get; set; }

			public Program(int id)
			{
				Id = id;
				ConnectedPrograms = new List<Program>();
			}
		}
    }
}
