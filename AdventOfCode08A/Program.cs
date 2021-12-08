// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 08 part 1");
string[] input = File.ReadAllLines("Input.txt");
int uniques = 0;
for (int i = 0; i < input.Length; i++)
{
	string[] split = input[i].Split('|');
	string[] output = split[1].Split(' ');
	for (int j = 0; j < output.Length; j++)
	{
		if (output[j].Length == 2
			|| output[j].Length == 3
			|| output[j].Length == 4
			|| output[j].Length == 7)
		{
			uniques++;
		}
	}
}
Console.WriteLine(uniques);