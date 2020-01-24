using System;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Implementation;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmploymentContext _context;

        public UnitOfWork(EmploymentContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Payments = new PaymentRepository(_context);
            Departments = new DepartmentRepository(_context);
            Credentials = new CredentialRepository(_context);
        }

        public IEmployeeRepository Employees { get; private set; }

        public IPaymentRepository Payments { get; private set; }

        public IDepartmentRepository Departments { get; private set; }

        public ICredentialRepository Credentials { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
