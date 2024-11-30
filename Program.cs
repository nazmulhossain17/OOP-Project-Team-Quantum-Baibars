public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString() => $"{Name,-20} | ${Price:F2}";
}