﻿using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
	public class Day01 : IDay
	{
		private List<int> expenses;

		public void GetInput()
		{
			expenses = InputHelper.GetIntegersFromInput(2020, 1);
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			for (int i = 0; i < expenses.Count; i++)
			{
				for (int j = 0; j < expenses.Count; j++)
				{
					if (i == j)
					{
						continue;
					}

					if (expenses[i] + expenses[j] == 2020)
					{
						var product = expenses[i] * expenses[j];
						Console.WriteLine($"Product of the two entries summing 2020 (part one) is: {product}");
						return;
					}
				}
			}
		}

		private void PartTwo()
		{
			for (int i = 0; i < expenses.Count; i++)
			{
				for (int j = 0; j < expenses.Count; j++)
				{
					for (int k = 0; k < expenses.Count; k++)
					{
						if (i == j || i == k || j == k)
						{
							continue;
						}

						if (expenses[i] + expenses[j] + expenses[k] == 2020)
						{
							var product = expenses[i] * expenses[j] * expenses[k];
							Console.WriteLine($"Product of the three entries summing 2020 (part two) is: {product}");
							return;
						}
					}
				}
			}
		}
	}
}
