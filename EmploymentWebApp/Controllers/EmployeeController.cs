using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Interface;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using EmploymentWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmploymentWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPaymentService _paymentService;
        private readonly IDepartmentsService _departmentsService;
        private static int _idEditedEmployee = 0;
        private static List<int> filterChecked;
        private static int filterMin;
        private static int filterMax;

        public EmployeeController(IEmployeeService employeeService, IPaymentService paymentService, IDepartmentsService departmentsService)
        {
            _employeeService = employeeService;
            _paymentService = paymentService;
            _departmentsService = departmentsService;
        }

        public IActionResult Index(string sortOrder, int? pageNumber, int minNum, int maxNum, List<int> AreChecked, bool filter = false, bool state = false)
        {
            int maxYears = (int)(DateTime.Now - _employeeService.GetAll().ToList().Min(e => e.DateOfHire)).Days / 365;
            ViewData["FilterMinNum"] = 0;
            ViewData["FilterMaxNum"] = maxYears;
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            List<Employee> list = _employeeService.GetAll().ToList();
            List<DepartmentViewModel> departmentViewModels = DepartmentsToDepartmentViewModels(_departmentsService.GetAll().ToList());
            if (filter)
            {
                filterChecked = AreChecked;
                filterMin = minNum;
                filterMax = maxNum;
            }
            if (state)
            {
                ViewData["SliderOneValue"] = filterMin;
                ViewData["SliderTwoValue"] = filterMax;
                departmentViewModels = FilterSelectedDepartments(filterChecked, departmentViewModels);
                list = FilterEmployees(filterMin, filterMax, departmentViewModels, list);
                if (filterChecked.Count == 0 || filterChecked.Count == _departmentsService.GetAll().ToList().Count)
                {
                    departmentViewModels.ForEach(d => d.IsChecked = false);
                }
            }
            else
            {
                ViewData["SliderOneValue"] = 0;
                ViewData["SliderTwoValue"] = maxYears;
                filterChecked = _departmentsService.GetAll().Select(d => d.Id).ToList();
                filterMin = 0;
                filterMax = (DateTime.Now - _employeeService.GetAll().ToList().Min(e => e.DateOfHire)).Days / 365;
            }
            switch (sortOrder)
            {
                case "Name":
                    list = list.OrderBy(e => e.LastName).ToList();
                    break;
                case "name_desc":
                    list = list.OrderByDescending(e => e.LastName).ToList();
                    break;
                case "Date":
                    list = list.OrderBy(e => e.DateOfHire).ToList();
                    break;
                case "date_desc":
                    list = list.OrderByDescending(e => e.DateOfHire).ToList();
                    break;
                default:
                    break;
            }
            
            ViewData["Departments"] = departmentViewModels;
            return View(PaginatedList<EmployeeViewModel>.Create(PrepareForView(list), pageNumber ?? 1, 9, sortOrder));
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmployeeViewModel() { HeaderString = "Add", DepartmentList = _departmentsService.GetAll().ToList() });
            }
            else
            {
                Employee employee = _employeeService.Get(id);
                _idEditedEmployee = employee.Id;
                EmployeeViewModel employeeViewModel = EmployeeToEmployeeViewModel(employee);
                employeeViewModel.HeaderString = "Edit";
                return View(employeeViewModel);
            }
        }

        public IActionResult Details(int id)
        {
            return View(EmployeeToEmployeeViewModel(_employeeService.Get(id)));
        }

        public IActionResult Save(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = EmployeeViewModelToEmployee(employeeViewModel);
                employee.Id = EmployeeController._idEditedEmployee;
                _idEditedEmployee = 0;
                if (!_employeeService.GetAllWithOutTracking().ToList().Contains(employee))
                {
                    employee.Id = _employeeService.GetUniqueId();
                    _employeeService.Add(employee);
                }
                else
                {
                    _employeeService.Update(employee);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _employeeService.Remove(_employeeService.Get(id));
            return RedirectToAction("Index");
        }

        public DateTime ParseDateTimeString(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

        public List<EmployeeViewModel> PrepareForView(List<Employee> employees)
        {
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (Employee employee in employees)
            {
                employeeViewModels.Add(EmployeeToEmployeeViewModel(employee));
            }
            return employeeViewModels;
        }

        public EmployeeViewModel EmployeeToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel()
            {
                EmployeeForm = employee,
                StringDateOfHire = employee.DateOfHire.ToString("dd-MMM-yyyy"),
                StringDateInput = employee.DateOfHire.ToString("dd-MM-yyyy"),
                StringDepartment = employee.Department.Name,
                DepartmentList = _departmentsService.GetAll().ToList()
            };
        }

        public Employee EmployeeViewModelToEmployee(EmployeeViewModel employeeViewModel)
        {
            Employee employee = employeeViewModel.EmployeeForm;
            employee.Department = _departmentsService.Find(d => d.Name == employeeViewModel.StringDepartment).ToList()[0];
            employee.DateOfHire = ParseDateTimeString(employeeViewModel.StringDateInput);
            return employee;
        }

        public List<DepartmentViewModel> DepartmentsToDepartmentViewModels(List<Department> departments)
        {
            List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
            foreach(Department department in departments)
            {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel();
                departmentViewModel.Department = department;
                departmentViewModels.Add(departmentViewModel);
            }
            return departmentViewModels;
        }

        public List<Employee> FilterEmployees(int minNum, int maxNum, List<DepartmentViewModel> departmentViewModels, List<Employee> list)
        {
            departmentViewModels = departmentViewModels.Where(d => d.IsChecked == true).ToList();
            list = list.Where(e => departmentViewModels.Any(d => d.Department.Id == e.Department.Id) && (DateTime.Now - e.DateOfHire).Days/365 >= minNum && (DateTime.Now - e.DateOfHire).Days / 365 <= maxNum).ToList();
            return list;
        }

        public List<DepartmentViewModel> FilterSelectedDepartments(List<int> AreChecked, List<DepartmentViewModel> departmentViewModels)
        {
            if (AreChecked.Count == 0)
            {
                departmentViewModels.ForEach(d => d.IsChecked = true);
            }
            else
            {
                foreach (int i in AreChecked)
                {
                    departmentViewModels.Find(d => d.Department.Id == i).IsChecked = true;
                }
            }
            return departmentViewModels;
        }
    }
}
