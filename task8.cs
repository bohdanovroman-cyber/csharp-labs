using System;
using System.Globalization;

public static class Task8
{
    public static double CalculateBMI(double weight, double height) => weight / (height * height);

    public static string GetBMICategory(double bmi)
    {
        if (bmi < 18.5) return "недостатня вага";
        if (bmi < 25.0) return "норма";
        if (bmi < 30.0) return "надмірна вага";
        return "ожиріння";
    }

    public static double CalculateCost(double price, int visits, int discount) => price * visits * (1.0 - (double)discount / 100.0);

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

    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double weight = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        double height = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        double price = double.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        int visits = int.Parse(Console.ReadLine());
        int discount = int.Parse(Console.ReadLine());
        int birthYear = int.Parse(Console.ReadLine());
        int sys = int.Parse(Console.ReadLine());
        int dia = int.Parse(Console.ReadLine());

        double bmi = CalculateBMI(weight, height);
        string bmiCat = GetBMICategory(bmi);
        double cost = CalculateCost(price, visits, discount);
        int age = 2026 - birthYear;
        string ageCat = GetAgeCategory(age);
        string pressStat = GetPressureStatus(sys, dia);

        Console.WriteLine($"ІМТ: {bmi:F2} -> {bmiCat}");
        Console.WriteLine($"Сума: {cost:F2} грн");
        Console.WriteLine($"Вік: {age} р., категорія: {ageCat}");
        Console.WriteLine($"Тиск: {sys}/{dia} — {pressStat}");
    }
}