// Console Application: Sort Array, Factorial, Fibonacci

// --- Sort Array ---
int[] array = { 5, 3, 8, 1, 9, 2, 7, 4, 6 };
Console.WriteLine("Original array: " + string.Join(", ", array));
int[] sorted = SortArray(array);
Console.WriteLine("Sorted array:   " + string.Join(", ", sorted));

// --- Factorial ---
int number = 10;
long factorial = Factorial(number);
Console.WriteLine($"\nFactorial of {number}: {factorial}");

// --- Fibonacci ---
int terms = 10;
long[] fibonacci = Fibonacci(terms);
Console.WriteLine($"\nFibonacci series ({terms} terms): " + string.Join(", ", fibonacci));

/// <summary>
/// Receives an integer array and returns a new array sorted in ascending order.
/// </summary>
static int[] SortArray(int[] input)
{
    int[] result = (int[])input.Clone();
    Array.Sort(result);
    return result;
}

/// <summary>
/// Returns the factorial of a non-negative integer n (n!).
/// </summary>
static long Factorial(int n)
{
    if (n < 0)
        throw new ArgumentException("Factorial is not defined for negative numbers.", nameof(n));

    long result = 1;
    for (int i = 2; i <= n; i++)
        result *= i;

    return result;
}

/// <summary>
/// Returns the Fibonacci series up to the specified number of terms.
/// </summary>
static long[] Fibonacci(int terms)
{
    if (terms <= 0)
        throw new ArgumentException("Number of terms must be positive.", nameof(terms));

    long[] series = new long[terms];
    for (int i = 0; i < terms; i++)
    {
        if (i == 0) series[i] = 0;
        else if (i == 1) series[i] = 1;
        else series[i] = series[i - 1] + series[i - 2];
    }
    return series;
}
