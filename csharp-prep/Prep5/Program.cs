class NumberAnalyzer
{
    static void Main(string[] args)
    {
        List<int> userInputList = new List<int>();
        
        int inputNumber = 1;
        while (inputNumber != 0)
        {
            Console.Write("Enter a number (0 to exit): ");
            
            string userInput = Console.ReadLine();
            inputNumber = int.Parse(userInput);
            
            if (inputNumber != 0)
            {
                userInputList.Add(inputNumber);
            }
        }

        int totalSum = 0;
        foreach (int num in userInputList)
        {
            totalSum += num;
        }

        Console.WriteLine($"Total sum: {totalSum}");

        float averageValue = ((float)totalSum) / userInputList.Count;
        Console.WriteLine($"Average: {averageValue}");

        int maximumValue = userInputList[0];
        foreach (int num in userInputList)
        {
            if (num > maximumValue)
            {
                maximumValue = num;
            }
        }

        Console.WriteLine($"Maximum value: {maximumValue}");
    }
}
