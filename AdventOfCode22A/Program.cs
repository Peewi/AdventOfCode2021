// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 22 part 1");
string[] input = File.ReadAllLines("Input.txt");
const int RANGEMIN = -50;
const int RANGEMAX = 50;
bool[,,] cubes = new bool[101, 101, 101];
for (int i = 0; i < input.Length; i++)
{
	bool newSetting = input[i][0..2] == "on";
	string[] ranges = input[i].Split(' ')[1].Split(',');
	string[] xSplit = ranges[0].Split('.');
	string[] ySplit = ranges[1].Split('.');
	string[] zSplit = ranges[2].Split('.');
	int xmin = int.Parse(xSplit[0].Substring(2));
	xmin = Math.Max(xmin, RANGEMIN);
	int xmax = int.Parse(xSplit[^1]);
	xmax = Math.Min(xmax, RANGEMAX);
	int ymin = int.Parse(ySplit[0].Substring(2));
	ymin = Math.Max(ymin, RANGEMIN);
	int ymax = int.Parse(ySplit[^1]);
	ymax = Math.Min(ymax, RANGEMAX);
	int zmin = int.Parse(zSplit[0].Substring(2));
	zmin = Math.Max(zmin, RANGEMIN);
	int zmax = int.Parse(zSplit[^1]);
	zmax = Math.Min(zmax, RANGEMAX);
	for (int x = xmin; x <= xmax; x++)
	{
		for (int y = ymin; y <= ymax; y++)
		{
			for (int z = zmin; z <= zmax; z++)
			{
				cubes[x - RANGEMIN, y - RANGEMIN, z - RANGEMIN] = newSetting;
			}
		}
	}
	Console.WriteLine($"Performed step {i + 1}");
}
int cubesOn = 0;
for (int x = 0; x <= cubes.GetUpperBound(0); x++)
{
	for (int y = 0; y <= cubes.GetUpperBound(1); y++)
	{
		for (int z = 0; z <= cubes.GetUpperBound(2); z++)
		{
			if (cubes[x, y, z])
			{
				cubesOn++;
			}
		}
	}
}
Console.WriteLine($"{cubesOn} cubes are on");