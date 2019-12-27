using System;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository.Implementation
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(EmploymentContext context) : base(context)
        {
        }

        public EmploymentContext context { get { return Context as EmploymentContext; } }
    }
}
