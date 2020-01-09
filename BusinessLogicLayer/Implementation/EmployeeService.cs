using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Employee employee)
        {
            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();
        }

        public void AddRange(IEnumerable<Employee> employees)
        {
            _unitOfWork.Employees.AddRange(employees);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>> expression)
        {
            return _unitOfWork.Employees.Find(expression);
        }

        public Employee Get(int id)
        {
            return _unitOfWork.Employees.Get(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _unitOfWork.Employees.GetAll();
        }

        public IEnumerable<Employee> GetAllWithOutTracking()
        {
            return _unitOfWork.Employees.GetAllNoTracking();
        }

        public List<int> GetIdList()
        {
            return GetAll().Select(e => e.Id).ToList();
        }

        public int GetUniqueId()
        {
            List<int> IdList = GetIdList();
            Random random = new Random();
            int randomInt;
            while (true)
            {
                randomInt = random.Next(100000, 999999);
                if (!IdList.Contains(randomInt)) break;
            }
            return randomInt;
        }

        public void Remove(Employee employee)
        {
            _unitOfWork.Employees.Remove(employee);
            _unitOfWork.Complete();
        }

        public void RemoveRange(IEnumerable<Employee> employees)
        {
            _unitOfWork.Employees.RemoveRange(employees);
            _unitOfWork.Complete();
        }

        public void Update(Employee employee)
        {
            _unitOfWork.Employees.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
