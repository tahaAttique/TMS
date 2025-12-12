using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using TMS.TicketCategories;
using TMS.Tickets;
using TMS.Comments;
using System.Reflection.Emit;

namespace TMS.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TMSDbContext :
    AbpDbContext<TMSDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<TicketCategory> TicketCategories { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Comment> Comments { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public TMSDbContext(DbContextOptions<TMSDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        builder.Entity<TicketCategory>(b =>
        {
            b.ToTable(TMSConsts.DbTablePrefix + "TicketCategories",
                TMSConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TicketCategoryConsts.MaxNameLength);

            b.HasIndex(x => x.Name);

            b.HasMany(x => x.Tickets).WithOne(x => x.TicketCategory).HasForeignKey(x => x.TicketCategoryId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Ticket>(b => {
            b.ToTable(TMSConsts.DbTablePrefix + "Tickets",
                 TMSConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(TicketConsts.MaxTitleLength);

            b.HasIndex(x => x.Title);

            b.HasOne(x => x.TicketCategory).WithMany(x => x.Tickets).HasForeignKey(x => x.TicketCategoryId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Comments).WithOne(x => x.Ticket).HasForeignKey(x => x.TicketId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Comment>(b => {
            b.ToTable(TMSConsts.DbTablePrefix + "Comments",
                 TMSConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Detail)
            .IsRequired()
            .HasMaxLength(TicketConsts.commentDetailLength);

            b.HasOne(x => x.Ticket).WithMany(x => x.Comments).HasForeignKey(x => x.TicketId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
