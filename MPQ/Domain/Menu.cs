using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Menu
    {
        public Menu()
        {
            GroupMenus = new HashSet<GroupMenu>();
        }

        public int Id { get; set; }
        public int Level { get; set; }
        public int Sequence { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }

        public virtual ICollection<GroupMenu> GroupMenus { get; set; }
    }
}
