using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day13Test
    {
		private List<string> partOneExample = new List<string>
		{
			"|",
			"v",
			"|",
			"|",
			"|",
			"^",
			"|"
		};

		private List<string> partOneExampleTwo = new List<string>
		{
			@"/->-\        ",
			@"|   |  /----\",
			@"| /-+--+-\  |",
			@"| | |  | v  |",
			@"\-+-/  \-+--/",
			@"\------/     "
		};

		private List<string> partTwoExample = new List<string>
		{
			@"/>-<\  ",
			@"|   |  ",
			@"| /<+-\",
			@"| | | v",
			@"\>+</ |",
			@"  |   ^",
			@"  \<->/"
		};

		[Fact]
		void Part_one_example_one()
		{
			var day13 = new AdventOfCode.Year2018.Day13();
			var answer = day13.Run(partOneExample);
			Assert.Equal((0, 3), answer);
		}

		[Fact]
		void Part_one_example_two()
		{
			var day13 = new AdventOfCode.Year2018.Day13();
			var answer = day13.Run(partOneExampleTwo);
			Assert.Equal((7, 3), answer);
		}

		[Fact]
		void Part_two_example()
		{
			var day13 = new AdventOfCode.Year2018.Day13();
			var answer = day13.Run(partTwoExample, true);
			Assert.Equal((6, 4), answer);
		}
	}
}
