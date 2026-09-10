using System;
using System.Globalization;

class Task2
{
    static void Main()
    {
        string inputPrice = Console.ReadLine();
        string inputCount = Console.ReadLine();
        string inputDiscount = Console.ReadLine();
        double price = double.Parse(inputPrice.Replace(',', '.'), CultureInfo.InvariantCulture);
        double count = double.Parse(inputCount.Replace(',', '.'), CultureInfo.InvariantCulture);
        double discount = double.Parse(inputDiscount.Replace(',', '.'), CultureInfo.InvariantCulture);
        double total = price * count * (1 - discount / 100);

        Console.WriteLine(total);
    }
}