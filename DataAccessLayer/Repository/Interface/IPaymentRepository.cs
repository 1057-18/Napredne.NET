using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.Interface
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        List<int> GetIdList();
        int GetUniqueId();
    }
}
