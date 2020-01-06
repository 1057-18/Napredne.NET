using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using EmploymentWebApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmploymentWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        IUnitOfWork _unitOfWork;
        private static int _idEditedEmployee = 0;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(PrepareForView(_unitOfWork.Employees.GetAll().ToList()));
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmployeeViewModel() { HeaderString = "Add", DepartmentList = _unitOfWork.Departments.GetAll().ToList() });
            }
            else
            {
                Employee employee = _unitOfWork.Employees.Get(id);
                _idEditedEmployee = employee.Id;
                EmployeeViewModel employeeViewModel = EmployeeToEmployeeViewModel(employee);
                employeeViewModel.HeaderString = "Edit";
                return View(employeeViewModel);
            }
        }

        public IActionResult Details(int id)
        {
            return View(EmployeeToEmployeeViewModel(_unitOfWork.Employees.Get(id)));
        }

        public IActionResult Save(EmployeeViewModel employeeViewModel)
        {
            Employee employee = EmployeeViewModelToEmployee(employeeViewModel);
            employee.Id = EmployeeController._idEditedEmployee;
            _idEditedEmployee = 0;
            if (!_unitOfWork.Employees.GetAllNoTracking().ToList().Contains(employee))
            {
                employee.Id = _unitOfWork.Employees.GetUniqueId();
                _unitOfWork.Employees.Add(employee);
            }
            else
            {
                _unitOfWork.Employees.Update(employee);
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _unitOfWork.Employees.Remove(_unitOfWork.Employees.Get(id));
            _unitOfWork.Complete();
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
                DepartmentList = _unitOfWork.Departments.GetAll().ToList()
            };
        }

        public Employee EmployeeViewModelToEmployee(EmployeeViewModel employeeViewModel)
        {
            Employee employee = employeeViewModel.EmployeeForm;
            employee.Department = _unitOfWork.Departments.Find(d => d.Name == employeeViewModel.StringDepartment).ToList()[0];
            employee.DateOfHire = ParseDateTimeString(employeeViewModel.StringDateInput);
            return employee;
        }
    }
}
