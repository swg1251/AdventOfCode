using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day07 : IDay
	{
		private List<Bag> bags;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 7);
			bags = new List<Bag>();

			foreach (var line in lines)
			{
				var lineParts = line.Split(", ");

				var firstColor = string.Concat(lineParts[0].Split(' ').Take(2));
				var firstBag = bags.FirstOrDefault(b => b.Color == firstColor);
				if (firstBag is null)
				{
					firstBag = new Bag { Color = firstColor };
					bags.Add(firstBag);
				}

				for (int i = 0; i < lineParts.Count(); i++)
				{
					var part = lineParts[i];

					if (i == 0)
					{
						part = part.Split("contain ")[1];
					}

					if (!int.TryParse(part.Split(' ')[0], out int count))
					{
						continue;
					}

					var color = string.Concat(part.Split(' ').Skip(1).Take(2));
					var bag = bags.FirstOrDefault(b => b.Color == color);
					if (bag is null)
					{
						bag = new Bag { Color = color };
						bags.Add(bag);
					}

					firstBag.ContainedBags.Add((bag, count));
				}
			}
		}

		public void Solve()
		{
			var containsGold = new HashSet<string>();

			foreach (var startingBag in bags)
			{
				if (containsGold.Contains(startingBag.Color))
				{
					continue;
				}

				var explored = new HashSet<string>();
				var queue = new Queue<Bag>();
				queue.Enqueue(startingBag);

				while (queue.Any())
				{
					var bag = queue.Dequeue();
					explored.Add(bag.Color);

					foreach (var childBag in bag.ContainedBags)
					{
						if (explored.Contains(childBag.bag.Color))
						{
							continue;
						}

						if (childBag.bag.Color == "shinygold")
						{
							containsGold.Add(startingBag.Color);
							containsGold.Add(bag.Color);
							break;
						}

						queue.Enqueue(childBag.bag);
					}
				}
			}

			Console.WriteLine($"The number of bags eventually containing gold (part one) is {containsGold.Count()}");

			var goldBag = bags.First(b => b.Color == "shinygold");
			Console.WriteLine($"The number of bags inside the gold bag (part two) is {goldBag.TotalBagsContained()}");
		}

		internal class Bag
		{
			public string Color { get; set; }

			public List<(Bag bag, int count)> ContainedBags = new List<(Bag bag, int count)>();

			public int TotalBagsContained()
			{
				var contained = ContainedBags.Sum(b => b.count);

				foreach (var bag in ContainedBags)
				{
					contained += bag.bag.TotalBagsContained() * bag.count;
				}

				return contained;
			}
		}
	}
}
