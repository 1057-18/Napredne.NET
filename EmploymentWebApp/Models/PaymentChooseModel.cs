using System;
using System.ComponentModel.DataAnnotations;

namespace EmploymentWebApp.Models
{
    public class PaymentChooseModel
    {
        [Required(ErrorMessage = " is required.")]
        public int? Id { get; set; }
        public string Helper { get; set; }
    }
}
