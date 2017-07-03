using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day04
{
    public class Day04 : IDay
    {
		private List<Room> rooms;

		public Day04()
		{
			rooms = new List<Room>();
		}

		public void Go()
		{
			GetRooms();
			var validRoomsSectorSum = rooms.Where(r => r.IsValid()).Sum(r => r.SectorID);
			Console.WriteLine($"The sum of valid sector IDs (part 1) is: {validRoomsSectorSum}");

			GetDecryptedRoomNames();
			var northPoleStorageRoom = rooms.Single(rn => rn.DecryptedName == "northpoleobjectstorage");
			Console.WriteLine($"The sector ID of the \"northpoleobjectstorage\" room (part 2) is: {northPoleStorageRoom.SectorID}");
		}

		private void GetRooms()
		{
			var roomsInput = File.ReadAllLines("Day04/input.txt")
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

		private void GetDecryptedRoomNames()
		{
			foreach (var room in rooms)
			{
				room.GetDecryptedName();
			}
		}
    }
}
