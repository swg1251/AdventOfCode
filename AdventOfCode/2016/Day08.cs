using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day08 : IDay
    {
        private const int SCREEN_HEIGHT = 6;
        private const int SCREEN_WIDTH = 50;
        private const int LETTER_WIDTH = 5;

        private IEnumerable<string> instructions { get; set; }

        public bool[,] Screen { get; set; }
        public int ActivatedPixelCount { get; set; }

        public Day08()
        {
            Screen = new bool[SCREEN_HEIGHT, SCREEN_WIDTH];
            ActivatedPixelCount = 0;
        }

        public void GetInput()
        {
            instructions = File.ReadAllLines("2016/input/day08.txt").Where(l => !string.IsNullOrWhiteSpace(l));
        }

        public void Solve()
        {
            foreach (var line in instructions)
            {
                ProcessInstruction(line);
            }
            CountActivePixels();

            Console.WriteLine($"There are {ActivatedPixelCount} pixels on (part one).");
            Console.WriteLine("Part two - the screen display:");
            PrintScreen();
        }

        private void ProcessInstruction(string line)
        {
            var tokens = line.Split(' ');
            if (tokens[0] == "rect")
            {
                var dimensions = tokens[1].Split('x');
                DrawRectangle(Convert.ToInt32(dimensions[0]), Convert.ToInt32(dimensions[1]));
            }
            else
            {
                var index = Convert.ToInt32(tokens[2].Split('=')[1]);
                var distance = Convert.ToInt32(tokens[4]);
                if (tokens[1] == "column")
                {
                    RotateColumn(index, distance);
                }
                else
                {
                    RotateRow(index, distance);
                }
            }
        }

        private void DrawRectangle(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Screen[y, x] = true;
                }
            }
        }

        private void RotateColumn(int column, int distance)
        {
            bool[] newColumn = new bool[SCREEN_HEIGHT];

            for (int i = 0; i < newColumn.Length; i++)
            {
                int newIndex = i + distance;
                if (newIndex >= newColumn.Length)
                {
                    newIndex -= newColumn.Length;
                }
                newColumn[newIndex] = Screen[i, column];
            }

            for (int i = 0; i < newColumn.Length; i++)
            {
                Screen[i, column] = newColumn[i];
            }
        }

        private void RotateRow(int row, int distance)
        {
            bool[] newRow = new bool[SCREEN_WIDTH];

            for (int i = 0; i < newRow.Length; i++)
            {
                int newIndex = i + distance;
                if (newIndex >= newRow.Length)
                {
                    newIndex -= newRow.Length;
                }
                newRow[newIndex] = Screen[row, i];
            }

            for (int i = 0; i < newRow.Length; i++)
            {
                Screen[row, i] = newRow[i];
            }
        }

        private void CountActivePixels()
        {
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    if (Screen[y, x])
                    {
                        ActivatedPixelCount++;
                    }
                }
            }
        }

        private void PrintScreen()
        {
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    if (x % LETTER_WIDTH == 0 && x > 0)
                    {
                        Console.Write("  ");
                    }
                    Console.Write(Screen[y, x] ? "X" : " ");
                }
                Console.Write("\n");
            }
        }
    }
}
