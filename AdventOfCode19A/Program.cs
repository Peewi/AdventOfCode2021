// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 19 part 1");
string[] input = File.ReadAllLines("Input.txt");
List<List<(int x, int y, int z)>> scanners = new List<List<(int x, int y, int z)>>();
foreach (var line in input)
{
	if (!string.IsNullOrWhiteSpace(line))
	{
		if (line[0..3] == "---")
		{
			scanners.Add(new List<(int, int, int)>());
		}
		else
		{
			var split = line.Split(',');
			scanners[^1].Add((int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
		}
	}
}
Console.WriteLine($"Read {scanners.Count} scanners from input.");
HashSet<(int x, int y, int z)> uniqueBeacons = new HashSet<(int x, int y, int z)>();
foreach (var beacon in scanners[0])
{
	uniqueBeacons.Add(beacon);
}
for (int i = 1; i < scanners.Count; i++)
{
	List<(int x, int y, int z)> topLeftCandidates = new List<(int x, int y, int z)>();
	topLeftCandidates.Add(scanners[i][0]);
	for (int j = 1; j < scanners[i].Count; j++)
	{
		bool better = scanners[i][j].x < topLeftCandidates[0].x;
		better = better || (scanners[i][j].x == topLeftCandidates[0].x
			&& scanners[i][j].y < topLeftCandidates[0].y);
		better = better || (scanners[i][j].y == topLeftCandidates[0].y
			&& scanners[i][j].z < topLeftCandidates[0].z);
		if (better)
		{
			topLeftCandidates.Insert(0, scanners[i][j]);
		}
	}
}
