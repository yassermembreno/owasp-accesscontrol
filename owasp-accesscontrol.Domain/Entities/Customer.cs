using System;
using System.Collections.Generic;

namespace owasp_accesscontrol.Domain.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Loans = new HashSet<Loan>();
        }

        public int CustomerId { get; set; }
        public string Names { get; set; } = null!;
        public string Lastnames { get; set; } = null!;
        public string? Dni { get; set; }
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
