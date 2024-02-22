namespace FinalProject;
        public class Tenant : Person
    {
        private FileManager fileManager;
        public int ApartmentNumber { get; protected set; }
        public List<FinancialTransaction> Charges { get; private set; }
        private List<MaintenanceRequest> maintenanceRequests;
        private Dictionary<string, TenantInfo> tenants;
        public class TenantInfo
{
    public int Age { get; }
    public string Password { get; }
    public string ApartmentNumber { get; }

    public TenantInfo(int age, string password, string apartmentNumber)
    {   
        
        Age = age;
        Password = password;
        ApartmentNumber = apartmentNumber;
    }
}


        

        public Tenant(string name) : base(name)
        {
            maintenanceRequests = new List<MaintenanceRequest>();
            Charges = new List<FinancialTransaction>(); // Initialize Charges list
            this.fileManager = new FileManager();
        }

         public List<MaintenanceRequest> GetMaintenanceRequests()
        {
            return maintenanceRequests;
        }

       public void SubmitMaintenanceRequest(string apartmentNumber, string issue)
{
    Apartment apartment = new(apartmentNumber);
    if (apartment != null)
    {
        MaintenanceRequest request = new MaintenanceRequest(issue, this);
        apartment.AddMaintenanceRequest(request);
        maintenanceRequests.Add(request);
        fileManager.SaveMaintenanceRequest(Name, issue); // Save the request to the tenant's file
    }
    else
    {
        Console.WriteLine("Invalid apartment.");
    }
}

    public void ViewMaintenanceRequests()
{
    // Load maintenance requests from the file associated with the tenant
    string maintenanceRequestFile = $"{Name}_MaintenanceRequests.txt";
    if (File.Exists(maintenanceRequestFile))
    {
        Console.WriteLine($"Maintenance requests for {Name}:");
        string[] requestLines = File.ReadAllLines(maintenanceRequestFile);
        foreach (string requestLine in requestLines)
        {
            // Assuming each line is formatted as "<Issue>:<Status>"
            string[] parts = requestLine.Split(':');
            if (parts.Length == 2)
            {
                string issue = parts[0].Trim();
                bool isResolved = parts[1].Trim().Equals("Resolved", StringComparison.OrdinalIgnoreCase);

                // Create a new MaintenanceRequest object and add it to the list
                MaintenanceRequest request = new MaintenanceRequest(issue, isResolved);
                maintenanceRequests.Add(request);
            }
            else
            {
                Console.WriteLine($"Invalid format in maintenance request file for {Name}: {requestLine}");
            }
        }
    }
    else
    {
        Console.WriteLine($"No maintenance requests found for {Name}.");
    }
}



    
    public void AddCharges(decimal amount, string description)
        {
            if (amount > 0)
            {
                Charges.Add(new FinancialTransaction
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    Status = "Unpaid", // Initial status can be set
                    TransactionId = Guid.NewGuid().ToString(),
                    AccountInformation = $"Charges for {description}",
                    PurposeDescription = description
                });
                fileManager.SaveTenantCharges(Name, amount, description);

                Console.WriteLine($"Charges added for '{description}' - ${amount}");
            }
            else
            {
                Console.WriteLine("Invalid charge amount.");
            }
        }

        
    }
        
