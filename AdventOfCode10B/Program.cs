// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 10 part 2");
string[] input = File.ReadAllLines("Input.txt");
Dictionary<char, char> bracketPairs = new Dictionary<char, char>();
bracketPairs.Add('(', ')');
bracketPairs.Add('{', '}');
bracketPairs.Add('[', ']');
bracketPairs.Add('<', '>');
Dictionary<char, int> pointValues = new Dictionary<char, int>();
pointValues.Add(')', 1);
pointValues.Add(']', 2);
pointValues.Add('}', 3);
pointValues.Add('>', 4);
List<long> points = new List<long>();
for (int line = 0; line < input.Length; line++)
{
	long linePoints = 0;
	bool corrupt = false;
	Stack<char> openings = new Stack<char>();
	for (int i = 0; i < input[line].Length; i++)
	{
		switch (input[line][i])
		{
			case '(':
			case '[':
			case '{':
			case '<':
				openings.Push(bracketPairs[input[line][i]]);
				break;
			case ')':
			case ']':
			case '}':
			case '>':
				if (input[line][i] == openings.Peek())
				{
					openings.Pop();
				}
				else
				{
					corrupt = true;
				}
				break;
			default:
				break;
		}
		if (corrupt)
		{
			break;
		}
	}
	while (!corrupt && openings.Count != 0)
	{
		linePoints *= 5;
		linePoints += pointValues[openings.Pop()];
	}
	if (linePoints != 0)
	{
		points.Add(linePoints);
		Console.WriteLine($"Line worth {linePoints} points");
	}
	else
	{
		Console.WriteLine("Corrupt line");
	}
}
points.Sort();
Console.WriteLine($"Middle points value {points[points.Count / 2]}");