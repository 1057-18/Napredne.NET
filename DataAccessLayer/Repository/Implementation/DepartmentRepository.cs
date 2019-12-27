using System;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext context) : base(context)
        {
        }

        public EmploymentContext context { get { return Context as EmploymentContext; } }

        public int GetDepartmentId(string name)
        {
            return Find(d => d.Name == name).ToList()[0].Id;
        }

        public string GetDepartmentName(int id)
        {
            return Find(d => d.Id == id).ToList()[0].Name;
        }
    }
}
