using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day10 : IDay
    {
		private List<Bot> bots { get; set; }
		private List<Bin> bins { get; set; }
		private List<string> processed { get; set; }
        private IEnumerable<string> instructions { get; set; }

        public void GetInput()
        {
            instructions = File.ReadAllLines("input/day10.txt").Where(l => !string.IsNullOrWhiteSpace(l));
        }

		public void Solve()
		{
			bots = new List<Bot>();
			bins = new List<Bin>();
			for (int i = 0; i < 1000; i++)
			{
				bots.Add(new Bot { Id = i, Chips = new List<int>() });
				bins.Add(new Bin { Id = i, Chips = new List<int>() });
			}
			processed = new List<string>();

			

			// loop until all instructions have been "processed"
			while (processed.Count < instructions.Count())
			{
				foreach (var instruction in instructions.Where(i => !processed.Contains(i)))
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

			var product = bins.First(b => b.Id == 0).Chips[0] * 
							bins.First(b => b.Id == 1).Chips[0] * 
							bins.First(b => b.Id == 2).Chips[0];
			Console.WriteLine($"The product of the first chip in bins 0, 1, 2 is {product} (part 2)");
		}

		private void GiveValueToBot(string instruction)
		{
			var instructionParts = instruction.Split(' ');
			var bot = bots.First(b => b.Id == Convert.ToInt32(instructionParts[5]));
			bot.Chips.Add(Convert.ToInt32(instructionParts[1]));
			processed.Add(instruction);
		}

		private void DistributeChips(string instruction)
		{
			var instructionParts = instruction.Split(' ');

			var bot = bots.First(b => b.Id == Convert.ToInt32(instructionParts[1]));

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
					var lowBot = bots.First(b => b.Id == Convert.ToInt32(instructionParts[6]));
					lowBot.Chips.Add(lowChip);
				}
				else
				{
					var lowBin = bins.First(b => b.Id == Convert.ToInt32(instructionParts[6]));
					lowBin.Chips.Add(lowChip);
				}
				bot.Chips.Remove(lowChip);

				if (instructionParts[10] == "bot")
				{
					var highBot = bots.First(b => b.Id == Convert.ToInt32(instructionParts[11]));
					highBot.Chips.Add(highChip);
				}
				else
				{
					var highBin = bins.First(b => b.Id == Convert.ToInt32(instructionParts[11]));
					highBin.Chips.Add(highChip);
				}
				bot.Chips.Remove(highChip);

				processed.Add(instruction);
			}
		}

        internal class Bot
        {
            public int Id { get; set; }
            public List<int> Chips { get; set; }
        }

        internal class Bin
        {
            public int Id { get; set; }
            public List<int> Chips { get; set; }
        }
    }
}
