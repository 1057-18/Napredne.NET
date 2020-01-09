using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Implementation
{
    public class DepartmentService : IDepartmentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Department department)
        {
            _unitOfWork.Departments.Add(department);
            _unitOfWork.Complete();
        }

        public void AddRange(IEnumerable<Department> departments)
        {
            _unitOfWork.Departments.AddRange(departments);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> Find(Expression<Func<Department, bool>> expression)
        {
            return _unitOfWork.Departments.Find(expression);
        }

        public Department Get(int id)
        {
            return _unitOfWork.Departments.Get(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _unitOfWork.Departments.GetAll();
        }

        public IEnumerable<Department> GetAllWithOutTracking()
        {
            return _unitOfWork.Departments.GetAllNoTracking();
        }

        public void Remove(Department department)
        {
            _unitOfWork.Departments.Remove(department);
            _unitOfWork.Complete();
        }

        public void RemoveRange(IEnumerable<Department> departments)
        {
            _unitOfWork.Departments.RemoveRange(departments);
            _unitOfWork.Complete();
        }

        public void Update(Department department)
        {
            _unitOfWork.Departments.Update(department);
            _unitOfWork.Complete();
        }
    }
}
