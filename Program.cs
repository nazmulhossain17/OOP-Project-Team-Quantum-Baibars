using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

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

    public void PlaceOrder(Customer customer, List<int> itemIds)
    {
        var orderedItems = Menu.Where(item => itemIds.Contains(item.Id)).ToList();

        if (orderedItems.Count == 0)
        {
            Console.WriteLine("No valid menu items selected.");
            return;
        }

        if (Employees.Count == 0)
        {
            Console.WriteLine("No employees available to assign the order.");
            return;
        }

        Order.PlaceOrder(customer, orderedItems, Employees);
    }
}


