// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 15 part 1");
string[] input = File.ReadAllLines("Input.txt");
int height = input.Length;
int width = input[0].Length;
int[,] risk = new int[width, height];
int[,] pathRisk = new int[width, height];
for (int x = 0; x < width; x++)
{
	for (int y = 0; y < height; y++)
	{
		risk[x, y] = int.Parse(input[y][x].ToString());
		pathRisk[x, y] = int.MaxValue;
	}
}
pathRisk[0, 0] = 0;
for (int i = 0; i < 10; i++)
{
	for (int x = 0; x < width; x++)
	{
		for (int y = 0; y < height; y++)
		{
			if (x - 1 >= 0)
			{
				pathRisk[x - 1, y] = Math.Min(pathRisk[x - 1, y], pathRisk[x, y] + risk[x - 1, y]);
			}
			if (x + 1 < width)
			{
				pathRisk[x + 1, y] = Math.Min(pathRisk[x + 1, y], pathRisk[x, y] + risk[x + 1, y]);
			}
			if (y - 1 >= 0)
			{
				pathRisk[x, y - 1] = Math.Min(pathRisk[x, y - 1], pathRisk[x, y] + risk[x, y - 1]);
			}
			if (y + 1 < height)
			{
				pathRisk[x, y + 1] = Math.Min(pathRisk[x, y + 1], pathRisk[x, y] + risk[x, y + 1]);
			}
		}
	}
	Console.WriteLine($"Path risk of bottom right: {pathRisk[width - 1, height - 1]}");
}
Console.WriteLine($"Path risk of bottom right: {pathRisk[width - 1, height - 1]}");