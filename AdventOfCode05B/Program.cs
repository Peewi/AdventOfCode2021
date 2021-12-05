// See https://aka.ms/new-console-template for more information
using AdventOfCode05A;

Console.WriteLine("Advent of Code day 05 part 1");
var input = File.ReadAllLines("Input.txt");
Line[] MyLines = new Line[input.Length];
for (int i = 0; i < input.Length; i++)
{
	string[] row = input[i].Split("->", StringSplitOptions.TrimEntries);
	string[] xy1 = row[0].Split(',');
	string[] xy2 = row[1].Split(',');
	MyLines[i] = new Line(int.Parse(xy1[0]), int.Parse(xy1[1]), int.Parse(xy2[0]), int.Parse(xy2[1]));
}
int[,] area = new int[1000, 1000];
for (int i = 0; i < MyLines.Length; i++)
{
	if (true)
	{
		int xDir = Math.Sign(MyLines[i].X2 - MyLines[i].X1);
		int yDir = Math.Sign(MyLines[i].Y2 - MyLines[i].Y1);
		int xPos = MyLines[i].X1;
		int yPos = MyLines[i].Y1;
		while (true)
		{
			area[xPos, yPos]++;
			xPos += xDir;
			yPos += yDir;
			if (xPos == MyLines[i].X2 && yPos == MyLines[i].Y2)
			{
				area[xPos, yPos]++;
				break;
			}
		}
	}
}
int twoplus = 0;
Console.WriteLine("Area diagram:");
for (int i = 0; i <= area.GetUpperBound(1); i++)
{
	for (int j = 0; j <= area.GetUpperBound(0); j++)
	{
		if (area[j, i] >= 2)
		{
			twoplus++;
		}
		//if (area[j, i] > 0)
		//{
		//	Console.Write(area[j, i]);
		//}
		//else
		//{
		//	Console.Write(".");
		//}
	}
	//Console.Write('\n');
}
Console.WriteLine($"Points with 2 or more overlaps: {twoplus}");