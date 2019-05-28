using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoAccountingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using DemoAccountingSystem.Data;

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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
