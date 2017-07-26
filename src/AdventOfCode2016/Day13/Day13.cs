using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016.Day13
{
    public class Day13 : IDay
    {
        private const int PUZZLE_INPUT = 1352;
        private const int GOAL_X = 31;
        private const int GOAL_Y = 39;

        public void Go()
        {
            Search();
        }

        private void Search()
        {
            var queue = new Queue<Position>();
            var exploredPositions = new HashSet<Tuple<int, int>>();
            var exploredPositionsFifty = new HashSet<Tuple<int, int>>();

            var start = new Position(1, 1, 0);
            queue.Enqueue(start);
            exploredPositions.Add(Tuple.Create(start.X, start.Y));

            while (queue.Any())
            {
                var currentPosition = queue.Dequeue();

                if (currentPosition.X == GOAL_X && currentPosition.Y == GOAL_Y)
                {
                    Console.WriteLine($"Completed in {currentPosition.Steps} steps (part one)");
                    Console.WriteLine(exploredPositionsFifty.Count);
                }

                foreach (var position in currentPosition.GetNextPositions().Where(p => p.IsOpen(PUZZLE_INPUT)))
                {
                    if (currentPosition.Steps < 50)
                    {
                        exploredPositionsFifty.Add(Tuple.Create(position.X, position.Y));
                    }

                    if (exploredPositions.Add(Tuple.Create(position.X, position.Y)))
                    {
                        queue.Enqueue(position);
                    }
                }
            }
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }
        public HashSet<Tuple<int, int>> PathLocations { get; set; }

        public Position(int x, int y, int steps)
        {
            X = x;
            Y = y;
            Steps = steps;
        }

        public bool IsOpen(int favoriteNumber)
        {
            var value = (X * X) + (3 * X) + (2 * X * Y) + Y + (Y * Y) + favoriteNumber;
            return Convert.ToString(value, 2).Count(d => d == '1') % 2 == 0;
        }

        public IEnumerable<Position> GetNextPositions()
        {
            var positions = new List<Position>();

            if (X > 0)
            {
                positions.Add(new Position(X - 1, Y, Steps + 1));
            }
            if (Y > 0)
            {
                positions.Add(new Position(X, Y - 1, Steps + 1));
            }
            positions.Add(new Position(X + 1, Y, Steps + 1));
            positions.Add(new Position(X, Y + 1, Steps + 1));

            return positions;
        }
    }
}
