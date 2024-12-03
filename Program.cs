public class Customer
{
    public string Name { get; set; }
    public string PhoneNumber;
    public string Role { get; set; } = "customer";

    public static void AddCustomer(string name, string phoneNumber, List<Customer> customers)
    {
        var customer = new Customer { Name = name, PhoneNumber = phoneNumber };
        customers.Add(customer);
        Console.WriteLine($"\nCustomer Created: {customer.Name}, Phone: {customer.PhoneNumber}\n");
    }
}