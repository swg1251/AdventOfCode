using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day09
{
    public class Day09 : IDay
    {
		private string Compressed { get; set; }
		private string Decompressed { get; set; }

		public void Go()
		{
			Compressed = File.ReadAllLines("Day09/input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
			Console.WriteLine($"The length of the decompressed string is: {GetDecompressedLength(Compressed, false)} (part one)");
			Console.WriteLine($"The length of the recursively decompressed string is: {GetDecompressedLength(Compressed, true)} (part two)");
		}

		private long GetDecompressedLength(string compressed, bool recurse)
		{
			// no markers
			if (!compressed.Contains('('))
			{
				return compressed.Length;
			}

			// use long (64-bit) as the part two result is too large for int (32-bit)
			long length = 0;

			while (compressed.Contains('('))
			{
				var leftParenIndex = compressed.IndexOf('(');

				// add length of all characters until first marker and discard
				length += leftParenIndex;
				compressed = compressed.Substring(leftParenIndex);

				// get the marker, sequence length, and sequence count
				var rightParenIndex = compressed.IndexOf(')');
				var markerParts = compressed.Substring(1, rightParenIndex - 1).Split('x');
				var sequenceLength = Convert.ToInt32(markerParts[0]);
				var sequenceCount = Convert.ToInt32(markerParts[1]);

				// discard the marker
				compressed = compressed.Substring(rightParenIndex + 1);

				if (recurse)
				{
					// part two - recursively decompress the sequence and add to the length
					length += GetDecompressedLength(compressed.Substring(0, sequenceLength), true) * sequenceCount;
				}
				else
				{
					// part one - just add the sequence length, no recursion
					length += sequenceLength * sequenceCount;
				}

				// discard the sequence
				compressed = compressed.Substring(sequenceLength);
			}

			length += compressed.Length;

			return length;
		}

		// GetDecompressedLength should be used instead of Decompress. This method preserces the decompressed string 
		// and was used initially for part one, since I guessed that part two would require the decompressed string.
		private void Decompress()
		{
			int i = 0;
			while (i < Compressed.Length)
			{
				if (Compressed[i] == '(')
				{
					// markers will be at least 5 characters long
					int markerLength = 5;
					while (Compressed[i + markerLength - 1] != ')')
					{
						markerLength++;
					}

					var markerParts = Compressed.Substring(i + 1, markerLength - 2).Split('x');
					var sequenceLength = Convert.ToInt32(markerParts[0]);
					var sequenceCount = Convert.ToInt32(markerParts[1]);

					for (int j = 0; j < sequenceCount; j++)
					{
						for (int k = 0; k < sequenceLength; k++)
						{
							Decompressed += Compressed[i + markerLength + k];
						}
					}

					i += markerLength;
					i += sequenceLength;
				}
				else
				{
					Decompressed += Compressed[i];
					i++;
				}
			}
		}
	}
}
