using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        List<int> GetIdList();
        int GetUniqueId();
    }
}
