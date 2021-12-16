// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 16 part 2");
string[] input = File.ReadAllLines("Input.txt");
Dictionary<char, string> hexToBin = new Dictionary<char, string>();
hexToBin.Add('0', "0000");
hexToBin.Add('1', "0001");
hexToBin.Add('2', "0010");
hexToBin.Add('3', "0011");
hexToBin.Add('4', "0100");
hexToBin.Add('5', "0101");
hexToBin.Add('6', "0110");
hexToBin.Add('7', "0111");
hexToBin.Add('8', "1000");
hexToBin.Add('9', "1001");
hexToBin.Add('A', "1010");
hexToBin.Add('B', "1011");
hexToBin.Add('C', "1100");
hexToBin.Add('D', "1101");
hexToBin.Add('E', "1110");
hexToBin.Add('F', "1111");
string bin = "";
for (int i = 0; i < input[0].Length; i++)
{
	bin += hexToBin[input[0][i]];
}
Console.WriteLine($"Final value: {packetVal(bin).Item1}");

(long val, int pkEnd) packetVal(string packet)
{
	int v = Convert.ToInt32(packet[0..3], 2);
	int t = Convert.ToInt32(packet[3..6], 2);
	int end = 0;
	if (t == 4)
	{
		int block = 6;
		string numBin = "";
		while (packet[block] == '1')
		{
			numBin += packet.Substring(block + 1, 4);
			block += 5;
		}
		numBin += packet.Substring(block + 1, 4);
		end = block + 5;
		long numLong = Convert.ToInt64(numBin, 2);
		Console.WriteLine($"Number literal: {numLong}");
		return (numLong, end);
	}
	List<long> numbers = new List<long>();
	if (packet[6] == '0')
	{
		int bitLength = Convert.ToInt32(packet.Substring(7, 15), 2);
		int start = 22;
		end = start;
		while (start < 22 + bitLength)
		{
			var pp = packetVal(packet.Substring(start));
			start += pp.pkEnd;
			end += pp.pkEnd;
			numbers.Add(pp.val);
		}
	}
	else
	{
		int subPacketCount = Convert.ToInt32(packet.Substring(7, 11), 2);
		int start = 18;
		end = start;
		for (int i = 0; i < subPacketCount; i++)
		{
			var pp = packetVal(packet.Substring(start));
			start += pp.pkEnd;
			end += pp.pkEnd;
			numbers.Add(pp.val);
		}
	}
	switch (t)
	{
		case 0:
			return (numbers.Sum(), end);
		case 1:
			//prod
			long prod = 1;
			foreach (var item in numbers)
			{
				prod *= item;
			}
			return (prod, end);
		case 2:
			//min
			return (numbers.Min(), end);
		case 3:
			//max
			return (numbers.Max(), end);
		case 5:
			//gt
			int gt = numbers[0] > numbers[1] ? 1 : 0;
			return (gt, end);
		case 6:
			int lt = numbers[0] < numbers[1] ? 1 : 0;
			return (lt, end);
		//lt
		case 7:
			int eq = numbers[0] == numbers[1] ? 1 : 0;
			return (eq, end);
		//equals
		default:
			break;
	}
	throw new Exception("You shouldn't be here");
}