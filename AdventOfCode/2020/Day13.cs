using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
	public class Day13 : IDay
	{
		private long timestamp;
		private List<Bus> buses;

		public void GetInput()
		{
			var lines = InputHelper.GetStringsFromInput(2020, 13);
			timestamp = Convert.ToInt32(lines[0]);

			buses = new List<Bus>();

			var busStrings = lines[1].Split(',');
			for (int i = 0; i < busStrings.Count(); i++)
			{
				if (int.TryParse(busStrings[i], out int busId))
				{
					buses.Add(new Bus { Id = busId, Index = i });
				}
			}
		}

		public void Solve()
		{
			var min = buses.Min(b => b.TimeAfterDesired(timestamp));
			var minBus = buses.First(b => b.TimeAfterDesired(timestamp) == min);
			Console.WriteLine($"The product of bus IDs and time to wait (part one) is {minBus.Id * min}");

			// start with first bus (will be least common multiple)
			buses = buses.OrderBy(b => b.Id).ToList();
			var time = 1L;
			var increment = 1L;

			foreach (var bus in buses)
			{
				// time + offset (bus index) must be divisible by bus id
				while ((time + bus.Index) % bus.Id != 0)
				{
					time += increment;
				}

				// increase LCM
				increment *= bus.Id;
			}

			Console.WriteLine($"The first valid time (part two) is {time}");
		}

		internal class Bus
		{
			public int Id { get; set; }

			public int Index { get; set; }

			public long TimeAfterDesired(long timestamp)
			{
				var time = 0L;
				while (time < timestamp)
				{
					time += Id;
				}
				return time - timestamp;
			}
		}
	}
}
