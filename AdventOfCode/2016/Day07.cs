using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day07 : IDay
    {
		private List<IpAddress> ipAddressses { get; set; }
		public Day07()
		{
			ipAddressses = new List<IpAddress>();
		}

        public void GetInput()
        {
            var inputLines = File.ReadAllLines("2016/input/day07.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Replace("\n", ""));

            foreach (var line in inputLines)
            {
                ipAddressses.Add(IpAddress.GetFromString(line));
            }
        }

        public void Solve()
		{
			Console.WriteLine($"The number of IPs supporting TLS (part one) is {ipAddressses.Count(ip => ip.SupportsTls())}");
			Console.WriteLine($"The number of IPs supporting SSL (part two) is {ipAddressses.Count(ip => ip.SupportsSsl())}");
		}

        internal class IpAddress
        {
            public List<string> Supernets { get; set; }
            public List<string> Hypernets { get; set; }

            public IpAddress()
            {
                Supernets = new List<string>();
                Hypernets = new List<string>();
            }

            public static IpAddress GetFromString(string addressString)
            {
                var ipAddress = new IpAddress();
                var currentSupernet = "";
                var currentHypernet = "";
                int i = 0;
                while (i < addressString.Length)
                {
                    while (i < addressString.Length && addressString[i] != '[')
                    {
                        currentSupernet += addressString[i];
                        i++;
                    }
                    if (currentSupernet != "")
                    {
                        ipAddress.Supernets.Add(currentSupernet);
                        currentSupernet = "";
                    }

                    i++;

                    while (i < addressString.Length && addressString[i] != ']')
                    {
                        currentHypernet += addressString[i];
                        i++;
                    }
                    if (currentHypernet != "")
                    {
                        ipAddress.Hypernets.Add(currentHypernet);
                        currentHypernet = "";
                    }

                    i++;
                }
                return ipAddress;
            }

            public bool HasAbba(string sequence)
            {
                for (int i = 0; i < sequence.Length - 3; i++)
                {
                    if (sequence[i] != sequence[i + 1] &&
                        sequence[i] == sequence[i + 3] &&
                        sequence[i + 1] == sequence[i + 2])
                    {
                        return true;
                    }
                }
                return false;
            }

            public List<string> GetAbas(List<string> sequences)
            {
                var abas = new List<string>();
                foreach (var sequence in sequences)
                {
                    for (int i = 0; i < sequence.Length - 2; i++)
                    {
                        if (sequence[i] != sequence[i + 1] && sequence[i] == sequence[i + 2])
                        {
                            abas.Add(sequence.Substring(i, 3));
                        }
                    }
                }
                return abas;
            }

            public string GetBab(string aba)
            {
                if (aba.Length != 3)
                {
                    throw new Exception("Incorrect string length in GetBab - Aba length must be 3.");
                }
                return aba[1].ToString() + aba[0].ToString() + aba[1].ToString();
            }

            public bool SupportsTls()
            {
                return Supernets.Any(s => HasAbba(s)) && !Hypernets.Any(s => HasAbba(s));
            }

            public bool SupportsSsl()
            {
                var supernetAbas = GetAbas(Supernets);
                var hypernetAbas = GetAbas(Hypernets);
                foreach (var aba in supernetAbas)
                {
                    if (hypernetAbas.Contains(GetBab(aba)))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
