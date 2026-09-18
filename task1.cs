using System;
using System.Globalization;

public static class Task1
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double weight = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        double height = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);

        double bmi = weight / (height * height);

        Console.WriteLine($"ІМТ: {bmi:F2}");
    }
}