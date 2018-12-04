using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
    public class Day12 : IDay
    {
		private string json;
		private List<Newtonsoft.Json.Linq.JValue> jObjects;

		public void GetInput()
		{
			json = File.ReadAllLines("2015/input/day12.txt").Where(l => !string.IsNullOrEmpty(l)).First();
			jObjects = new List<Newtonsoft.Json.Linq.JValue>();
		}

		public void Solve()
		{
			PartOne();
			PartTwo();
		}

		private void PartOne()
		{
			var total = 0;

			var regex = new Regex("-*[0-9]+");
			foreach (var match in regex.Matches(json))
			{
				total += Convert.ToInt32(match.ToString());
			}

			Console.WriteLine($"The total of all numbers (part one) is: {total}");
		}

		private void PartTwo()
		{
			dynamic jsonObject = JsonConvert.DeserializeObject(json);
			var nonRedTotal = GetSum(jsonObject);
			Console.WriteLine($"The total of all non-red numbers (part two) is: {nonRedTotal}");
		}

		private int GetSum(JObject obj)
		{
			// Exclude any properties containing a JValue with "red" value
			if (obj.Properties()
				.Select(p => p.Value)
				.OfType<JValue>()
				.Select(v => v.Value).Contains("red"))
			{
				return 0;
			}

			var sum = 0;
			foreach (dynamic d in obj.Properties())
			{
				// dynamic dispatch!
				sum += GetSum(d.Value);
			}
			return sum;
		}

		private int GetSum(JArray array)
		{
			var sum = 0;
			foreach (dynamic d in array)
			{
				sum += GetSum(d);
			}
			return sum;
		}

		private int GetSum(JValue value)
		{
			return value.Type == JTokenType.Integer ? Convert.ToInt32(value.Value) : 0;
		}
    }
}
