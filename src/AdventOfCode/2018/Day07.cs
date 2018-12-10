using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day07 : IDay
	{
		private Dictionary<char, List<char>> initialSteps;

		public void GetInput()
		{
			var input = File.ReadAllLines("2018/input/day07.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
			initialSteps = new Dictionary<char, List<char>>();
			foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
			{
				initialSteps[c] = new List<char>();
			}

			foreach (var lineParts in input.Select(l => l.Split(' ')))
			{
				var targetStep = lineParts[7][0];
				var predecessor = lineParts[1][0];
				initialSteps[targetStep].Add(predecessor);
			}
		}

		public void Solve()
		{
			Console.WriteLine($"The order of the steps (part one) is: {GetOrderAndTime(initialSteps).order}");
			GetInput();
			Console.WriteLine($"The order of the steps with 5 workers (part two) is: {GetOrderAndTime(initialSteps, 5).time}");
		}

		private (string order, int time) GetOrderAndTime(Dictionary<char, List<char>> steps, int workerCount = 1)
		{
			var order = "";
			var stepCount = steps.Count;
			var stepsInProgress = new List<Step>();
			var totalTime = 0;

			while (order.Length < stepCount)
			{
				// anything with no predecessors is available
				var availableSteps = steps.Where(s => !s.Value.Any());
				availableSteps = availableSteps.OrderBy(s => s.Key).ToList();

				foreach (var step in availableSteps)
				{
					if (stepsInProgress.Count(c => !c.Done) > workerCount - 1)
					{
						continue;
					}

					steps.Remove(step.Key);
					stepsInProgress.Add(new Step
					{
						Id = step.Key,
						WorkDone = 0
					});
				}

				foreach (var ipStep in stepsInProgress)
				{
					if (ipStep.Done)
					{
						continue;
					}

					ipStep.WorkDone++;

					// A == 65
					if (ipStep.WorkDone == 60 + ipStep.Id - 64)
					{
						ipStep.Done = true;
						foreach (var step in steps)
						{
							step.Value.Remove(ipStep.Id);
						}

						order += ipStep.Id;
					}
				}

				totalTime++;
			}

			return (order, totalTime);
		}

		internal class Step
		{
			public char Id { get; set; }

			public int WorkDone { get; set; }

			public bool Done { get; set; }
		}
	}
}
