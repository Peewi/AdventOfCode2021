// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 06 part 2");
const int SIMLENGTH = 256;
const int SPAWNINTERVAL = 7;
const int SPAWNDELAY = 2;
string[] input = File.ReadAllLines("Input.txt");
string[] fishStrings = input[0].Split(',');
long[] fishTimers = new long[SPAWNINTERVAL + SPAWNDELAY];
for (int i = 0; i < fishStrings.Length; i++)
{
	fishTimers[int.Parse(fishStrings[i])]++;
}
for (int i = 0; i < SIMLENGTH; i++)
{
	long spawners = fishTimers[0];
	for (int j = 0; j < SPAWNINTERVAL + SPAWNDELAY; j++)
	{
		if (j + 1 < SPAWNINTERVAL + SPAWNDELAY)
		{
			fishTimers[j] = fishTimers[j + 1];
			if (j == SPAWNINTERVAL - 1)
			{
				fishTimers[j] += spawners;
			}
		}
		else
		{
			fishTimers[j] = spawners;
		}
	}
	Console.WriteLine($"After {i+1} days, there are {fishTimers.Sum()} fish");
}
Console.WriteLine($"After {SIMLENGTH} days, there are {fishTimers.Sum()} fish");