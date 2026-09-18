using System;

class Task4
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int sys = int.Parse(Console.ReadLine());
        int dia = int.Parse(Console.ReadLine());

        string status = "";

        if (sys < 120 && dia < 80)
        {
            status = "норма";
        }
        else if (sys < 130 && dia < 80)
        {
            status = "підвищений";
        }
        else if (sys < 140 || dia < 90)
        {
            status = "гіпертонія 1 ступеня";
        }
        else
        {
            status = "гіпертонія 2 ступеня";
        }

        Console.WriteLine(status);
    }
}