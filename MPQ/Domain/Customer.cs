using System;
using System.Collections.Generic;

#nullable disable

namespace MPQ.Domain
{
    public partial class Customer
    {
        public Customer()
        {
            GoalCustomers = new HashSet<GoalCustomer>();
            GoalProductionLines = new HashSet<GoalProductionLine>();
            ProductionLines = new HashSet<ProductionLine>();
            Projects = new HashSet<Project>();
            Complaints = new HashSet<Complaint>();
            ComplaintWarrantyParts = new HashSet<ComplaintWarrantyPart>();
            CustomerViews = new HashSet<CustomerView>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GoalCustomer> GoalCustomers { get; set; }
        public virtual ICollection<GoalProductionLine> GoalProductionLines { get; set; }
        public virtual ICollection<ProductionLine> ProductionLines { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<ComplaintWarrantyPart> ComplaintWarrantyParts { get; set; }
        public virtual ICollection<CustomerView> CustomerViews { get; set; }
    }
}
