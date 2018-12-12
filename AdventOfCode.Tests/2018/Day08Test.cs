using System;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day08Test
    {
		[Fact]
		void Sum_value_example_input()
		{
			var day08 = new AdventOfCode.Year2018.Day08
			{
				Input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2".Split(' ').Select(n => Convert.ToInt32(n)).ToList()
			};
			var root = day08.GetNode();
			var sum = root.Sum();
			var value = root.Value();

			Assert.Equal(138, sum);
			Assert.Equal(66, value);
		}
    }
}
