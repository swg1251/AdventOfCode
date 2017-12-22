using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day14 : IDay
    {
		private string key;
		private List<List<bool>> binaryHashes;

		public Day14()
		{
			binaryHashes = new List<List<bool>>();
		}

		public void GetInput()
		{
			key = File.ReadAllLines("2017/input/day14.txt").Where(l => !string.IsNullOrEmpty(l)).First();
		}

		public void Solve()
		{
			for (int i = 0; i < 128; i++)
			{
				var hexHash = KnotHash($"{key}-{i}");
				var binaryHash = HexToBinary(hexHash);
				binaryHashes.Add(binaryHash);
			}
			
			Console.WriteLine($"The number of used squares (part one) is {binaryHashes.Sum(bh => bh.Count(c => c))}");

			var onPositions = new List<Position>();
			for (int i = 0; i < 128; i++)
			{
				for (int j = 0; j < 128; j++)
				{
					if (binaryHashes[i][j])
					{
						var onPosition = new Position(i, j);
						onPositions.Add(onPosition);

						var left = onPositions.Find(op => op.X == onPosition.X - 1 && op.Y == onPosition.Y);
						var right = onPositions.Find(op => op.X == onPosition.X + 1 && op.Y == onPosition.Y);
						var up = onPositions.Find(op => op.X == onPosition.X && op.Y == onPosition.Y - 1);
						var down = onPositions.Find(op => op.X == onPosition.X && op.Y == onPosition.Y + 1);

						if (left != null)
						{
							onPosition.Left = left;
							left.Right = onPosition;
						}
						if (right != null)
						{
							onPosition.Right = right;
							right.Left = onPosition;
						}
						if (up != null)
						{
							onPosition.Up = up;
							up.Down = onPosition;
						}
						if (down != null)
						{
							onPosition.Down = down;
							down.Up = onPosition;
						}
					}
				}
			}

			var regionCount = 0;
			while (onPositions.Any())
			{
				var root = onPositions[0];
				root.CanReachGroupRoot = true;

				var foundNewConnection = false;
				do
				{
					foundNewConnection = false;

					foreach (var onPosition in onPositions)
					{
						if (onPosition.Neighbors().Any(rp => rp.CanReachGroupRoot))
						{
							if (!onPosition.CanReachGroupRoot)
							{
								foundNewConnection = true;
							}
							onPosition.CanReachGroupRoot = true;
						}
					}
				}
				while (foundNewConnection);

				regionCount++;
				onPositions.RemoveAll(op => op.CanReachGroupRoot);
			}

			Console.WriteLine(($"The number of regions (part two) is {regionCount}"));
		}

		private string KnotHash(string input)
		{
			var position = 0;
			var skipSize = 0;

			var lengths = new List<int>();
			foreach (var c in input)
			{
				lengths.Add(c);
			}
			lengths.Add(17);
			lengths.Add(31);
			lengths.Add(73);
			lengths.Add(47);
			lengths.Add(23);

			var hash = new List<int>();
			for (int i = 0; i < 256; i++)
			{
				hash.Add(i);
			}

			// KnotHash 64 rounds, not resetting position/skip size
			for (int iterations = 0; iterations < 64; iterations++)
			{
				foreach (var length in lengths)
				{
					// reverse the length
					// - build a list of the length
					var subList = new List<int>();
					for (int i = 0; i < length; i++)
					{
						subList.Add(hash[Loop255(i + position)]);
					}

					// - reverse it
					subList.Reverse();

					// - replace original list elements with reversed list elements
					for (int i = 0; i < length; i++)
					{
						hash[Loop255(i + position)] = subList[i];
					}

					// move forward length + skip size
					position = Loop255(position + length + skipSize);

					// increment skip size
					skipSize++;
				}
			}

			// condense hash via XOR'ing each 16 element sublist
			var denseHash = new List<int>();
			for (int i = 0; i < 16; i++)
			{
				var result = hash[i * 16];
				for (int j = 1; j < 16; j++)
				{
					result ^= hash[(i * 16) + j];
				}
				denseHash.Add(result);
			}

			// get hexadecimal
			var hashHexadecimal = "";
			for (int i = 0; i < 16; i++)
			{
				hashHexadecimal += Convert.ToString(denseHash[i], 16).PadLeft(2, '0');
			}

			return hashHexadecimal;
		}

		private List<bool> HexToBinary(string hexString)
		{
			var binaryString = "";
			
			foreach (var c in hexString)
			{
				binaryString += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
			}

			var binary = new List<bool>();
			foreach (var c in binaryString)
			{
				binary.Add(c == '1');
			}

			return binary;
		}

		private int Loop255(int value)
		{
			while (value > 255)
			{
				value -= 256;
			}
			return value;
		}

		internal class Position
		{
			public int X { get; set; }
			public int Y { get; set; }
			public bool CanReachGroupRoot { get; set; }

			public Position Left { get; set; }
			public Position Right { get; set; }
			public Position Up { get; set; }
			public Position Down { get; set; }

			public IEnumerable<Position> Neighbors()
			{
				var neighbors = new List<Position>();
				if (Left != null)
				{
					neighbors.Add(Left);
				}
				if (Right != null)
				{
					neighbors.Add(Right);
				}
				if (Up != null)
				{
					neighbors.Add(Up);
				}
				if (Down != null)
				{
					neighbors.Add(Down);
				}
				return neighbors;
			}

			public Position(int x, int y)
			{
				X = x;
				Y = y;
			}
		}
	}
}
