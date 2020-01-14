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

        // injectujemo conn string iz app settings, napraviti konstruktor naseg contexta
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
            modelBuilder.Entity<Payment>().Property(p => p.PaymentType).HasConversion(c => c.ToString(), c => Enum.Parse<PaymentType>(c));
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p  => p.EmployeeId);
        }
    }
}
