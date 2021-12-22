// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 21 part 1");
string[] input = File.ReadAllLines("Input.txt");
int[] playerPos = new int[input.Length];
for (int i = 0; i < input.Length; i++)
{
	playerPos[i] = int.Parse(input[i][^1].ToString());
}
int[] playerScore = new int[input.Length];
int diceRolls = 0;
const int DIESIDES = 100;

int winner = -1;
while (winner == -1)
{
	for (int p = 0; p < playerPos.Length; p++)
	{
		Console.Write($"Player {p + 1} rolls ");
		for (int i = 0; i < 3; i++)
		{
			diceRolls++;
			playerPos[p] += (diceRolls - 1) % DIESIDES + 1;
			Console.Write($"{(diceRolls - 1) % DIESIDES + 1}, ");
		}
		playerPos[p] = (playerPos[p] - 1) % 10 + 1;
		Console.Write($"and moves to space {playerPos[p]}");
		playerScore[p] += playerPos[p];
		Console.WriteLine($"Player {p + 1} has {playerScore[p]} points. ");
		if (playerScore[p] >= 1000)
		{
			Console.WriteLine($"Player {p + 1} Wins!");
			winner = p;
			break;
		}
	}
}
Console.WriteLine($"The die was rolled {diceRolls} times.");
for (int i = 0; i < playerScore.Length; i++)
{
	Console.WriteLine($"Player {i+1}: {playerScore[i]} * {diceRolls} = {playerScore[i] * diceRolls}");
}