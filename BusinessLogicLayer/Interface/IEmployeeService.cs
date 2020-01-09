using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interface
{
    public interface IEmployeeService : IService<Employee>
    {
        List<int> GetIdList();
        int GetUniqueId();
    }
}
