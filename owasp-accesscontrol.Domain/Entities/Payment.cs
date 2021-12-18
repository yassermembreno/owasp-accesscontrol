using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            PaymentSchedules = new HashSet<PaymentSchedule>();
        }

        public int PaymentId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal CollectedPrincipal { get; set; }
        public decimal CollectedInterest { get; set; }
        public decimal BeginningPrincipal { get; set; }
        public decimal EndingPrincipal { get; set; }
        public decimal BeginningInterest { get; set; }
        public decimal EndingInterest { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; } = null!;
        public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; }
    }
}
