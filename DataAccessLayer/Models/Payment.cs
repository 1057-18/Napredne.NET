using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.Models
{
    public class Payment : IEquatable<Payment>
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateAndTime { get; set; }
        public PaymentType PaymentType { get; set; }
        [StringLength(120, ErrorMessage = " (max 150 char)")]
        public string Note { get; set; }
        [Required(ErrorMessage = " is required.")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public bool Equals([AllowNull] Payment other)
        {
            return other != null && Id == other.Id;
        }
    }
}