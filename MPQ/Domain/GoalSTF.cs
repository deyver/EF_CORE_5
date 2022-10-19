using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GoalStf
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int IndicatorId { get; set; }
        public int GoalId { get; set; }
        public int StfNumber { get; set; }
        public decimal GeneralTarget { get; set; }
        public decimal JulTarget { get; set; }
        public decimal AugTarget { get; set; }
        public decimal SepTarget { get; set; }
        public decimal OctTarget { get; set; }
        public decimal NovTarget { get; set; }
        public decimal DecTarget { get; set; }
        public decimal JanTarget { get; set; }
        public decimal FebTarget { get; set; }
        public decimal MarTarget { get; set; }
        public decimal AprTarget { get; set; }
        public decimal MayTarget { get; set; }
        public decimal JunTarget { get; set; }
        public int BusinessUnitId { get; set; }

        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual Goal Goal { get; set; }
        public virtual Indicator Indicator { get; set; }
        public virtual Site Site { get; set; }
    }
}
