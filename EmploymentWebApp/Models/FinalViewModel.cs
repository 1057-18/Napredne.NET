using System;
using System.Collections.Generic;

namespace EmploymentWebApp.Models
{
    public class FinalViewModel
    {
        public PaginatedList<EmployeeViewModel> PaginatedList { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
    }
}
