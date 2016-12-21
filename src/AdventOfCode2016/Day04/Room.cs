using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day04
{
    public class Room
    {
		public string Name { get; set; }
		public int SectorID { get; set; }
		public string Checksum { get; set; }
		public string DecryptedName { get; set; }

		public bool IsValid()
		{
			var letterCounts = new Dictionary<char, int>();
			for (int i = 0; i < Name.Length; i++)
			{
				if (letterCounts.ContainsKey(Name[i]))
				{
					letterCounts[Name[i]]++;
				}
				else
				{
					letterCounts[Name[i]] = 1;
				}
			}

			var checksumChars = "";
			foreach (var character in letterCounts.OrderByDescending(l => l.Value).ThenBy(l => l.Key).Take(5))
			{
				checksumChars += character.Key;
			}

			return checksumChars == Checksum;
		}
		
		public void GetDecryptedName()
		{
			var decryptedName = "";
			for (int i = 0; i < Name.Length; i++)
			{
				var currentChar = Name[i];
				for (int j = 0; j < SectorID; j++)
				{
					currentChar = GetNextChar(currentChar);
				}
				decryptedName += currentChar;
			}
			DecryptedName = decryptedName;
		}

		private char GetNextChar(char currentChar)
		{
			return currentChar == 'z'
				? 'a'
				: (char) (currentChar + 1);
		}
    }
}
