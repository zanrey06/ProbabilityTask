using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the number of items: ");
        int n = int.Parse(Console.ReadLine());

        double[] values = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter value {i + 1}: ");
            values[i] = double.Parse(Console.ReadLine());
        }

        Array.Sort(values);

        double median = GetMedian(values);
        double mode = GetMode(values);
        double range = GetRange(values);
        double q1 = GetQuartile(values, 0.25);
        double q3 = GetQuartile(values, 0.75);
        double p90 = GetPercentile(values, 0.9);
        double iqr = q3 - q1;
        double lowerBound = q1 - 1.5 * iqr;
        double upperBound = q3 + 1.5 * iqr;

        Console.WriteLine($"Median: {median}");
        Console.WriteLine($"Mode: {mode}");
        Console.WriteLine($"Range: {range}");
        Console.WriteLine($"First Quartile: {q1}");
        Console.WriteLine($"Third Quartile: {q3}");
        Console.WriteLine($"P90: {p90}");
        Console.WriteLine($"Interquartile Range: {iqr}");
        Console.WriteLine($"Outlier region: [{lowerBound}, {upperBound}]");

        Console.Write("Enter a value to check if it's an outlier: ");
        double input = double.Parse(Console.ReadLine());
        if (IsOutlier(input, lowerBound, upperBound))
        {
            Console.WriteLine($"{input} is an outlier.");
        }
        else
        {
            Console.WriteLine($"{input} is not an outlier.");
        }
    }

    static double GetMedian(double[] values)
    {
        int n = values.Length;
        if (n % 2 == 0)
        {
            return (values[n / 2 - 1] + values[n / 2]) / 2;
        }
        else
        {
            return values[n / 2];
        }
    }

    static double GetMode(double[] values)
    {
        var groups = values.GroupBy(x => x);
        int maxCount = groups.Max(g => g.Count());
        return groups.Where(g => g.Count() == maxCount).Select(g => g.Key).FirstOrDefault();
    }

    static double GetRange(double[] values)
    {
        return values[values.Length - 1] - values[0];
    }

    static double GetQuartile(double[] values, double fraction)
    {
        int n = values.Length;
        double index = fraction * (n - 1);
        int lowerIndex = (int)Math.Floor(index);
        int upperIndex = (int)Math.Ceiling(index);
        double fractionBetween = index - lowerIndex;
        return (1 - fractionBetween) * values[lowerIndex] + fractionBetween * values[upperIndex];
    }

    static double GetPercentile(double[] values, double fraction)
    {
        int n = values.Length;
        double index = fraction * (n - 1);
        int lowerIndex = (int)Math.Floor(index);
        int upperIndex = (int)Math.Ceiling(index);
        double fractionBetween = index - lowerIndex;
        return (1 - fractionBetween) * values[lowerIndex] + fractionBetween * values[upperIndex];
    }

    static bool IsOutlier(double value, double lowerBound, double upperBound)
    {
        return value < lowerBound || value > upperBound;
    }
}
