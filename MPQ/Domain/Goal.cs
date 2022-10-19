using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Goal
    {
        public Goal()
        {
            GoalStfs = new HashSet<GoalStf>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GoalStf> GoalStfs { get; set; }
    }
}
