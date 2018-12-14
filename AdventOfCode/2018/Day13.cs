using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day13 : IDay
	{
		private List<string> inputGrid;

		public void GetInput()
		{
			inputGrid = File.ReadAllLines("2018/input/day13.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
		}

		public void Solve()
		{
			var partOne = Run(inputGrid);
			Console.WriteLine($"The first collision happens at (part one): {partOne}");

			GetInput();
			var partTwo = Run(inputGrid, true);
			Console.WriteLine($"The last surviving cart is at (part two): {partTwo}");
		}

		public (int x, int y) Run(List<string> grid, bool partTwo = false)
		{
			var carts = GetCarts(grid);

			while (true)
			{
				foreach (var cart in carts.OrderBy(c => c.Y).ThenBy(c => c.X))
				{
					if (cart.Crashed)
					{
						continue;
					}

					cart.Move();

					// collision
					if (carts.Count(c => !c.Crashed && c.X == cart.X && c.Y == cart.Y) > 1)
					{
						if (!partTwo)
						{
							return (cart.X, cart.Y);
						}
						foreach (var crashedCart in carts.Where(c => c.X == cart.X && c.Y == cart.Y))
						{
							crashedCart.Crashed = true;
						}
					}

					switch (grid[cart.Y][cart.X])
					{
						case '/':
							switch (cart.Direction)
							{
								case Direction.Up:
								case Direction.Down:
									cart.TurnRight();
									break;
								case Direction.Left:
								case Direction.Right:
									cart.TurnLeft();
									break;
							}
							break;
						case '\\':
							switch (cart.Direction)
							{
								case Direction.Up:
								case Direction.Down:
									cart.TurnLeft();
									break;
								case Direction.Left:
								case Direction.Right:
									cart.TurnRight();
									break;
							}
							break;
						case '+':
							switch (cart.NextTurn)
							{
								case Direction.Straight:
									cart.NextTurn = Direction.Right;
									break;
								case Direction.Left:
									cart.NextTurn = Direction.Straight;
									cart.TurnLeft();
									break;
								case Direction.Right:
									cart.NextTurn = Direction.Left;
									cart.TurnRight();
									break;
							}
							break;
					}
				}

				if (carts.Count(c => !c.Crashed) == 1)
				{
					var lastCart = carts.First(c => !c.Crashed);
					return (lastCart.X, lastCart.Y);
				}
			}
		}

		private List<Cart> GetCarts(List<string> grid)
		{
			var carts = new List<Cart>();
			for (int y = 0; y < grid.Count(); y++)
			{
				for (int x = 0; x < grid[y].Length; x++)
				{
					if (grid[y][x] == '^')
					{
						carts.Add(new Cart { X = x, Y = y, Direction = Direction.Up });
						grid[y] = grid[y].Remove(x, 1).Insert(x, "|");
					}
					else if (grid[y][x] == 'v')
					{
						carts.Add(new Cart { X = x, Y = y, Direction = Direction.Down });
						grid[y] = grid[y].Remove(x, 1).Insert(x, "|");
					}
					else if (grid[y][x] == '<')
					{
						carts.Add(new Cart { X = x, Y = y, Direction = Direction.Left });
						grid[y] = grid[y].Remove(x, 1).Insert(x, "-");
					}
					else if (grid[y][x] == '>')
					{
						carts.Add(new Cart { X = x, Y = y, Direction = Direction.Right });
						grid[y] = grid[y].Remove(x, 1).Insert(x, "-");
					}
				}
			}
			return carts;
		}

		public class Cart
		{
			public int X { get; set; }

			public int Y { get; set; }

			public Direction Direction { get; set; }

			public Direction NextTurn { get; set; } = Direction.Left;

			// Needed to "remove" carts - normal remove results in collection changing while iterating
			public bool Crashed { get; set; }

			public void Move()
			{
				switch (Direction)
				{
					case Direction.Up:
						Y--;
						break;
					case Direction.Down:
						Y++;
						break;
					case Direction.Left:
						X--;
						break;
					case Direction.Right:
						X++;
						break;
				}
			}

			public void TurnLeft()
			{
				switch (Direction)
				{
					case Direction.Up:
						Direction = Direction.Left;
						break;
					case Direction.Down:
						Direction = Direction.Right;
						break;
					case Direction.Left:
						Direction = Direction.Down;
						break;
					case Direction.Right:
						Direction = Direction.Up;
						break;
				}
			}

			public void TurnRight()
			{
				switch (Direction)
				{
					case Direction.Up:
						Direction = Direction.Right;
						break;
					case Direction.Down:
						Direction = Direction.Left;
						break;
					case Direction.Left:
						Direction = Direction.Up;
						break;
					case Direction.Right:
						Direction = Direction.Down;
						break;
				}
			}
		}

		public enum Direction
		{
			Up,
			Down,
			Left,
			Right,
			Straight
		}
	}
}
