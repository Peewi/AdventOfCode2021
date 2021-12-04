// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 04 part 1");
var lines = File.ReadAllLines("Input.txt");
var drawnNumbers = lines[0].Split(',');
int boardStart = 0;
bool[] columnWins = new bool[5];
int winningNumberIndex = 0;
for (int bingoNum = 0; bingoNum < drawnNumbers.Length; bingoNum++)
{
	bool winner = false;
	winningNumberIndex = bingoNum;
	for (int i = 1; i < lines.Length; i++)
	{
		if (string.IsNullOrWhiteSpace(lines[i]))
		{
			if (columnWins.Contains(true))
			{
				winner = true;
				break;
			}
			else
			{
				boardStart = i + 1;
				columnWins = new bool[] { true, true, true, true, true };
			}
		}
		else
		{
			var row = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
			bool WinningRow = true;
			for (int j = 0; j < row.Length; j++)
			{
				bool numberFound = drawnNumbers[0..(1+bingoNum)].Contains(row[j]);
				WinningRow &= numberFound;
				columnWins[j] &= numberFound;
			}
			if (WinningRow)
			{
				winner = true;
				break;
			}
		}
	}
	if (winner)
	{
		break;
	}
}
// sum it up.
int boardSum = 0;
int winningNumber = int.Parse(drawnNumbers[winningNumberIndex]);
for (int i = boardStart; i < lines.Length; i++)
{
	if (string.IsNullOrWhiteSpace(lines[i]))
	{
		break;
	}
	var row = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
	for (int j = 0; j < row.Length; j++)
	{
		bool numberFound = drawnNumbers[0..(1 + winningNumberIndex)].Contains(row[j]);
		if (!numberFound)
		{
			boardSum += int.Parse(row[j]);
		}
	}
}
Console.WriteLine($"The winning number is {winningNumber}");
Console.WriteLine($"The sum of the board's remaining numbers is {boardSum}");
Console.WriteLine($"The final score is {winningNumber * boardSum}");