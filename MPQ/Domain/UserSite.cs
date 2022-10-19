using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class UserSite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SiteId { get; set; }

        public virtual Site Site { get; set; }
        public virtual User User { get; set; }
    }
}
