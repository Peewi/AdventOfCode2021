// See https://aka.ms/new-console-template for more information
using AdventOfCode22B;

Console.WriteLine("Advent of Code day 22 part 2");
string[] input = File.ReadAllLines("Input.txt");
List<Cube> cubes = new List<Cube>(input.Length * 2);
for (int i = 0; i < input.Length; i++)
{
	bool newSetting = input[i][0..2] == "on";
	string[] ranges = input[i].Split(' ')[1].Split(',');
	string[] xSplit = ranges[0].Split('.');
	string[] ySplit = ranges[1].Split('.');
	string[] zSplit = ranges[2].Split('.');
	int xmin = int.Parse(xSplit[0].Substring(2));
	int xmax = int.Parse(xSplit[^1]);
	int ymin = int.Parse(ySplit[0].Substring(2));
	int ymax = int.Parse(ySplit[^1]);
	int zmin = int.Parse(zSplit[0].Substring(2));
	int zmax = int.Parse(zSplit[^1]);
	var newcube = new Cube(xmin, xmax, ymin, ymax, zmin, zmax);
	for (int j = cubes.Count - 1; j >= 0; j--)
	{
		if (newcube.Overlaps(cubes[j]))
		{
			Console.WriteLine($"New cube overlapped other cube");
			cubes.AddRange(cubes[j].Carve(newcube));
			cubes.RemoveAt(j);
		}
	}
	if (newSetting)
	{
		cubes.Add(newcube);
		Console.WriteLine($"Added cube");
	}
	Console.WriteLine($"Performed step {i + 1}");
}
long cubesOn = 0;
foreach (var item in cubes)
{
	cubesOn += item.Area();
}
Console.WriteLine($"{cubesOn} cubes are on");