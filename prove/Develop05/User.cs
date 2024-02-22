
namespace System.Threading;

public class User
{
    private string _userName;
    private List<Goal> _goals;
    private int _totalPoints;

    public User(string name)
{
    _userName = name;
    _goals = new List<Goal>(); 
}

 public void CreateNewGoal(string name, string description, int points)
{
    try
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.Write("Enter choice: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }

        Goal newGoal = null; // Initialize newGoal outside the conditional blocks

        if (choice == 1)
        {
            newGoal = new SimpleGoal(name, description, points, false);
        }
        else if (choice == 2)
        {
            Console.Write("Enter times accomplished: ");
            int timesAccomplished;
            while (!int.TryParse(Console.ReadLine(), out timesAccomplished) || timesAccomplished < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative integer value.");
            }
            newGoal = new EternalGoal(name, description, timesAccomplished, points);
        }

        // Set the Name property of the newGoal
        newGoal.Name = name;

        // Ensure newGoal is not null before adding it to _goals list
        if (newGoal != null)
        {
            _goals.Add(newGoal);
            Console.WriteLine("New goal created successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}



    public void DisplayGoals()
    {
        try
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals to display.");
                return;
            }

            int i = 1;
            foreach (Goal goal in _goals)
            {
                Console.Write($"{i}. ");
                goal.DisplayGoal(0);
                i++;
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void Save()
{
    try
    {
        Console.Clear();
        Console.Write("Enter filename: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine($"00|{_userName}|{_totalPoints}");

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRep());
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

public void Load(string filename)
{
    try
    {
        Console.Clear();
        using (StreamReader inputFile = new StreamReader(filename))
        {
            string firstLine = inputFile.ReadLine();
            string[] userParts = firstLine.Split('|');
            if (userParts.Length >= 3)
            {
                _userName = userParts[1];
                _totalPoints = int.Parse(userParts[2]);
            }

            _goals.Clear(); // Clear existing goals before loading

            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                string[] parts = line.Split('|');

                // Check if the line contains enough parts
                if (parts.Length < 5)
                {
                    Console.WriteLine("Invalid format: Line does not contain enough parts.");
                    continue; // Skip to the next line
                }

                int type = int.Parse(parts[0]);
                string Name = parts[1];
                string description = parts[2];
                int Points = int.Parse(parts[3]);
                bool isCompleted = parts[4] == "1"; // Convert "1" to true, "0" to false

                Goal loadedGoal = null;
                if (type == 1)
                {
                    loadedGoal = new SimpleGoal(Name, description, Points, isCompleted);
                }
                else if (type == 2)
                {
                    if (parts.Length < 6)
                    {
                        Console.WriteLine("Invalid format: EternalGoal requires additional information.");
                        continue; // Skip to the next line
                    }

                    int timesAccomplished = int.Parse(parts[5]);
                    loadedGoal = new EternalGoal(Name, description, timesAccomplished, Points);
                }

                _goals.Add(loadedGoal);
            }
        }

        Console.WriteLine("Goals loaded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}




public void RecordEvent()
{
    try
    {
        // Check if _goals is null or empty
        if (_goals == null || _goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
            return;
        }

        // Display the list of goals
        Console.WriteLine("List of Goals:");
        DisplayGoals();

        // Prompt the user to select a goal by its index
        Console.Write("Enter the number of the goal to record achievement: ");
        int selectedGoalIndex;
        while (!int.TryParse(Console.ReadLine(), out selectedGoalIndex) || selectedGoalIndex < 1 || selectedGoalIndex > _goals.Count)
        {
            Console.WriteLine("Invalid input. Please enter a valid goal number.");
            Console.Write("Enter the number of the goal to record achievement: ");
        }

        // Get the selected goal
        Goal selectedGoal = _goals[selectedGoalIndex - 1];

        // Mark the selected goal as completed
        selectedGoal.SetIsCompleted();

        // Update the total points based on the selected goal's points
        _totalPoints += selectedGoal.Points;

        Console.WriteLine($"Event recorded for goal '{selectedGoal.Name}'.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}









    public int GetTotalPoints()
    {
        try
        {
            return _totalPoints;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return 0;
        }
    }
}
