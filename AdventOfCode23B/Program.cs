// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 23 part 2");
string[] input = File.ReadAllLines("Input.txt");
string[] goal = File.ReadAllLines("Goal.txt");
Dictionary<string, int> StateEnergy = new Dictionary<string, int>();
Dictionary<string, Amphipod[,]> StringToState = new Dictionary<string, Amphipod[,]>();
Amphipod[,] startingState = new Amphipod[input[0].Length, input.Length];
Amphipod[,] goalState = new Amphipod[input[0].Length, input.Length];
for (int i = 0; i < input.Length; i++)
{
	for (int j = 0; j < input[i].Length; j++)
	{
		switch (input[i][j])
		{
			case 'A':
				startingState[j, i] = Amphipod.A;
				break;
			case 'B':
				startingState[j, i] = Amphipod.B;
				break;
			case 'C':
				startingState[j, i] = Amphipod.C;
				break;
			case 'D':
				startingState[j, i] = Amphipod.D;
				break;
			case '.':
				startingState[j, i] = Amphipod.Empty;
				break;
			case ' ':
			case '#':
			default:
				startingState[j, i] = Amphipod.Wall;
				break;
		}
	}
}
for (int i = 0; i < goal.Length; i++)
{
	for (int j = 0; j < goal[i].Length; j++)
	{
		switch (goal[i][j])
		{
			case 'A':
				goalState[j, i] = Amphipod.A;
				break;
			case 'B':
				goalState[j, i] = Amphipod.B;
				break;
			case 'C':
				goalState[j, i] = Amphipod.C;
				break;
			case 'D':
				goalState[j, i] = Amphipod.D;
				break;
			case '.':
				goalState[j, i] = Amphipod.Empty;
				break;
			case ' ':
			case '#':
			default:
				goalState[j, i] = Amphipod.Wall;
				break;
		}
	}
}
StateEnergy.Add(stringify(startingState), 0);
StringToState.Add(stringify(startingState), startingState);
string goalString = stringify(goalState);
Dictionary<Amphipod, int> EnergyUse = new Dictionary<Amphipod, int>();
EnergyUse.Add(Amphipod.A, 1);
EnergyUse.Add(Amphipod.B, 10);
EnergyUse.Add(Amphipod.C, 100);
EnergyUse.Add(Amphipod.D, 1000);
Dictionary<Amphipod, int> RoomX = new Dictionary<Amphipod, int>();
RoomX.Add(Amphipod.A, 3);
RoomX.Add(Amphipod.B, 5);
RoomX.Add(Amphipod.C, 7);
RoomX.Add(Amphipod.D, 9);
const int HALLWAYY = 1;
const int ROOMTOPY = 2;
const int ROOMBOTY = 5;
HashSet<string> CheckedStates = new HashSet<string>();
while (CheckedStates.Count < StateEnergy.Count)
{
	Console.WriteLine($"Checking state {CheckedStates.Count}.");
	Dictionary<Amphipod[,], int> NewStateEnergy = new Dictionary<Amphipod[,], int>();
	foreach (var item in StateEnergy)
	{
		if (!CheckedStates.Contains(item.Key))
		{
			var currentState = StringToState[item.Key];
			for (int x = 0; x < currentState.GetUpperBound(0) + 1; x++)
			{
				for (int y = 0; y < currentState.GetUpperBound(1) + 1; y++)
				{
					if (currentState[x, y] != Amphipod.Empty && currentState[x, y] != Amphipod.Wall)
					{
						if (y == HALLWAYY)
						{// Is in hallway
							int targetX = RoomX[currentState[x, y]];
							bool targetClear = false;
							int targetY = 0;
							for (int i = ROOMBOTY; i >= ROOMTOPY; i--)
							{
								if (currentState[targetX, i] == Amphipod.Empty)
								{
									targetClear = true;
									targetY = i;
									break;
								}
								else if (currentState[targetX, i] != currentState[x, y])
								{
									break;
								}
							}
							if (targetClear)
							{
								bool pathClear = true;
								int moveDir = Math.Sign(RoomX[currentState[x, y]] - x);
								//int moveX = x;
								for (int moveX = x + moveDir; moveX != targetX; moveX += moveDir)
								{
									pathClear = pathClear && currentState[moveX, y] == Amphipod.Empty;
								}
								if (pathClear && currentState.Clone() is Amphipod[,] stateClone)
								{
									stateClone[x, y] = Amphipod.Empty;
									stateClone[targetX, targetY] = currentState[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - targetX);
									dist += Math.Abs(y - targetY);
									cost += dist * EnergyUse[currentState[x, y]];
									NewStateEnergy[stateClone] = cost;
								}
							}
						}
						bool emptySpace = currentState[x, y - 1] == Amphipod.Empty;
						bool belongs = x == RoomX[currentState[x, y]];
						for (int i = y; i <= ROOMBOTY; i++)
						{
							belongs = belongs && x == RoomX[currentState[x, i]];
						}
						if (y != HALLWAYY && emptySpace && !belongs)
						{
							// Moving right
							for (int moveX = x + 1; moveX < currentState.GetUpperBound(0) + 1; moveX++)
							{
								if (currentState[moveX, HALLWAYY] != Amphipod.Empty)
								{
									break;
								}
								if (!RoomX.ContainsValue(moveX) && currentState.Clone() is Amphipod[,] stateClone)
								{ // don't stop outside a room.
									stateClone[x, y] = Amphipod.Empty;
									stateClone[moveX, HALLWAYY] = currentState[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - moveX);
									dist += Math.Abs(y - HALLWAYY);
									cost += dist * EnergyUse[currentState[x, y]];
									NewStateEnergy[stateClone] = cost;
								}
							}
							// Moving left
							for (int moveX = x - 1; moveX > 0; moveX--)
							{
								if (currentState[moveX, HALLWAYY] != Amphipod.Empty)
								{
									break;
								}
								if (!RoomX.ContainsValue(moveX) && currentState.Clone() is Amphipod[,] stateClone)
								{ // don't stop outside a room.
									stateClone[x, y] = Amphipod.Empty;
									stateClone[moveX, HALLWAYY] = currentState[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - moveX);
									dist += Math.Abs(y - HALLWAYY);
									cost += dist * EnergyUse[currentState[x, y]];
									NewStateEnergy[stateClone] = cost;
								}
							}
						}
					}
				}
			}
			CheckedStates.Add(item.Key);
			//break;
		}
	}
	foreach (var item in NewStateEnergy)
	{
		var stringed = stringify(item.Key);
		if (StateEnergy.ContainsKey(stringed))
		{
			StateEnergy[stringed] = Math.Min(StateEnergy[stringed], NewStateEnergy[item.Key]);
		}
		else
		{
			StateEnergy[stringed] = item.Value;
			StringToState[stringed] = item.Key;
		}
	}
	if (StateEnergy.ContainsKey(goalString))
	{
		Console.WriteLine($"Goal state minimum energy: {StateEnergy[goalString]}");
	}
}
if (StateEnergy.ContainsKey(goalString))
{
	Console.WriteLine($"Goal state minimum energy: {StateEnergy[goalString]}");
}
else
{
	Console.WriteLine("Goal state not reached");
}

string stringify(Amphipod[,] pod)
{
	string retVal = "";
	foreach (var item in pod)
	{
		switch (item)
		{
			case Amphipod.Wall:
				retVal += "#";
				break;
			case Amphipod.Empty:
				retVal += " ";
				break;
			case Amphipod.A:
				retVal += "A";
				break;
			case Amphipod.B:
				retVal += "B";
				break;
			case Amphipod.C:
				retVal += "C";
				break;
			case Amphipod.D:
				retVal += "D";
				break;
			default:
				break;
		}
	}
	return retVal;
}
enum Amphipod : byte
{
	Wall,
	Empty,
	A,
	B,
	C,
	D
}