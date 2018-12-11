using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
	public class Day19 : IDay
	{
		private string inputMolecule;
		private Dictionary<string, List<string>> reactions;
		private Dictionary<string, string> reverseReactions;

		public void GetInput()
		{
			reactions = new Dictionary<string, List<string>>();
			reverseReactions = new Dictionary<string, string>();
			/*
			inputMolecule = "HOH";
			reactions["H"] = new List<string>();
			reactions["H"].Add("HO");
			reactions["H"].Add("OH");
			reactions["O"] = new List<string>();
			reactions["O"].Add("HH");
			*/

			var input = File.ReadAllLines("2015/input/day19.txt").Where(l => !string.IsNullOrEmpty(l));
			foreach (var line in input.SkipLast(1))
			{
				var lineParts = line.Split(" => ");
				var atom = lineParts[0];
				var reaction = lineParts[1];

				if (!reactions.ContainsKey(atom))
				{
					reactions[atom] = new List<string>();
				}
				reactions[atom].Add(reaction);

				reverseReactions[reaction] = atom;
			}
			inputMolecule = input.Last();
		}

		public void Solve()
		{
			Console.WriteLine($"The number of distinct molecules after one reaction (part one) is: {GetReactions(inputMolecule).Count}");
			Console.WriteLine($"The number of steps from e to medicine (part two) is: {GetStepsToMolecule()}");
		}

		private int GetStepsToMolecule()
		{
			var molecules = new HashSet<string> { inputMolecule };
			var exploredMolecules = new HashSet<string>();
			var steps = 0;

			while (!molecules.Contains("e"))
			{
				steps++;

				var newMolecules = new HashSet<string>();
				foreach (var m in molecules)
				{
					foreach (var nm in ReverseReactions(m))
					{
						if (!exploredMolecules.Add(nm))
						{
							continue;
						}
						newMolecules.Add(nm);
					}
				}
				molecules = newMolecules;
			}
			return steps;
		}

		private HashSet<string> GetReactions(string startMolecule)
		{
			var molecules = new HashSet<string>();
			for (int i = 0; i < startMolecule.Length; i++)
			{
				var atom = string.Empty;
				var ss1 = startMolecule[i].ToString();
				if (reactions.ContainsKey(ss1))
				{
					atom = ss1;
				}
				else if (i < startMolecule.Length - 1)
				{
					var ss2 = startMolecule.Substring(i, 2);
					if (reactions.ContainsKey(ss2))
					{
						atom = ss2;
					}
				}

				if (atom == string.Empty)
				{
					continue;
				}

				foreach (var reaction in reactions[atom])
				{
					var left = startMolecule.Substring(0, i);
					var right = string.Empty;
					if (i < startMolecule.Length - 1)
					{
						right = startMolecule.Substring(i + atom.Length, startMolecule.Length - (i + atom.Length));
					}

					var newMolecule = left + reaction + right;
					molecules.Add(newMolecule);
				}
			}
			return molecules;
		}

		private HashSet<string> ReverseReactions(string startMolecule)
		{
			var molecules = new HashSet<string>();
			var maxReactionLength = reverseReactions.Keys.Max(k => k.Length);

			for (int i = 0; i < startMolecule.Length; i++)
			{
				for (int j = maxReactionLength; j > 0; j--)
				{
					if (i + j > startMolecule.Length - 1)
					{
						continue;
					}

					// if this substring matches one of the reactions, do the replacement
					var ss = startMolecule.Substring(i, j);
					if (reverseReactions.ContainsKey(ss))
					{
						var left = startMolecule.Substring(0, i);
						var right = startMolecule.Substring(i + ss.Length, startMolecule.Length - (i + ss.Length));

						var newMolecule = left + reverseReactions[ss] + right;
						if (newMolecule.Length < startMolecule.Length)
						{
							molecules.Add(newMolecule);
						}
					}
				}
			}
			return molecules;
		}
	}
}
