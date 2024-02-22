class NumberProcessor
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        
        int userInput = -1;
        while (userInput != 0)
        {
            Console.Write("Enter a number (0 to quit): ");
            
            string input = Console.ReadLine();
            userInput = int.Parse(input);
            
            if (userInput != 0)
            {
                numbers.Add(userInput);
            }
        }

        int total = 0;
        foreach (int num in numbers)
        {
            total += num;
        }

        Console.WriteLine($"The sum is: {total}");

        float average = ((float)total) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        int maximum = numbers[0];
        foreach (int num in numbers)
        {
            if (num > maximum)
            {
                maximum = num;
            }
        }

        Console.WriteLine($"The max is: {maximum}");

        numbers.Sort();
        Console.Write("Sorted numbers: ");
        foreach (int num in numbers)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();
    }
}
