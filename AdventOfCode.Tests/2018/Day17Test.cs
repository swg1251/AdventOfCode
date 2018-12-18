using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
	public class Day17Test
	{
		private List<string> input = new List<string>
		{
			"x=495, y=2..7",
			"y=7, x=495..501",
			"x=501, y=3..7",
			"x=498, y=2..4",
			"x=506, y=1..2",
			"x=498, y=10..13",
			"x=504, y=10..13",
			"y=13, x=498..504"
		};

		[Fact]
		void Example_input_test()
		{
			var day17 = new AdventOfCode.Year2018.Day17();
			var grid = day17.GetClay(input);
			var answer = day17.Flow(grid);
			Assert.Equal(57, answer.running + answer.standing);
			Assert.Equal(29, answer.standing);
		}
	}
}
