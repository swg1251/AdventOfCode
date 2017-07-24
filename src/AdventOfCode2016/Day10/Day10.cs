using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day10
{
    public class Day10 : IDay
    {
		private List<Bot> Bots { get; set; }
		private List<Bin> Bins { get; set; }
		private List<string> Processed { get; set; }

		public void Go()
		{
			Bots = new List<Bot>();
			Bins = new List<Bin>();
			for (int i = 0; i < 1000; i++)
			{
				Bots.Add(new Bot { Id = i, Chips = new List<int>() });
				Bins.Add(new Bin { Id = i, Chips = new List<int>() });
			}
			Processed = new List<string>();

			var instructions = File.ReadAllLines("Day10/input.txt").Where(l => !string.IsNullOrWhiteSpace(l));

			// loop until all instructions have been "processed"
			while (Processed.Count < instructions.Count())
			{
				foreach (var instruction in instructions.Where(i => !Processed.Contains(i)))
				{
					if (instruction.StartsWith("value"))
					{
						GiveValueToBot(instruction);
					}
					else
					{
						DistributeChips(instruction);
					}
				}
			}

			var product = Bins.First(b => b.Id == 0).Chips[0] * 
							Bins.First(b => b.Id == 1).Chips[0] * 
							Bins.First(b => b.Id == 2).Chips[0];
			Console.WriteLine($"The product of the first chip in bins 0, 1, 2 is {product} (part 2)");
		}

		private void GiveValueToBot(string instruction)
		{
			var instructionParts = instruction.Split(' ');
			var bot = Bots.First(b => b.Id == Convert.ToInt32(instructionParts[5]));
			bot.Chips.Add(Convert.ToInt32(instructionParts[1]));
			Processed.Add(instruction);
		}

		private void DistributeChips(string instruction)
		{
			var instructionParts = instruction.Split(' ');

			var bot = Bots.First(b => b.Id == Convert.ToInt32(instructionParts[1]));

			// only distribute chips if the bot has two
			if (bot.Chips.Count == 2)
			{
				var lowChip = bot.Chips.Min();
				var highChip = bot.Chips.Max();

				if (lowChip == 17 && highChip == 61)
				{
					Console.WriteLine($"Bot {bot.Id} compares chips 17 and 61 (part 1)");
				}

				if (instructionParts[5] == "bot")
				{
					var lowBot = Bots.First(b => b.Id == Convert.ToInt32(instructionParts[6]));
					lowBot.Chips.Add(lowChip);
				}
				else
				{
					var lowBin = Bins.First(b => b.Id == Convert.ToInt32(instructionParts[6]));
					lowBin.Chips.Add(lowChip);
				}
				bot.Chips.Remove(lowChip);

				if (instructionParts[10] == "bot")
				{
					var highBot = Bots.First(b => b.Id == Convert.ToInt32(instructionParts[11]));
					highBot.Chips.Add(highChip);
				}
				else
				{
					var highBin = Bins.First(b => b.Id == Convert.ToInt32(instructionParts[11]));
					highBin.Chips.Add(highChip);
				}
				bot.Chips.Remove(highChip);

				Processed.Add(instruction);
			}
		}
    }

	public class Bot
	{
		public int Id { get; set; }
		public List<int> Chips { get; set; }
	}

	public class Bin
	{
		public int Id { get; set; }
		public List<int> Chips { get; set; }
	}
}
