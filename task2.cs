using System;
using System.Globalization;

public static class Task2
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double price = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        int visits = int.Parse(Console.ReadLine());
        int discount = int.Parse(Console.ReadLine());

        double total = price * visits * (1.0 - (double)discount / 100.0);

        Console.WriteLine($"Сума: {total:F2} грн");
    }
}