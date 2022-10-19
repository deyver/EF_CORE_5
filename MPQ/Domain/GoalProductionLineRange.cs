using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GoalProductionLineRange
    {
        public int Id { get; set; }
        public int GoalProductionLineId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Color { get; set; }
        public int OperatorId { get; set; }

        public virtual GoalProductionLine GoalProductionLine { get; set; }
        public virtual ComparisonOperator Operator { get; set; }
    }
}
