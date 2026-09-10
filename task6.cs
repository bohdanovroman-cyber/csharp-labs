using System;

class Task6
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int cardNumber = int.Parse(Console.ReadLine());

       
        int deptRem = cardNumber % 10;
        string department = "";

        if (deptRem == 0 || deptRem == 1)
            department = "відділення: загальна терапія";
        else if (deptRem == 2 || deptRem == 3)
            department = "відділення: хірургія";
        else if (deptRem == 4 || deptRem == 5)
            department = "відділення: кардіологія";
        else if (deptRem == 6 || deptRem == 7)
            department = "відділення: неврологія";
        else if (deptRem == 8 || deptRem == 9)
            department = "відділення: офтальмологія";

        Console.WriteLine(department);


        if (cardNumber % 2 == 0)
        {
            Console.WriteLine("пільгова картка: так");
        }
        if (cardNumber % 3 == 0)
        {
            Console.WriteLine("черговий огляд: так");
        }
    }
}