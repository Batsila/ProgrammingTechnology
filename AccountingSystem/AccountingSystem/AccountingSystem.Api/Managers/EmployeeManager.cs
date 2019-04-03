using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Api.Entity;
using AccountingSystem.Api.Helpers;
using AccountingSystem.Api.Managers.Exceptions;
using AccountingSystem.Api.Models;
using AccountingSystem.Api.Models.Requests;

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
        public WebEmployee CreateEmployee(CreateEmployeeRequest employee)
        {
            var dbDepartment = _dbContext.Departments.First(d => d.Id == employee.DepartmentId);

            if (dbDepartment == null)
            {
                throw new NotFoundException($"Department {employee.DepartmentId} does not exist");
            }

            var dbSalaryInfo = _dbContext.Salaries.First(d => d.Id == employee.SalaryId);

            if (dbSalaryInfo == null)
            {
                throw new NotFoundException($"Salary info {employee.DepartmentId} does not exist");
            }

            var dbEmployee = new Employee
            {
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Address = employee.Address,
                Department = dbDepartment,
                DepartmentId = employee.DepartmentId,
                SalaryInfo = dbSalaryInfo,
                SalaryInfoId = employee.SalaryId,
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

            return dbEmployee.EmployeeToWebEmployee();
        }

        /// <summary>
        /// Updates exist employee
        /// </summary>
        /// <param name="employee">Employee to update</param>
        public WebEmployee UpdateEmployee(UpdateEmployeeRequest employee)
        {
            var dbEmployee = _dbContext.Employees.FirstOrDefault(m => m.Id == employee.Id);

            if (dbEmployee == null)
            {
                throw new NotFoundException($"Employee with id '{employee.Id}' not exist");
            }

            if (!string.IsNullOrEmpty(employee.FirstName))
            {
                dbEmployee.FirstName = employee.FirstName;
            }

            if (!string.IsNullOrEmpty(employee.SecondName))
            {
                dbEmployee.SecondName = employee.SecondName;
            }

            if (!string.IsNullOrEmpty(employee.Address))
            {
                dbEmployee.Address = employee.Address;
            }

            if (employee.DepartmentId > 0)
            {
                var dbDepartment = _dbContext.Departments.First(d => d.Id == employee.DepartmentId);

                dbEmployee.Department = dbDepartment ?? throw new NotFoundException($"Department {employee.DepartmentId} does not exist");
                dbEmployee.DepartmentId = employee.DepartmentId;
            }

            if (employee.SalaryId > 0)
            {
                var dbSalaryInfo = _dbContext.Salaries.First(d => d.Id == employee.SalaryId);

                dbEmployee.SalaryInfo = dbSalaryInfo ?? throw new NotFoundException($"Salary info {employee.DepartmentId} does not exist");
                dbEmployee.SalaryInfoId = employee.SalaryId;
            }

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

            return dbEmployee.EmployeeToWebEmployee();
        }

        /// <summary>
        /// Deletes employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        public void DeleteEmployee(int employeeId)
        {
            var dbEmploye = _dbContext.Employees.FirstOrDefault(m => m.Id == employeeId);

            if (dbEmploye == null)
            {
                throw new NotFoundException($"Employee with id '{employeeId}' not exist");
            }

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
        public WebEmployee GetEmployee(int id)
        {
            var dbEmployee = _dbContext.Employees.FirstOrDefault(m => m.Id == id);

            if (dbEmployee == null)
            {
                throw new NotFoundException($"Employee with id '{id}' not exist");
            }

            return dbEmployee.EmployeeToWebEmployee();
        }

        /// <summary>
        /// Returns all employees
        /// </summary>
        /// <returns>All employees</returns>
        public IEnumerable<WebEmployee> GetAllEmployees()
        {
            var employees = _dbContext.Employees.Select(e => e.EmployeeToWebEmployee());
            return employees;
        }
    }
}
