using Xunit;

namespace AdventOfCode.Tests.Year2018
{
    public class Day09Test
    {
		[Fact]
		void Score_10_players_1618_points()
		{
			var day09 = new AdventOfCode.Year2018.Day09();
			var score = day09.GetMaxScore(10, 1618);
			Assert.Equal(8317, score);
		}

		[Fact]
		void Score_13_players_7999_points()
		{
			var day09 = new AdventOfCode.Year2018.Day09();
			var score = day09.GetMaxScore(13, 7999);
			Assert.Equal(146373, score);
		}

		[Fact]
		void Score_17_players_1104_points()
		{
			var day09 = new AdventOfCode.Year2018.Day09();
			var score = day09.GetMaxScore(17, 1104);
			Assert.Equal(2764, score);
		}

		[Fact]
		void Score_21_players_6111_points()
		{
			var day09 = new AdventOfCode.Year2018.Day09();
			var score = day09.GetMaxScore(21, 6111);
			Assert.Equal(54718, score);
		}

		[Fact]
		void Score_30_players_5807_points()
		{
			var day09 = new AdventOfCode.Year2018.Day09();
			var score = day09.GetMaxScore(30, 5807);
			Assert.Equal(37305, score);
		}
	}
}
