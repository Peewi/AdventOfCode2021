// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 14 part 2");
string[] input = File.ReadAllLines("Input.txt");
string template = input[0];
Console.WriteLine($"Template:	{template}");
Dictionary<string, long> pairCounts = new Dictionary<string, long>();
for (int i = 0; i < template.Length - 1; i++)
{
	string pair = template.Substring(i, 2);
	pairCounts[pair] = pairCounts.GetValueOrDefault(pair) + 1;
}
Dictionary<string, (string, string)> pairKey = new Dictionary<string, (string, string)>();
for (int i = 2; i < input.Length; i++)
{
	pairKey[input[i][0..2]] = (input[i][0].ToString() + input[i][^1].ToString(), input[i][^1].ToString() + input[i][1].ToString());
}
Console.WriteLine(pairKey.Count);
for (int i = 0; i < 40; i++)
{
	Dictionary<string, long> newPairCounts = new Dictionary<string, long>();
	foreach (var item in pairCounts)
	{
		newPairCounts[pairKey[item.Key].Item1] = newPairCounts.GetValueOrDefault(pairKey[item.Key].Item1) + item.Value;
		newPairCounts[pairKey[item.Key].Item2] = newPairCounts.GetValueOrDefault(pairKey[item.Key].Item2) + item.Value;
	}
	pairCounts = newPairCounts;
	Console.WriteLine($"Step {i + 1}");
}
Dictionary<char, long> elementCount = new Dictionary<char, long>();
foreach (var item in pairCounts)
{
	elementCount[item.Key[0]] = elementCount.GetValueOrDefault(item.Key[0]) + item.Value;
	elementCount[item.Key[1]] = elementCount.GetValueOrDefault(item.Key[1]) + item.Value;
}
elementCount[template[0]]++;
elementCount[template[^1]]++;
long mostCommon = 0;
long leastCommon = long.MaxValue;
foreach (var item in elementCount)
{
	mostCommon = Math.Max(item.Value / 2, mostCommon);
	leastCommon = Math.Min(item.Value / 2, leastCommon);
}
Console.WriteLine($"Most common: {mostCommon}, least common: {leastCommon}");
Console.WriteLine($"Most common - least common: {mostCommon - leastCommon}");