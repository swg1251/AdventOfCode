using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode.Year2018
{
	public class Day20 : IDay
	{
		private string input;

		public void GetInput()
		{
			input = "^ENWWW(NEEE|SSE(EE|N))$";
		}

		public void Solve()
		{
			var map = GetMap(input);
			PrintMap(map);
		}

		public Dictionary<(int x, int y), Room> GetMap(string paths)
		{
			var room = new Room();
			int x = 0, y = 0;
			var grid = new Dictionary<(int x, int y), Room>
			{
				[(x, y)] = room
			};
			var positions = new Stack<(int x, int y)>();
			positions.Push((x, y));

			foreach (var c in paths.Substring(1, paths.Length - 2))
			{
				if (c == '(')
				{
					positions.Push((x, y));
				}
				else if (c == ')')
				{
					var pos = positions.Pop();
					x = pos.x;
					y = pos.y;
				}
				else if (c == '|')
				{
					var pos = positions.Peek();
					x = pos.x;
					y = pos.y;
				}
				else
				{
					Room currentRoom;
					if (!grid.TryGetValue((x, y), out currentRoom))
					{
						currentRoom = new Room();
						grid[(x, y)] = currentRoom;
					}

					if (c == 'N')
					{
						Room adjacentRoom;
						if (!grid.TryGetValue((x, y - 1), out adjacentRoom))
						{
							adjacentRoom = new Room();
							grid[(x, y - 1)] = adjacentRoom;
						}
						currentRoom.Up = adjacentRoom;
						adjacentRoom.Down = currentRoom;
						y--;
					}
					else if (c == 'E')
					{
						Room adjacentRoom;
						if (!grid.TryGetValue((x + 1, y), out adjacentRoom))
						{
							adjacentRoom = new Room();
							grid[(x + 1, y)] = adjacentRoom;
						}
						currentRoom.Right = adjacentRoom;
						adjacentRoom.Left = currentRoom;
						x++;
					}
					else if (c == 'S')
					{
						Room adjacentRoom;
						if (!grid.TryGetValue((x, y + 1), out adjacentRoom))
						{
							adjacentRoom = new Room();
							grid[(x, y + 1)] = adjacentRoom;
						}
						currentRoom.Down = adjacentRoom;
						adjacentRoom.Up = currentRoom;
						y++;
					}
					else if (c == 'W')
					{
						Room adjacentRoom;
						if (!grid.TryGetValue((x - 1, y), out adjacentRoom))
						{
							adjacentRoom = new Room();
							grid[(x - 1, y)] = adjacentRoom;
						}
						currentRoom.Left = adjacentRoom;
						adjacentRoom.Right = currentRoom;
						x--;
					}
				}
			}
			return grid;
		}

		public void PrintMap(Dictionary<(int x, int y), Room> map)
		{
			var minX = map.Keys.Min(k => k.x);
			var maxX = map.Keys.Max(k => k.x);
			var minY = map.Keys.Min(k => k.y);
			var maxY = map.Keys.Max(k => k.y);

			var line = "#";
			for (int x = minX; x < maxX; x++)
			{
				line += "##";
			}
			line += "#";
			Console.WriteLine(line);

			for (int y = minY - 1; y < maxY + 1; y++)
			{
				var topLine = "#";
				var bottomLine = "#";

				for (int x = minX - 1; x < maxX + 1; x++)
				{
					topLine += ".";
					Room room;
					if (!map.TryGetValue((x, y), out room) || room.Right == null)
					{
						topLine += '|';
					}
					else
					{
						topLine += '#';
					}

					if (room == null || room.Down == null)
					{
						bottomLine += '#';
					}
					else
					{
						bottomLine += "-";
					}
				}

				topLine += "#";
				bottomLine += "#";

				Console.WriteLine(topLine);
				Console.WriteLine(bottomLine);
			}

			line = "#";
			for (int x = minX - 1; x < maxX + 1; x++)
			{
				line += "##";
			}
			line += "#";
			Console.WriteLine(line);
		}

		public class Room
		{
			public Room Left;
			public Room Right;
			public Room Up;
			public Room Down;
		}
	}
}