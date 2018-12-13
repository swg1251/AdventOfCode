using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
	public class Day12Test
	{
		private IEnumerable<string> input = new List<string>
		{
			"...## => #",
			"..#.. => #",
			".#... => #",
			".#.#. => #",
			".#.## => #",
			".##.. => #",
			".#### => #",
			"#.#.# => #",
			"#.### => #",
			"##.#. => #",
			"##.## => #",
			"###.. => #",
			"###.# => #",
			"####. => #"
		};

		private string initialState = "#..#.#..##......###...###";

		[Fact]
		void Part_one_example()
		{
			var day12 = new AdventOfCode.Year2018.Day12();
			var goodInputs = day12.GetGoodInputs(input);
			var plants = day12.GetInitialState(initialState);
			var answer = day12.PartOne(goodInputs, plants);
			Assert.Equal(325, answer);
		}
	}
}
