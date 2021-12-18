// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 18 part 1");
string[] input = File.ReadAllLines("Input.txt");
// SNAILFISH NUMBERS!!!
for (int i = 0; i < input.Length; i++)
{
	Console.WriteLine(input[i]);
}
string workingNumber = input[0];
for (int i = 1; i < input.Length; i++)
{
	Console.WriteLine("");
	Console.WriteLine("  " + workingNumber);
	Console.WriteLine("+ " + input[i]);
	workingNumber = add(workingNumber, input[i]);
	Console.WriteLine($"= {workingNumber}");
}
Console.WriteLine($"Magnitude of final number: {magnitude(workingNumber)}");

string reduce(string fishnumber)
{
	Console.WriteLine($"Reducing {fishnumber}");
	bool actionTaken = false;
	do
	{
		actionTaken = false;
		int nestingDepth = 0;
		int leftOuterPos = 0;
		for (int i = 0; i < fishnumber.Length; i++)
		{
			switch (fishnumber[i])
			{
				case '[':
					nestingDepth++;
					break;
				case ']':
					nestingDepth--;
					break;
				case ',':
					break;
				default:
					leftOuterPos = i;
					break;
			}
			if (nestingDepth >= 5)
			{
				string beforeThis = fishnumber[0..(i)];
				string afterThis = "";
				string inner = "";
				for (int j = i; j < fishnumber.Length; j++)
				{
					if (fishnumber[j] == ']')
					{
						afterThis = fishnumber.Substring(j + 1);
						inner = fishnumber[(i+1)..j];
						break;
					}
				}
				var innerParts = inner.Split(',');
				int left = int.Parse(innerParts[0]);
				int right = int.Parse(innerParts[1]);
				for (int j = leftOuterPos - 1; j >= 0; j--)
				{
					bool done = false;
					switch (fishnumber[j])
					{
						case '[':
						case ']':
						case ',':
							int leftOuter = int.Parse(fishnumber[(j + 1)..(leftOuterPos + 1)]);
							leftOuter += left;
							beforeThis = $"{beforeThis[0..(j+1)]}{leftOuter}{beforeThis[(leftOuterPos+1)..^0]}";
							done = true;
							break;
						default:
							break;
					}
					if (done)
					{
						break;
					}
				}
				string outerRightStr = "";
				for (int j = 0; j < afterThis.Length; j++)
				{
					bool done = false;
					switch (afterThis[j])
					{
						case '[':
						case ']':
						case ',':
							if (!string.IsNullOrWhiteSpace(outerRightStr))
							{
								int rightOuter = int.Parse(outerRightStr);
								rightOuter += right;
								afterThis = $"{afterThis[0..(j-outerRightStr.Length)]}{rightOuter}{afterThis[j..^0]}";
								done = true;
							}
							break;
						default:
							outerRightStr += afterThis[j];
							break;
					}
					if (done)
					{
						break;
					}
				}
				fishnumber = $"{beforeThis}0{afterThis}";
				actionTaken = true;
				Console.WriteLine($"Exploded to {fishnumber}");
				break;
			}
		}
		if (!actionTaken)
		{ // only split if not exploded
			string numStr = "";
			for (int i = 0; i < fishnumber.Length; i++)
			{
				bool done = false;
				switch (fishnumber[i])
				{
					case '[':
					case ']':
					case ',':
						if (int.TryParse(numStr, out int res) && res >= 10)
						{
							done = true;
							int oddeven = res % 2 == 0 ? 0 : 1;
							fishnumber = $"{fishnumber[0..(i - numStr.Length)]}[{res / 2},{res / 2 + oddeven}]{fishnumber[i..^0]}";
						}
						numStr = "";
						break;
					default:
						numStr += fishnumber[i];
						break;
				}
				if (done)
				{
					Console.WriteLine($"Split to {fishnumber}");
					actionTaken = true;
					break;
				}
			}
		}
	} while (actionTaken);
	return fishnumber;
}

string add(string left, string right)
{
	return reduce($"[{left},{right}]");
}

int magnitude(string fishnumber)
{
	if (int.TryParse(fishnumber, out int res))
	{
		return res;
	}
	int pairDepth = 0;
	for (int i = 0; i < fishnumber.Length; i++)
	{
		switch (fishnumber[i])
		{
			case '[':
				pairDepth++;
				break;
			case ']':
				pairDepth--;
				break;
			case ',':
				if (pairDepth == 1)
				{
					return 3 * magnitude(fishnumber[1..i]) + 2 * magnitude(fishnumber[(i + 1)..^1]);
				}
				break;
			default:
				break;
		}
	}
	throw new Exception("couldn't find pairs");
}