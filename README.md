# Restaurant Management System

This project is a Restaurant Management System, built with C# to manage customers, employees, menu items, and orders. It includes features for displaying the menu, adding and saving customers and employees, placing orders, and generating reports. The system is designed to work using JSON files to persist data for the menu, employees, and customers.

## Team Members
- **Syed Nazmul Hossain** - 2233081426
- **Mahbub Alom Jony** - 2233081482
- **Mariam Mim** - 2233081428
- **Samia Akter** - 2233081435
- **Tamjid Hossain** - 2233081440

## Features
1. Customer Management
   - Add new customers
   - Store customer information (name, phone number)

2. Menu Management
   - Load and save menu items from/to a JSON file
   - Display menu items in a formatted table

3. Employee Management
   - Load and save employee information from/to a JSON file
   - Assign employees to orders randomly

4. Order Processing
   - Allow customers to select menu items and specify quantities
   - Calculate total order amount
   - Generate and display order reports

5. Restaurant Operations
   - Initialize restaurant with menu and employees
   - Add new menu items
   - Add new employees
   - Place orders

## Class Descriptions

1. **Customer**: 
   - Represents a customer with properties for name, phone number, and role. 
   - Includes a static method `AddCustomer` to add a new customer to a list of customers.

2. **MenuItem**: 
   - Represents a menu item with properties for ID, name, and price. 
   - Includes static methods `LoadMenu` and `SaveMenu` to load and save the menu items from/to a JSON file. 
   - Includes a static method `ShowMenu` to display the menu in a formatted table.

3. **Employee**: 
   - Represents an employee with properties for name, phone number, employee ID, and position. 
   - Includes static methods `LoadEmployees` and `SaveEmployees` to load and save the employees from/to a JSON file.

4. **Order**: 
   - Represents an order with properties for customer name, phone number, ordered items (as a dictionary with menu items and quantities), total amount, assigned employee, and order date. 
   - Includes static methods `PlaceOrder` and `ShowOrderReport` to place an order and display the order report.

5. **Restaurant**: 
   - Represents a restaurant with properties for name, menu (a list of menu items), and employees (a list of employees). 
   - Includes methods to display the menu, add a new menu item, add a new employee, and place an order.

6. **Program**: 
   - The main entry point of the application. 
   - It creates a new restaurant, a list of customers, and prompts the user to enter their name and phone number. 
   - It then displays the menu, allows the user to select items and quantities, calculates the total amount, and asks for confirmation to place the order. 
   - If the order is confirmed, it calls the `PlaceOrder` method of the restaurant.

## How to Use

1. Run the program.
2. Enter your name and phone number when prompted.
3. View the displayed menu.
4. Enter the IDs of items you want to order, separated by commas.
5. Specify the quantity for each item.
6. Review the total amount and confirm the order.
7. If confirmed, an order report will be generated and displayed.

## Data Persistence

- Menu items are stored in `menu.json`
- Employee information is stored in `employees.json`

## Getting Started

### Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (version x.x.x or later)

### Clone the Repository

```bash
git clone https://github.com/your-username/restaurant-management-system.git
```
### Navigate to the project directory
```bash
cd restaurant-management-system
```
### Run the Project
```bash
dotnet run
```
