using System;

class Task8
{
   
    public static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    
    public static string GetBMICategory(double bmi)
    {
        if (bmi < 18.5) return "недостатня вага";
        if (bmi < 25) return "норма";
        if (bmi < 30) return "надмірна вага";
        return "ожиріння";
    }

    
    public static double CalculateCost(double price, int count, int discount)
    {
        return price * count * (1 - (double)discount / 100);
    }
    public static string GetAgeCategory(int age)
    {
        if (age >= 0 && age <= 17) return "дитина";
        if (age >= 18 && age <= 59) return "дорослий";
        return "пенсіонер";
    }
    public static string GetPressureStatus(int sys, int dia)
    {
        if (sys < 120 && dia < 80) return "норма";
        if (sys < 130 && dia < 80) return "підвищений";
        if (sys < 140 || dia < 90) return "гіпертонія 1 ступеня";
        return "гіпертонія 2 ступеня";
    }

    static void Main()
    {
        
    }
}