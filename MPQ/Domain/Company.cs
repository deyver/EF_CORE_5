using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Company
    {
        public Company()
        {
            Sites = new HashSet<Site>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}
