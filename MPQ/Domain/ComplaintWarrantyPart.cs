using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class ComplaintWarrantyPart
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int PartQuantity { get; set; }
        public string Issue { get; set; }
        public bool? Legitimate { get; set; }
        public string Status { get; set; }
        public int BusinessUnitId { get; set; }

        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Site Site { get; set; }
    }
}
