using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2016
{
    public class Day11 : IDay
    {
        private State initialState { get; set; }
        private HashSet<string> strStates { get; set; }
        private bool part2 { get; set; }

        public void GetInput()
        {
            var initialFloors = new List<List<string>>();

            foreach (var line in File.ReadAllLines("input/day11.txt").Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var floorItems = new List<string>();

                var words = line.Split(' ');
                if (words[4][0] != 'n')
                {
                    for (int i = 5; i < words.Length; i += 3)
                    {
                        if (words[i] == "a")
                        {
                            i++;
                        }

                        floorItems.Add(words[i].Substring(0, 2) + words[i + 1][0].ToString());
                    }
                }

                initialFloors.Add(floorItems);
            }

            initialState = new State(0, initialFloors, 0);
        }

        public void Solve()
        {
            Search();

            SetupPartTwo();
            Console.WriteLine("Beginning part two. This may take a few minutes unless I decide to optimize.");
            Search();
        }

        public void SetupPartTwo()
        {
            GetInput();
            part2 = true;

            initialState.Floors[0].Add("elg");
            initialState.Floors[0].Add("elm");
            initialState.Floors[0].Add("dig");
            initialState.Floors[0].Add("dim");   
        }

        private void Search()
        {
            int step = 0;
            var queue = new Queue<State>();
            strStates = new HashSet<string>();

            queue.Enqueue(initialState);
            strStates.Add(GetStateString(initialState));

            while (queue.Any())
            {
                var currentState = queue.Dequeue();

                if (currentState.IsGoal())
                {
                    Console.WriteLine($"Solved in {currentState.Depth} steps (part {(part2 ? "two" : "one")})");
                    return;
                }

                if (step < currentState.Depth && part2)
                {
                    step = currentState.Depth;
                    Console.WriteLine($"On step {step} ({DateTime.Now.ToString("HH:mm:ss")})");
                }

                foreach (var state in currentState.GetNewStates())
                {
                    if (strStates.Add(GetStateString(state)))
                    {
                        queue.Enqueue(state);
                    }
                }
            }
        }

        // TODO: come up with a better way of comparing state equality that is still fast, but allows
        // better pruning to reduce search space. Current method takes a few minutes to complete :(
        private string GetStateString(State state)
        {
            var sb = new StringBuilder();
            sb.Append(state.CurrentFloor);

            var floors = State.CopyFloors(state.Floors);

            for (int i = 0; i < floors.Count; i++)
            {
                sb.Append(i);

                floors[i].Sort();

                foreach (var item in floors[i])
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        internal class State
        {
            public int CurrentFloor { get; set; }
            public List<List<string>> Floors { get; set; }
            public int Depth { get; set; }

            public State(int currentFloor, List<List<string>> floors, int depth)
            {
                CurrentFloor = currentFloor;
                Floors = floors;
                Depth = depth;
            }

            public IEnumerable<State> GetNewStates()
            {
                var newStates = new List<State>();

                // move each item down alone
                if (CurrentFloor > 0)
                {
                    foreach (var item in Floors[CurrentFloor])
                    {
                        var newState = new State(CurrentFloor - 1, CopyFloors(Floors), Depth + 1);
                        newState.Floors[CurrentFloor].Remove(item);
                        newState.Floors[CurrentFloor - 1].Add(item);
                        newStates.Add(newState);
                    }
                }

                // move each item up alone
                if (CurrentFloor + 1 < Floors.Count)
                {
                    foreach (var item in Floors[CurrentFloor])
                    {
                        var newState = new State(CurrentFloor + 1, CopyFloors(Floors), Depth + 1);
                        newState.Floors[CurrentFloor].Remove(item);
                        newState.Floors[CurrentFloor + 1].Add(item);
                        newStates.Add(newState);
                    }
                }

                // move each combination of two items down
                if (Floors[CurrentFloor].Count > 1)
                {
                    if (CurrentFloor > 0)
                    {
                        for (int i = 0; i < Floors[CurrentFloor].Count - 1; i++)
                        {
                            for (int j = i + 1; j < Floors[CurrentFloor].Count; j++)
                            {
                                var item1 = Floors[CurrentFloor][i];
                                var item2 = Floors[CurrentFloor][j];
                                if (item1[2] != item2[2] && item1.Substring(0, 2) != item2.Substring(0, 2))
                                {
                                    continue;
                                }

                                var newState = new State(CurrentFloor - 1, CopyFloors(Floors), Depth + 1);
                                newState.Floors[CurrentFloor].Remove(item1);
                                newState.Floors[CurrentFloor].Remove(item2);
                                newState.Floors[CurrentFloor - 1].Add(item1);
                                newState.Floors[CurrentFloor - 1].Add(item2);
                                newStates.Add(newState);
                            }
                        }
                    }

                    // move each combination of two items up
                    if (CurrentFloor + 1 < Floors.Count)
                    {
                        for (int i = 0; i < Floors[CurrentFloor].Count - 1; i++)
                        {
                            for (int j = i + 1; j < Floors[CurrentFloor].Count; j++)
                            {
                                var item1 = Floors[CurrentFloor][i];
                                var item2 = Floors[CurrentFloor][j];
                                if (item1[2] != item2[2] && item1.Substring(0, 2) != item2.Substring(0, 2))
                                {
                                    continue;
                                }

                                var newState = new State(CurrentFloor + 1, CopyFloors(Floors), Depth + 1);
                                newState.Floors[CurrentFloor].Remove(item1);
                                newState.Floors[CurrentFloor].Remove(item2);
                                newState.Floors[CurrentFloor + 1].Add(item1);
                                newState.Floors[CurrentFloor + 1].Add(item2);
                                newStates.Add(newState);
                            }
                        }
                    }
                }

                return newStates.Where(s => s.IsValid());
            }

            public bool IsValid()
            {
                // chip cannot be in same area as other rtg when not powered by its own
                foreach (var floor in Floors)
                {
                    foreach (var chip in floor.Where(c => c[2] == 'm').Select(c => c.Substring(0, 2)))
                    {
                        var generators = floor.Where(g => g[2] == 'g').Select(g => g.Substring(0, 2));
                        if (!generators.Contains(chip) && generators.Any())
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public bool IsGoal()
            {
                for (int i = 0; i < Floors.Count - 1; i++)
                {
                    if (Floors[i].Any())
                    {
                        return false;
                    }
                }
                return true;
            }

            public static List<List<string>> CopyFloors(List<List<string>> floors)
            {
                var newFloors = new List<List<string>>();
                foreach (var floor in floors)
                {
                    var newFloor = new List<string>();
                    foreach (var item in floor)
                    {
                        newFloor.Add(new string(item.ToCharArray()));
                    }
                    newFloors.Add(newFloor);
                }
                return newFloors;
            }
        }
    }
}
