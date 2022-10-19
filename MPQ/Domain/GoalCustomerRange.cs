using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GoalCustomerRange
    {
        public int Id { get; set; }
        public int GoalCustomerId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Color { get; set; }
        public int OperatorId { get; set; }

        public virtual GoalCustomer GoalCustomer { get; set; }
        public virtual ComparisonOperator Operator { get; set; }
    }
}
