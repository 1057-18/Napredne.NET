using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentWebApp.Models
{
    public class PaymentController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPaymentService _paymentService;
        private readonly IDepartmentsService _departmentsService;
        private static int _idEditedPayment = 0;
        private static List<PaymentType> filterChecked;
        private static int filterMin;
        private static int filterMax;

        public PaymentController(IEmployeeService employeeService, IPaymentService paymentService, IDepartmentsService departmentsService)
        {
            _employeeService = employeeService;
            _paymentService = paymentService;
            _departmentsService = departmentsService;
        }

        public IActionResult Index(string sortOrder, int? pageNumber, int minAmo, int maxAmo, List<PaymentType> AreChecked, bool filter = false, bool state = false)
        {
            ViewData["FilterMinAmo"] = 0;
            ViewData["FilterMaxAmo"] = 30000;
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            List<Payment> list = _paymentService.GetAll().ToList();
            List<PaymentTypeViewModel> paymentTypeViewModels = GetPaymentTypeViewModels();
            if (filter)
            {
                filterChecked = AreChecked;
                filterMin = minAmo;
                filterMax = maxAmo;
            }
            if (state)
            {
                ViewData["FirstSliderValue"] = filterMin;
                ViewData["SecondSliderValue"] = filterMax;
                paymentTypeViewModels = FilterSelectedPaymentTypes(filterChecked, paymentTypeViewModels);
                list = FilterPayments(filterMin, filterMax, paymentTypeViewModels, list);
                if (filterChecked.Count == 0 || filterChecked.Count == Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList().Count)
                {
                    paymentTypeViewModels.ForEach(d => d.IsChecked = false);
                }
            }
            else
            {
                filterMin = 0;
                filterMax = 30000;
                ViewData["FirstSliderValue"] = filterMin;
                ViewData["SecondSliderValue"] = filterMax;
                filterChecked = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList();
            }
            switch (sortOrder)
            {
                case "Date":
                    list = list.OrderBy(p => p.DateAndTime).ToList();
                    break;
                case "date_desc":
                    list = list.OrderByDescending(p => p.DateAndTime).ToList();
                    break;
                case "Amount":
                    list = list.OrderBy(p => p.Amount).ToList();
                    break;
                case "amount_desc":
                    list = list.OrderByDescending(p => p.Amount).ToList();
                    break;
                default:
                    break;
            }
            ViewData["PaymentTypes"] = paymentTypeViewModels;
            return View(PaginatedList<PaymentViewModel>.Create(PrepareForView(list), pageNumber ?? 1, 9, sortOrder));
        }
        
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new PaymentViewModel() { HeaderString = "Add" }); ;
            }
            else
            {
                Payment payment = _paymentService.Get(id);
                _idEditedPayment = payment.Id;
                PaymentViewModel paymentViewModel = PaymentToPaymentViewModel(payment);
                paymentViewModel.HeaderString = "Edit";
                return View(paymentViewModel);
            }
        }

        public IActionResult Details(int id)
        {
            return View(PaymentToPaymentViewModel(_paymentService.Get(id)));
        }

        public IActionResult Save(PaymentViewModel paymentViewModel)
        {
            Payment payment = PaymentViewModelToPayment(paymentViewModel);
            payment.Id = _idEditedPayment;
            _idEditedPayment = 0;
            if (!_paymentService.GetAllWithOutTracking().ToList().Contains(payment))
            {
                payment.Id = _paymentService.GetUniqueId();
                _paymentService.Add(payment);
            }
            else
            {
                _paymentService.Update(payment);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _paymentService.Remove(_paymentService.Get(id));
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
                StringPaymentType = payment.PaymentType.GetDescription()
            };
        }

        private Payment PaymentViewModelToPayment(PaymentViewModel paymentViewModel)
        {
            Payment payment = paymentViewModel.PaymentForm;
            payment.Amount = ParseAmount(paymentViewModel.StringAmount);
            payment.DateAndTime = DateTime.Now;
            payment.PaymentType = DescriptionToEnum(paymentViewModel.StringPaymentType);
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

        private PaymentType DescriptionToEnum(string description)
        {
            return Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList().Find(p => p.GetDescription() == description);
        }

        private List<PaymentTypeViewModel> GetPaymentTypeViewModels()
        {
            List<PaymentTypeViewModel> result = new List<PaymentTypeViewModel>();
            foreach (PaymentType paymentType in Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList())
            {
                PaymentTypeViewModel paymentTypeViewModel = new PaymentTypeViewModel();
                paymentTypeViewModel.PaymentType = paymentType;
                paymentTypeViewModel.StringPaymentType = paymentType.GetDescription();
                result.Add(paymentTypeViewModel);
            }
            return result;
        }

        public List<Payment> FilterPayments(int minNum, int maxNum, List<PaymentTypeViewModel> paymentTypeViewModels, List<Payment> list)
        {
            paymentTypeViewModels = paymentTypeViewModels.Where(p => p.IsChecked == true).ToList();
            list = list.Where(e => paymentTypeViewModels.Any(p => p.PaymentType == e.PaymentType) && e.Amount >= minNum && e.Amount <= maxNum).ToList();
            return list;
        }

        public List<PaymentTypeViewModel> FilterSelectedPaymentTypes(List<PaymentType> AreChecked, List<PaymentTypeViewModel> paymentTypeViewModels)
        {
            if (AreChecked.Count == 0)
            {
                paymentTypeViewModels.ForEach(d => d.IsChecked = true);
            }
            else
            {
                foreach (PaymentType s in AreChecked)
                {
                    paymentTypeViewModels.Find(p => p.PaymentType == s).IsChecked = true;
                }
            }
            return paymentTypeViewModels;
        }
    }
}