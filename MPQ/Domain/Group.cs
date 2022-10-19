using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Group
    {
        public Group()
        {
            GroupMenus = new HashSet<GroupMenu>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GroupMenu> GroupMenus { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
