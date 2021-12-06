// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 06 part 1");
string[] input = File.ReadAllLines("Input.txt");
string[] fishStrings = input[0].Split(',');
List<int> fishTimers = new List<int>(fishStrings.Length);
for (int i = 0; i < fishStrings.Length; i++)
{
	fishTimers.Add(int.Parse(fishStrings[i]));
}
Console.WriteLine($"There are {fishTimers.Count} fish at the beginning");
List<int> newFish = new List<int>();
for (int day = 0; day < 80; day++)
{
	newFish.Clear();
	for (int i = 0; i < fishTimers.Count; i++)
	{
		if (fishTimers[i] == 0)
		{
			fishTimers[i] = 6;
			newFish.Add(8);
		}
		else
		{
			fishTimers[i]--;
		}
	}
	fishTimers.AddRange(newFish);
	Console.WriteLine($"After {day+1} days, there are {fishTimers.Count} fish");
}
Console.WriteLine($"After 80 days, there are {fishTimers.Count} fish");