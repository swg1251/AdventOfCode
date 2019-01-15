using Xunit;

namespace AdventOfCode.Tests.Year2018
{
	public class Day20Test
	{
		private readonly string exampleOneMap =
@"#########
#.|.|.|.#
#-#######
#.|.|.|.#
#-#####-#
#.#.#X|.#
#-#-#####
#.|.|.|.#
#########";


		private readonly string exampleTwoMap =
@"###########
#.|.#.|.#.#
#-###-#-#-#
#.|.|.#.#.#
#-#####-#-#
#.#.#X|.#.#
#-#-#####-#
#.#.|.|.|.#
#-###-###-#
#.|.|.#.|.#
###########";

		private readonly string exampleThreeMap =
@"#############
#.|.|.|.|.|.#
#-#####-###-#
#.#.|.#.#.#.#
#-#-###-#-#-#
#.#.#.|.#.|.#
#-#-#-#####-#
#.#.#.#X|.#.#
#-#-#-###-#-#
#.|.#.|.#.#.#
###-#-###-#-#
#.|.#.|.|.#.#
#############";

		private readonly string exampleFourMap =
@"###############
#.|.|.|.#.|.|.#
#-###-###-#-#-#
#.|.#.|.|.#.#.#
#-#########-#-#
#.#.|.|.|.|.#.#
#-#-#########-#
#.#.#.|X#.|.#.#
###-#-###-#-#-#
#.|.#.#.|.#.|.#
#-###-#####-###
#.|.#.|.|.#.#.#
#-#-#####-#-#-#
#.#.|.|.|.#.|.#
###############";

		[Fact]
		void Example_one()
		{
			var day20 = new AdventOfCode.Year2018.Day20();
			var map = day20.GetMap("^ENWWW(NEEE|SSE(EE|N))$");
			var stringMap = day20.GetPrintableMap(map);
			var result = day20.Search(map);
			Assert.Equal(exampleOneMap, stringMap);
			Assert.Equal(10, result.partOne);
		}

		[Fact]
		void Example_two()
		{
			var day20 = new AdventOfCode.Year2018.Day20();
			var map = day20.GetMap("^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$");
			var stringMap = day20.GetPrintableMap(map);
			var result = day20.Search(map);
			Assert.Equal(exampleTwoMap, stringMap);
			Assert.Equal(18, result.partOne);
		}

		[Fact]
		void Example_three()
		{
			var day20 = new AdventOfCode.Year2018.Day20();
			var map = day20.GetMap("^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$");
			var stringMap = day20.GetPrintableMap(map);
			var result = day20.Search(map);
			Assert.Equal(exampleThreeMap, stringMap);
			Assert.Equal(23, result.partOne);
		}

		[Fact]
		void Example_four()
		{
			var day20 = new AdventOfCode.Year2018.Day20();
			var map = day20.GetMap("^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$");
			var stringMap = day20.GetPrintableMap(map);
			var result = day20.Search(map);
			Assert.Equal(exampleFourMap, stringMap);
			Assert.Equal(31, result.partOne);
		}
	}
}