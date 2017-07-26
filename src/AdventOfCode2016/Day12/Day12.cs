using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2016.Day12
{
    public class Day12 : IDay
    {
        private Dictionary<char, int> registers { get; set; }
        private bool PartTwo { get; set; }
        public void Go()
        {
            InitializeRegisters();
            ProcessInstructions();
            Console.WriteLine($"The value in register 'a' is {registers['a']} (part one)");

            PartTwo = true;
            InitializeRegisters();
            ProcessInstructions();
            Console.WriteLine($"The value in register 'a' is {registers['a']} (part two)");
        }

        private void InitializeRegisters()
        {
            registers = new Dictionary<char, int>();
            registers['a'] = 0;
            registers['b'] = 0;
            registers['c'] = PartTwo ? 1 : 0;
            registers['d'] = 0;
        }

        private void ProcessInstructions()
        {
            var instructions = File.ReadAllLines("Day12/input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

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
