using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Site
    {
        public Site()
        {
            GoalCustomers = new HashSet<GoalCustomer>();
            GoalProductionLines = new HashSet<GoalProductionLine>();
            GoalStfs = new HashSet<GoalStf>();
            ProductionLines = new HashSet<ProductionLine>();
            Projects = new HashSet<Project>();
            UserSites = new HashSet<UserSite>();
            Users = new HashSet<User>();
            Complaints = new HashSet<Complaint>();
            ComplaintWarrantyParts = new HashSet<ComplaintWarrantyPart>();
            CustomerViews = new HashSet<CustomerView>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<GoalCustomer> GoalCustomers { get; set; }
        public virtual ICollection<GoalProductionLine> GoalProductionLines { get; set; }
        public virtual ICollection<GoalStf> GoalStfs { get; set; }
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserSite> UserSites { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ProducedPart> ProducedParts { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<ComplaintWarrantyPart> ComplaintWarrantyParts { get; set; }
        public virtual ICollection<CustomerView> CustomerViews { get; set; }
    }
}
