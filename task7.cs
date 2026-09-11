using System;
using System.Globalization;

public static class Task7
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int n = int.Parse(Console.ReadLine());
        decimal[] costs = new decimal[n];

        for (int i = 0; i < n; i++)
        {
            costs[i] = decimal.Parse(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        
        decimal sum = 0;
        decimal min = n > 0 ? costs[0] : 0;
        decimal max = n > 0 ? costs[0] : 0;

        foreach (var c in costs)
        {
            sum += c;
            if (c < min) min = c;
            if (c > max) max = c;
        }

        decimal avg = n > 0 ? sum / n : 0;

        
        int countAboveAvg = 0;
        for (int i = 0; i < n; i++)
        {
            if (costs[i] > avg) countAboveAvg++;
        }

        
        int idx = 0;
        string firstOver1000 = "немає";
        while (idx < n)
        {
            if (costs[idx] > 1000)
            {
                firstOver1000 = $"№{idx + 1} ({costs[idx]:F2} грн)";
                break;
            }
            idx++;
        }

        // Вивід звіту
        Console.WriteLine("=== Звіт по прийомах ===");
        Console.WriteLine($"Кількість прийомів: {n}");
        Console.WriteLine($"Загальна сума: {sum:F2} грн");
        Console.WriteLine($"Середня вартість: {avg:F2} грн");
        Console.WriteLine($"Мінімальна вартість: {min:F2} грн");
        Console.WriteLine($"Максимальна вартість: {max:F2} грн");
        Console.WriteLine($"Прийомів дорожчих за середнє: {countAboveAvg}");
        Console.WriteLine($"Перший прийом > 1000 грн: {firstOver1000}");
    }
}