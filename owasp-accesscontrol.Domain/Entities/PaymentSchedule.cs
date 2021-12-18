using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class PaymentSchedule
    {
        public int PaymentScheduleId { get; set; }
        public string Type { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? PrincipalPaid { get; set; }
        public decimal? InterestPaid { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DatePaid { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int LoanId { get; set; }
        public int? PaymentId { get; set; }

        public virtual Loan Loan { get; set; } = null!;
        public virtual Payment? Payment { get; set; }
    }
}
