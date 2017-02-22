using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day07
{
    public class Day07
    {
		public List<IpAddress> IpAddressses { get; set; }
		public Day07()
		{
			IpAddressses = new List<IpAddress>();
		}

		public void Go()
		{
			GetIpAddresses();
			Console.WriteLine($"The number of IPs supporting TLS (part 1) is {IpAddressses.Count(ip => ip.SupportsTls())}");
			Console.WriteLine($"The number of IPs supporting SSL (part 2) is {IpAddressses.Count(ip => ip.SupportsSsl())}");
		}

		public void GetIpAddresses()
		{
			var inputLines = File.ReadAllLines("Day07/input.txt")
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => l.Replace("\n", ""));

			foreach (var line in inputLines)
			{
				IpAddressses.Add(IpAddress.GetFromString(line));
			}
		}
    }
}
