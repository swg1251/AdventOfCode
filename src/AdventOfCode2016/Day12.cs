using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016
{
    public class Day12 : IDay
    {
        private Dictionary<char, int> registers { get; set; }
        private bool part2 { get; set; }
        private List<string> instructions { get; set; }

        public void GetInput()
        {
            instructions = File.ReadAllLines("input/day12.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        }

        public void Solve()
        {
            InitializeRegisters();
            ProcessInstructions();
            Console.WriteLine($"The value in register 'a' is {registers['a']} (part one)");

            part2 = true;
            InitializeRegisters();
            ProcessInstructions();
            Console.WriteLine($"The value in register 'a' is {registers['a']} (part two)");
        }

        private void InitializeRegisters()
        {
            registers = new Dictionary<char, int>();
            registers['a'] = 0;
            registers['b'] = 0;
            registers['c'] = part2 ? 1 : 0;
            registers['d'] = 0;
        }

        private void ProcessInstructions()
        {
            for (int i = 0; i < instructions.Count(); i++)
            {
                var instructionParts = instructions[i].Split(' ');

                if (instructionParts[0] == "cpy")
                {
                    int value;
                    if (int.TryParse(instructionParts[1], out value))
                    {
                        registers[instructionParts[2][0]] = value;
                    }
                    else
                    {
                        registers[instructionParts[2][0]] = registers[instructionParts[1][0]];
                    }
                }
                else if (instructionParts[0] == "inc")
                {
                    registers[instructionParts[1][0]]++;
                }
                else if (instructionParts[0] == "dec")
                {
                    registers[instructionParts[1][0]]--;
                }
                else
                {
                    int value;
                    if (!int.TryParse(instructionParts[1], out value))
                    {
                        value = registers[instructionParts[1][0]];
                    }
                    if (value != 0)
                    {
                        i += Convert.ToInt32(instructionParts[2]) - 1;
                    }
                }
            }
        }
    }
}
