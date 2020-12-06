using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day06 : IDay
	{
		private List<Form> forms;

		public void GetInput()
		{
			forms = new List<Form>();
			var lines = File.ReadAllLines("2020/input/day06.txt");
			var form = new Form();
			var personCount = 0;

			foreach (var line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					form.PersonCount = personCount;
					forms.Add(form);
					form = new Form();
					personCount = 0;
					continue;
				}

				personCount++;

				foreach (var c in line)
				{
					if (!form.Answers.ContainsKey(c))
					{
						form.Answers[c] = 0;
					}
					form.Answers[c]++;
				}
			}

			// make sure the last one gets added
			form.PersonCount = personCount;
			forms.Add(form);
		}

		public void Solve()
		{
			Console.WriteLine($"The sum of all questions anyone answered (part one) is {forms.Sum(f => f.Answers.Keys.Count)}");
			Console.WriteLine($"The sum of all questions everyone answered (part two) is {forms.Sum(f => f.AnsweredByAll)}");
		}

		internal class Form
		{
			public Dictionary<char, int> Answers = new Dictionary<char, int>();
			public int PersonCount { get; set; }
			public int AnsweredByAll => Answers.Values.Count(v => v == PersonCount);
		}
	}
}
