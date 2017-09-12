using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day15 : IDay
    {
		private List<Disc> discs { get; set; }
		public void GetInput()
		{
			discs = new List<Disc>();
			foreach (var line in File.ReadAllLines("input/day15.txt").Where(l => !string.IsNullOrWhiteSpace(l)))
			{
				var words = line.Split(' ');
				discs.Add(new Disc(Convert.ToInt32(words[3]), Convert.ToInt32(words[11].Split('.')[0])));
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The first time you can press the button to get a capsule (part one) is {CheckTimes()}");

			discs.Add(new Disc(11, 0));
			Console.WriteLine($"The first time you can press the button to get a capsule (part two) is {CheckTimes()}");
		}

		private int CheckTimes()
		{
			int i = -1;
			var solved = false;
			while (!solved)
			{
				solved = true;
				i++;

				int j = 0;
				foreach (var disc in discs)
				{
					j++;

					if ((disc.InitialPosition + i + j) % disc.Positions != 0)
					{
						solved = false;
						break;
					}
				}
			}
			return i;
		}

		internal class Disc
		{
			public int Positions { get; set; }
			public int InitialPosition { get; set; }

			public Disc(int positions, int initialPosition)
			{
				Positions = positions;
				InitialPosition = initialPosition;
			}
		}
    }
}
