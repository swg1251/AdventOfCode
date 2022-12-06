using System;
using System.Collections.Generic;
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
			var potentialMarker = new Queue<char>(datastream.Substring(0, markerLength));
			for (int i = markerLength; i < datastream.Length; i++)
			{
				if (potentialMarker.Distinct().Count() == markerLength)
				{
					return i;
				}
				potentialMarker.Dequeue();
				potentialMarker.Enqueue(datastream[i]);
			}
			throw new Exception($"Failed to find marker of length {markerLength}");
		}
	}
}
