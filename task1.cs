namespace Lab02;

public class Task1
{
    public static void Run()
    {
        int number = int.Parse(Console.ReadLine());

        double[] weights = new double[number];

        for (int i = 0; i < number; i++)
        {
            weights[i] = double.Parse(Console.ReadLine());
        }

        double sum = 0;
        double min = weights[0];
        double max = weights[0];

        for (int i = 0; i < number; i++)
        {
            sum += weights[i];

            if (weights[i] < min)
            {
                min = weights[i];
            }

            if (weights[i] > max)
            {
                max = weights[i];
            }
        }

        double average = sum / number;

        int countBiggerThanAvg = 0;

        for (int i = 0; i < number; i++)
        {
            if (weights[i] > average)
            {
                countBiggerThanAvg++;
            }
        }

        Console.WriteLine(
            $"Кількість: {number} / Середня вага: {average:F1} кг / Мін / Макс: {min:F1} / {max:F1} кг / Вище середнього: {countBiggerThanAvg} з {number}");
    }
}