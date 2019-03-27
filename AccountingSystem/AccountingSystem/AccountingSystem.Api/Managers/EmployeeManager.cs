using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Helpers;
using AccountingSystem.Api.Managers.Exceptions;
using AccountingSystem.Api.Models;

namespace AccountingSystem.Api.Managers
{
    /// <summary>
    /// Manager for employees
    /// </summary>
    public class EmployeeManager
    {
        private readonly AccountingSystemContext _dbContext;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public EmployeeManager(AccountingSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates new employee
        /// </summary>
        /// <param name="employee">Create employee request</param>
        public Employee CreateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new IncorrectDataException("First name required");

            if (string.IsNullOrWhiteSpace(employee.SecondName))
                throw new IncorrectDataException("Second name required");

            var dbEmployee = new Employee
            {
                FirstName = employee.FirstName,
                SecondName = employee.SecondName
            };

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Employees.Add(dbEmployee);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return dbEmployee;
        }

        /// <summary>
        /// Updates exist employee
        /// </summary>
        /// <param name="employee">Employee to update</param>
        public Employee UpdateEmployee(Employee employee)
        {
            var dbEmployee = _dbContext.Employees.FirstOrDefault(m => m.Id == employee.Id);

            if (dbEmployee == null)
                throw new NotFoundException($"Employee with id '{employee.Id}' not exist");

            if (employee.FirstName == string.Empty)
                throw new IncorrectDataException("Name must be not empty");

            if (employee.SecondName == string.Empty)
                throw new IncorrectDataException("Second name must be not empty");

            dbEmployee.FirstName = employee.FirstName ?? dbEmployee.FirstName;
            dbEmployee.SecondName = employee.SecondName ?? dbEmployee.SecondName;

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Employees.Update(dbEmployee);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }

            return dbEmployee;
        }

        /// <summary>
        /// Deletes employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        public void DeleteEmployee(int employeeId)
        {
            var dbEmploye = _dbContext.Employees.FirstOrDefault(m => m.Id == employeeId);

            if (dbEmploye == null)
                throw new NotFoundException($"Employee with id '{employeeId}' not exist");

            using (var txn = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Employees.Remove(dbEmploye);
                    _dbContext.SaveChanges();

                    txn.Commit();
                }
                catch
                {
                    txn.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        public Employee GetEmployee(int id)
        {
            var dbEmployee = _dbContext.Employees.FirstOrDefault(m => m.Id == id);

            if (dbEmployee == null)
                throw new NotFoundException($"Employee with id '{id}' not exist");

            return dbEmployee;
        }

        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns>All employees</returns>
        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = _dbContext.Employees;
            return employees;
        }
    }
}
