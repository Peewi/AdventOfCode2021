// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 10 part 1");
string[] input = File.ReadAllLines("Input.txt");
Dictionary<char, char> bracketPairs = new Dictionary<char, char>();
bracketPairs.Add('(', ')');
bracketPairs.Add('{', '}');
bracketPairs.Add('[', ']');
bracketPairs.Add('<', '>');
Dictionary<char, int> pointValues = new Dictionary<char, int>();
pointValues.Add(')', 3);
pointValues.Add(']', 57);
pointValues.Add('}', 1197);
pointValues.Add('>', 25137);
int points = 0;
for (int line = 0; line < input.Length; line++)
{
	Stack<char> openings = new Stack<char>();
	for (int i = 0; i < input[line].Length; i++)
	{
		bool illegalFound = false;
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
					Console.WriteLine($"Found {input[line][i]}, expected {openings.Peek()}");
					points += pointValues[input[line][i]];
					illegalFound = true;
				}
				break;
			default:
				break;
		}
		if (illegalFound)
		{
			break;
		}
	}
}
Console.WriteLine($"Points {points}");