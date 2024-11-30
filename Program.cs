public class Order
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public List<MenuItem> OrderedItems { get; set; }
    public decimal TotalAmount { get; set; }
    public string AssignedEmployee { get; set; }
    public DateTime OrderDate { get; set; }
}
