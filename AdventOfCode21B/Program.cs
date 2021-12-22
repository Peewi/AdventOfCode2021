// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 21 part 2");
string[] input = File.ReadAllLines("Input.txt");
int[] diceResults = new int[] { 3, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 9 };
// p1 pos, p2 pos, p1 score, p2 score
long[,,,] gameStates = new long[10, 10, 31, 31];
int p1Start = int.Parse(input[0][^1].ToString()) - 1;
int p2Start = int.Parse(input[1][^1].ToString()) - 1;
gameStates[p1Start, p2Start, 0, 0] = 1;

const int WINNINGSCORE = 21;

bool cond = false;
int i = 0;
long totalUniverses = 0;
long[,,,] newStates;
do
{
	if (gameStates.Clone() is long[,,,] clone)
	{
		newStates = clone;
	}
	else
	{
		throw new Exception("Bad clone");
	}
	cond = false;
	
	for (int p1S = WINNINGSCORE - 1; p1S >= 0; p1S--)
	{
		for (int p2S = WINNINGSCORE - 1; p2S >= 0; p2S--)
		{
			for (int p1 = gameStates.GetUpperBound(0); p1 >= 0; p1--)
			{
				for (int p2 = gameStates.GetUpperBound(1); p2 >= 0; p2--)
				{
					long oldCount = gameStates[p1, p2, p1S, p2S];
					cond = cond || oldCount != 0;
					newStates[p1, p2, p1S, p2S] = 0;
					if (oldCount != 0)
					{
						foreach (var d in diceResults)
						{
							int newP1 = (p1 + d) % 10;
							int newP1S = p1S + newP1 + 1;
							//totalUniverses += oldCount;
							if (newP1S >= WINNINGSCORE)
							{
								newStates[newP1, p2, newP1S, p2S] += oldCount;
								totalUniverses += oldCount;
							}
							else
							{
								foreach (var d2 in diceResults)
								{
									int newP2 = (p2 + d2) % 10;
									int newP2S = p2S + newP2 + 1;
									newStates[newP1, newP2, newP1S, newP2S] += oldCount;
									totalUniverses += oldCount;
								}
							}
						}
					}
				}
			}
		}
	}
	gameStates = newStates;
	i++;
	Console.WriteLine($"Iteration {i}. There are now {totalUniverses} universes.");
} while (cond);
long p1Wins = 0;
long p2Wins = 0;
for (int p1 = gameStates.GetUpperBound(0); p1 >= 0; p1--)
{
	for (int p2 = gameStates.GetUpperBound(1); p2 >= 0; p2--)
	{
		for (int p1S = gameStates.GetUpperBound(2); p1S >= WINNINGSCORE; p1S--)
		{
			for (int p2S = gameStates.GetUpperBound(3); p2S >= 0; p2S--)
			{
				p1Wins += gameStates[p1, p2, p1S, p2S];
				p2Wins += gameStates[p1, p2, p2S, p1S];
			}
		}
	}
}
Console.WriteLine($"Player 1 won in {p1Wins} universes");
Console.WriteLine($"Player 2 won in {p2Wins} universes");
Console.WriteLine($"combined {p1Wins + p2Wins} universes");
