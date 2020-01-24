using System;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IPaymentRepository Payments { get; }
        IDepartmentRepository Departments { get; }
        ICredentialRepository Credentials { get;  }

        int Complete(); 
    }
}
