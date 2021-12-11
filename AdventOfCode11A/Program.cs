// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 11 part 1");
string[] input = File.ReadAllLines("Input.txt");
int[,] grid = new int[input[0].Length, input.Length];
bool[,] flashThisStep = new bool[input[0].Length, input.Length];
for (int line = 0; line < input.Length; line++)
{
	for (int i = 0; i < input[line].Length; i++)
	{
		grid[i, line] = int.Parse(input[line][i].ToString());
	}
}
int flashCount = 0;
for (int i = 0; i < 100; i++)
{
	flashThisStep = new bool[input[0].Length, input.Length];
	for (int x = 0; x < grid.GetUpperBound(0) + 1; x++)
	{
		for (int y = 0; y < grid.GetUpperBound(1) + 1; y++)
		{
			grid[x, y]++;
		}
	}
	for (int x = 0; x < grid.GetUpperBound(0) + 1; x++)
	{
		for (int y = 0; y < grid.GetUpperBound(1) + 1; y++)
		{
			if (!flashThisStep[x, y] && grid[x, y] > 9)
			{
				Flash(x, y);
			}
		}
	}
	for (int x = 0; x < grid.GetUpperBound(0) + 1; x++)
	{
		for (int y = 0; y < grid.GetUpperBound(1) + 1; y++)
		{
			if (grid[x, y] > 9)
			{
				flashCount++;
				grid[x, y] = 0;
			}
		}
	}
	Console.WriteLine($"Iteration {i}, {flashCount} total flashes");
}

void Flash(int x, int y)
{
	flashThisStep[x, y] = true;
	for (int adjX = Math.Max(x - 1, 0); adjX < Math.Min(x + 2, grid.GetUpperBound(0) + 1); adjX++)
	{
		for (int adjY = Math.Max(y - 1, 0); adjY < Math.Min(y + 2, grid.GetUpperBound(1) + 1); adjY++)
		{
			if (true)
			{
				grid[adjX, adjY]++;
				if (!flashThisStep[adjX, adjY] && grid[adjX, adjY] > 9)
				{
					flashThisStep[adjX, adjY] = true;
					Flash(adjX, adjY);
				}
			}
		}
	}
}