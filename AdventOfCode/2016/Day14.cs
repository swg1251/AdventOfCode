using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016
{
    public class Day14 : IDay
    {
        private string salt { get; set; }
        private Dictionary<char, List<int>> triplets { get; set; }
        private HashSet<int> keyIndeces { get; set; }
        private bool part2 { get; set; }


        public void GetInput()
        {
            salt = File.ReadAllLines("2016/input/day14.txt").Where(l => !string.IsNullOrWhiteSpace(l)).First();
        }

        public void Solve()
        {
            GetKeys();

            part2 = true;
            Console.WriteLine("Beginning part two. Buckle up.");
            GetKeys();
        }

        private void GetKeys()
        {
            int i = 0;
            var current = "";
            keyIndeces = new HashSet<int>();
            triplets = new Dictionary<char, List<int>>();

            using (var md5 = MD5.Create())
            {
                while (true)
                {
                    current = salt + i.ToString();

                    var hash = GetMd5Hash(md5, current, part2);
                    if (hash != null)
                    {
                        var triplet = GetTriplet(hash);
                        if (triplet != '\0')
                        {
                            if (!triplets.ContainsKey(triplet))
                            {
                                triplets[triplet] = new List<int>();
                            }

                            // keep track of the character,index of all triplets
                            triplets[triplet].Add(i);

                            // any quintuplets also contain a triplet; check for quintuplets inside triplet loop
                            var quintuplet = GetQuintuplet(hash);
                            if (quintuplet != '\0')
                            {
                                List<int> matchingTriplets;
                                if (triplets.TryGetValue(quintuplet, out matchingTriplets))
                                {
                                    // get the index of all triplets within 1000
                                    foreach (var index in matchingTriplets.Where(t => i != t && i - t < 1000))
                                    {
                                        keyIndeces.Add(index);

                                        if (part2)
                                        {
                                            Console.WriteLine($"Found key {keyIndeces.Count} at index {index}");
                                        }

                                        if (keyIndeces.Count == 64)
                                        {
                                            Console.WriteLine($"The 64th key was found at index {keyIndeces.Max()} (part {(part2 ? "two" : "one")})");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    i++;
                }
            }
        }

        private char GetTriplet(string md5)
        {
            // match any character that repeats over 2 times
            var match = new Regex("(.)\\1{2,}").Match(md5);
            if (match.Success)
            {
                return match.Value[0];
            }
            return '\0';
        }

        private char GetQuintuplet(string md5)
        {
            // match any character that repeats over 4 times
            var match = new Regex("(.)\\1{4,}").Match(md5);
            if (match.Success)
            {
                return match.Value[0];
            }
            return '\0';
        }

        private static string Hash(MD5 md5Hash, string input)
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

        private static string GetMd5Hash(MD5 md5Hash, string input, bool stretch)
        {
            // hash 2017 times for part two
            int iterations = stretch ? 2017 : 1;

            var hash = input;
            for (int i = 0; i < iterations; i++)
            {
                hash = Hash(md5Hash, hash);
            }

            return hash;
        }
    }
}
