// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 03 part 1");
var lines = File.ReadAllLines("Input.txt");
int[] ones = new int[lines[0].Length];
for (int i = 0; i < lines.Length; i++)
{
	for (int j = 0; j < lines[i].Length; j++)
	{
		if (lines[i][j] == '1')
		{
			ones[j]++;
		}
	}
}
int gammaRate = 0;
int epsilonRate = 0;
for (int i = 0; i < ones.Length; i++)
{
	if (ones[^(1+i)] > lines.Length / 2)
	{
		gammaRate += (int)Math.Pow(2, i);
	}
	else
	{
		epsilonRate += (int)Math.Pow(2, i);
	}
}
Console.WriteLine($"Gamme rate: {gammaRate}");
Console.WriteLine($"Epsilon rate: {epsilonRate}");
Console.WriteLine($"Gamme * Epsilon: {gammaRate * epsilonRate}");