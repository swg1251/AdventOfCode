using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day16Test
    {
		private List<string> input = new List<string>
		{
			"Before: [3, 2, 1, 1]",
			"9 2 1 2",
			"After:  [3, 2, 2, 1]"
		};

		[Fact]
		void Part_one_example()
		{
			var day16 = new AdventOfCode.Year2018.Day16();
			var scenarios = day16.ProcessScenarios(input);
			var answer = scenarios.First().validOps.Count;
			Assert.Equal(3, answer);
		}
    }
}
