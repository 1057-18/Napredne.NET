using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmploymentWebApp.Models;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DataAccessLayer.Repository;
using BusinessLogicLayer.Interface;

namespace EmploymentWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IPaymentService _paymentService;
        private readonly IDepartmentsService _departmentsService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IPaymentService paymentService, IDepartmentsService departmentsService)
        {
            _logger = logger;
            _departmentsService = departmentsService;
            _paymentService = paymentService;
            _employeeService = employeeService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Validate(string username, string password)
        {
            if (username != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("SignIn");
            }
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeService.GetAll().ToList();
            ViewBag.NumberOfEmployees = employees.Count();

            DateTime lower;
            DateTime upper;
            if (DateTime.Now.Month == 1)
            {
                lower = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1);
                upper = new DateTime(DateTime.Now.Year - 1, 12, 31);
            }
            else
            {
                lower = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1);
                upper = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 31);
            }
            List<Payment> currentYearPayments = _paymentService.GetAll().Where(p => p.DateAndTime >= lower && p.DateAndTime <= upper).ToList();
            List<DateTime> monthsOrdered = new List<DateTime>();
            int start = DateTime.Today.Month;
            for(int i = 1; i <= 12; i++)
            {
                if (start > 12) start = 1;
                monthsOrdered.Add(new DateTime(1997, start, 10));
                start++;
            }
            List<string> months = monthsOrdered.Select(m => m.ToString("MMM")).ToList();
            List<double> paymentAmountPerMonth = new List<double>();
            foreach(DateTime d in monthsOrdered)
            {
                List<Payment> singleMonthPayments = currentYearPayments.Where(p => p.DateAndTime.Month == d.Month).ToList();
                paymentAmountPerMonth.Add(singleMonthPayments.Sum(s => s.Amount));
            }
            ViewBag.Months = months;
            ViewBag.Amounts = paymentAmountPerMonth;
            ViewBag.YearPaymentSum = currentYearPayments.Sum(p => p.Amount);

            List<int> numberOfDepartmentEmp = new List<int>();
            List<string> departmentsNames = _departmentsService.GetAll().Select(d => d.Name).ToList();
            foreach (string d in departmentsNames)
            {
                numberOfDepartmentEmp.Add(employees.Where(e => e.Department.Name == d).Count());
            }
            ViewBag.DepartmentNames = departmentsNames;
            ViewBag.NumberOfDepartmentEmployees = numberOfDepartmentEmp;
            
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Save()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
