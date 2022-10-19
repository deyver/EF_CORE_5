using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Complaint
    {
        public int Id { get; set; }
        public int IndicatorId { get; set; }
        public DateTime ComplaintDate { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string Issue { get; set; }
        public string ContainmentAction { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionResponsible { get; set; }
        public string GqrsNumber { get; set; }
        public string SummarySent { get; set; }
        public int SiteId { get; set; }
        public int BusinessUnitId { get; set; }

        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Indicator Indicator { get; set; }
        public virtual Site Site { get; set; }
    }
}
