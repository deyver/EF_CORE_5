using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class User
    {
        public User()
        {
            UserGroups = new HashSet<UserGroup>();
            UserSites = new HashSet<UserSite>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int DefaultSiteId { get; set; }
        public string DefaultLanguage { get; set; }

        public virtual Site DefaultSite { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserSite> UserSites { get; set; }
    }
}
