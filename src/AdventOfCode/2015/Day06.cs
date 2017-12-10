using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day06 : IDay
    {
		private List<List<Light>> lights;
		private IEnumerable<string> instructions;

		public Day06()
		{
			instructions = new List<string>();
			lights = new List<List<Light>>();

			for (int i = 0; i < 1000; i++)
			{
				var row = new List<Light>();
				for (int j = 0; j < 1000; j++)
				{
					row.Add(new Light());
				}
				lights.Add(row);
			}
		}

		public void GetInput()
		{
			instructions = File.ReadAllLines("2015/input/day06.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			foreach (var instruction in instructions.Select(i => i.Split(' ')))
			{
				var offset = (instruction[0] == "toggle") ? 0 : 1;
				var x1 = Convert.ToInt32(instruction[1 + offset].Split(',')[0]);
				var y1 = Convert.ToInt32(instruction[1 + offset].Split(',')[1]);
				var x2 = Convert.ToInt32(instruction[3 + offset].Split(',')[0]);
				var y2 = Convert.ToInt32(instruction[3 + offset].Split(',')[1]);

				for (int i = x1; i <= x2; i++)
				{
					for (int j = y1; j <= y2; j++)
					{
						if (instruction[0] == "toggle")
						{
							lights[i][j].IsOn = !lights[i][j].IsOn;
							lights[i][j].Brightness += 2;
						}
						else if (instruction[1] == "on")
						{
							lights[i][j].IsOn = true;
							lights[i][j].Brightness++;
						}
						else
						{
							lights[i][j].IsOn = false;
							if (lights[i][j].Brightness > 0)
							{
								lights[i][j].Brightness--;
							}
						}
					}
				}
			}

			Console.WriteLine($"The number of lights on (part one) is {lights.Sum(r => r.Count(l => l.IsOn))}");
			Console.WriteLine($"The total brightness (part two) is {lights.Sum(r => r.Sum(l => l.Brightness))}");
		}

		internal class Light
		{
			public bool IsOn { get; set; }
			public int Brightness { get; set; }
		}
    }
}
