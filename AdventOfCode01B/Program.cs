// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var lines = File.ReadAllLines("Input.txt");
int[] numbers = new int[lines.Length];
int increases = 0;
for (int i = 0; i < lines.Length; i++)
{
	numbers[i] = int.Parse(lines[i]);
	if (i >= 3 && numbers[i - 1] + numbers[i - 2] + numbers[i - 3] < numbers[i - 0] + numbers[i - 1] + numbers[i - 2])
	{
		increases++;
	}
}
Console.WriteLine( $"Increases: {increases}");