using System;
using DataAccessLayer.Models;

namespace EmploymentWebApp.Models
{
    public class PaymentTypeViewModel
    {
        public PaymentType PaymentType { get; set; }
        public string StringPaymentType { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
