using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
    }

    public void Task1()
    {
        var query =
            from emp in employeeList
            join sal in salaryList
            on emp.EmployeeID equals sal.EmployeeID into g
            select new
            {
                FirstName = emp.EmployeeFirstName,
                SumSalary = g.Sum(x => x.Amount)
            };
        Console.WriteLine("The total Salary of the Employee:\n");
        foreach (var item in query.OrderBy(z => z.SumSalary))
        {
            Console.WriteLine(item.FirstName + ":" + item.SumSalary);
        }
    }

    public void Task2()
    {
        var query =
            from emp in employeeList
            join sal in salaryList.Where
            (m => m.Type == SalaryType.Monthly)
            on emp.EmployeeID equals sal.EmployeeID into g
            select new
            {
                FirstName = emp.EmployeeFirstName,
                LastName = emp.EmployeeLastName,
                ID = emp.EmployeeID,
                EmployeeAge = emp.Age,
                SumSalary = g.Sum(x => x.Amount)
            };
        foreach (var item in query.OrderBy(a => a.EmployeeAge).AsEnumerable().Skip(1).Take(1))
        {
            Console.WriteLine("\n" + item.FirstName + " " + item.LastName + " of age " + item.EmployeeAge + "years and employee Id: " + item.ID +
                "\ngets an Monthly Salary of: " + item.SumSalary);
        }
    }

    public void Task3()
    {
        var query3 =
            from emp in employeeList.Where
            (a => a.Age > 30)
            join sal in salaryList
            on emp.EmployeeID equals sal.EmployeeID into g
            orderby emp.EmployeeID
            select new
            {
                //FirstName = emp.EmployeeFirstName,
                AvgSalary = g.Average(x => x.Amount)
            };
        Console.WriteLine("\nThe Average of the Salary is:");
        foreach (var item in query3)
        {
            Console.WriteLine(item.AvgSalary );
        }
    }
}
public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}
