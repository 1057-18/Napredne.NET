using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository.Implementation
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmploymentContext context) : base(context)
        {
        }

        public EmploymentContext context { get { return Context as EmploymentContext; } }
    }
}
