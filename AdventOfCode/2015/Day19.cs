using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
	public class Day19 : IDay
	{
		public string InputMolecule;
		public Dictionary<string, List<string>> Reactions;
		public Dictionary<string, string> ReverseReactions;

		public void GetInput()
		{
			Reactions = new Dictionary<string, List<string>>();
			ReverseReactions = new Dictionary<string, string>();

			var input = File.ReadAllLines("2015/input/day19.txt").Where(l => !string.IsNullOrEmpty(l));
			foreach (var line in input.SkipLast(1))
			{
				var lineParts = line.Split(" => ");
				var atom = lineParts[0];
				var reaction = lineParts[1];

				if (!Reactions.ContainsKey(atom))
				{
					Reactions[atom] = new List<string>();
				}
				Reactions[atom].Add(reaction);

				ReverseReactions[reaction] = atom;
			}
			InputMolecule = input.Last();
		}

		public void Solve()
		{
			Console.WriteLine($"The number of distinct molecules after one reaction (part one) is: {PartOne()}");
			Console.WriteLine($"The number of steps from e to medicine (part two) is: {GetStepsToMolecule()}");
		}

		public int PartOne()
		{
			return GetReactions(InputMolecule).Count;
		}

		private int GetStepsToMolecule()
		{
			var orderedReverse = ReverseReactions.OrderByDescending(r => r.Key.Length - r.Value.Length);

			var molecule = InputMolecule;
			var steps = 0;

			while (molecule != "e")
			{
				steps++;

				molecule = Reverse(molecule, orderedReverse);
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
				if (Reactions.ContainsKey(ss1))
				{
					atom = ss1;
				}
				else if (i < startMolecule.Length - 1)
				{
					var ss2 = startMolecule.Substring(i, 2);
					if (Reactions.ContainsKey(ss2))
					{
						atom = ss2;
					}
				}

				if (atom == string.Empty)
				{
					continue;
				}

				foreach (var reaction in Reactions[atom])
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

		private string Reverse(string startMolecule, IEnumerable<KeyValuePair<string, string>> reverseOrdered)
		{
			foreach (var reverse in reverseOrdered)
			{
				if (startMolecule.Contains(reverse.Key))
				{
					var regex = new Regex(reverse.Key);
					return regex.Replace(startMolecule, reverse.Value);
				}
			}

			return startMolecule;

			/*
			var maxReactionLength = ReverseReactions.Keys.Max(k => k.Length);

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
					if (ReverseReactions.ContainsKey(ss))
					{
						var left = startMolecule.Substring(0, i);
						var right = startMolecule.Substring(i + ss.Length, startMolecule.Length - (i + ss.Length));

						var newMolecule = left + ReverseReactions[ss] + right;
						if (newMolecule.Length < startMolecule.Length)
						{
							molecules.Add(newMolecule);
						}
					}
				}
			}

			return molecules;
			*/
		}
	}
}
