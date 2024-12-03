using System;

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
