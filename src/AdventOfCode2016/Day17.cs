using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    public class Day17 : IDay
    {
		private static string input { get; set; }
		private static char[] openChars = new char[] { 'b', 'c', 'd', 'e', 'f' };

		public void GetInput()
		{
			input = File.ReadAllLines("input/day17.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
		}

		public void Solve()
		{
			Search();
		}

		private void Search()
		{
			var queue = new Queue<State>();
			queue.Enqueue(new State(0, 0, ""));

			while (queue.Any())
			{
				var currentState = queue.Dequeue();

				if (currentState.IsGoal())
				{
					Console.WriteLine($"Reached the goal in {currentState.Path.Length} steps (part one). Path: {currentState.Path}");
					return;
				}

				foreach (var state in currentState.GetNewStates())
				{
					queue.Enqueue(state);
				}
			}
		}

		private static string Hash(string input)
		{
			using (var md5Hash = MD5.Create())
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

		internal class State
		{
			public int X { get; set; }
			public int Y { get; set; }
			public string Path { get; set; }

			public State(int x, int y, string path)
			{
				X = x;
				Y = y;
				Path = path;
			}

			public bool IsGoal()
			{
				return X == 3 && Y == 3;
			}

			public List<State> GetNewStates()
			{
				var newStates = new List<State>();
				var hash = Hash(input + Path);

				if (X > 0 && openChars.Contains(hash[0]))
				{
					newStates.Add(new State(X - 1, Y, Path + "U"));
				}
				if (X < 3 && openChars.Contains(hash[1]))
				{
					newStates.Add(new State(X + 1, Y, Path + "D"));
				}
				if (Y > 0 && openChars.Contains(hash[2]))
				{
					newStates.Add(new State(X, Y - 1, Path + "L"));
				}
				if (Y < 3 && openChars.Contains(hash[3]))
				{
					newStates.Add(new State(X, Y + 1, Path + "R"));
				}

				return newStates;
			}
		}
    }
}
