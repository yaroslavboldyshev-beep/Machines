using System.Text;

int[,] goodProducts = new int[10, 7];
int[,] badProducts = new int[10, 7];

static string[] GetCodes()
{
    Console.WriteLine("""Voer codes in, druk op "*" om te stoppen""");
    Console.ForegroundColor = ConsoleColor.Blue;
    StringBuilder codesString = new StringBuilder();
    string input;
    int count = 0;

    do
    {
        input = Console.ReadLine();

        if (input?.Length > 0 && input != "*")
        {
            codesString.Append(input + ' ');
            count++;
        }

    }
    while (input != "*" && count < 100);

    string[] array = codesString.ToString().Trim().Split(' ');

    Console.ResetColor();
    return array;
}

static string[] FilterCodes(string[] codes)
{
    var filtered = codes.Where(code => code.Length == 3
    && "ABCDEF".Contains(code[0])
    && "123456789".Contains(code[1])
    && "01".Contains(code[2])).ToArray();

    return filtered;
}

static void SortCodes(string[] codes, int[,] bad, int[,] good)
{
    for (int i = 0; i < codes.Length; i++)
    {
        var code = codes[i];
        int row = code[1] - '1';
        int column = code[0] - 'A';

        if (code[2] == '0') UpdateArray(bad, row, column);
        else UpdateArray(good, row, column);
    }
}

static void UpdateArray(int[,] arr, int row, int column)
{
    // total in cell
    arr[row, column]++;
    // total in row
    arr[row, 6]++;
    // total in column
    arr[9, column]++;
    // total in array
    arr[9, 6]++;
}

static void ShowArray(int[,] arr)
{
    // table header
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("    ");
    for (int i = 65; i <= 70; i++)
        Console.Write(char.ConvertFromUtf32(i) + " ");

    Console.WriteLine("TOT");

    // table body
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        if (i == arr.GetLength(0) - 1) Console.Write("TOT ");
        else Console.Write((i + 1) + "   ");


        for (int j = 0; j < arr.GetLength(1); j++)
            Console.Write(arr[i, j] + " ");

        Console.WriteLine();
    }

    Console.ResetColor();
}

// -------------- start ---------------
var allCodes = GetCodes();
var filteredCodes = FilterCodes(allCodes);
Console.WriteLine("Correcte codes: {0}", filteredCodes.Length);
Console.WriteLine("Foutieve codes: {0}", allCodes.Length - filteredCodes.Length);
Console.WriteLine();
SortCodes(filteredCodes, badProducts, goodProducts);
Console.WriteLine("Goede afwerking");
ShowArray(goodProducts);
Console.WriteLine("Slechte afwerking");
ShowArray(badProducts);

