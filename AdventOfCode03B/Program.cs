// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 03 part 2");
var lines = File.ReadAllLines("Input.txt");
// initial values
bool[] oxygenValid = new bool[lines.Length];
int oxygenValidCount = lines.Length;
bool[] CO2Valid = new bool[lines.Length];
int CO2ValidCount = lines.Length;
for (int i = 0; i < lines.Length; i++)
{
	oxygenValid[i] = true;
	CO2Valid[i] = true;
}
//
int oxygenRating = 0;
int co2Rating = 0;
// oxygen
for (int space = 0; space < lines[0].Length; space++)
{
	int ones = 0;
	for (int j = 0; j < lines.Length; j++)
	{
		if (oxygenValid[j] && lines[j][space] == '1')
		{
			ones++;
		}
	}
	char target = '0';
	if (ones >= oxygenValidCount / 2)
	{
		target = '1';
	}
	oxygenValidCount = 0;
	for (int j = 0; j < lines.Length; j++)
	{
		oxygenValid[j] = oxygenValid[j] && lines[j][space] == target;
		if (oxygenValid[j])
		{
			oxygenValidCount++;
		}
	}
	if (oxygenValidCount == 1)
	{
		break;
	}
}
for (int i = 0; i < lines.Length; i++)
{
	if (oxygenValid[i])
	{
		oxygenRating = BinaryStringToInt(lines[i]);
	}
}
// co2
for (int space = 0; space < lines[0].Length; space++)
{
	int ones = 0;
	for (int j = 0; j < lines.Length; j++)
	{
		if (CO2Valid[j] && lines[j][space] == '1')
		{
			ones++;
		}
	}
	char target = '0';
	if (ones < CO2ValidCount / 2)
	{
		target = '1';
	}
	CO2ValidCount = 0;
	for (int j = 0; j < lines.Length; j++)
	{
		CO2Valid[j] = CO2Valid[j] && lines[j][space] == target;
		if (CO2Valid[j])
		{
			CO2ValidCount++;
		}
	}
	if (CO2ValidCount == 1)
	{
		break;
	}
}
for (int i = 0; i < lines.Length; i++)
{
	if (CO2Valid[i])
	{
		co2Rating = BinaryStringToInt(lines[i]);
	}
}
Console.WriteLine($"Oxygen: {oxygenRating}");
Console.WriteLine($"CO2: {co2Rating}");
Console.WriteLine($"Oxygen * CO2: {oxygenRating * co2Rating}");

/// <summary>
/// What it says
/// </summary>
static int BinaryStringToInt(string bin)
{
	int retval = 0;
	for (int i = 0; i < bin.Length; i++)
	{
		if (bin[^(1 + i)] == '1')
		{
			retval += (int)Math.Pow(2, i);
		}
	}
	return retval;
}