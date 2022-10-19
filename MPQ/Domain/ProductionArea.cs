using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class ProductionArea
    {
        public ProductionArea()
        {
            GoalProductionLines = new HashSet<GoalProductionLine>();
            ProductionLines = new HashSet<ProductionLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GoalProductionLine> GoalProductionLines { get; set; }
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
        public virtual ICollection<ProducedPart> ProducedParts { get; set; }
    }
}
