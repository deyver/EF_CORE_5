using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Project
    {
        public Project()
        {
            GoalProductionLines = new HashSet<GoalProductionLine>();
            ProductionLines = new HashSet<ProductionLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public int SiteId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<GoalProductionLine> GoalProductionLines { get; set; }
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
    }
}
