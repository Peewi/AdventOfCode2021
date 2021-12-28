// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 23 part 1");
string[] input = File.ReadAllLines("Input.txt");
string[] goal = File.ReadAllLines("Goal.txt");
Dictionary<Amphipod[,], int> StateEnergy = new Dictionary<Amphipod[,], int>();
Amphipod[,] startingState = new Amphipod[input[0].Length,input.Length];
Amphipod[,] goalState = new Amphipod[input[0].Length,input.Length];
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
StateEnergy.Add(startingState, 0);
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
const int ROOMBOTY = 3;
HashSet<Amphipod[,]> CheckedStates = new HashSet<Amphipod[,]>();
while (CheckedStates.Count < StateEnergy.Count)
{
	Console.WriteLine($"Checking state {CheckedStates.Count}.");
	Dictionary<Amphipod[,], int> NewStateEnergy = new Dictionary<Amphipod[,], int>();
	foreach (var item in StateEnergy)
	{
		if (!CheckedStates.Contains(item.Key))
		{
			for (int x = 0; x < item.Key.GetUpperBound(0) + 1; x++)
			{
				for (int y = 0; y < item.Key.GetUpperBound(1) + 1; y++)
				{
					if (item.Key[x, y] != Amphipod.Empty && item.Key[x, y] != Amphipod.Wall)
					{
						if (y == HALLWAYY)
						{// Is in hallway
							int targetX = RoomX[item.Key[x, y]];
							var roomBot = item.Key[targetX, ROOMBOTY];
							var roomTop = item.Key[targetX, ROOMTOPY];
							bool targetClear = false;
							int targetY = 0;
							if (roomTop == Amphipod.Empty && roomBot == Amphipod.Empty)
							{ // target room is fully empty
								targetClear = true;
								targetY = ROOMBOTY;
							}
							else if (roomTop == Amphipod.Empty && roomBot == item.Key[x, y])
							{ // Front part of target room is empty and back part matches
								targetClear = true;
								targetY = ROOMTOPY;
							}
							if (targetClear)
							{
								bool pathClear = true;
								int moveDir = Math.Sign(RoomX[item.Key[x, y]] - x);
								//int moveX = x;
								for (int moveX = x + moveDir; moveX != targetX; moveX+=moveDir)
								{
									pathClear = pathClear && item.Key[moveX, y] == Amphipod.Empty;
								}
								if (pathClear && item.Key.Clone() is Amphipod[,] stateClone)
								{
									stateClone[x, y] = Amphipod.Empty;
									stateClone[targetX, targetY] = item.Key[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - targetX);
									dist += Math.Abs(y - targetY);
									cost += dist * EnergyUse[item.Key[x, y]];
									NewStateEnergy[stateClone] = cost;
								}
							}
						}
						bool hallwayMove = false;
						bool belongs = x == RoomX[item.Key[x, y]];
						if (y == ROOMTOPY)
						{ // You can move out from the front if you or the one behind you don't belong 
							hallwayMove = !belongs || RoomX[item.Key[x, ROOMBOTY]] != x;
						}
						else if (y == ROOMBOTY)
						{ // You can move out from the back if you don't belong and the space in front is empty
							hallwayMove = !belongs && item.Key[x, ROOMTOPY] == Amphipod.Empty;
						}
						if (hallwayMove)
						{
							// Moving right
							for (int moveX = x+1; moveX < item.Key.GetUpperBound(0)+1; moveX++)
							{
								if (item.Key[moveX, HALLWAYY] != Amphipod.Empty)
								{
									break;
								}
								if (!RoomX.ContainsValue(moveX) && item.Key.Clone() is Amphipod[,] stateClone)
								{ // don't stop outside a room.
									stateClone[x, y] = Amphipod.Empty;
									stateClone[moveX, HALLWAYY] = item.Key[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - moveX);
									dist += Math.Abs(y - HALLWAYY);
									cost += dist * EnergyUse[item.Key[x, y]];
									NewStateEnergy[stateClone] = cost;
								}
							}
							// Moving left
							for (int moveX = x-1; moveX > 0; moveX--)
							{
								if (item.Key[moveX, HALLWAYY] != Amphipod.Empty)
								{
									break;
								}
								if (!RoomX.ContainsValue(moveX) && item.Key.Clone() is Amphipod[,] stateClone)
								{ // don't stop outside a room.
									stateClone[x, y] = Amphipod.Empty;
									stateClone[moveX, HALLWAYY] = item.Key[x, y];
									int cost = item.Value;
									int dist = Math.Abs(x - moveX);
									dist += Math.Abs(y - HALLWAYY);
									cost += dist * EnergyUse[item.Key[x, y]];
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
		if (StateEnergy.ContainsKey(item.Key))
		{
			StateEnergy[item.Key] = Math.Min(StateEnergy[item.Key], NewStateEnergy[item.Key]);
		}
		else
		{
			StateEnergy[item.Key] = item.Value;
		}
	}
	if (StateEnergy.ContainsKey(goalState))
	{
		Console.WriteLine($"Goal state minimum energy: {StateEnergy[goalState]}");
	}
}
if (StateEnergy.ContainsKey(goalState))
{
	Console.WriteLine($"Goal state minimum energy: {StateEnergy[goalState]}");
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