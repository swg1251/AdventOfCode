using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2015
{
    public class Day19Test
    {
		[Fact]
		void Part_one_test()
		{
			var day19 = new AdventOfCode.Year2015.Day19
			{
				InputMolecule = "HOH",
				Reactions = new Dictionary<string, List<string>>(),
			};
			day19.Reactions["H"] = new List<string>();
			day19.Reactions["H"].Add("HO");
			day19.Reactions["H"].Add("OH");
			day19.Reactions["O"] = new List<string>();
			day19.Reactions["O"].Add("HH");

			var answer = day19.PartOne();
			Assert.Equal(4, answer);
		}
    }
}
