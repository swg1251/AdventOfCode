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
			input = File.ReadAllLines("2018/input/day20.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			var map = GetMap(input);

			var result = Search(map);
			Console.WriteLine($"The shortest path to the farthest room (part one) is: {result.partOne}");
			Console.WriteLine($"The amount of room with a shortest path of over 1000 doors is: {result.partTwo}");
		}

		public Dictionary<(int x, int y), Room> GetMap(string paths)
		{
			var room = new Room();
			int x = 0, y = 0;
			var grid = new Dictionary<(int x, int y), Room>{ [(x, y)] = room };
			var positions = new Stack<(int x, int y)>();
			positions.Push((x, y));

			foreach (var c in paths.Substring(1, paths.Length - 2))
			{
				if (c == '(')
				{
					// Push current position to the stack so we can come back later
					positions.Push((x, y));
				}
				else if (c == ')')
				{
					// Done exploring options for this group, go back to beginning of grouping (pop to remove)
					var pos = positions.Pop();
					x = pos.x;
					y = pos.y;
				}
				else if (c == '|')
				{
					// Done with this option, go back to beginning of grouping (peek to preserve location)
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

		public string GetPrintableMap(Dictionary<(int x, int y), Room> map)
		{
			var minX = map.Keys.Min(k => k.x);
			var maxX = map.Keys.Max(k => k.x);
			var minY = map.Keys.Min(k => k.y);
			var maxY = map.Keys.Max(k => k.y);

			var grid = "#";
			for (int x = minX; x <= maxX; x++)
			{
				grid += "##";
			}
			grid += "\r\n";

			for (int y = minY; y <= maxY; y++)
			{
				var topLine = "#";
				var bottomLine = "#";

				for (int x = minX; x <= maxX; x++)
				{
					topLine += (x == 0 && y == 0) ? "X" : ".";
					if (!map.TryGetValue((x, y), out Room room) || room.Right == null)
					{
						topLine += '#';
					}
					else
					{
						topLine += '|';
					}

					if (room == null || room.Down == null)
					{
						bottomLine += '#';
					}
					else
					{
						bottomLine += "-";
					}

					bottomLine += "#";
				}

				grid += topLine + "\r\n";
				grid += bottomLine + "\r\n";
			}
			return grid.TrimEnd(new char[] { '\n', '\r' });
		}

		public (int partOne, int partTwo) Search(Dictionary<(int x, int y), Room> map)
		{
			var distances = new Dictionary<(int x, int y), int>();
			var state = new State
			{
				CurrentRoom = (0, 0, map[(0, 0)]),
				Seen = new HashSet<(int x, int y)> { (0, 0) }
			};

			var states = new Queue<State>();
			states.Enqueue(state);

			while (states.Any())
			{
				state = states.Dequeue();
				if (!distances.ContainsKey((state.CurrentRoom.x, state.CurrentRoom.y)))
				{
					// Haven't found a path to this room yet, min distance is current distance
					distances[((state.CurrentRoom.x, state.CurrentRoom.y))] = state.Seen.Count - 1;
				}
				else
				{
					// Min distance to this room is either current distance, or a shorter path was already found
					distances[((state.CurrentRoom.x, state.CurrentRoom.y))] =
						Math.Min(distances[((state.CurrentRoom.x, state.CurrentRoom.y))], state.Seen.Count - 1);
				}

				if (state.CurrentRoom.room.Up != null)
				{
					if (!state.Seen.Contains((state.CurrentRoom.x, state.CurrentRoom.y - 1)))
					{
						var newState = new State
						{
							CurrentRoom = (state.CurrentRoom.x, state.CurrentRoom.y - 1, state.CurrentRoom.room.Up),
							Seen = new HashSet<(int x, int y)>()
						};

						foreach (var seenPos in state.Seen)
						{
							newState.Seen.Add(seenPos);
						}
						newState.Seen.Add((state.CurrentRoom.x, state.CurrentRoom.y - 1));

						states.Enqueue(newState);
					}
				}
				if (state.CurrentRoom.room.Right != null)
				{
					if (!state.Seen.Contains((state.CurrentRoom.x + 1, state.CurrentRoom.y)))
					{
						var newState = new State
						{
							CurrentRoom = (state.CurrentRoom.x + 1, state.CurrentRoom.y, state.CurrentRoom.room.Right),
							Seen = new HashSet<(int x, int y)>()
						};

						foreach (var seenPos in state.Seen)
						{
							newState.Seen.Add(seenPos);
						}
						newState.Seen.Add((state.CurrentRoom.x + 1, state.CurrentRoom.y));

						states.Enqueue(newState);
					}
				}
				if (state.CurrentRoom.room.Down != null)
				{
					if (!state.Seen.Contains((state.CurrentRoom.x, state.CurrentRoom.y + 1)))
					{
						var newState = new State
						{
							CurrentRoom = (state.CurrentRoom.x, state.CurrentRoom.y + 1, state.CurrentRoom.room.Down),
							Seen = new HashSet<(int x, int y)>()
						};

						foreach (var seenPos in state.Seen)
						{
							newState.Seen.Add(seenPos);
						}
						newState.Seen.Add((state.CurrentRoom.x, state.CurrentRoom.y + 1));

						states.Enqueue(newState);
					}
				}
				if (state.CurrentRoom.room.Left != null)
				{
					if (!state.Seen.Contains((state.CurrentRoom.x - 1, state.CurrentRoom.y)))
					{
						var newState = new State
						{
							CurrentRoom = (state.CurrentRoom.x - 1, state.CurrentRoom.y, state.CurrentRoom.room.Left),
							Seen = new HashSet<(int x, int y)>()
						};

						foreach (var seenPos in state.Seen)
						{
							newState.Seen.Add(seenPos);
						}
						newState.Seen.Add((state.CurrentRoom.x - 1, state.CurrentRoom.y));

						states.Enqueue(newState);
					}
				}
			}

			return (distances.Values.Max(), distances.Values.Count(d => d >= 1000));
		}

		public class Room
		{
			public Room Left;
			public Room Right;
			public Room Up;
			public Room Down;
		}

		public class State
		{
			public (int x, int y, Room room) CurrentRoom;
			public HashSet<(int x, int y)> Seen;
		}
	}
}