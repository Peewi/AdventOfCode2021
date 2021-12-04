// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 04 part 2");
var lines = File.ReadAllLines("Input.txt");
var drawnNumbers = lines[0].Split(',');
int boardStart = 0;
bool[] columnWins = new bool[5];
int winningNumberIndex = 0;
int numberOfBoards = 0;
for (int i = 0; i < lines.Length; i++)
{
	if (string.IsNullOrWhiteSpace(lines[i]))
	{
		numberOfBoards++;
	}
}
HashSet<int> winningBoards = new HashSet<int>();
int currentBoard = 0;
int lastWinningBoard = 0;
for (int bingoNum = 0; bingoNum < drawnNumbers.Length; bingoNum++)
{
	bool winner = false;
	int boardschecked = 0;
	for (int i = 1; i < lines.Length; i++)
	{
		if (string.IsNullOrWhiteSpace(lines[i]))
		{
			if (columnWins.Contains(true))
			{
				winner = true;
				if (winningBoards.Add(currentBoard))
				{
					lastWinningBoard = currentBoard;
					winningNumberIndex = bingoNum;
				}
			}
			boardschecked++;
			boardStart = i + 1;
			currentBoard = i + 1;
			columnWins = new bool[] { true, true, true, true, true };
			
		}
		else if (true)
		{
			var row = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
			bool WinningRow = true;
			for (int j = 0; j < row.Length; j++)
			{
				bool numberFound = drawnNumbers[0..(1 + bingoNum)].Contains(row[j]);
				WinningRow &= numberFound;
				columnWins[j] &= numberFound;
			}
			if (WinningRow)
			{
				winner = true;
				if (winningBoards.Add(currentBoard))
				{
					lastWinningBoard = currentBoard;
					winningNumberIndex = bingoNum;
				}
			}
		}
	}
	if (winningBoards.Count == numberOfBoards)
	{
		break;
	}
}
// sum it up.
int boardSum = 0;
int winningNumber = int.Parse(drawnNumbers[winningNumberIndex]);
for (int i = lastWinningBoard; i < lines.Length; i++)
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
Console.WriteLine($"{winningBoards.Count} boards have won");
Console.WriteLine($"The final winning number is {winningNumber}");
Console.WriteLine($"The sum of the board's remaining numbers is {boardSum}");
Console.WriteLine($"The final score is {winningNumber * boardSum}");