using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentWebApp.Models
{
    public class PaymentController : Controller
    {
        IUnitOfWork _unitOfWork;
        private static int _idEditedPayment = 0;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(PrepareForView(_unitOfWork.Payments.GetAll().ToList()));
        }
        
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new PaymentViewModel() { HeaderString = "Add" }); ;
            }
            else
            {
                Payment payment = _unitOfWork.Payments.Get(id);
                _idEditedPayment = payment.Id;
                PaymentViewModel paymentViewModel = PaymentToPaymentViewModel(payment);
                paymentViewModel.HeaderString = "Edit";
                return View(paymentViewModel);
            }
        }

        public IActionResult Details(int id)
        {
            return View(PaymentToPaymentViewModel(_unitOfWork.Payments.Get(id)));
        }

        public IActionResult Save(PaymentViewModel paymentViewModel)
        {
            Payment payment = PaymentViewModelToPayment(paymentViewModel);
            payment.Id = _idEditedPayment;
            _idEditedPayment = 0;
            if (!_unitOfWork.Payments.GetAllNoTracking().ToList().Contains(payment))
            {
                payment.Id = _unitOfWork.Payments.GetUniqueId();
                _unitOfWork.Payments.Add(payment);
            }
            else
            {
                _unitOfWork.Payments.Update(payment);
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _unitOfWork.Payments.Remove(_unitOfWork.Payments.Get(id));
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        private List<PaymentViewModel> PrepareForView(List<Payment> list)
        {
            return list.ConvertAll(p => PaymentToPaymentViewModel(p));
        }

        private PaymentViewModel PaymentToPaymentViewModel(Payment payment)
        {
            DateTimeToString(payment.DateAndTime, out string date, out string time);
            return new PaymentViewModel()
            {
                PaymentForm = payment,
                StringAmount = AmountToString(payment.Amount),
                StringDate = date,
                StringTime = time,
                StringPaymentType = payment.PaymentType.ToString()
            };
        }

        private Payment PaymentViewModelToPayment(PaymentViewModel paymentViewModel)
        {
            Payment payment = paymentViewModel.PaymentForm;
            payment.Amount = ParseAmount(paymentViewModel.StringAmount);
            payment.DateAndTime = DateTime.Now;
            payment.PaymentType = Enum.Parse<PaymentType>(paymentViewModel.StringPaymentType);
            return payment;
        }

        private DateTime ParseDateAndTime(string date, string time)
        {
            string dateTimeString = String.Join(' ', date, time);
            DateTime dateTime = DateTime.ParseExact(dateTimeString, "dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
            return dateTime;
        }

        private void DateTimeToString(DateTime dateTime, out string date, out string time)
        {
            List<string> dateTimeString = dateTime.ToString("dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).Split(' ').ToList();
            date = dateTimeString[0];
            time = dateTimeString[1];
        }

        private double ParseAmount(string amount)
        {
            return Double.Parse(amount, CultureInfo.CreateSpecificCulture("es-ES"));
        }

        private string AmountToString(double amount)
        {
            return amount.ToString("N2", CultureInfo.CreateSpecificCulture("es-ES"));
        }
    }
}