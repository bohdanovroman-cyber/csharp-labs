using System;
using System.Globalization;

class Task7
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        int n = int.Parse(Console.ReadLine());

        double total = 0;
        double min = double.MaxValue;
        double max = double.MinValue;

        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            double price = double.Parse(input.Replace(',', '.'), CultureInfo.InvariantCulture);

            total += price;

            if (price < min) min = price;
            if (price > max) max = price;
        }

        double average = n > 0 ? total / n : 0;

        
        Console.WriteLine($"Загальна сума: {total}");
        Console.WriteLine($"Середня вартість: {average}");
        Console.WriteLine($"Мінімальна вартість: {min}");
        Console.WriteLine($"Максимальна вартість: {max}");
    }
}