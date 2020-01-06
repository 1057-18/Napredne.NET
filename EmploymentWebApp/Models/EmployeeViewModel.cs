using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace EmploymentWebApp.Models
{
    public class EmployeeViewModel
    {
        public Employee EmployeeForm { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^(0[1-9]|[12]\d|3[01])[-](Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[-](19|20)\d{2}$", ErrorMessage = " (dd-Mmm-yyyy).")]
        public string StringDateOfHire { get; set; }
        [Required(ErrorMessage = " is required.")]
        [RegularExpression(@"^(0[1-9]|[12]\d|3[01])[-](0[1-9]|1[0-2])[-](19|20)\d{2}$", ErrorMessage = " (dd-mm-yyyy).")]
        public string StringDateInput { get; set; }
        [Required(ErrorMessage = " must be selected.")]
        public string StringDepartment { get; set; }
        public string HeaderString { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}
