using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class Warranty
    {
        public int WarrantyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Documents { get; set; }
        public int? ExecutionTerms { get; set; }
        public string? Frequency { get; set; }
        public float? PenaltyRate { get; set; }
        public DateTime? Dofd { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? WarrantyStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; } = null!;
    }
}
