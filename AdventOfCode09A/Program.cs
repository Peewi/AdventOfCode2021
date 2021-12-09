// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 09 part 1");
string[] input = File.ReadAllLines("Input.txt");
int[,] heightmap = new int[input[0].Length, input.Length];
for (int i = 0; i < input.Length; i++)
{
	for (int j = 0; j < input[i].Length; j++)
	{
		heightmap[j, i] = int.Parse(input[i][j].ToString());
	}
}
int sum = 0;
for (int x = heightmap.GetUpperBound(0); x >= 0; x--)
{
	for (int y = heightmap.GetUpperBound(1); y >= 0; y--)
	{
		bool left = x == 0 || heightmap[x, y] < heightmap[x - 1, y];
		bool right = x == heightmap.GetUpperBound(0) || heightmap[x, y] < heightmap[x + 1, y];
		bool up = y == 0 || heightmap[x, y] < heightmap[x, y - 1];
		bool down = y == heightmap.GetUpperBound(1) || heightmap[x, y] < heightmap[x, y + 1];
		if (left && right && down && up)
		{
			sum += 1 + heightmap[x, y];
		}
	}
}
Console.WriteLine(sum);