using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day07 : IDay
    {
		private IEnumerable<string> instructions;
		private List<string> processedInstructions;
		private Dictionary<string, ushort> wires;

		public Day07()
		{
			instructions = new List<string>();
			processedInstructions = new List<string>();
			wires = new Dictionary<string, ushort>();
		}

		public void GetInput()
		{
			instructions = File.ReadAllLines("2015/input/day07.txt").Where(l => !string.IsNullOrEmpty(l));
		}

		public void Solve()
		{
			ProcessInstructions();
			var wireA = wires["a"];
			Console.WriteLine($"The value supplied to wire a (part one) is {wireA}");

			wires = new Dictionary<string, ushort>();
			wires["b"] = wireA;
			processedInstructions.Clear();
			ProcessInstructions();
			Console.WriteLine($"The value supplied to wire a (part two) is {wires["a"]}");
		}

		private void ProcessInstructions()
		{
			while (!wires.ContainsKey("a"))
			{
				foreach (var instruction in instructions)
				{
					if (processedInstructions.Contains(instruction))
					{
						continue;
					}

					var instructionParts = instruction.Split(' ');

					// and/or instructions have same structure
					if (instructionParts[1] == "AND" || instructionParts[1] == "OR")
					{
						var wire1 = instructionParts[0];
						var wire2 = instructionParts[2];
						var targetWire = instructionParts[4];

						if (!wires.ContainsKey(wire2) || wires.ContainsKey(targetWire))
						{
							continue;
						}

						// 1 AND wire -> target wire
						if (wire1 == "1")
						{							
							wires[targetWire] = (ushort)(1 & wires[wire2]);
						}

						// wire 1 AND/OR wire 2 -> target wire
						else
						{
							if (!wires.ContainsKey(wire1))
							{
								continue;
							}

							if (instructionParts[1] == "AND")
							{
								wires[targetWire] = (ushort)(wires[wire1] & wires[wire2]);
							}
							else
							{
								wires[targetWire] = (ushort)(wires[wire1] | wires[wire2]);
							}
						}
					}

					// lshift/rshift instructions have same structure
					else if (instructionParts[1].Contains("SHIFT"))
					{
						var wire = instructionParts[0];
						var targetWire = instructionParts[4];

						if (!wires.ContainsKey(wire) || wires.ContainsKey(targetWire))
						{
							continue;
						}
						
						var shift = Convert.ToUInt16(instructionParts[2]);

						if (instructionParts[1] == "LSHIFT")
						{
							wires[targetWire] = (ushort)(wires[wire] << shift);
						}
						else
						{
							wires[targetWire] = (ushort)(wires[wire] >> shift);
						}
					}

					// not
					else if (instructionParts[0] == "NOT")
					{
						var wire = instructionParts[1];
						var targetWire = instructionParts[3];

						if (!wires.ContainsKey(wire) || wires.ContainsKey(targetWire))
						{
							continue;
						}

						wires[targetWire] = (ushort)(~wires[wire]);
					}

					else
					{
						var wire = instructionParts[0];
						var targetWire = instructionParts[2];

						if (wires.ContainsKey(targetWire))
						{
							continue;
						}

						// value -> wire
						ushort value;
						if (UInt16.TryParse(wire, out value))
						{
							wires[targetWire] = value;
						}

						// wire -> other wire
						else
						{
							if (!wires.ContainsKey(wire))
							{
								continue;
							}

							wires[targetWire] = wires[wire];
						}
					}

					processedInstructions.Add(instruction);
				}
			}
		}
	}
}
