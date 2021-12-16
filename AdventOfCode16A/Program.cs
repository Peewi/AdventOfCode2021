// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code day 16 part 1");
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
Console.WriteLine($"Version sum: {versionSum(bin).Item1}");

(int vSum, int pkEnd) versionSum(string packet)
{
	int v = Convert.ToInt32(packet[0..3], 2);
	Console.WriteLine($"Packet version: {v}");
	int t = Convert.ToInt32(packet[3..6], 2);
	int end = 0;
	if (t == 4)
	{
		int block = 6;
		string num = "";
		while (packet[block] == '1')
		{
			num += packet.Substring(block + 1, 4);
			block += 5;
		}
		num += packet.Substring(block + 1, 4);
		end = block + 5;
		return (v, end);
	}
	if (packet[6] == '0')
	{
		int bitLength = Convert.ToInt32(packet.Substring(7, 15), 2);
		int start = 22;
		end = start;
		while (start < 22 + bitLength)
		{
			var pp = versionSum(packet.Substring(start));
			start += pp.pkEnd;
			end += pp.pkEnd;
			v += pp.vSum;
		}
	}
	else
	{
		int subPacketCount = Convert.ToInt32(packet.Substring(7, 11), 2);
		int start = 18;
		end = start;
		for (int i = 0; i < subPacketCount; i++)
		{
			var pp = versionSum(packet.Substring(start));
			start += pp.pkEnd;
			end += pp.pkEnd;
			v += pp.vSum;
		}
	}
	return (v, end);
}