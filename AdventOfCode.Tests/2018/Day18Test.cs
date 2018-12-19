using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
	public class Day18Test
	{
		private IEnumerable<string> input = new List<string>
		{
			".#.#...|#.",
			".....#|##|",
			".|..|...#.",
			"..|#.....#",
			"#.#|||#|#|",
			"...#.||...",
			".|....|...",
			"||...#|.#|",
			"|.||||..|.",
			"...#.|..|."
		};

		[Fact]
		void Part_one_example()
		{
			var day18 = new AdventOfCode.Year2018.Day18();
			var acres = day18.GetAcres(input);
			var answer = day18.GetResourceValue(acres, 10);
			Assert.Equal(1147, answer);
		}
	}
}
