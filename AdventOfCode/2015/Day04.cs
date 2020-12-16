using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day04 : IDay
    {
		private string input;

		public void GetInput()
		{
			input = File.ReadAllLines("2015/input/day04.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			var partOneDone = false;
			var i = 0;

			using (var md5 = MD5.Create())
			{
				while (true)
				{
					i++;
					var seed = input + i.ToString();
					var hash = Hash(md5, seed);

					if (hash.StartsWith("00000"))
					{
						if (!partOneDone)
						{
							partOneDone = true;
							Console.WriteLine($"The first number which produces a 5 zero-leading hash (part one) is {i}");
						}

						if (hash.StartsWith("000000"))
						{
							Console.WriteLine($"The first number which produces a 6 zero-leading hash (part two) is {i}");
							return;
						}
					}
				}
			}
		}

		private static string Hash(MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}
	}
}
