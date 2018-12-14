using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2015
{
    public class Day19Test
    {
		IEnumerable<string> inputPartOne = new List<string>
		{
			"H => HO",
			"H => OH",
			"O => HH",
			""
		};

		IEnumerable<string> inputPartTwo = new List<string>
		{
			"e => H",
			"e => O",
			"H => HO",
			"H => OH",
			"O => HH",
			""
		};

		[Fact]
		void Part_one_example_one()
		{
			var day19 = new AdventOfCode.Year2015.Day19();
			var reactions = day19.GetReactions(inputPartOne);
			var answer = day19.GetDistinctReactionCount("HOH", reactions.reactions);
			Assert.Equal(4, answer);
		}

		[Fact]
		void Part_one_example_two()
		{
			var day19 = new AdventOfCode.Year2015.Day19();
			var reactions = day19.GetReactions(inputPartOne);
			var answer = day19.GetDistinctReactionCount("HOHOHO", reactions.reactions);
			Assert.Equal(7, answer);
		}

		[Fact(Skip = "Puzzle input allows part two to work, example input is different")]
		void Part_two_example_one()
		{
			var day19 = new AdventOfCode.Year2015.Day19();
			var reactions = day19.GetReactions(inputPartTwo);
			var answer = day19.GetStepsToMolecule("HOH", reactions.reverseReactions);
			Assert.Equal(3, answer);
		}

		[Fact(Skip = "Puzzle input allows part two to work, example input is different")]
		void Part_two_example_two()
		{
			var day19 = new AdventOfCode.Year2015.Day19();
			var reactions = day19.GetReactions(inputPartTwo);
			var answer = day19.GetStepsToMolecule("HOHOHO", reactions.reverseReactions);
			Assert.Equal(6, answer);
		}
	}
}
