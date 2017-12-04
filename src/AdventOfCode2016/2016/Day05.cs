using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day05 : IDay
    {
		private string input;
		private string password;
		private char[] password2;

		public Day05()
		{
			password = "";
			password2 = new char[8];
		}

        public void GetInput()
        {
            input = File.ReadAllLines("2016/input/day05.txt").Where(l => !string.IsNullOrEmpty(l)).First();
        }

		public void Solve()
		{
			GetPasswords();
			Console.WriteLine($"The password (part 1) is: {password.Substring(0, 8)}");
			Console.WriteLine($"The password (part 2) is: {new string(password2)}");
		}

		private void GetPasswords()
		{
			int i = -1;
			var currentInput = "";
			var charsInPassword2 = 0;
			using (var md5 = MD5.Create())
			{
				while (password.Length < 8 || charsInPassword2 < 8)
				{
					i++;
					currentInput = input + i.ToString();

					var hash = GetMd5Hash(md5, currentInput);
					if (hash != null)
					{
						password += hash[5];

						int index;
						if (int.TryParse(hash[5].ToString(), out index) && index < 8 && password2[index] == 0)
						{
							password2[index] = hash[6];
							charsInPassword2++;
						}
					}
				}
			}
		}

		// Adapted from MSDN - we only need 4 bytes (8 chars) of the hash
		// Return null as soon as we encounter a non-zero in the first 5 chars
		// https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
		static string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			if (data[0] != 0 || data[1] != 0)
			{
				return null;
			}
			var chars2 = data[2].ToString("x2");
			if (chars2[0] != '0')
			{
				return null;
			}
			return data[0].ToString("x2") + data[1].ToString("x2") + data[2].ToString("x2") + data[3].ToString("x2");
		}
	}
}
