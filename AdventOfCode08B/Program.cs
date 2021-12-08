// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 08 part 2");
string[] input = File.ReadAllLines("Input.txt");
int totalSum = 0;
// Length 2: 1
// Length 3: 7
// Length 4: 4
// Length 5: 2, 3, 5
// Length 6: 0, 6, 9
// Length 7: 8
for (int line = 0; line < input.Length; line++)
{
	string[] split = input[line].Split('|');
	string[] digits = split[0].Split(' ');
	string[] digitPattern = new string[10];
	int digitsFound = 0;
	while (digitsFound < 10)
	{
		for (int i = 0; i < digits.Length; i++)
		{
			if (digits[i].Length == 2 && string.IsNullOrEmpty(digitPattern[1]))
			{ // find 1
				digitPattern[1] = digits[i];
				digitsFound++;
			}
			else if (digits[i].Length == 3 && string.IsNullOrEmpty(digitPattern[7]))
			{ // find 7
				digitPattern[7] = digits[i];
				digitsFound++;
			}
			else if (digits[i].Length == 4 && string.IsNullOrEmpty(digitPattern[4]))
			{ // find 4
				digitPattern[4] = digits[i];
				digitsFound++;
			}
			else if (digits[i].Length == 7 && string.IsNullOrEmpty(digitPattern[8]))
			{ // find 8
				digitPattern[8] = digits[i];
				digitsFound++;
			}
			else if (digits[i].Length == 5
				&& !string.IsNullOrEmpty(digitPattern[1])
				&& !string.IsNullOrEmpty(digitPattern[4]))
			{ // find 2, 3, or 5. Needs 1 and 4 having been found.
				if (string.IsNullOrEmpty(digitPattern[2]))
				{ // find 2
					int partsFound = 0;
					for (int j = 0; j < digitPattern[4].Length; j++)
					{
						if (digits[i].Contains(digitPattern[4][j]))
						{
							partsFound++;
						}
					}
					if (partsFound == 2)
					{
						digitPattern[2] = digits[i];
						digitsFound++;
					}
				}
				else if (string.IsNullOrEmpty(digitPattern[3]))
				{ // find 3
					int partsFound = 0;
					for (int j = 0; j < digitPattern[1].Length; j++)
					{
						if (digits[i].Contains(digitPattern[1][j]))
						{
							partsFound++;
						}
					}
					if (partsFound == 2)
					{
						digitPattern[3] = digits[i];
						digitsFound++;
					}
				}
				else if (string.IsNullOrEmpty(digitPattern[5]))
				{ // find 5
					int partsFound = 0;
					for (int j = 0; j < digitPattern[4].Length; j++)
					{
						if (digits[i].Contains(digitPattern[4][j]))
						{
							partsFound++;
						}
					}
					if (partsFound == 3)
					{
						digitPattern[5] = digits[i];
						digitsFound++;
					}
				}
			}
			else if (digits[i].Length == 6
				&& !string.IsNullOrEmpty(digitPattern[1])
				&& !string.IsNullOrEmpty(digitPattern[5]))
			{ // find 0, 6, or 9. Needs 1 and 5 having been found.
				if (string.IsNullOrEmpty(digitPattern[0]))
				{ // find 0
					int partsFound = 0;
					for (int j = 0; j < digitPattern[5].Length; j++)
					{
						if (digits[i].Contains(digitPattern[5][j]))
						{
							partsFound++;
						}
					}
					if (partsFound == 4)
					{
						digitPattern[0] = digits[i];
						digitsFound++;
					}
				}
				else if (string.IsNullOrEmpty(digitPattern[6]))
				{ // find 6
					int partsFound = 0;
					for (int j = 0; j < digitPattern[1].Length; j++)
					{
						if (digits[i].Contains(digitPattern[1][j]))
						{
							partsFound++;
						}
					}
					if (partsFound == 1)
					{
						digitPattern[6] = digits[i];
						digitsFound++;
					}
				}
				else if (string.IsNullOrEmpty(digitPattern[9]))
				{ // find 9
					int fivepartsFound = 0;
					for (int j = 0; j < digitPattern[5].Length; j++)
					{
						if (digits[i].Contains(digitPattern[5][j]))
						{
							fivepartsFound++;
						}
					}
					if (fivepartsFound == 5)
					{
						int onepartsFound = 0;
						for (int j = 0; j < digitPattern[1].Length; j++)
						{
							if (digits[i].Contains(digitPattern[1][j]))
							{
								onepartsFound++;
							}
						}
						if (onepartsFound == 2)
						{
							digitPattern[9] = digits[i];
							digitsFound++;
						}
					}
				}
			}
		}
	}
	string[] display = split[1].Split(' ');
	int displayValue = 0;
	for (int i = 0; i < display.Length; i++)
	{
		for (int j = 0; j < digitPattern.Length; j++)
		{
			if (display[^(1+i)].Length == digitPattern[j].Length)
			{
				bool segmentsMatch = true;
				for (int k = 0; k < digitPattern[j].Length; k++)
				{
					segmentsMatch = segmentsMatch && display[^(1 + i)].Contains(digitPattern[j][k]);
				}
				if (segmentsMatch)
				{
					displayValue += j * (int)Math.Pow(10, i);
					break;
				}
			}
		}
	}
	totalSum += displayValue;
	Console.WriteLine($"Display {line}: {displayValue}");
}
Console.WriteLine($"The final sum is {totalSum}");