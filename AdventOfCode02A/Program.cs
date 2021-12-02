// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 02 part 1");
int depth = 0;
int forward = 0;
var input = File.ReadAllLines("Input.txt");
for (int i = 0; i < input.Length; i++)
{
	string[] command = input[i].Split(' ');
	int commandAmnt = int.Parse(command[1]);
	switch (command[0])
	{
		case "forward":
			forward += commandAmnt;
			break;
		case "down":
			depth += commandAmnt;
			break;
		case "up":
			depth -= commandAmnt;
			break;
		default:
			break;
	}
}
Console.WriteLine($"Depth: {depth}, Forward: {forward}, Depth*Forward: {depth * forward}");