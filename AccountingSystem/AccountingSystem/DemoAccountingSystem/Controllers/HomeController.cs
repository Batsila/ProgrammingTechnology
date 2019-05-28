using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoAccountingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using DemoAccountingSystem.Data;
using DemoAccountingSystem.Data.Entities;

namespace DemoAccountingSystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        [Authorize]
        public IActionResult EmployeeList()
        {
            var employees = _dbContext.Employees.ToList();
            return View(new EmployeeListViewModel { Employees = employees });
        }

        [Authorize]
        public IActionResult CreateEmployeeList()
        {
            var employees = _dbContext.Employees.ToList();

            var remoteEmployess = _dbContext.RemoteEmployees
                .Where(re => !employees.Where(e => e.FirstName == re.FirstName &&
                e.LastName == re.LastName).Any()).ToList();

            return View(new CreateEmployListViewModel { RemoteEmployees = remoteEmployess });
        }

        [Authorize]
        public IActionResult CreateEmployee(int id)
        {
            var remoteEmployee = _dbContext.RemoteEmployees.First(re => re.Id == id);

            var viewModel = new CreateEmployeeViewModel
            {
                FirstName = remoteEmployee.FirstName,
                LastName = remoteEmployee.LastName,
                Position = remoteEmployee.Position
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEmployee(CreateEmployeeViewModel model)
        {
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position
            };

            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Employees.Add(employee);

                _dbContext.SaveChanges();

                dbContextTransaction.Commit();
            }

            var employees = _dbContext.Employees.ToList();

            var remoteEmployess = _dbContext.RemoteEmployees
                .Where(re => !employees.Where(e => e.FirstName == re.FirstName &&
                e.LastName == re.LastName).Any()).ToList();

            return View("CreateEmployeeList", new CreateEmployListViewModel { RemoteEmployees = remoteEmployess });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
