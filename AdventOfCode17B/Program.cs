// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 17 part 2");
string[] input = File.ReadAllLines("Input.txt");
string xPart = input[0].Split(',', StringSplitOptions.TrimEntries)[0];
xPart = xPart.Substring(15);
int targetXLow = int.Parse(xPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[0]);
int targetXHigh = int.Parse(xPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[1]);
Console.WriteLine($"X target min: {targetXLow}, X target max: {targetXHigh}");
string yPart = input[0].Split(',', StringSplitOptions.TrimEntries)[1];
yPart = yPart.Substring(2);
int targetYLow = int.Parse(yPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[0]);
int targetYHigh = int.Parse(yPart.Split(".", StringSplitOptions.RemoveEmptyEntries)[1]);
Console.WriteLine($"Y target min: {targetYLow}, Y target max: {targetYHigh}");
// 
int hits = 0;
for (int x = 1; x < 1000; x++)
{
	for (int y = -1000; y < 1000; y++)
	{
		int xPos = 0;
		int yPos = 0;
		int xVel = x;
		int yVel = y;
		while (yPos > targetYLow && xPos < targetXHigh)
		{
			xPos += xVel;
			yPos += yVel;
			xVel -= Math.Sign(xVel);
			yVel--;
			if (yPos <= targetYHigh && yPos >= targetYLow
				&& xPos >= targetXLow && xPos <= targetXHigh)
			{
				hits++;
				Console.WriteLine($"Hitting trajectory: {x},{y}");
				break;
			}
		}
	}
}
Console.WriteLine($"Total hits: {hits}");
