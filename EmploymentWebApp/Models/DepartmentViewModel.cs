using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace EmploymentWebApp.Models
{
    public class DepartmentViewModel
    {
        public Department Department { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
