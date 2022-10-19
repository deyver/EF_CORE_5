using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Indicator
    {
        public Indicator()
        {
            GoalStfs = new HashSet<GoalStf>();
            Complaints = new HashSet<Complaint>();
            CustomerViews = new HashSet<CustomerView>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GoalStf> GoalStfs { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<CustomerView> CustomerViews { get; set; }
    }
}
