using System;

public class Order
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public List<MenuItem> OrderedItems { get; set; }
    public decimal TotalAmount { get; set; }
    public string AssignedEmployee { get; set; }
    public DateTime OrderDate { get; set; }

    public static void PlaceOrder(Customer customer, List<MenuItem> orderedItems, List<Employee> employees)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available to assign the order.");
            return;
        }

        var totalAmount = orderedItems.Sum(item => item.Price);
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

        ShowOrderReport(order);
    }

    public static void ShowOrderReport(Order order)
    {
        Console.WriteLine("========== Order Report ==========");
        Console.WriteLine($"Customer: {order.CustomerName} | Phone: {order.PhoneNumber}");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Assigned Employee: {order.AssignedEmployee}");
        Console.ResetColor();
        Console.WriteLine("{0,-5} {1,-20} | {2,8}", "ID", "Menu Item", "Price");
        Console.WriteLine(new string('-', 50));

        decimal totalAmount = 0;
        int itemIndex = 1;

        foreach (var item in order.OrderedItems)
        {
            totalAmount += item.Price;
            Console.WriteLine($"{itemIndex++,-5} {item.Name,-20} | {item.Price,8:F2}");
        }

        Console.WriteLine(new string('-', 50));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total Amount: ${totalAmount:F2}");
        Console.ResetColor();
        Console.WriteLine(new string('-', 50));
    }
}
