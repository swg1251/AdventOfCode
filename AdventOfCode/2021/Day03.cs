using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2021
{
	public class Day03 : IDay
	{
		private List<string> lines;

		public void GetInput()
		{
			lines = InputHelper.GetStringsFromInput(2021, 3);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var gammaBinaryString = string.Empty;
			var epsilonBinaryString = string.Empty;

			for (int i = 0; i < lines[0].Length; i++)
			{
				if (lines.Count(l => l[i] == '0') > lines.Count(l => l[i] == '1'))
				{
					gammaBinaryString += '0';
					epsilonBinaryString += '1';
				}
				else
				{
					gammaBinaryString += '1';
					epsilonBinaryString += '0';
				}
			}

			var gamma = Convert.ToInt32(gammaBinaryString, 2);
			var epsilon = Convert.ToInt32(epsilonBinaryString, 2);

			Console.WriteLine($"The power consumption (part one) is {gamma * epsilon}");
		}

		private void PartTwo()
		{
			var length = lines[0].Length;

			var oxygenCandidates = InputHelper.GetStringsFromInput(2021, 3);
			for (int i = 0; i < length; i++)
			{
				if (oxygenCandidates.Count(l => l[i] == '0') > oxygenCandidates.Count(l => l[i] == '1'))
				{
					oxygenCandidates = oxygenCandidates.Where(l => l[i] == '0').ToList();
				}
				else
				{
					oxygenCandidates = oxygenCandidates.Where(l => l[i] == '1').ToList();
				}

				if (oxygenCandidates.Count == 1)
				{
					break;
				}
			}

			var co2Candidates = InputHelper.GetStringsFromInput(2021, 3);
			for (int i = 0; i < length; i++)
			{
				if (co2Candidates.Count(l => l[i] == '1') >= co2Candidates.Count(l => l[i] == '0'))
				{
					co2Candidates = co2Candidates.Where(l => l[i] == '0').ToList();
				}
				else
				{
					co2Candidates = co2Candidates.Where(l => l[i] == '1').ToList();
				}

				if (co2Candidates.Count == 1)
				{
					break;
				}
			}

			var oxygen = Convert.ToInt32(oxygenCandidates.Single(), 2);
			var co2 = Convert.ToInt32(co2Candidates.Single(), 2);

			Console.WriteLine($"The life support rating (part two) is {oxygen * co2}");
		}
	}
}
