using System;

class Task3
{
    static void Main()
    {

        int birthYear = int.Parse(Console.ReadLine());
        int age = 2026 - birthYear;
        string category = "";
        if (age >= 0 && age <= 17)
        {
            category = "дитина";
        }
        else if (age >= 18 && age <= 59)
        {
            category = "дорослий";
        }
        else if (age >= 60)
        {
            category = "пенсіонер";
        }
        Console.WriteLine($"{age} {category}");
    }
}