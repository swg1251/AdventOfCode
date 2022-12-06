using System;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day06 : IDay
	{
		private string input;

		public void GetInput()
		{
			input = InputHelper.GetStringsFromInput(2022, 6).Single();
		}

		public void Solve()
		{
			Console.WriteLine($"The first start-of-packet marker (part one) occurs after {GetFirstMarkerIndex(input, 4)} characters");
			Console.WriteLine($"The first start-of-message marker (part two) occurs after {GetFirstMarkerIndex(input, 14)} characters");
		}

		private int GetFirstMarkerIndex(string datastream, int markerLength)
		{
			for (int i = 0; i < datastream.Length + markerLength; i++)
			{
				var potentialMarker = datastream.Substring(i, markerLength);
				if (potentialMarker.Distinct().Count() == markerLength)
				{
					return i + markerLength;
				}
			}
			throw new Exception($"Failed to find marker of length {markerLength}");
		}
	}
}
