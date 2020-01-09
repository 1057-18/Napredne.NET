using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Payment payment)
        {
            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Complete();
        }

        public void AddRange(IEnumerable<Payment> payments)
        {
            _unitOfWork.Payments.AddRange(payments);
            _unitOfWork.Complete();
        }

        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> expression)
        {
            return _unitOfWork.Payments.Find(expression);
        }

        public Payment Get(int id)
        {
            return _unitOfWork.Payments.Get(id);
        }

        public IEnumerable<Payment> GetAll()
        {
            return _unitOfWork.Payments.GetAll();
        }

        public IEnumerable<Payment> GetAllWithOutTracking()
        {
            return _unitOfWork.Payments.GetAllNoTracking();
        }

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
                randomInt = random.Next(10000000, 99999999);
                if (!IdList.Contains(randomInt)) break;
            }
            return randomInt;
        }

        public void Remove(Payment payment)
        {
            _unitOfWork.Payments.Remove(payment);
            _unitOfWork.Complete();
        }

        public void RemoveRange(IEnumerable<Payment> payments)
        {
            _unitOfWork.Payments.RemoveRange(payments);
            _unitOfWork.Complete();
        }

        public void Update(Payment payment)
        {
            _unitOfWork.Payments.Update(payment);
            _unitOfWork.Complete();
        }
    }
}
