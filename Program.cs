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