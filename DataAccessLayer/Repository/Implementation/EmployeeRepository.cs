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
    }
}
