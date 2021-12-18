using System;
using Microsoft.EntityFrameworkCore;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_accesscontrol.Domain.Interfaces
{
    public interface IDBLoanManagerContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warranty> Warranties { get; set; }

        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

