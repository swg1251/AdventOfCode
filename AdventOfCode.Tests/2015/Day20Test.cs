using Xunit;

namespace AdventOfCode.Tests.Year2015
{
    public class Day20Test
    {
		[Fact]
		void Part_one_test()
		{
			var day20 = new AdventOfCode.Year2015.Day20();
			Assert.Equal(10, day20.GetPresentCount(1));
			Assert.Equal(30, day20.GetPresentCount(2));
			Assert.Equal(40, day20.GetPresentCount(3));
			Assert.Equal(70, day20.GetPresentCount(4));
			Assert.Equal(60, day20.GetPresentCount(5));
			Assert.Equal(120, day20.GetPresentCount(6));
			Assert.Equal(80, day20.GetPresentCount(7));
			Assert.Equal(150, day20.GetPresentCount(8));
			Assert.Equal(130, day20.GetPresentCount(9));
		}
    }
}
