public class EternalGoal : Goal
{
    private int _timesAccomplished; // Declare _timesAccomplished

    public EternalGoal(string name, string description, int timesAccomplished, int points) : base(name, description, points, false)
    {
        _type = 2;
        _timesAccomplished = timesAccomplished; // Initialize _timesAccomplished
    }

    public override void SetIsCompleted()
    {
        try
        {
            _timesAccomplished += 1;
            _isCompleted = false;
            Console.WriteLine($"Congratulations! You earned {_points} points");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public override void DisplayGoal(int option)
    {
        try
        {
            if (option == 0)
            {
                Console.WriteLine($"    {_name} ({_description}) -- Times accomplished: {_timesAccomplished}");
            }
            else
            {
                Console.WriteLine($"{_name}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

   public override string GetStringRep()
{
    try
    {
        // Convert boolean values to '1' or '0' strings
        string isCompletedString = _isCompleted ? "1" : "0";
        return $"{_type}|{_name}|{_description}|{_points}|{isCompletedString}|{_timesAccomplished}";
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        return string.Empty;
    }
}

}
