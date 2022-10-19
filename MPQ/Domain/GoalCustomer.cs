using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GoalCustomer
    {
        public GoalCustomer()
        {
            GoalCustomerRanges = new HashSet<GoalCustomerRange>();
        }

        public int Id { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int StfNumber { get; set; }
        public string Name { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<GoalCustomerRange> GoalCustomerRanges { get; set; }
    }
}
