using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Customer
{
    public string Name;
    public string PhoneNumber;
    public string Role = "customer";

    public static void AddCustomer(string name, string phoneNumber, List<Customer> customers)
    {
        var customer = new Customer { Name = name, PhoneNumber = phoneNumber };
        customers.Add(customer);
        Console.WriteLine($"\nCustomer Created: {customer.Name}, Phone: {customer.PhoneNumber}\n");
    }
}

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    private const string MenuFilePath = "menu.json";

    public static List<MenuItem> LoadMenu()
    {
        if (File.Exists(MenuFilePath))
        {
            var menuJson = File.ReadAllText(MenuFilePath);
            return JsonSerializer.Deserialize<List<MenuItem>>(menuJson) ?? new List<MenuItem>();
        }
        return new List<MenuItem>();
    }

    public static void SaveMenu(List<MenuItem> menu)
    {
        var menuJson = JsonSerializer.Serialize(menu, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(MenuFilePath, menuJson);
    }

    public static void ShowMenu(List<MenuItem> menu)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═════════════════════ Restaurant Menu ════════════════════╗");
        Console.ResetColor();
        Console.WriteLine("║ ID  │ Item Name                    │ Price               ║");
        Console.WriteLine("╠═════╪══════════════════════════════╪═════════════════════╣");

        foreach (var item in menu)
        {
            Console.WriteLine($"║ {item.Id,-3} │ {item.Name,-27}  │ ${item.Price,-18:F2} ║");
        }

        Console.WriteLine("╚═════╧══════════════════════════════╧═════════════════════╝");
        Console.ResetColor();
    }

    public override string ToString() => $"{Name,-20} | ${Price:F2}";
}

public class Employee
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string EmployeeId { get; set; }
    public string Position { get; set; }

    private const string EmployeeFilePath = "employees.json";

    public static List<Employee> LoadEmployees()
    {
        if (File.Exists(EmployeeFilePath))
        {
            var employeeJson = File.ReadAllText(EmployeeFilePath);
            return JsonSerializer.Deserialize<List<Employee>>(employeeJson) ?? new List<Employee>();
        }
        return new List<Employee>();
    }

    public static void SaveEmployees(List<Employee> employees)
    {
        var employeeJson = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(EmployeeFilePath, employeeJson);
    }

    public override string ToString()
    {
        return $"Employee Name: {Name}, ID: {EmployeeId}, Position: {Position}, Phone: {PhoneNumber}";
    }
}

public class Order
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public Dictionary<MenuItem, int> OrderedItems { get; set; }  // Track item and quantity
    public decimal TotalAmount { get; set; }
    public string AssignedEmployee { get; set; }
    public DateTime OrderDate { get; set; }

    public static void PlaceOrder(Customer customer, Dictionary<MenuItem, int> orderedItems, List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available to assign the order.");
            return;
        }

        var totalAmount = orderedItems.Sum(item => item.Key.Price * item.Value);
        var assignedEmployee = employees[new Random().Next(employees.Count)];

        var order = new Order
        {
            CustomerName = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            OrderedItems = orderedItems,
            TotalAmount = totalAmount,
            AssignedEmployee = assignedEmployee.Name,
            OrderDate = DateTime.Now
        };

        ShowOrderReport(order);  // Use the ShowOrderReport method from the Order class
    }

    public static void ShowOrderReport(Order order)
    {
        Console.WriteLine("========== Order Report ==========");
        Console.WriteLine($"Customer: {order.CustomerName} | Phone: {order.PhoneNumber}");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Assigned Employee: {order.AssignedEmployee}");
        Console.ResetColor();
        Console.WriteLine("{0,-5} {1,-20} | {2,8} | {3,10}", "ID", "Menu Item", "Price", "Quantity");
        Console.WriteLine(new string('-', 60));

        decimal totalAmount = 0;
        int itemIndex = 1;

        foreach (var item in order.OrderedItems)
        {
            var itemTotal = item.Key.Price * item.Value;
            totalAmount += itemTotal;
            Console.WriteLine($"{itemIndex++,-5} {item.Key.Name,-20} | {item.Key.Price,8:F2} | {item.Value,10}");
        }

        Console.WriteLine(new string('-', 60));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total Amount: ${totalAmount:F2}");
        Console.ResetColor();
        Console.WriteLine(new string('-', 60));
    }
}

public class Restaurant
{
    public string Name { get; set; }
    public List<MenuItem> Menu { get; private set; }
    public List<Employee> Employees { get; private set; }

    public Restaurant(string name)
    {
        Name = name;
        Menu = MenuItem.LoadMenu();
        Employees = Employee.LoadEmployees();
    }

    public void DisplayMenu()
    {
        MenuItem.ShowMenu(Menu);
    }

    public void AddMenuItem(MenuItem item)
    {
        Menu.Add(item);
        MenuItem.SaveMenu(Menu);
    }

    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
        Employee.SaveEmployees(Employees);
    }

    public void PlaceOrder(Customer customer, Dictionary<MenuItem, int> orderedItems) // Updated signature
    {
        if (Employees.Count == 0)
        {
            Console.WriteLine("No employees available to assign the order.");
            return;
        }

        var totalAmount = orderedItems.Sum(item => item.Key.Price * item.Value);
        var assignedEmployee = Employees[new Random().Next(Employees.Count)];

        var order = new Order
        {
            CustomerName = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            OrderedItems = orderedItems,
            TotalAmount = totalAmount,
            AssignedEmployee = assignedEmployee.Name,
            OrderDate = DateTime.Now
        };

        Order.ShowOrderReport(order);  // Correctly call ShowOrderReport using the Order class
    }
}

class Program
{
    static void Main(string[] args)
    {
        var restaurant = new Restaurant("My Restaurant");
        var customers = new List<Customer>();

        Console.Write("Enter your name: ");
        var name = Console.ReadLine();
        Console.Write("Enter your phone number: ");
        var phoneNumber = Console.ReadLine();

        var customer = customers.FirstOrDefault(c => c.Name == name && c.PhoneNumber == phoneNumber);
        if (customer == null)
        {
            Customer.AddCustomer(name, phoneNumber, customers);
            customer = customers.Last(); // Get the last added customer
        }

        restaurant.DisplayMenu();

        Console.WriteLine("\nEnter the IDs of items you'd like to order, separated by commas (e.g., 1,3): ");
        var input = Console.ReadLine();
        var itemIds = input.Split(',').Select(id => int.Parse(id.Trim())).ToList();

        var orderedItems = new Dictionary<MenuItem, int>();

        foreach (var itemId in itemIds)
        {
            var selectedItem = restaurant.Menu.FirstOrDefault(item => item.Id == itemId);

            if (selectedItem != null)
            {
                // Check if item is already in orderedItems dictionary
                if (orderedItems.ContainsKey(selectedItem))
                {
                    // Increase quantity if item already ordered
                    orderedItems[selectedItem]++;
                }
                else
                {
                    // If it's the first time selecting this item, ask for quantity
                    Console.Write($"Enter the quantity for {selectedItem.Name}: ");
                    int quantity = int.Parse(Console.ReadLine());
                    orderedItems[selectedItem] = quantity; // Store item with quantity
                }
            }
            else
            {
                Console.WriteLine($"Item with ID {itemId} not found.");
            }
        }

        // Calculate the total amount for all items and quantities
        var totalAmount = orderedItems.Sum(item => item.Key.Price * item.Value);
        Console.WriteLine($"\nTotal amount for your order: ${totalAmount:F2}");

        // Confirmation step
        Console.Write("\nAre you sure you want to proceed with the purchase? (y/n): ");
        var confirm = Console.ReadLine();

        if (confirm.ToLower() == "y")
        {
            restaurant.PlaceOrder(customer, orderedItems);
        }
        else
        {
            Console.WriteLine("Order canceled.");
        }
    }
}
