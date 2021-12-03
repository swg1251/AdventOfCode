using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day09 : IDay
	{
		private const int preambleLength = 25;
		private List<long> numbers;

		public void GetInput()
		{
			numbers = InputHelper.GetLongIntegersFromInput(2020, 9);
		}

		public void Solve()
		{
			var invalidNumber = GetInvalidNumber();
			Console.WriteLine($"The first invalid number (part one) is {invalidNumber}");
			Console.WriteLine($"The encryption weakness (part two) is {GetWeakness(invalidNumber)}");
		}

		private long GetInvalidNumber()
		{
			for (int i = 0; i < numbers.Count; i++)
			{
				var target = numbers[i + preambleLength];
				var previousNumbers = numbers.Skip(i).Take(preambleLength).ToList();

				if (!IsValid(target, previousNumbers))
				{
					return target;
				}
			}

			return -1;
		}

		private long GetWeakness(long invalidNumber)
		{
			for (int i = 0; i < numbers.Count - 1; i++)
			{
				var sum = 0L;
				var length = 2;

				while (sum < invalidNumber)
				{
					var sequence = numbers.Skip(i).Take(length);

					sum = sequence.Sum();
					if (sum == invalidNumber)
					{
						return sequence.Min() + sequence.Max();
					}

					length++;
				}
			}

			return -1;
		}

		private bool IsValid(long target, List<long> previousNumbers)
		{
			for (int i = 0; i < preambleLength; i++)
			{
				for (int j = i + 1; j < preambleLength; j++)
				{
					if (previousNumbers[i] + previousNumbers[j] == target)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
