// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 20 part 2");
string[] input = File.ReadAllLines("Input.txt");
bool[] algo = new bool[input[0].Length];
for (int i = 0; i < input[0].Length; i++)
{
	algo[i] = input[0][i] == '#';
}
const int ITERATIONS = 100;
bool[,] img = new bool[input[2].Length + 2 * ITERATIONS, input.Length - 2 + 2 * ITERATIONS];
for (int y = 2; y < input.Length; y++)
{
	for (int x = 0; x < input[y].Length; x++)
	{
		img[y - 2 + ITERATIONS, x + ITERATIONS] = input[y][x] == '#';
	}
}
for (int x = 0; x < img.GetUpperBound(0) + 1; x++)
{
	for (int y = 0; y < img.GetUpperBound(1) + 1; y++)
	{
		char vis = img[x, y] ? '#' : '.';
		Console.Write(vis);
	}
	Console.Write('\n');
}
Console.WriteLine("Image parsed");
int ones = 0;
foreach (var item in img)
{
	if (item)
	{
		ones++;
	}
}
Console.WriteLine($"{ones} pixels lit up");
// DO STUFF
for (int i = 0; i < ITERATIONS; i++)
{
	bool[,] newImg;
	if (img.Clone() is bool[,] cloned)
	{
		newImg = cloned;
	}
	else
	{
		throw new Exception("WHAT?");
	}
	for (int x = 0; x < img.GetUpperBound(0) + 1; x++)
	{
		for (int y = 0; y < img.GetUpperBound(1) + 1; y++)
		{
			int newPixelIndex = 0;
			int pow = 0;
			for (int xOff = 1; xOff >= -1; xOff--)
			{
				for (int yOff = 1; yOff >= -1; yOff--)
				{
					if (x + xOff >= 0 && y + yOff >= 0
						&& x + xOff <= img.GetUpperBound(0)
						&& y + yOff <= img.GetUpperBound(1))
					{ // in bounds
						if (img[x + xOff, y + yOff])
						{
							newPixelIndex += (int)Math.Pow(2, pow);
						}
					}
					else
					{ // out of bounds
						if (i % 2 != 0)
						{
							newPixelIndex += (int)Math.Pow(2, pow);
						}
					}
					pow++;
				}
			}
			newImg[x, y] = algo[newPixelIndex];
		}
	}
	img = newImg;
	//for (int x = 0; x < img.GetUpperBound(0) + 1; x++)
	//{
	//	for (int y = 0; y < img.GetUpperBound(1) + 1; y++)
	//	{
	//		char vis = img[x, y] ? '#' : '.';
	//		Console.Write(vis);
	//	}
	//	Console.Write('\n');
	//}
	Console.WriteLine($"Image enhanced {i + 1} times");
	ones = 0;
	foreach (var item in img)
	{
		if (item)
		{
			ones++;
		}
	}
	Console.WriteLine($"{ones} pixels lit up");
	if (i == 49)
	{
		break;
	}
}
