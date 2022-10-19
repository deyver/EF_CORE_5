using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GoalProductionLine
    {
        public GoalProductionLine()
        {
            GoalProductionLineRanges = new HashSet<GoalProductionLineRange>();
        }

        public int Id { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public int? ProductionLineId { get; set; }
        public int StfNumber { get; set; }
        public decimal Target { get; set; }
        public int ProductionAreaId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProductionArea ProductionArea { get; set; }
        public virtual ProductionLine ProductionLine { get; set; }
        public virtual Project Project { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<GoalProductionLineRange> GoalProductionLineRanges { get; set; }
    }
}
