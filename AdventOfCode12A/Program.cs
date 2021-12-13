// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 12 part 1");
string[] input = File.ReadAllLines("Input.txt");
List<string> paths = new List<string>();
paths.Add("start,");
bool pathAdded = true;
while (pathAdded)
{
	pathAdded = false;
	for (int i = 0; i < paths.Count; i++)
	{
		int connectorsFound = 0;
		string path = paths[i];
		string connector = paths[i].Split(',', StringSplitOptions.RemoveEmptyEntries)[^1];
		if (connector != "end")
		{
			for (int j = 0; j < input.Length; j++)
			{
				string[] connection = input[j].Split('-');
				string next = null;
				if (connector == connection[0])
				{
					next = connection[1];
				}
				else if (connector == connection[1])
				{
					next = connection[0];
				}
				if (next != null && (next != next.ToLower() || !path.Contains($"{next},")))
				{
					pathAdded = true;
					if (connectorsFound == 0)
					{
						paths[i] = path + next + ",";
					}
					else
					{
						paths.Add(path + next + ",");
					}
					connectorsFound++;
				}
			}
		}
	}
}
int finishedPaths = 0;
foreach (var item in paths)
{
	if (item[^4..^0] == "end,")
	{
		finishedPaths++;
	}
}
Console.WriteLine($"{paths.Count} paths");
Console.WriteLine($"{finishedPaths} finished paths");