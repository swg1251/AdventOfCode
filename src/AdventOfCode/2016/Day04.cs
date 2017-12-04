using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day04 : IDay
    {
		private List<Room> rooms;

		public Day04()
		{
			rooms = new List<Room>();
		}

        public void GetInput()
        {
            var roomsInput = File.ReadAllLines("2016/input/day04.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Replace("\n", ""));
            foreach (var room in roomsInput)
            {
                var splitRoom = room.Replace("]", "").Split('[');
                var checksum = splitRoom[1];
                var sections = splitRoom[0].Split('-');
                var sectorID = sections[sections.Length - 1];
                var name = splitRoom[0].Replace(sectorID, "").Replace("-", "");
                rooms.Add(new Room { Name = name, SectorID = Convert.ToInt32(sectorID), Checksum = checksum });
            }
        }

        public void Solve()
		{
			var validRoomsSectorSum = rooms.Where(r => r.IsValid()).Sum(r => r.SectorID);
			Console.WriteLine($"The sum of valid sector IDs (part 1) is: {validRoomsSectorSum}");

			GetDecryptedRoomNames();
			var northPoleStorageRoom = rooms.Single(rn => rn.DecryptedName == "northpoleobjectstorage");
			Console.WriteLine($"The sector ID of the \"northpoleobjectstorage\" room (part 2) is: {northPoleStorageRoom.SectorID}");
		}

		

		private void GetDecryptedRoomNames()
		{
			foreach (var room in rooms)
			{
				room.GetDecryptedName();
			}
		}

        internal class Room
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
                    : (char)(currentChar + 1);
            }
        }
    }
}
