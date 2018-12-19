using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day16Test
    {
		private List<string> scenario = new List<string>
		{
			"Before: [3, 2, 1, 1]",
			"9 2 1 2",
			"After:  [3, 2, 2, 1]"
		};

		[Fact]
		void Part_one_example()
		{
			var day16 = new AdventOfCode.Year2018.Day16();
			var answer = day16.GetMatchingMethodCount(scenario);
			Assert.Equal(3, answer);
		}
    }
}
