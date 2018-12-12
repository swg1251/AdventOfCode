using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day15 : IDay
	{
		private List<Ingredient> ingredients;

		public void GetInput()
		{
			var input = File.ReadAllLines("2015/input/day15.txt").Where(l => !string.IsNullOrEmpty(l));

			ingredients = new List<Ingredient>();
			foreach (var line in input)
			{
				var lineParts = line.Split(' ');
				ingredients.Add(new Ingredient
				{
					Capacity = Convert.ToInt32(lineParts[2].TrimEnd(',')),
					Durability = Convert.ToInt32(lineParts[4].TrimEnd(',')),
					Flavor = Convert.ToInt32(lineParts[6].TrimEnd(',')),
					Texture = Convert.ToInt32(lineParts[8].TrimEnd(',')),
					Calories = Convert.ToInt32(lineParts[10].TrimEnd(','))
				});
			}
		}

		public void Solve()
		{
			var cookiesPartOne = new List<int>();
			var cookiesPartTwo = new List<int>();

			// assumes input has four ingredients
			for (int i = 0; i < 101; i++)
			for (int j = 0; j < 101 - i; j++)
			for (int k = 0; k < 101 - i - j; k++)
			for (int l = 0; l < 101 - i - j - k; l++)
			{
				if (i + j + k + l == 100)
				{
					var capacity = (ingredients[0].Capacity * i) +
						(ingredients[1].Capacity * j) +
						(ingredients[2].Capacity * k) +
						(ingredients[3].Capacity * l);

					var durability = (ingredients[0].Durability * i) +
						(ingredients[1].Durability * j) +
						(ingredients[2].Durability * k) +
						(ingredients[3].Durability * l);

					var flavor = (ingredients[0].Flavor * i) +
						(ingredients[1].Flavor * j) +
						(ingredients[2].Flavor * k) +
						(ingredients[3].Flavor * l);

					var texture = (ingredients[0].Texture * i) +
						(ingredients[1].Texture * j) +
						(ingredients[2].Texture * k) +
						(ingredients[3].Texture * l);

					var calories = (ingredients[0].Calories * i) +
						(ingredients[1].Calories * j) +
						(ingredients[2].Calories * k) +
						(ingredients[3].Calories * l);

					if (capacity < 1 || durability < 1 || flavor < 1 || texture < 1)
					{
						continue;
					}

					var score = capacity * durability * flavor * texture;
					cookiesPartOne.Add(score);
					if (calories == 500)
					{
						cookiesPartTwo.Add(score);
					}
				}
			}

			Console.WriteLine($"The best cookie (part one) is worth {cookiesPartOne.Max()} points");
			Console.WriteLine($"The best 500 calorie cookie (part two) is worth {cookiesPartTwo.Max()} points");
		}

		internal class Ingredient
		{
			public int Capacity { get; set; }

			public int Durability { get; set; }

			public int Flavor { get; set; }

			public int Texture { get; set; }

			public int Calories { get; set; }
		}
	}
}
