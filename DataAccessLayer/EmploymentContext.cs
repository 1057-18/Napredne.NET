using System;
using System.Linq;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class EmploymentContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // kada zelimo da injectujemo conn string iz app settings potrebno je napraviti konstruktor naseg contexta
        // koji ce da prima te opcije i salje konstruktoru DbContext klase
        public EmploymentContext(DbContextOptions<EmploymentContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasKey(d => d.Id);

            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.DateOfHire).HasColumnType("date");
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Payment>().HasKey(p => p.Id);
            modelBuilder.Entity<Payment>().Property(p => p.PaymentType).HasConversion<string>();
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p  => p.EmployeeId);
        }

        public int Save(Employee employee)
        {
            Employees.Add(employee);
            return SaveChanges();
        }

        public int Delete(Employee employee)
        {
            Employees.Remove(employee);
            return SaveChanges();
        }

        public IQueryable<Employee> Get(Employee employee)
        {
            IQueryable<Employee> Result = Employees;
            if (employee.Id > 0) Result = Result.Where<Employee>(e => e.Id == employee.Id);
            if (employee.FirstName != null) Result = Result.Where<Employee>(e => e.FirstName == employee.FirstName);
            if (employee.LastName != null) Result = Result.Where<Employee>(e => e.LastName == employee.FirstName);
            if (employee.DepartmentId > 0) Result = Result.Where<Employee>(e => e.DepartmentId == employee.DepartmentId);
            return Result;
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return Employees;
        }
    }
}
