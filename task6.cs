using System;

public static class Task6
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int card = int.Parse(Console.ReadLine());
        int lastDigit = card % 10;

        string dept = lastDigit switch
        {
            0 or 1 => "загальна терапія",
            2 or 3 => "хірургія",
            4 or 5 => "кардіологія",
            6 or 7 => "неврологія",
            8 or 9 => "офтальмологія",
            _ => ""
        };

        Console.WriteLine($"Відділення: {dept}");
        Console.WriteLine($"пільгова картка: {(card % 2 == 0 ? "так" : "ні")}");
        Console.WriteLine($"черговий огляд: {(card % 3 == 0 ? "так" : "ні")}");
    }
}