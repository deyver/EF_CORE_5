using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class ProductionLine
    {
        public ProductionLine()
        {
            GoalProductionLines = new HashSet<GoalProductionLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public int ProductionAreaId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProductionArea ProductionArea { get; set; }
        public virtual Project Project { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<GoalProductionLine> GoalProductionLines { get; set; }
    }
}
