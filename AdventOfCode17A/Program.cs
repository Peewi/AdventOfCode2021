// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 17 part 1");
string[] input = File.ReadAllLines("Input.txt");
string yPart = input[0].Split(',', StringSplitOptions.TrimEntries)[1];
yPart = yPart.Substring(2);
int targetYLow = int.Parse(yPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[0]);
int targetYHigh = int.Parse(yPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[1]);
Console.WriteLine($"Y target min: {targetYLow}, Y target max: {targetYHigh}");
int yHigh = 0;
int misses = 0;
int i = 1;
while (misses < 10)
{
	int yPos = 0;
	int yVel = i;
	int attemptYHigh = 0;
	while (yPos > targetYLow)
	{
		yPos += yVel;
		yVel--;
		attemptYHigh = Math.Max(attemptYHigh, yPos);
		if (yPos <= targetYHigh && yPos >= targetYLow)
		{
			yHigh = Math.Max(yHigh, attemptYHigh);
			misses = 0;
			break;
		}
	}
	i++;
	misses++;
}

Console.WriteLine($"Highest height: {yHigh}");
