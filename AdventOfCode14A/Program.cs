// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 14 part 1");
string[] input = File.ReadAllLines("Input.txt");
string working = input[0];
Console.WriteLine($"Template:	{working}");
for (int i = 0; i < 10; i++)
{
	string newThing = "";
	for (int j = 0; j < working.Length - 1; j++)
	{
		for (int k = 2; k < input.Length; k++)
		{
			if (working.Substring(j, 2) == input[k].Substring(0, 2))
			{
				newThing += working[j];
				newThing += input[k][^1];
				break;
			}
		}
	}
	newThing += working[^1];
	working = newThing;
	Console.WriteLine($"After step {i+1}:	{working}");
}
Dictionary<char, int> occurrences = new Dictionary<char, int>();
for (int i = 0; i < working.Length; i++)
{
	if (occurrences.ContainsKey(working[i]))
	{
		occurrences[working[i]]++;
	}
	else
	{
		occurrences[working[i]] = 1;
	}
}
int mostCommon = 0;
int leastCommon = working.Length;
foreach (var item in occurrences)
{
	mostCommon = Math.Max(item.Value, mostCommon);
	leastCommon = Math.Min(item.Value, leastCommon);
}
Console.WriteLine($"Most common: {mostCommon}, least common: {leastCommon}");
Console.WriteLine($"Most common - least common: {mostCommon - leastCommon}");