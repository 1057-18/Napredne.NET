using System;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        string GetDepartmentName(int id);
        int GetDepartmentId(string name);
    }
}
