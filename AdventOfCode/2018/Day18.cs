using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day18 : IDay
	{
		private IEnumerable<string> input;

		public void GetInput()
		{
			input = File.ReadAllLines("2018/input/day18.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			var acres = GetAcres(input);
			Console.WriteLine($"After 10 generations (part one), the score is {GetResourceValue(acres, 10)}");
			Console.WriteLine($"After 1 billion generations (part two), the score is {GetResourceValue(acres, 1000000000)}");
		}

		public int GetResourceValue(List<List<Acre>> acres, long generations)
		{
			// track states we've seen so we can find a pattern for part two
			var seenPatterns = new List<string>();
			var cycleFound = false;

			for (long i = 0; i < generations; i++)
			{
				var newAcres = new List<List<Acre>>();
				var acreString = string.Empty;

				for (int y = 0; y < acres.Count; y++)
				{
					var newRow = new List<Acre>();
					var rowString = string.Empty;

					for (int x = 0; x < acres[y].Count; x++)
					{
						var neighbors = new Dictionary<Acre, int>
						{
							{ Acre.Open, 0 },
							{ Acre.Trees, 0 },
							{ Acre.Lumberyard, 0 }
						};

						// up-left
						if (y > 0 && x > 0)
						{
							neighbors[acres[y - 1][x - 1]]++;
						}
						// up
						if (y > 0)
						{
							neighbors[acres[y - 1][x]]++;
						}
						// up-right
						if (y > 0 && x < acres[y].Count - 1)
						{
							neighbors[acres[y - 1][x + 1]]++;
						}
						// left
						if (x > 0)
						{
							neighbors[acres[y][x - 1]]++;
						}
						// right
						if (x < acres[y].Count - 1)
						{
							neighbors[acres[y][x + 1]]++;
						}
						// down-left
						if (y < acres.Count - 1 && x > 0)
						{
							neighbors[acres[y + 1][x - 1]]++;
						}
						// down
						if (y < acres.Count - 1)
						{
							neighbors[acres[y + 1][x]]++;
						}
						// down-right
						if (y < acres.Count - 1 && x < acres[y].Count - 1)
						{
							neighbors[acres[y + 1][x + 1]]++;
						}

						if (acres[y][x] == Acre.Open)
						{
							if (neighbors[Acre.Trees] >= 3)
							{
								newRow.Add(Acre.Trees);
							}
							else
							{
								newRow.Add(Acre.Open);
							}
						}
						else if (acres[y][x] == Acre.Trees)
						{
							if (neighbors[Acre.Lumberyard] >= 3)
							{
								newRow.Add(Acre.Lumberyard);
							}
							else
							{
								newRow.Add(Acre.Trees);
							}
						}
						else
						{
							if (neighbors[Acre.Lumberyard] >= 1 && neighbors[Acre.Trees] >= 1)
							{
								newRow.Add(Acre.Lumberyard);
							}
							else
							{
								newRow.Add(Acre.Open);
							}
						}
					}

					newAcres.Add(newRow);
					acreString += string.Join("", newRow.Select(a => a.ToString()));
				}

				acres = newAcres;

				// we've found a cycle
				if (seenPatterns.Contains(acreString) && !cycleFound)
				{
					cycleFound = true;
					var startIndex = seenPatterns.IndexOf(acreString);
					var cycleSize = i - startIndex;

					// advance until the final cycle
					while (i < 1000000000 - cycleSize)
					{
						i += cycleSize;
					}
				}
				else
				{
					seenPatterns.Add(acreString);
				}
			}

			var trees = acres.Sum(r => r.Count(a => a == Acre.Trees));
			var lumber = acres.Sum(r => r.Count(a => a == Acre.Lumberyard));
			return trees * lumber;
		}

		public List<List<Acre>> GetAcres(IEnumerable<string> input)
		{
			var acres = new List<List<Acre>>();

			foreach (var line in input)
			{
				var row = new List<Acre>();
				foreach (var acre in line)
				{
					row.Add(acre == '.' ? Acre.Open : acre == '|' ? Acre.Trees : Acre.Lumberyard);
				}
				acres.Add(row);
			}
			return acres;
		}

		private void Print(List<List<Acre>> acres)
		{
			for (int y = 0; y < acres.Count; y++)
			{
				var row = "";
				for (int x = 0; x < acres[y].Count; x++)
				{
					var c = acres[y][x] == Acre.Open ? '.' : acres[y][x] == Acre.Trees ? '|' : '#';
					row += c;
				}
				Console.WriteLine(row);
			}
			Console.WriteLine();
		}

		public enum Acre
		{
			Open = 0,
			Trees = 1,
			Lumberyard = 2
		}
	}
}
