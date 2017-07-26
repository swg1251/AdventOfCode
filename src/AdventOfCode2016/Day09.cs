using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day09 : IDay
    {
		private string compressed { get; set; }
		private string decompressed { get; set; }

        public void GetInput()
        {
            compressed = File.ReadAllLines("input/Day09.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
        }

		public void Solve()
		{
			Console.WriteLine($"The length of the decompressed string is: {GetDecompressedLength(compressed, false)} (part one)");
			Console.WriteLine($"The length of the recursively decompressed string is: {GetDecompressedLength(compressed, true)} (part two)");
		}

		private long GetDecompressedLength(string stringToDecompress, bool recurse)
		{
			// no markers
			if (!stringToDecompress.Contains('('))
			{
				return stringToDecompress.Length;
			}

			// use long (64-bit) as the part two result is too large for int (32-bit)
			long length = 0;

			while (stringToDecompress.Contains('('))
			{
				var leftParenIndex = stringToDecompress.IndexOf('(');

				// add length of all characters until first marker and discard
				length += leftParenIndex;
				stringToDecompress = stringToDecompress.Substring(leftParenIndex);

				// get the marker, sequence length, and sequence count
				var rightParenIndex = stringToDecompress.IndexOf(')');
				var markerParts = stringToDecompress.Substring(1, rightParenIndex - 1).Split('x');
				var sequenceLength = Convert.ToInt32(markerParts[0]);
				var sequenceCount = Convert.ToInt32(markerParts[1]);

				// discard the marker
				stringToDecompress = stringToDecompress.Substring(rightParenIndex + 1);

				if (recurse)
				{
					// part two - recursively decompress the sequence and add to the length
					length += GetDecompressedLength(stringToDecompress.Substring(0, sequenceLength), true) * sequenceCount;
				}
				else
				{
					// part one - just add the sequence length, no recursion
					length += sequenceLength * sequenceCount;
				}

				// discard the sequence
				stringToDecompress = stringToDecompress.Substring(sequenceLength);
			}

			length += stringToDecompress.Length;

			return length;
		}

		// GetDecompressedLength should be used instead of Decompress. This method preserces the decompressed string 
		// and was used initially for part one, since I guessed that part two would require the decompressed string.
		private void Decompress()
		{
			int i = 0;
			while (i < compressed.Length)
			{
				if (compressed[i] == '(')
				{
					// markers will be at least 5 characters long
					int markerLength = 5;
					while (compressed[i + markerLength - 1] != ')')
					{
						markerLength++;
					}

					var markerParts = compressed.Substring(i + 1, markerLength - 2).Split('x');
					var sequenceLength = Convert.ToInt32(markerParts[0]);
					var sequenceCount = Convert.ToInt32(markerParts[1]);

					for (int j = 0; j < sequenceCount; j++)
					{
						for (int k = 0; k < sequenceLength; k++)
						{
							decompressed += compressed[i + markerLength + k];
						}
					}

					i += markerLength;
					i += sequenceLength;
				}
				else
				{
					decompressed += compressed[i];
					i++;
				}
			}
		}
	}
}
