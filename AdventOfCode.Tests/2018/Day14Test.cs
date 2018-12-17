using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day14Test
    {
		[Fact(Skip = "Very slow due to inefficient part two")]
		void Part_one_example()
		{
			var day14 = new AdventOfCode.Year2018.Day14();
			var answer = day14.GetScore(9);
			Assert.Equal("5158916779", answer.partOne);
		}

		[Fact(Skip = "Very slow due to inefficient part two")]
		void Part_one_example_two()
		{
			var day14 = new AdventOfCode.Year2018.Day14();
			var answer = day14.GetScore(5);
			Assert.Equal("0124515891", answer.partOne);
		}

		[Fact(Skip = "Very slow due to inefficient part two")]
		void Part_one_example_three()
		{
			var day14 = new AdventOfCode.Year2018.Day14();
			var answer = day14.GetScore(18);
			Assert.Equal("9251071085", answer.partOne);
		}

		[Fact(Skip = "Very slow due to inefficient part two")]
		void Part_one_example_four()
		{
			var day14 = new AdventOfCode.Year2018.Day14();
			var answer = day14.GetScore(2018);
			Assert.Equal("5941429882", answer.partOne);
		}
	}
}
