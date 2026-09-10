using System;

class Task5
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int day = int.Parse(Console.ReadLine());

        switch (day)
        {
            case 1:
                Console.WriteLine("Понеділок 08:00-18:00");
                break;
            case 2:
                Console.WriteLine("Вівторок 08:00-18:00");
                break;
            case 3:
                Console.WriteLine("Середа 09:00-17:00");
                break;
            case 4:
                Console.WriteLine("Четвер 08:00-18:00");
                break;
            case 5:
                Console.WriteLine("П'ятниця 08:00-18:00");
                break;
            case 6:
                Console.WriteLine("Субота 09:00-14:00");
                break;
            case 7:
                Console.WriteLine("Неділя вихідний");
                break;
        }
    }
}