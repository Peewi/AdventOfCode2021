// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 02 part 2");
int depth = 0;
int forward = 0;
int aim = 0;
var input = File.ReadAllLines("Input.txt");
for (int i = 0; i < input.Length; i++)
{
	string[] command = input[i].Split(' ');
	int commandAmnt = int.Parse(command[1]);
	switch (command[0])
	{
		case "forward":
			forward += commandAmnt;
			depth += aim * commandAmnt;
			break;
		case "down":
			aim += commandAmnt;
			break;
		case "up":
			aim -= commandAmnt;
			break;
		default:
			break;
	}
}
Console.WriteLine($"Depth: {depth}, Forward: {forward}, Depth*Forward: {depth * forward}");