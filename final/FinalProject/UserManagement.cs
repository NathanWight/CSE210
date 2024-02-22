

namespace FinalProject
{
    public class UserManagement
    {
        private readonly Dictionary<string, string> tenants; // Username, Password for tenants

        public UserManagement()
        {
            tenants = new Dictionary<string, string>();
            LoadTenants(); // Load existing tenants from file when UserManagement is instantiated
        }

        public bool AddTenant(string username, string password)
        {
            if (!tenants.ContainsKey(username))
            {
                tenants.Add(username, password);
                SaveTenants(); // Save updated tenant information to file
                return true; // Tenant added successfully
            }
            return false; // Username already exists
        }

        public bool AuthenticateTenant(string username, string password)
        {
            if (tenants.ContainsKey(username) && tenants[username] == password)
            {
                return true; // Username and password match
            }
            return false; // Username or password doesn't match
        }

        private void SaveTenants()
        {
            // Save tenant login information to a file
            using (StreamWriter writer = new StreamWriter("tenants.txt"))
            {
                foreach (var kvp in tenants)
                {
                    writer.WriteLine($"{kvp.Key},{kvp.Value}");
                }
            }
        }

        private void LoadTenants()
        {
            // Load tenant login information from a file
            if (File.Exists("tenants.txt"))
            {
                using (StreamReader reader = new StreamReader("tenants.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            tenants.Add(parts[0], parts[1]);
                        }
                    }
                }
            }
        }
    }
}
