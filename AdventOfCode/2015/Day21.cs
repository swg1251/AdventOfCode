using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015
{
	public class Day21 : IDay
	{
		private int bossHp;
		private int bossDamage;
		private int bossArmor;

		public void GetInput()
		{
			var input = File.ReadAllLines("2015/input/day21.txt").Where(l => !string.IsNullOrEmpty(l)).ToList();
			bossHp = Convert.ToInt32(input[0].Split(' ').Last().Trim());
			bossDamage = Convert.ToInt32(input[1].Split(' ').Last().Trim());
			bossArmor = Convert.ToInt32(input[2].Split(' ').Last().Trim());
		}

		public void Solve()
		{
			// must use ine weapon, so combos are just each weapon
			var weaponCombos = new List<(int cost, int damage, int armor)>
			{
				(8, 4, 0),
				(10, 5, 0),
				(25, 6, 0),
				(40, 7, 0),
				(74, 8, 0)
			};

			// can use no armor, so add 0's
			var armorCombos = new List<(int cost, int damage, int armor)>
			{
				(13, 0, 1),
				(31, 0, 2),
				(53, 0, 3),
				(75, 0, 4),
				(102, 0, 5),
				(0, 0, 0)
			};

			var rings = new List<(int cost, int damage, int armor)>
			{
				(25, 1, 0),
				(50, 2, 0),
				(100, 3, 0),
				(20, 0, 1),
				(40, 0, 2),
				(80, 0, 3)
			};

			// 0-2 rings
			var ringCombos = new List<(int cost, int damage, int armor)>();
			ringCombos.Add((0, 0, 0));
			foreach (var ring1 in rings)
			{
				ringCombos.Add((ring1.cost, ring1.damage, ring1.armor));
				foreach (var ring2 in rings.Where(r2 => !r2.Equals(ring1)))
				{
					ringCombos.Add((ring1.cost + ring2.cost, ring1.damage + ring2.damage, ring1.armor + ring2.armor));
				}
			}
			ringCombos = ringCombos.Distinct().ToList();

			var players = new List<Player>();
			foreach (var wc in weaponCombos)
			{
				foreach (var ac in armorCombos)
				{
					foreach (var rc in ringCombos)
					{
						var damage = wc.damage + ac.damage + rc.damage;
						var armor = wc.armor + ac.armor + rc.armor;
						var cost = wc.cost + ac.cost + rc.cost;
						players.Add(new Player
						{
							Damage = damage,
							Armor = armor,
							AmountSpent = cost
						});
					}
				}
			}

			var boss = new Player { Hp = bossHp, Damage = bossDamage, Armor = 2 };

			foreach (var player in players)
			{
				while (true)
				{
					// player turn
					var damageDealt = player.Damage - boss.Armor;
					if (damageDealt < 1)
					{
						damageDealt = 1;
					}
					boss.Hp -= damageDealt;

					// player wins
					if (boss.Hp < 1)
					{
						player.Won = true;
						boss.Hp = bossHp;
						break;
					}

					// boss turn
					var damageTaken = boss.Damage - player.Armor;
					if (damageTaken < 1)
					{
						damageTaken = 1;
					}
					player.Hp -= damageTaken;

					// player loses
					if (player.Hp < 1)
					{
						boss.Hp = bossHp;
						break;
					}
				}
			}

			Console.WriteLine($"The least amount spent resulting in a victory (part one) is: {players.Where(p => p.Won).Min(p => p.AmountSpent)}");
			Console.WriteLine($"The most amount spent resulting in a loss (part two) is: {players.Where(p => !p.Won).Max(p => p.AmountSpent)}");
		}

		public class Player
		{
			public int Hp { get; set; } = 100;

			public int Damage { get; set; }

			public int Armor { get; set; }

			public int AmountSpent { get; set; }

			public bool Won { get; set; }
		}
	}
}
