using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using owasp_accesscontrol.Domain.Interfaces;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class dbloanmanagerContext : DbContext , IDBLoanManagerContext
    {
        public dbloanmanagerContext()
        {
        }

        public dbloanmanagerContext(DbContextOptions<dbloanmanagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentSchedule> PaymentSchedules { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Warranty> Warranties { get; set; } = null!;

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dni)
                    .HasMaxLength(16)
                    .HasColumnName("DNI");

                entity.Property(e => e.Lastnames).HasMaxLength(45);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Names).HasMaxLength(45);

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.HasIndex(e => e.CustomerId, "fk_Loan_Customer_idx");

                entity.Property(e => e.Amount).HasPrecision(9, 2);

                entity.Property(e => e.Balance).HasPrecision(9, 2);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Currency).HasColumnType("enum('Dolar','Euro','Cordobas')");

                entity.Property(e => e.Frequency).HasColumnType("enum('Once','Weekly','BiWeekly','Monthly')");

                entity.Property(e => e.InterestRate).HasPrecision(9, 2);

                entity.Property(e => e.LoanDate).HasColumnType("datetime");

                entity.Property(e => e.LoanType).HasColumnType("enum('Regular','NoCalendar')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ScheduleMethod).HasColumnType("enum('Straight Line','Declining balance')");

                entity.Property(e => e.Status).HasColumnType("enum('PastDue','PaidInFull','WriteOff','UpToDay','PayOff')");

                entity.Property(e => e.Terms).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Loan_Customer");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.HasIndex(e => e.LoanId, "fk_Payment_Loan1_idx");

                entity.Property(e => e.AmountReceived).HasPrecision(9, 2);

                entity.Property(e => e.BeginningInterest).HasPrecision(9, 2);

                entity.Property(e => e.BeginningPrincipal).HasPrecision(9, 2);

                entity.Property(e => e.CollectedInterest).HasPrecision(9, 2);

                entity.Property(e => e.CollectedPrincipal).HasPrecision(9, 2);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EndingInterest).HasPrecision(9, 2);

                entity.Property(e => e.EndingPrincipal).HasPrecision(9, 2);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Type).HasColumnType("enum('Book','Regular','PayOff','WriteOff','Principal Only')");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Payment_Loan1");
            });

            modelBuilder.Entity<PaymentSchedule>(entity =>
            {
                entity.ToTable("PaymentSchedule");

                entity.HasIndex(e => e.LoanId, "fk_PaymentSchedule_Loan1_idx");

                entity.HasIndex(e => e.PaymentId, "fk_PaymentSchedule_Payment1_idx");

                entity.Property(e => e.Amount).HasPrecision(9, 2);

                entity.Property(e => e.AmountPaid).HasPrecision(9, 2);

                entity.Property(e => e.Createdon).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DatePaid).HasColumnType("datetime");

                entity.Property(e => e.Interest).HasPrecision(9, 2);

                entity.Property(e => e.InterestPaid).HasPrecision(9, 2);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Principal).HasPrecision(9, 2);

                entity.Property(e => e.PrincipalPaid).HasPrecision(9, 2);

                entity.Property(e => e.Status).HasColumnType("enum('Pending','Paid','Deleted')");

                entity.Property(e => e.Type).HasColumnType("enum('Regular')");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.PaymentSchedules)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PaymentSchedule_Loan1");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PaymentSchedules)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("fk_PaymentSchedule_Payment1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.UserName).HasMaxLength(45);
            });

            modelBuilder.Entity<Warranty>(entity =>
            {
                entity.ToTable("Warranty");

                entity.HasIndex(e => e.LoanId, "fk_Warranty_Loan1_idx");

                entity.Property(e => e.WarrantyId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(45);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Documents).HasMaxLength(250);

                entity.Property(e => e.Dofd)
                    .HasColumnType("datetime")
                    .HasColumnName("DOFD");

                entity.Property(e => e.ExecutionDate).HasColumnType("datetime");

                entity.Property(e => e.Frequency).HasColumnType("enum('Weekly','BiWeekly','Monthly')");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.WarrantyStatus).HasColumnType("enum('guaranteed','Executed','Execution Process')");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.Warranties)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Warranty_Loan1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
