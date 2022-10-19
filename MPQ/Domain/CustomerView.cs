using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class CustomerView
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int IndicatorId { get; set; }
        public int CustomerId { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public decimal Result { get; set; }
        public int BusinessUnitId { get; set; }

        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Indicator Indicator { get; set; }
        public virtual Site Site { get; set; }
    }
}
