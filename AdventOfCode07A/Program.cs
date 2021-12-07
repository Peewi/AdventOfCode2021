// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 07 part 1");
string[] input = File.ReadAllLines("Input.txt");
string[] posStrings = input[0].Split(',');
int[] pos = new int[posStrings.Length];
int maxPos = 0;
for (int i = 0; i < posStrings.Length; i++)
{
	pos[i] = int.Parse(posStrings[i]);
	maxPos = Math.Max(maxPos, pos[i]);
}
int[] fuelPositions = new int[maxPos];
for (int i = 0; i < maxPos; i++)
{
	for (int j = 0; j < pos.Length; j++)
	{
		fuelPositions[i] += Math.Abs(pos[j] - i);
	}
}
Console.WriteLine($"The smallest fuel expenditure is {fuelPositions.Min()}");