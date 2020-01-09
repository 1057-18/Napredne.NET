using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interface
{
    public interface IPaymentService : IService<Payment>
    {
        List<int> GetIdList();
        int GetUniqueId();
    }
}
