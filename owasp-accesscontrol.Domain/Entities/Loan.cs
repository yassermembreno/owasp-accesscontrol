using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class Loan
    {
        public Loan()
        {
            PaymentSchedules = new HashSet<PaymentSchedule>();
            Payments = new HashSet<Payment>();
            Warranties = new HashSet<Warranty>();
        }

        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public string? Frequency { get; set; }
        public int? Terms { get; set; }
        public string LoanType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CustomerId { get; set; }
        public string? ScheduleMethod { get; set; }
        public decimal? Balance { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Warranty> Warranties { get; set; }
    }
}
