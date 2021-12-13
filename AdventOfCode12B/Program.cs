// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 12 part 2");
string[] input = File.ReadAllLines("Input.txt");
List<string> paths = new List<string>();
List<bool> pathRevisit = new List<bool>();
paths.Add("start,");
pathRevisit.Add(false);
bool pathAdded = true;
while (pathAdded)
{
	pathAdded = false;
	for (int i = 0; i < paths.Count; i++)
	{
		int connectorsFound = 0;
		string path = paths[i];
		bool revisited = pathRevisit[i];
		string connector = paths[i].Split(',', StringSplitOptions.RemoveEmptyEntries)[^1];
		if (connector != "end")
		{
			for (int j = 0; j < input.Length; j++)
			{
				string[] connection = input[j].Split('-');
				string next = null;
				bool alreadyUsed = false;
				if (connector == connection[0])
				{
					next = connection[1];
					alreadyUsed = next == next.ToLower() && path.Contains($"{next},");
				}
				else if (connector == connection[1])
				{
					next = connection[0];
					alreadyUsed = next == next.ToLower() && path.Contains($"{next},");
				}
				if (next == "start")
				{
					next = null;
				}
				if (next != null && (next != next.ToLower() || (!alreadyUsed || !revisited)))
				{
					pathAdded = true;
					if (connectorsFound == 0)
					{
						paths[i] = path + next + ",";
						pathRevisit[i] = revisited || alreadyUsed;
					}
					else
					{
						paths.Add(path + next + ",");
						pathRevisit.Add(revisited || alreadyUsed);
					}
					connectorsFound++;
				}
			}
		}
	}
}
int finishedPaths = 0;
paths.Sort();
foreach (var item in paths)
{
	if (item[^4..^0] == "end,")
	{
		finishedPaths++;
		Console.WriteLine(item);
	}
}
Console.WriteLine($"{paths.Count} paths");
Console.WriteLine($"{finishedPaths} finished paths");