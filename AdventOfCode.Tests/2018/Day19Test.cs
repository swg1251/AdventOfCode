using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
	public class Day19Test
	{
		private List<string> input = new List<string>
		{
			"#ip 0",
			"seti 5 0 1",
			"seti 6 0 2",
			"addi 0 1 0",
			"addr 1 2 3",
			"setr 1 0 0",
			"seti 8 0 4",
			"seti 9 0 5"
		};

		[Fact]
		void Part_one_example()
		{
			var day19 = new AdventOfCode.Year2018.Day19();
			var answer = day19.ProcessInstructions(input);
			Assert.Equal(6, answer[0]);
		}
	}
}
