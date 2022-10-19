using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Domain
{
    public partial class ProducedPart
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int ProductionAreaId { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public int Quantity { get; set; }

        public virtual Site Site { get; set; }
        public virtual ProductionArea ProductionArea { get; set; }
    }
}
