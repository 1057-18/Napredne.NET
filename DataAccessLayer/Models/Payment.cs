using System;
namespace DataAccessLayer.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
