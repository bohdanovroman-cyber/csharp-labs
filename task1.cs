using System;
using System.Globalization;

class Task1
{
    static void Main()
    {
        
        string inputWeight = Console.ReadLine();
        string inputHeight = Console.ReadLine();
        double weight = double.Parse(inputWeight.Replace(',', '.'), CultureInfo.InvariantCulture);
        double height = double.Parse(inputHeight.Replace(',', '.'), CultureInfo.InvariantCulture);

        
        double bmi = weight / (height * height);
        Console.WriteLine(bmi);
    }
}