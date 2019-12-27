using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.Models
{
    public class Employee : IEquatable<Employee>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " is rerquired.")]
        [StringLength(20, ErrorMessage = " (between 2 and 20 char)", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = " is required.")]
        [StringLength(20, ErrorMessage = " (between 2 and 30 char)", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = " is required.")]
        [StringLength(25, ErrorMessage = " (between 3 and 25 char)", MinimumLength = 3)]
        public string JobTitle { get; set; }
        public DateTime DateOfHire { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Employee);
        }

        public bool Equals([AllowNull] Employee other)
        {
            return other != null &&
                   Id == other.Id;
        }
    }
}
