using Microsoft.EntityFrameworkCore;
using Pcbuilder.Api.Models;

namespace Pcbuilder.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Core
    public DbSet<Market> Markets => Set<Market>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Vendor> Vendors => Set<Vendor>();

    // Lookup
    public DbSet<PartType> PartTypes => Set<PartType>();
    public DbSet<UseCase> UseCases => Set<UseCase>();

    // Catalog
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<CpuSpec> CpuSpecs => Set<CpuSpec>();
    public DbSet<GpuSpec> GpuSpecs => Set<GpuSpec>();
    public DbSet<MotherboardSpec> MotherboardSpecs => Set<MotherboardSpec>();
    public DbSet<RamSpec> RamSpecs => Set<RamSpec>();
    public DbSet<StorageSpec> StorageSpecs => Set<StorageSpec>();
    public DbSet<PsuSpec> PsuSpecs => Set<PsuSpec>();
    public DbSet<CaseSpec> CaseSpecs => Set<CaseSpec>();
    public DbSet<CoolerSpec> CoolerSpecs => Set<CoolerSpec>();

    public DbSet<PartOffer> PartOffers => Set<PartOffer>();

    // Builds
    public DbSet<Build> Builds => Set<Build>();
    public DbSet<BuildPart> BuildParts => Set<BuildPart>();
    public DbSet<BuildWarning> BuildWarnings => Set<BuildWarning>();

    // Build-for-me + recos
    public DbSet<BuildRequest> BuildRequests => Set<BuildRequest>();
    public DbSet<BuildRecommendation> BuildRecommendations => Set<BuildRecommendation>();

    // Upgrade advisor
    public DbSet<UserPc> UserPcs => Set<UserPc>();
    public DbSet<UpgradeRequest> UpgradeRequests => Set<UpgradeRequest>();
    public DbSet<UpgradePlan> UpgradePlans => Set<UpgradePlan>();
    public DbSet<UpgradePlanChange> UpgradePlanChanges => Set<UpgradePlanChange>();

    // Activity / realtime
    public DbSet<BuildActivity> BuildActivities => Set<BuildActivity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PostgreSQL UUID generation (recommended)
        modelBuilder.HasPostgresExtension("pgcrypto");

        // ---------- Table names (snake_case, predictable) ----------
        modelBuilder.Entity<Market>().ToTable("markets");
        modelBuilder.Entity<AppUser>().ToTable("app_users");
        modelBuilder.Entity<Vendor>().ToTable("vendors");

        modelBuilder.Entity<PartType>().ToTable("part_types");
        modelBuilder.Entity<UseCase>().ToTable("use_cases");

        modelBuilder.Entity<Part>().ToTable("parts");
        modelBuilder.Entity<CpuSpec>().ToTable("cpu_specs");
        modelBuilder.Entity<GpuSpec>().ToTable("gpu_specs");
        modelBuilder.Entity<MotherboardSpec>().ToTable("motherboard_specs");
        modelBuilder.Entity<RamSpec>().ToTable("ram_specs");
        modelBuilder.Entity<StorageSpec>().ToTable("storage_specs");
        modelBuilder.Entity<PsuSpec>().ToTable("psu_specs");
        modelBuilder.Entity<CaseSpec>().ToTable("case_specs");
        modelBuilder.Entity<CoolerSpec>().ToTable("cooler_specs");

        modelBuilder.Entity<PartOffer>().ToTable("part_offers");

        modelBuilder.Entity<Build>().ToTable("builds");
        modelBuilder.Entity<BuildPart>().ToTable("build_parts");
        modelBuilder.Entity<BuildWarning>().ToTable("build_warnings");

        modelBuilder.Entity<BuildRequest>().ToTable("build_requests");
        modelBuilder.Entity<BuildRecommendation>().ToTable("build_recommendations");

        modelBuilder.Entity<UserPc>().ToTable("user_pcs");
        modelBuilder.Entity<UpgradeRequest>().ToTable("upgrade_requests");
        modelBuilder.Entity<UpgradePlan>().ToTable("upgrade_plans");
        modelBuilder.Entity<UpgradePlanChange>().ToTable("upgrade_plan_changes");

        modelBuilder.Entity<BuildActivity>().ToTable("build_activity");

        // ---------- Default UUIDs ----------
        // Use DB-side UUIDs. If you prefer client-side Guid.NewGuid(), remove these.
        void GuidDefault<T>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> e) where T : class
            => e.Property("Id").HasDefaultValueSql("gen_random_uuid()");

        GuidDefault(modelBuilder.Entity<Market>());
        GuidDefault(modelBuilder.Entity<AppUser>());
        GuidDefault(modelBuilder.Entity<Vendor>());
        GuidDefault(modelBuilder.Entity<Part>());
        GuidDefault(modelBuilder.Entity<PartOffer>());
        GuidDefault(modelBuilder.Entity<Build>());
        GuidDefault(modelBuilder.Entity<BuildWarning>());
        GuidDefault(modelBuilder.Entity<BuildRequest>());
        GuidDefault(modelBuilder.Entity<BuildRecommendation>());
        GuidDefault(modelBuilder.Entity<UserPc>());
        GuidDefault(modelBuilder.Entity<UpgradeRequest>());
        GuidDefault(modelBuilder.Entity<UpgradePlan>());
        GuidDefault(modelBuilder.Entity<BuildActivity>());

        // ---------- Uniques / Indexes ----------
        modelBuilder.Entity<Market>()
            .HasIndex(m => m.Code)
            .IsUnique();

        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Vendor>()
            .HasIndex(v => v.Name)
            .IsUnique();

        modelBuilder.Entity<PartType>()
            .HasIndex(t => t.Code)
            .IsUnique();

        modelBuilder.Entity<UseCase>()
            .HasIndex(u => u.Code)
            .IsUnique();

        modelBuilder.Entity<Part>()
            .HasIndex(p => new { p.TypeId, p.Brand, p.Model });

        modelBuilder.Entity<PartOffer>()
            .HasIndex(o => new { o.PartId, o.MarketId });

        modelBuilder.Entity<PartOffer>()
            .HasIndex(o => new { o.MarketId, o.InStock });

        modelBuilder.Entity<BuildRecommendation>()
            .HasIndex(r => new { r.RequestId, r.Rank })
            .IsUnique();

        modelBuilder.Entity<UpgradePlan>()
            .HasIndex(p => new { p.RequestId, p.Rank })
            .IsUnique();

        modelBuilder.Entity<BuildActivity>()
            .HasIndex(a => a.CreatedAt);

        // ---------- Relationships ----------
        // Part -> PartType
        modelBuilder.Entity<Part>()
            .HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // One-to-one specs (PartId is PK/FK)
        modelBuilder.Entity<CpuSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<GpuSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<MotherboardSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<RamSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<StorageSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<PsuSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<CaseSpec>().HasKey(x => x.PartId);
        modelBuilder.Entity<CoolerSpec>().HasKey(x => x.PartId);

        modelBuilder.Entity<CpuSpec>()
            .HasOne(x => x.Part).WithOne(p => p.CpuSpec)
            .HasForeignKey<CpuSpec>(x => x.PartId);

        modelBuilder.Entity<GpuSpec>()
            .HasOne(x => x.Part).WithOne(p => p.GpuSpec)
            .HasForeignKey<GpuSpec>(x => x.PartId);

        modelBuilder.Entity<MotherboardSpec>()
            .HasOne(x => x.Part).WithOne(p => p.MotherboardSpec)
            .HasForeignKey<MotherboardSpec>(x => x.PartId);

        modelBuilder.Entity<RamSpec>()
            .HasOne(x => x.Part).WithOne(p => p.RamSpec)
            .HasForeignKey<RamSpec>(x => x.PartId);

        modelBuilder.Entity<StorageSpec>()
            .HasOne(x => x.Part).WithOne(p => p.StorageSpec)
            .HasForeignKey<StorageSpec>(x => x.PartId);

        modelBuilder.Entity<PsuSpec>()
            .HasOne(x => x.Part).WithOne(p => p.PsuSpec)
            .HasForeignKey<PsuSpec>(x => x.PartId);

        modelBuilder.Entity<CaseSpec>()
            .HasOne(x => x.Part).WithOne(p => p.CaseSpec)
            .HasForeignKey<CaseSpec>(x => x.PartId);

        modelBuilder.Entity<CoolerSpec>()
            .HasOne(x => x.Part).WithOne(p => p.CoolerSpec)
            .HasForeignKey<CoolerSpec>(x => x.PartId);

        // Offers
        modelBuilder.Entity<PartOffer>()
            .HasOne(o => o.Part)
            .WithMany()
            .HasForeignKey(o => o.PartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PartOffer>()
            .HasOne(o => o.Vendor)
            .WithMany()
            .HasForeignKey(o => o.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PartOffer>()
            .HasOne(o => o.Market)
            .WithMany()
            .HasForeignKey(o => o.MarketId)
            .OnDelete(DeleteBehavior.Restrict);

        // Build parts (composite key)
        modelBuilder.Entity<BuildPart>()
            .HasKey(bp => new { bp.BuildId, bp.PartId });

        modelBuilder.Entity<BuildPart>()
            .HasOne(bp => bp.Build)
            .WithMany(b => b.BuildParts)
            .HasForeignKey(bp => bp.BuildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildPart>()
            .HasOne(bp => bp.Part)
            .WithMany()
            .HasForeignKey(bp => bp.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        // Build warnings
        modelBuilder.Entity<BuildWarning>()
            .HasOne(w => w.Build)
            .WithMany(b => b.Warnings)
            .HasForeignKey(w => w.BuildId)
            .OnDelete(DeleteBehavior.Cascade);

        // Build requests + recommendations
        modelBuilder.Entity<BuildRequest>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BuildRequest>()
            .HasOne(r => r.Market)
            .WithMany()
            .HasForeignKey(r => r.MarketId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BuildRequest>()
            .HasOne(r => r.UseCase)
            .WithMany()
            .HasForeignKey(r => r.UseCaseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BuildRecommendation>()
            .HasOne(rr => rr.Request)
            .WithMany(r => r.Recommendations)
            .HasForeignKey(rr => rr.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildRecommendation>()
            .HasOne(rr => rr.Build)
            .WithMany()
            .HasForeignKey(rr => rr.BuildId)
            .OnDelete(DeleteBehavior.Restrict);

        // User PC
        modelBuilder.Entity<UserPc>()
            .HasOne(upc => upc.User)
            .WithMany()
            .HasForeignKey(upc => upc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserPc>()
            .HasOne(upc => upc.Build)
            .WithMany()
            .HasForeignKey(upc => upc.BuildId)
            .OnDelete(DeleteBehavior.Restrict);

        // Upgrade request / plans
        modelBuilder.Entity<UpgradeRequest>()
            .HasOne(r => r.UserPc)
            .WithMany(pc => pc.UpgradeRequests)
            .HasForeignKey(r => r.UserPcId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UpgradeRequest>()
            .HasOne(r => r.Market)
            .WithMany()
            .HasForeignKey(r => r.MarketId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UpgradePlan>()
            .HasOne(p => p.Request)
            .WithMany(r => r.Plans)
            .HasForeignKey(p => p.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UpgradePlan>()
            .HasOne(p => p.FromBuild)
            .WithMany()
            .HasForeignKey(p => p.FromBuildId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UpgradePlan>()
            .HasOne(p => p.ToBuild)
            .WithMany()
            .HasForeignKey(p => p.ToBuildId)
            .OnDelete(DeleteBehavior.Restrict);

        // Upgrade plan change (composite key)
        modelBuilder.Entity<UpgradePlanChange>()
            .HasKey(c => new { c.PlanId, c.PartId, c.ChangeType });

        modelBuilder.Entity<UpgradePlanChange>()
            .HasOne(c => c.Plan)
            .WithMany(p => p.Changes)
            .HasForeignKey(c => c.PlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UpgradePlanChange>()
            .HasOne(c => c.Part)
            .WithMany()
            .HasForeignKey(c => c.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        // Activity
        modelBuilder.Entity<BuildActivity>()
            .HasOne(a => a.Market)
            .WithMany()
            .HasForeignKey(a => a.MarketId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BuildActivity>()
            .HasOne(a => a.UseCase)
            .WithMany()
            .HasForeignKey(a => a.UseCaseId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BuildActivity>()
            .HasOne(a => a.Build)
            .WithMany()
            .HasForeignKey(a => a.BuildId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BuildActivity>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Timestamps defaults
        modelBuilder.Entity<Market>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<AppUser>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<Vendor>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<Part>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<Part>().Property(x => x.UpdatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<PartOffer>().Property(x => x.LastChecked).HasDefaultValueSql("now()");
        modelBuilder.Entity<Build>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<BuildWarning>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<BuildRequest>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<BuildRecommendation>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<UserPc>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<UpgradeRequest>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<UpgradePlan>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        modelBuilder.Entity<BuildActivity>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");
    }
}
