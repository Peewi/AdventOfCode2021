// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 25 part 1");
string[] input = File.ReadAllLines("Input.txt");
int height = input.Length;
int width = input[0].Length;
cucumber[,] grid = new cucumber[width, height];
for (int i = 0; i < input.Length; i++)
{
	for (int j = 0; j < input[i].Length; j++)
	{
		switch (input[i][j])
		{
			case '>':
				grid[j, i] = cucumber.EastFacing;
				break;
			case 'v':
				grid[j, i] = cucumber.SouthFacing;
				break;
			default:
				break;
		}
	}
}
Console.WriteLine("Read input");
bool moved = true;
int steps = 0;
cucumber[,] newGrid;
while (moved)
{
	if (grid.Clone() is cucumber[,] clone)
	{
		newGrid = clone;
	}
	else
	{
		throw new Exception("???");
	}
	moved = false;
	for (int x = 0; x < width; x++)
	{
		for (int y = 0; y < height; y++)
		{
			if (grid[x,y] == cucumber.EastFacing)
			{
				int targetX = (x + 1) % width;
				if (grid[targetX, y] == cucumber.Empty)
				{
					moved = true;
					newGrid[x, y] = cucumber.Empty;
					newGrid[targetX, y] = cucumber.EastFacing;
				}
			}
		}
	}
	grid = newGrid;
	if (grid.Clone() is cucumber[,] clone2)
	{
		newGrid = clone2;
	}
	else
	{
		throw new Exception("???");
	}
	for (int x = 0; x < width; x++)
	{
		for (int y = 0; y < height; y++)
		{
			if (grid[x, y] == cucumber.SouthFacing)
			{
				int targetY = (y + 1) % height;
				if (grid[x, targetY] == cucumber.Empty)
				{
					moved = true;
					newGrid[x, y] = cucumber.Empty;
					newGrid[x, targetY] = cucumber.SouthFacing;
				}
			}
		}
	}
	grid = newGrid;
	steps++;
	Console.WriteLine($"Step {steps} done.");
}
enum cucumber
{
	Empty,
	SouthFacing,
	EastFacing
}