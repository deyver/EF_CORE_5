using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class GroupMenu
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int MenuId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
