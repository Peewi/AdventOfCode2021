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
for (int day = 0; day < SIMLENGTH; day++)
{
	long spawners = fishTimers[0];
	for (int i = 0; i < SPAWNINTERVAL + SPAWNDELAY; i++)
	{
		if (i + 1 < SPAWNINTERVAL + SPAWNDELAY)
		{
			fishTimers[i] = fishTimers[i + 1];
			if (i == SPAWNINTERVAL - 1)
			{
				fishTimers[i] += spawners;
			}
		}
		else
		{
			fishTimers[i] = spawners;
		}
	}
	Console.WriteLine($"After {day+1} days, there are {fishTimers.Sum()} fish");
}
Console.WriteLine($"After {SIMLENGTH} days, there are {fishTimers.Sum()} fish");