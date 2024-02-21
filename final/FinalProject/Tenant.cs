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
        Console.WriteLine($"Maintenance requests for {Name}:");
        int count = 0;
        foreach (MaintenanceRequest request in maintenanceRequests)
        {
            Console.WriteLine($"{count++}. {request.Issue} - Status: {(request.IsResolved ? "Resolved" : "Pending")}");
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
        
