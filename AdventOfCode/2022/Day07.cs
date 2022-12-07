using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
	public class Day07 : IDay
	{
		private List<string> input;

		public void GetInput()
		{
			input = InputHelper.GetStringsFromInput(2022, 7);
		}

		public void Solve()
		{
			var root = GetFilesystemRoot(input);
			var directorySizes = GetDirectorySizes(root);

			var partOne = directorySizes.Where(dir => dir.Value <= 100000).Sum(dir => dir.Value);
			Console.WriteLine($"The total size of all directories of size <= 100000 (part one) is {partOne}");

			var totalSpace = 70000000;
			var targetFreeSpace = 30000000;
			var currentFreeSpace = totalSpace - root.GetTotalDirectorySize();
			var spaceToFreeUp = targetFreeSpace - currentFreeSpace;

			var partTwo = directorySizes.Where(dir => dir.Value >= spaceToFreeUp).Min(dir => dir.Value);
			Console.WriteLine($"The smallest directory to delete to achieve needed free space (part two) has size {partTwo}");
		}

		private Directory GetFilesystemRoot(List<string> lines)
		{
			var root = new Directory
			{
				Name = "",
				Parent = null,
				Children = new List<Directory>(),
				Files = new List<File>(),
			};

			var currentDir = root;
			foreach (var line in lines)
			{
				if (line.StartsWith("dir"))
				{
					currentDir.Children.Add(new Directory
					{
						Name = line.Split(' ')[1],
						Parent = currentDir,
						Children = new List<Directory>(),
						Files = new List<File>(),
					});
				}
				else if (line.StartsWith("$ cd"))
				{
					var destination = line.Split(' ').Last();
					if (destination == "..")
					{
						currentDir = currentDir.Parent;
					}
					else if (destination == "/")
					{
						currentDir = root;
					}
					else
					{
						currentDir = currentDir.Children.Single(d => d.Name == destination);
					}
				}
				else if (int.TryParse(line.Split(' ')[0], out int fileSize))
				{
					currentDir.Files.Add(new File { Name = line.Split(' ')[1], Size = fileSize });
				}
			}

			return root;
		}

		private Dictionary<string, int> GetDirectorySizes(Directory root)
		{
			var directorySizes = new Dictionary<string, int>();
			var q = new Queue<Directory>();
			q.Enqueue(root);
			while (q.Any())
			{
				var currentDir = q.Dequeue();
				directorySizes[currentDir.GetFullDirectoryPath()] = currentDir.GetTotalDirectorySize();
				foreach (var child in currentDir.Children)
				{
					q.Enqueue(child);
				}
			}
			return directorySizes;
		}

		internal class Directory
		{
			public string Name { get; set; }
			public Directory Parent { get; set; }
			public List<Directory> Children { get; set; }
			public List<File> Files { get; set; }

			public int GetTotalDirectorySize()
			{
				var sum = Files.Sum(f => f.Size);
				foreach (var child in Children)
				{
					sum += child.GetTotalDirectorySize();
				}
				return sum;
			}

			public string GetFullDirectoryPath()
			{
				var name = Name;
				var parent = Parent;
				while (parent != null)
				{
					name = $"{parent.Name}/{name}";
					parent = parent.Parent;
				}
				return name;
			}

		}

		internal class File
		{
			public string Name { get; set; }
			public int Size { get; set; }
		}
	}
}
