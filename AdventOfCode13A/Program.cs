// See https://aka.ms/new-console-template for more information
using System.Drawing;

Console.WriteLine("Advent of Code day 13 part 1");
string[] input = File.ReadAllLines("Input.txt");
List<Point> dots = new List<Point>();
HashSet<Point> uniqueDots = new HashSet<Point>();
int foldInstructionStart = 0;
for (int i = 0; i < input.Length; i++)
{
	if (string.IsNullOrWhiteSpace(input[i]))
	{
		foldInstructionStart = i + 1;
		break;
	}
	var foo = input[i].Split(',');
	dots.Add(new Point(int.Parse(foo[0]), int.Parse(foo[1])));
}
Console.WriteLine($"Before any folding, there are {dots.Count} dots");
for (int i = foldInstructionStart; i < input.Length; i++)
{
	var foo = input[i].Split('=');
	int foldCoord = int.Parse(foo[1]);
	bool foldX = foo[0][^1] == 'x';
	for (int j = 0; j < dots.Count; j++)
	{
		if (foldX)
		{
			if (dots[j].X > foldCoord)
			{
				int newX = foldCoord + foldCoord - dots[j].X;
				dots[j] = new Point(newX, dots[j].Y);
			}
			uniqueDots.Add(dots[j]);
		}
		else
		{
			if (dots[j].Y > foldCoord)
			{
				int newY = foldCoord + foldCoord - dots[j].Y;
				dots[j] = new Point(dots[j].X, newY);
			}
			uniqueDots.Add(dots[j]);
		} 
	}
	dots.Clear();
	dots.AddRange(uniqueDots);
	uniqueDots.Clear();
	Console.WriteLine($"After folding, there are {dots.Count} dots");
	break;
}