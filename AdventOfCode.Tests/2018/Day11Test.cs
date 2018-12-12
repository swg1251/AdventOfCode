using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day11Test
    {
		[Fact]
		void Get_power_level_3_5_8_returns_4()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var powerLevel = day11.GetPowerLevel(3, 5, 8);
			Assert.Equal(4, powerLevel);
		}

		[Fact]
		void Get_power_level_122_79_57_returns_neg5()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var powerLevel = day11.GetPowerLevel(122, 79, 57);
			Assert.Equal(-5, powerLevel);
		}

		[Fact]
		void Get_power_level_217_196_39_returns_0()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var powerLevel = day11.GetPowerLevel(217, 196, 39);
			Assert.Equal(0, powerLevel);
		}

		[Fact]
		void Get_power_level_101_153_71_returns_4()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var powerLevel = day11.GetPowerLevel(101, 153, 71);
			Assert.Equal(4, powerLevel);
		}

		[Fact]
		void Part_one_18()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var answer = day11.PartOne(18);
			Assert.Equal((33, 45, 29), answer);
		}

		[Fact]
		void Part_one_42()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var answer = day11.PartOne(42);
			Assert.Equal((21, 61, 30), answer);
		}

		[Fact(Skip = "Takes minutes to run")]
		void Part_two_18()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var answer = day11.PartTwo(18);
			Assert.Equal((90, 269, 113, 16), answer);
		}

		[Fact(Skip = "Takes minutes to run")]
		void Part_two_42()
		{
			var day11 = new AdventOfCode.Year2018.Day11();
			var answer = day11.PartTwo(42);
			Assert.Equal((232, 251, 119, 12), answer);
		}
	}
}
