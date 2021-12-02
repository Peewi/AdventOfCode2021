// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var lines = File.ReadAllLines("Input.txt");
int prev = int.Parse(lines[0]);
int increases = 0;
for (int i = 1; i < lines.Length; i++)
{
	int current = int.Parse(lines[i]);
	if (current > prev)
	{
		increases++;
	}
	prev = current;
}
Console.WriteLine($"Increases: {increases}");