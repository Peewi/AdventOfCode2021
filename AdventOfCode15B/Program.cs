// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 15 part 2");
string[] input = File.ReadAllLines("Input.txt");
int baseHeight = input.Length;
int realHeight = baseHeight * 5;
int baseWidth = input[0].Length;
int realWidth = baseWidth * 5;
long[,] risk = new long[realWidth, realHeight];
long[,] pathRisk = new long[realWidth, realHeight];
for (int x = 0; x < realWidth; x++)
{
	for (int y = 0; y < realHeight; y++)
	{
		int danger = int.Parse(input[y % baseHeight][x % baseWidth].ToString());
		int bonusDanger = x / baseWidth + y / baseHeight;
		risk[x, y] = (danger + bonusDanger - 1) % 9 + 1;
		pathRisk[x, y] = long.MaxValue;
	}
}
pathRisk[0, 0] = 0;
for (int i = 0; i < 10; i++)
{
	for (int x = 0; x < realWidth; x++)
	{
		for (int y = 0; y < realHeight; y++)
		{
			if (x - 1 >= 0)
			{
				pathRisk[x - 1, y] = Math.Min(pathRisk[x - 1, y], pathRisk[x, y] + risk[x - 1, y]);
			}
			if (x + 1 < realWidth)
			{
				pathRisk[x + 1, y] = Math.Min(pathRisk[x + 1, y], pathRisk[x, y] + risk[x + 1, y]);
			}
			if (y - 1 >= 0)
			{
				pathRisk[x, y - 1] = Math.Min(pathRisk[x, y - 1], pathRisk[x, y] + risk[x, y - 1]);
			}
			if (y + 1 < realHeight)
			{
				pathRisk[x, y + 1] = Math.Min(pathRisk[x, y + 1], pathRisk[x, y] + risk[x, y + 1]);
			}
		}
	}
	Console.WriteLine($"Path risk of bottom right: {pathRisk[realWidth - 1, realHeight - 1]}");
}
Console.WriteLine($"Path risk of bottom right: {pathRisk[realWidth - 1, realHeight - 1]}");