using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataAccessLayer.Models;

namespace EmploymentWebApp.Models
{
    public class PaymentViewModel
    {
        public Payment PaymentForm { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^([1-9]{1}[0-9]{0,2})(\.\d{3}){0,1}(\,\d{2})$|^(0)(\,\d{2})$", ErrorMessage = " (123.456,78)")]
        public string StringAmount { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^(0[1-9]|[12]\d|3[01])[-](Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\d{2}$", ErrorMessage = " (dd-Mmm-yyyy)")]
        public string StringDate { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^(0[1-9]|[12]\d|3[01])[-](0[1-9]|1[0-2])[-](19|20)\d{2}$", ErrorMessage = " (dd-mm-yyyy).")]
        public string StringDateInput { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):[0-5][0-9]:([0-5][0-9])$", ErrorMessage = " (hh-mm-ss)")]
        public string StringTime { get; set; }
        [Required(ErrorMessage = " must be selected.")]
        public string StringPaymentType { get; set; }
        public string HeaderString { get; set; }
        public List<string> PaymentTypes { get; set; } = Enum.GetNames(typeof(PaymentType)).ToList();

        public PaymentViewModel()
        {
            
        }
    }
}
