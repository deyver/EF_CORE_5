using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPQ.Domain;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MPQ.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationContext(
            DbContextOptions<ApplicationContext> options,
            IConfiguration configuration) : base(options) {
            this._configuration = configuration;
        }

        public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ComparisonOperator> ComparisonOperators { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ComplaintWarrantyPart> ComplaintWarrantyParts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerView> CustomerViews { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalCustomer> GoalCustomers { get; set; }
        public virtual DbSet<GoalCustomerRange> GoalCustomerRanges { get; set; }
        public virtual DbSet<GoalProductionLine> GoalProductionLines { get; set; }
        public virtual DbSet<GoalProductionLineRange> GoalProductionLineRanges { get; set; }
        public virtual DbSet<GoalStf> GoalStfs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMenu> GroupMenus { get; set; }
        public virtual DbSet<Indicator> Indicators { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<ProducedPart> ProducedParts { get; set; }
        public virtual DbSet<ProductionArea> ProductionAreas { get; set; }
        public virtual DbSet<ProductionLine> ProductionLines { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserSite> UserSites { get; set; }

        private readonly StreamWriter _logStream = new StreamWriter("Logs/Database_Log.txt", append: true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                string strConnection = _configuration.GetConnectionString("DefaultConnection");
                optionBuilder
                    .UseSqlServer(strConnection);
            }

            optionBuilder
                //.EnableDetailedErrors()
                //.EnableSensitiveDataLogging()
                .LogTo(_logStream.WriteLine, LogLevel.Information);
        }

        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BusinessUnit>(entity =>
            {
                entity.ToTable("BusinessUnit");

                entity.HasIndex(e => e.Name, "UK-BusinessUnit")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.HasIndex(e => e.Name, "UK-CompanyName")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ComparisonOperator>(entity =>
            {
                entity.ToTable("ComparisonOperator");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.ToTable("Complaint");

                entity.Property(e => e.ActionDate).HasColumnType("date");

                entity.Property(e => e.ActionResponsible)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ComplaintDate).HasColumnType("date");

                entity.Property(e => e.ContainmentAction)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.GqrsNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Issue)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SummarySent)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaint_BusinessUnit");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaint_Customer");

                entity.HasOne(d => d.Indicator)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.IndicatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaint_Indicator");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Complaints)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Complaint_Site");
            });

            modelBuilder.Entity<ComplaintWarrantyPart>(entity =>
            {
                entity.ToTable("ComplaintWarrantyPart");

                entity.Property(e => e.Issue)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.ComplaintWarrantyParts)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplaintWarrantyPart_BusinessUnit");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ComplaintWarrantyParts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplaintWarrantyPart_Customer");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.ComplaintWarrantyParts)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplaintWarrantyPart_Site");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Name, "UK-Customer")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerView>(entity =>
            {
                entity.ToTable("CustomerView");

                entity.Property(e => e.Result).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.CustomerViews)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerView_BusinessUnit");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerViews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerView_Customer");

                entity.HasOne(d => d.Indicator)
                    .WithMany(p => p.CustomerViews)
                    .HasForeignKey(d => d.IndicatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerView_Indicator");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.CustomerViews)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerView_Site");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("Goal");

                entity.HasIndex(e => e.Name, "UK-Goal")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GoalCustomer>(entity =>
            {
                entity.ToTable("GoalCustomer");

                entity.HasIndex(e => e.Name, "UK-GoalCustomer")
                    .IsUnique();

                entity.HasIndex(e => new { e.SiteId, e.CustomerId, e.StfNumber, e.Name }, "UK-GoalCustomerStf")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitOfMeasurement)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.GoalCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalCustomer_Customer");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.GoalCustomers)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalCustomer_Site");
            });

            modelBuilder.Entity<GoalCustomerRange>(entity =>
            {
                entity.ToTable("GoalCustomerRange");

                entity.Property(e => e.Color)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.GoalCustomer)
                    .WithMany(p => p.GoalCustomerRanges)
                    .HasForeignKey(d => d.GoalCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalCustomerRange_GoalCustomer");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.GoalCustomerRanges)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalCustomerRange_ComparisonOperator");
            });

            modelBuilder.Entity<GoalProductionLine>(entity =>
            {
                entity.ToTable("GoalProductionLine");

                entity.Property(e => e.Target).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.GoalProductionLines)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalProductionLine_Customer");

                entity.HasOne(d => d.ProductionArea)
                    .WithMany(p => p.GoalProductionLines)
                    .HasForeignKey(d => d.ProductionAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalProductionLine_ProductionArea");

                entity.HasOne(d => d.ProductionLine)
                    .WithMany(p => p.GoalProductionLines)
                    .HasForeignKey(d => d.ProductionLineId)
                    .HasConstraintName("FK_GoalProductionLine_ProductionLine");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.GoalProductionLines)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_GoalProductionLine_Project");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.GoalProductionLines)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalProductionLine_Site");
            });

            modelBuilder.Entity<GoalProductionLineRange>(entity =>
            {
                entity.ToTable("GoalProductionLineRange");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.GoalProductionLine)
                    .WithMany(p => p.GoalProductionLineRanges)
                    .HasForeignKey(d => d.GoalProductionLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalProductionLineRange_GoalProductionLine");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.GoalProductionLineRanges)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalProductionLineRange_ComparisonOperator");
            });

            modelBuilder.Entity<GoalStf>(entity =>
            {
                entity.ToTable("GoalStf");

                entity.HasIndex(e => new { e.SiteId, e.IndicatorId, e.GoalId, e.StfNumber, e.BusinessUnitId }, "UK-GoalStf")
                    .IsUnique();

                entity.Property(e => e.AprTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AugTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DecTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FebTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GeneralTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.JanTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.JulTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.JunTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MarTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MayTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NovTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OctTarget).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SepTarget).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.GoalStfs)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalStf_BusinessUnit");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.GoalStfs)
                    .HasForeignKey(d => d.GoalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalStf_Goal");

                entity.HasOne(d => d.Indicator)
                    .WithMany(p => p.GoalStfs)
                    .HasForeignKey(d => d.IndicatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalStf_Indicator");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.GoalStfs)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoalStf_Site");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.Name, "UK-GroupName")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupMenu>(entity =>
            {
                entity.ToTable("GroupMenu");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMenus)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMenu_Group");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.GroupMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMenu_Menu");
            });

            modelBuilder.Entity<Indicator>(entity =>
            {
                entity.ToTable("Indicator");

                entity.HasIndex(e => e.Name, "UK-IndicatorName")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasIndex(e => e.Name, "UK-MenuName")
                    .IsUnique();

                entity.Property(e => e.IconUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductionArea>(entity =>
            {
                entity.ToTable("ProductionArea");

                entity.HasIndex(e => e.Name, "UK-ProductionArea")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductionLine>(entity =>
            {
                entity.ToTable("ProductionLine");

                entity.HasIndex(e => e.Name, "UK-ProductionLine")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProductionLines)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionLine_Customer");

                entity.HasOne(d => d.ProductionArea)
                    .WithMany(p => p.ProductionLines)
                    .HasForeignKey(d => d.ProductionAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionLine_ProductionArea");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProductionLines)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProductionLine_Project");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.ProductionLines)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductionLine_Site");
            });

            modelBuilder.Entity<ProducedPart>(entity =>
            {
                entity.ToTable("ProducedPart");

                entity.Property(e => e.SiteId).HasColumnType("int");
                entity.Property(e => e.ProductionAreaId).HasColumnType("int");
                entity.Property(e => e.Year).HasColumnType("int");
                entity.Property(e => e.WeekNumber).HasColumnType("int");
                entity.Property(e => e.Quantity).HasColumnType("int");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.ProducedParts)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProducedPart_Site");

                entity.HasOne(d => d.ProductionArea)
                    .WithMany(p => p.ProducedParts)
                    .HasForeignKey(d => d.ProductionAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProducedPart_ProductionArea");

                
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.Name, "UK-Project")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Customer");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Site");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("Site");

                entity.HasIndex(e => e.Name, "UK-SiteName")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Site_Company");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Login, "UK-UserLogin")
                    .IsUnique();

                entity.Property(e => e.DefaultLanguage)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.DefaultSite)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DefaultSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Site");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("UserGroup");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGroup_User");
            });

            modelBuilder.Entity<UserSite>(entity =>
            {
                entity.ToTable("UserSite");

                entity.HasIndex(e => new { e.UserId, e.SiteId }, "UK-UserSiteId")
                    .IsUnique();

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.UserSites)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSite_Site");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSite_User");
            });

        }

    }
}