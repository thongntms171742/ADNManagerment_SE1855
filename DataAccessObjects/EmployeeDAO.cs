using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class EmployeeDAO
    {
        private static EmployeeDAO? instance;
        private static readonly object instanceLock = new object();

        private EmployeeDAO() { }

        public static EmployeeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new EmployeeDAO();
                    return instance;
                }
            }
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees;
            try
            {
                var context = new LucyDbContext();
                employees = context.Employees.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employees;
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            Employee? employee = null;
            try
            {
                var context = new LucyDbContext();
                employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employee;
        }

        public Employee? Login(string username, string password)
        {
            Employee? employee = null;
            try
            {
                var context = new LucyDbContext();
                employee = context.Employees.SingleOrDefault(e => e.UserName == username && e.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                var context = new LucyDbContext();
                context.Employees.Add(employee);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                var context = new LucyDbContext();
                context.Entry(employee).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                var context = new LucyDbContext();
                var employee = context.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
} 