using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EmploymentContextDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<EmploymentContext>
    {
        protected override EmploymentContext CreateNewInstance(DbContextOptions<EmploymentContext> options)
        {
            return new EmploymentContext(options);
        }
    }
}
