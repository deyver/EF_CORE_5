using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class ComparisonOperator
    {
        public ComparisonOperator()
        {
            GoalCustomerRanges = new HashSet<GoalCustomerRange>();
            GoalProductionLineRanges = new HashSet<GoalProductionLineRange>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GoalCustomerRange> GoalCustomerRanges { get; set; }
        public virtual ICollection<GoalProductionLineRange> GoalProductionLineRanges { get; set; }
    }
}
