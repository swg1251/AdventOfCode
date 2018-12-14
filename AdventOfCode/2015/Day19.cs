using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
	public class Day19 : IDay
	{
		private IEnumerable<string> input;

		public void GetInput()
		{
			input = File.ReadAllLines("2015/input/day19.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			var reactions = GetReactions(input);
			var inputMolecule = input.Last();

			var partOne = GetDistinctReactionCount(inputMolecule, reactions.reactions);
			Console.WriteLine($"The number of distinct molecules after one reaction (part one) is: {partOne}");

			var partTwo = GetStepsToMolecule(inputMolecule, reactions.reverseReactions);
			Console.WriteLine($"The number of steps from e to medicine (part two) is: {partTwo}");
		}

		public (Dictionary<string, List<string>> reactions, Dictionary<string, string> reverseReactions) GetReactions(IEnumerable<string> input)
		{
			var reactions = new Dictionary<string, List<string>>();
			var reverseReactions = new Dictionary<string, string>();
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

				reverseReactions[new string(reaction.Reverse().ToArray())] = new string(atom.Reverse().ToArray());
			}
			return (reactions, reverseReactions);
		}

		public int GetStepsToMolecule(string startMolecule, Dictionary<string, string> reverseReactions)
		{
			// work backwards
			var molecule = new string(startMolecule.Reverse().ToArray());
			var steps = 0;

			while (molecule != "e")
			{
				molecule = Replace(molecule, reverseReactions);
				steps++;
			}
			return steps;
		}

		public int GetDistinctReactionCount(string startMolecule, Dictionary<string, List<string>> reactions)
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
			return molecules.Count();
		}

		private string Replace(string startMolecule, Dictionary<string, string> reverseReactions)
		{
			// replace the first match of any
			var regex = new Regex(string.Join('|', reverseReactions.Keys));
			var match = regex.Match(startMolecule);
			if (!match.Success)
			{
				return null;
			}
			return regex.Replace(startMolecule, reverseReactions[match.Value], 1);
		}
	}
}
