
using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    // Employee Data Model - 10 Columns
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Department { get; set; } = "";
    public string JobTitle { get; set; } = "";
    public decimal MonthlySalary { get; set; }
    public double TaxRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; } = "";
}

class Program
{
    // Empty employee list - no predefined records
    static List<Employee> employees = new List<Employee>();

    // Employee ID starts at 1001
    static int nextId = 1001;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine(" EMPLOYEE PAYROLL & HR SYSTEM - FEATURE VERSION 2");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. Create Employee");
            Console.WriteLine("2. Read All Employees");
            Console.WriteLine("3. Search / Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.WriteLine("==============================================");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine() ?? "";

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;

                case "2":
                    ReadEmployees();
                    break;

                case "3":
                    SearchEmployees();
                    break;

                case "4":
                    UpdateEmployee();
                    break;

                case "5":
                    DeleteEmployee();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Exiting Employee Payroll & HR Management System...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1-6.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    // CREATE: Add a new employee
    static void CreateEmployee()
    {
        Console.WriteLine("--- CREATE NEW EMPLOYEE ---");

        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Enter Department: ");
        string department = Console.ReadLine() ?? "";

        Console.Write("Enter Job Title: ");
        string jobTitle = Console.ReadLine() ?? "";

        Console.Write("Enter Monthly Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary < 0)
        {
            Console.WriteLine("Invalid salary. Employee was not added.");
            return;
        }

        Console.Write("Enter Tax Rate (e.g., 0.12 for 12%): ");
        if (!double.TryParse(Console.ReadLine(), out double taxRate)
            || taxRate < 0 || taxRate > 1)
        {
            Console.WriteLine("Invalid tax rate. Enter a value from 0 to 1.");
            return;
        }

        Console.Write("Enter Hire Date (MM/dd/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
        {
            Console.WriteLine("Invalid date. Employee was not added.");
            return;
        }

        Console.Write("Is Active? (true/false): ");
        if (!bool.TryParse(Console.ReadLine(), out bool isActive))
        {
            Console.WriteLine("Invalid employment status.");
            return;
        }

        Console.Write("Enter Company Email: ");
        string email = Console.ReadLine() ?? "";

        // Instantiate a new Employee object
        Employee newEmployee = new Employee
        {
            Id = nextId++,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = salary,
            TaxRate = taxRate,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        };

        // Add the employee to the generic List
        employees.Add(newEmployee);

        Console.WriteLine("\nEmployee added successfully!");
        Console.WriteLine($"Assigned Employee ID: {newEmployee.Id}");
    }

    // READ: Display all employees
    static void ReadEmployees()
    {
        Console.WriteLine("--- ALL EMPLOYEES ---");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        // Display all employees using foreach
        DisplayEmployees(employees);
    }

    // Helper method for displaying employee records
    static void DisplayEmployees(IEnumerable<Employee> employeeList)
    {
        foreach (Employee employee in employeeList)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"ID:             {employee.Id}");
            Console.WriteLine($"First Name:     {employee.FirstName}");
            Console.WriteLine($"Last Name:      {employee.LastName}");
            Console.WriteLine($"Department:     {employee.Department}");
            Console.WriteLine($"Job Title:      {employee.JobTitle}");
            Console.WriteLine($"Monthly Salary: {employee.MonthlySalary:C}");
            Console.WriteLine($"Tax Rate:       {employee.TaxRate:P}");
            Console.WriteLine($"Hire Date:      {employee.HireDate:MM/dd/yyyy}");
            Console.WriteLine($"Is Active:      {employee.IsActive}");
            Console.WriteLine($"Email:          {employee.Email}");
        }

        Console.WriteLine("----------------------------------------------");
    }

    // SEARCH / FILTER: Find employees using LINQ
    static void SearchEmployees()
    {
        Console.WriteLine("--- SEARCH / FILTER EMPLOYEES ---");
        Console.WriteLine("1. Search by Employee Name");
        Console.WriteLine("2. Filter by Department");
        Console.WriteLine("3. Filter by Employment Status");

        Console.Write("\nSelect search option: ");
        string choice = Console.ReadLine() ?? "";

        IEnumerable<Employee> results;

        switch (choice)
        {
            case "1":
                Console.Write("Enter employee name: ");
                string name = Console.ReadLine() ?? "";

                results = employees.Where(e =>
                    e.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    e.LastName.Contains(name, StringComparison.OrdinalIgnoreCase));

                break;

            case "2":
                Console.Write("Enter department: ");
                string department = Console.ReadLine() ?? "";

                results = employees.Where(e =>
                    e.Department.Contains(department, StringComparison.OrdinalIgnoreCase));

                break;

            case "3":
                Console.Write("Enter status (true = Active, false = Inactive): ");

                if (!bool.TryParse(Console.ReadLine(), out bool status))
                {
                    Console.WriteLine("Invalid status.");
                    return;
                }

                results = employees.Where(e => e.IsActive == status);

                break;

            default:
                Console.WriteLine("Invalid search option.");
                return;
        }

        // Convert LINQ results into a list
        List<Employee> filteredEmployees = results.ToList();

        Console.WriteLine("\n--- SEARCH RESULTS ---");

        if (filteredEmployees.Count == 0)
        {
            Console.WriteLine("No matching employees found.");
        }
        else
        {
            Console.WriteLine($"Found {filteredEmployees.Count} employee(s).");
            DisplayEmployees(filteredEmployees);
        }
    }

    // UPDATE: Modify an existing employee
    static void UpdateEmployee()
    {
        Console.WriteLine("--- UPDATE EMPLOYEE ---");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available to update.");
            return;
        }

        Console.Write("Enter Employee ID to update: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        // Find employee using LINQ FirstOrDefault
        Employee? employee = employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine("\nLeave a field blank to keep its current value.");

        Console.Write($"First Name ({employee.FirstName}): ");
        string firstName = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(firstName))
            employee.FirstName = firstName;

        Console.Write($"Last Name ({employee.LastName}): ");
        string lastName = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(lastName))
            employee.LastName = lastName;

        Console.Write($"Department ({employee.Department}): ");
        string department = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(department))
            employee.Department = department;

        Console.Write($"Job Title ({employee.JobTitle}): ");
        string jobTitle = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(jobTitle))
            employee.JobTitle = jobTitle;

        Console.Write($"Monthly Salary ({employee.MonthlySalary}): ");
        string salaryInput = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(salaryInput))
        {
            if (decimal.TryParse(salaryInput, out decimal salary) && salary >= 0)
                employee.MonthlySalary = salary;
            else
                Console.WriteLine("Invalid salary. Keeping current value.");
        }

        Console.Write($"Tax Rate ({employee.TaxRate}): ");
        string taxInput = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(taxInput))
        {
            if (double.TryParse(taxInput, out double taxRate)
                && taxRate >= 0 && taxRate <= 1)
                employee.TaxRate = taxRate;
            else
                Console.WriteLine("Invalid tax rate. Keeping current value.");
        }

        Console.Write($"Hire Date ({employee.HireDate:MM/dd/yyyy}): ");
        string dateInput = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime hireDate))
                employee.HireDate = hireDate;
            else
                Console.WriteLine("Invalid date. Keeping current value.");
        }

        Console.Write($"Is Active ({employee.IsActive}) [true/false]: ");
        string statusInput = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(statusInput))
        {
            if (bool.TryParse(statusInput, out bool isActive))
                employee.IsActive = isActive;
            else
                Console.WriteLine("Invalid status. Keeping current value.");
        }

        Console.Write($"Email ({employee.Email}): ");
        string email = Console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(email))
            employee.Email = email;

        Console.WriteLine("\nEmployee updated successfully!");
    }

    // DELETE: Remove an employee
    static void DeleteEmployee()
    {
        Console.WriteLine("--- DELETE EMPLOYEE ---");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available to delete.");
            return;
        }

        Console.Write("Enter Employee ID to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        // Remove the matching employee using RemoveAll
        int removedCount = employees.RemoveAll(e => e.Id == id);

        if (removedCount > 0)
            Console.WriteLine("\nEmployee deleted successfully!");
        else
            Console.WriteLine("\nEmployee not found.");
    }
}
