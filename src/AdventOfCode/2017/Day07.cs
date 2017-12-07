using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day07 : IDay
    {
		private List<Program> programs;

		public Day07()
		{
			programs = new List<Program>();
		}

		public void GetInput()
		{
			var input = File.ReadAllLines("2017/input/day07.txt").Where(l => !string.IsNullOrEmpty(l));
			var inputLinesSplit = input.Select(l => l.Split(' '));

			// add each program, ignoring the programs they support for now
			foreach (var programParts in inputLinesSplit)
			{
				var programName = programParts[0];
				var programSize = Convert.ToInt32(programParts[1].Replace("(", "").Replace(")", ""));

				programs.Add(new Program { Name = programName, Size = programSize });
			}

			foreach (var programParts in inputLinesSplit.Where(l => l.Length > 2))
			{
				var parent = programs.Find(p => p.Name == programParts[0]);
				parent.Children = new List<Program>();

				for (int i = 3; i < programParts.Length; i++)
				{
					var child = programs.Find(p => p.Name == programParts[i].Replace(",", "").Replace("\n", ""));
					parent.Children.Add(child);
				}
			}
		}

		public void Solve()
		{
			// root program is the one not contained in any child-having programs' list of children
			var rootProgram = programs.Find(p => !programs.Any(p2 => p2.Children != null && p2.Children.Select(c => c.Name).Contains(p.Name)));
			Console.WriteLine($"The bottom program's name (part one) is {rootProgram.Name}");

			Console.WriteLine(rootProgram.GetTotalChildWeight());
			Console.WriteLine(programs.Sum(p => p.Size));

			var maxLevel = programs.Max(p => p.Level);

			for (int i = 1; i <= maxLevel; i++)
			{
				foreach (var program in programs.Where(p => p.Level == i))
				{
					var expectedSize = program.Children[0].GetTotalChildWeight();

					// is this the parent of the offending program?
					if (program.Children.Any(p => p.GetTotalChildWeight() != expectedSize))
					{
						Console.WriteLine($"Offending program's parent is {program.Name}: weight {program.GetTotalChildWeight()}");
						foreach (var child in program.Children)
						{
							Console.WriteLine($"{child.Name}: {child.GetTotalChildWeight()}");
						}
						Console.WriteLine();
					}
				}
			}
		}

		internal class Program
		{
			public string Name { get; set; }
			public int Size { get; set; }
			public Program Parent { get; set; }
			public List<Program> Children { get; set; }

			public int Level
			{
				get
				{
					var currentProgram = this;
					var level = 0;

					while (currentProgram.Children != null)
					{
						level++;
						currentProgram = currentProgram.Children.First();
					}

					return level;
				}
			}

			public int GetTotalChildWeight()
			{
				if (Children == null)
				{
					return Size;
				}

				return Size + Children.Sum(c => c.GetTotalChildWeight());
			}
		}
    }
}
