// See https://aka.ms/new-console-template for more information
using System.Drawing;

Console.WriteLine("Advent of Code day 09 part 2");
string[] input = File.ReadAllLines("Input.txt");
int[,] heightmap = new int[input[0].Length, input.Length];
for (int i = 0; i < input.Length; i++)
{
	for (int j = 0; j < input[i].Length; j++)
	{
		heightmap[j, i] = int.Parse(input[i][j].ToString());
	}
}
int[] topThree = new int[3];
for (int x = heightmap.GetUpperBound(0); x >= 0; x--)
{
	for (int y = heightmap.GetUpperBound(1); y >= 0; y--)
	{
		HashSet<Point> basinPoints = new HashSet<Point>();
		HashSet<Point> checkedPoints = new HashSet<Point>();
		bool left = x == 0 || heightmap[x, y] < heightmap[x - 1, y];
		bool right = x == heightmap.GetUpperBound(0) || heightmap[x, y] < heightmap[x + 1, y];
		bool up = y == 0 || heightmap[x, y] < heightmap[x, y - 1];
		bool down = y == heightmap.GetUpperBound(1) || heightmap[x, y] < heightmap[x, y + 1];
		if (left && right && down && up)
		{
			basinPoints.Add(new Point(x, y));
			var n = basinNeighbors(x, y);
			foreach (var item in n)
			{
				basinPoints.Add(item);
			}
			List<Point> newNeighbors = new List<Point>();
			while (checkedPoints.Count < basinPoints.Count)
			{
				newNeighbors.Clear();
				foreach (var bp in basinPoints)
				{
					if (!checkedPoints.Contains(bp))
					{
						checkedPoints.Add(bp);
						newNeighbors.AddRange(basinNeighbors(bp.X, bp.Y));
					}
				}
				foreach (var nbp in newNeighbors)
				{
					basinPoints.Add(nbp);
				}
			}
			Console.WriteLine($"Basin found with size {basinPoints.Count}");
			for (int i = 0; i < topThree.Length; i++)
			{
				bool inserted = false;
				if (basinPoints.Count > topThree[i])
				{
					inserted = true;
					for (int j = topThree.Length - 1; j > i; j--)
					{
						topThree[j] = topThree[j - 1];
					}
					topThree[i] = basinPoints.Count;
				}
				if (inserted)
				{
					break;
				}
			}
		}
	}
}
Console.WriteLine($"Top three sizes: {topThree[0]}, {topThree[1]} and {topThree[2]}");
Console.WriteLine($"{topThree[0]} * {topThree[1]} * {topThree[2]} = {topThree[0]* topThree[1]* topThree[2]}");

HashSet<Point> basinNeighbors(int x, int y)
{
	var retval = new HashSet<Point>();
	bool left = x != 0 && heightmap[x, y] < heightmap[x - 1, y] && heightmap[x - 1, y] < 9;
	if (left)
	{
		retval.Add(new Point(x - 1, y));
	}
	bool right = x != heightmap.GetUpperBound(0) && heightmap[x, y] < heightmap[x + 1, y] && heightmap[x + 1, y] < 9;
	if (right)
	{
		retval.Add(new Point(x + 1, y));
	}
	bool up = y != 0 && heightmap[x, y] < heightmap[x, y - 1] && heightmap[x, y - 1] < 9;
	if (up)
	{
		retval.Add(new Point(x, y - 1));
	}
	bool down = y != heightmap.GetUpperBound(1) && heightmap[x, y] < heightmap[x, y + 1] && heightmap[x, y + 1] < 9;
	if (down)
	{
		retval.Add(new Point(x, y + 1));
	}
	return retval;
}