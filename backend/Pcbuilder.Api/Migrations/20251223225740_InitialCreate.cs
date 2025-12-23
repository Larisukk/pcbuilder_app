using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pcbuilder.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Email = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "markets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CurrencyCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_markets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "part_types",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "use_cases",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_use_cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "builds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    EstPowerW = table.Column<int>(type: "integer", nullable: false),
                    PowerSafe = table.Column<bool>(type: "boolean", nullable: false),
                    PerfScore = table.Column<decimal>(type: "numeric", nullable: false),
                    UpgradabilityScore = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_builds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_builds_app_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "app_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "parts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Msrp = table.Column<decimal>(type: "numeric", nullable: true),
                    ReleaseYear = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PerfScore = table.Column<decimal>(type: "numeric", nullable: false),
                    TdpWatts = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parts_part_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "part_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "build_requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    MarketId = table.Column<Guid>(type: "uuid", nullable: false),
                    UseCaseId = table.Column<short>(type: "smallint", nullable: false),
                    BudgetMin = table.Column<decimal>(type: "numeric", nullable: false),
                    BudgetMax = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_build_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_build_requests_app_users_UserId",
                        column: x => x.UserId,
                        principalTable: "app_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_build_requests_markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_build_requests_use_cases_UseCaseId",
                        column: x => x.UseCaseId,
                        principalTable: "use_cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "build_activity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    MarketId = table.Column<Guid>(type: "uuid", nullable: true),
                    UseCaseId = table.Column<short>(type: "smallint", nullable: true),
                    BuildId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_build_activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_build_activity_app_users_UserId",
                        column: x => x.UserId,
                        principalTable: "app_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_build_activity_builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_build_activity_markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_build_activity_use_cases_UseCaseId",
                        column: x => x.UseCaseId,
                        principalTable: "use_cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "build_warnings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    BuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_build_warnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_build_warnings_builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_pcs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_pcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_pcs_app_users_UserId",
                        column: x => x.UserId,
                        principalTable: "app_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_pcs_builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "build_parts",
                columns: table => new
                {
                    BuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_build_parts", x => new { x.BuildId, x.PartId });
                    table.ForeignKey(
                        name: "FK_build_parts_builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_build_parts_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "case_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormFactorSupport = table.Column<string>(type: "text", nullable: false),
                    MaxGpuLengthMm = table.Column<int>(type: "integer", nullable: true),
                    MaxCoolerHeightMm = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_case_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cooler_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    MaxTdpWatts = table.Column<int>(type: "integer", nullable: true),
                    HeightMm = table.Column<int>(type: "integer", nullable: true),
                    SocketSupport = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cooler_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_cooler_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cpu_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Socket = table.Column<string>(type: "text", nullable: false),
                    Cores = table.Column<int>(type: "integer", nullable: false),
                    Threads = table.Column<int>(type: "integer", nullable: false),
                    BaseGhz = table.Column<decimal>(type: "numeric", nullable: true),
                    BoostGhz = table.Column<decimal>(type: "numeric", nullable: true),
                    Igpu = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpu_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_cpu_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gpu_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    VramGb = table.Column<int>(type: "integer", nullable: false),
                    LengthMm = table.Column<int>(type: "integer", nullable: true),
                    PcieGen = table.Column<int>(type: "integer", nullable: true),
                    RecommendedPsuWatts = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpu_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_gpu_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "motherboard_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Socket = table.Column<string>(type: "text", nullable: false),
                    Chipset = table.Column<string>(type: "text", nullable: true),
                    FormFactor = table.Column<string>(type: "text", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    MemorySlots = table.Column<int>(type: "integer", nullable: false),
                    MaxMemoryGb = table.Column<int>(type: "integer", nullable: true),
                    M2Slots = table.Column<int>(type: "integer", nullable: false),
                    SataPorts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motherboard_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_motherboard_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "part_offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    MarketId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyCode = table.Column<string>(type: "text", nullable: false),
                    InStock = table.Column<bool>(type: "boolean", nullable: false),
                    LastChecked = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_part_offers_markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_part_offers_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_part_offers_vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "psu_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Wattage = table.Column<int>(type: "integer", nullable: false),
                    Efficiency = table.Column<string>(type: "text", nullable: true),
                    Modular = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_psu_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_psu_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ram_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    CapacityGb = table.Column<int>(type: "integer", nullable: false),
                    Sticks = table.Column<int>(type: "integer", nullable: false),
                    SpeedMhz = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ram_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_ram_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storage_specs",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    CapacityGb = table.Column<int>(type: "integer", nullable: false),
                    Interface = table.Column<string>(type: "text", nullable: true),
                    SeqReadMbS = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage_specs", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_storage_specs_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "build_recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    PerformanceScore = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceEfficiency = table.Column<decimal>(type: "numeric", nullable: false),
                    Upgradability = table.Column<decimal>(type: "numeric", nullable: false),
                    PowerSafetyScore = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_build_recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_build_recommendations_build_requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "build_requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_build_recommendations_builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "upgrade_requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UserPcId = table.Column<Guid>(type: "uuid", nullable: false),
                    MarketId = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetMax = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upgrade_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_upgrade_requests_markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upgrade_requests_user_pcs_UserPcId",
                        column: x => x.UserPcId,
                        principalTable: "user_pcs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "upgrade_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromBuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToBuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    FpsGain = table.Column<decimal>(type: "numeric", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    FpsGainPerCost = table.Column<decimal>(type: "numeric", nullable: false),
                    PowerSafetyScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ResaleValueEst = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upgrade_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_upgrade_plans_builds_FromBuildId",
                        column: x => x.FromBuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upgrade_plans_builds_ToBuildId",
                        column: x => x.ToBuildId,
                        principalTable: "builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upgrade_plans_upgrade_requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "upgrade_requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "upgrade_plan_changes",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeType = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upgrade_plan_changes", x => new { x.PlanId, x.PartId, x.ChangeType });
                    table.ForeignKey(
                        name: "FK_upgrade_plan_changes_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_upgrade_plan_changes_upgrade_plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "upgrade_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_users_Email",
                table: "app_users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_build_activity_BuildId",
                table: "build_activity",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_build_activity_CreatedAt",
                table: "build_activity",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_build_activity_MarketId",
                table: "build_activity",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_build_activity_UseCaseId",
                table: "build_activity",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_build_activity_UserId",
                table: "build_activity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_build_parts_PartId",
                table: "build_parts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_build_recommendations_BuildId",
                table: "build_recommendations",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_build_recommendations_RequestId_Rank",
                table: "build_recommendations",
                columns: new[] { "RequestId", "Rank" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_build_requests_MarketId",
                table: "build_requests",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_build_requests_UseCaseId",
                table: "build_requests",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_build_requests_UserId",
                table: "build_requests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_build_warnings_BuildId",
                table: "build_warnings",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_builds_CreatedById",
                table: "builds",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_markets_Code",
                table: "markets",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_part_offers_MarketId_InStock",
                table: "part_offers",
                columns: new[] { "MarketId", "InStock" });

            migrationBuilder.CreateIndex(
                name: "IX_part_offers_PartId_MarketId",
                table: "part_offers",
                columns: new[] { "PartId", "MarketId" });

            migrationBuilder.CreateIndex(
                name: "IX_part_offers_VendorId",
                table: "part_offers",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_part_types_Code",
                table: "part_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_parts_TypeId_Brand_Model",
                table: "parts",
                columns: new[] { "TypeId", "Brand", "Model" });

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_plan_changes_PartId",
                table: "upgrade_plan_changes",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_plans_FromBuildId",
                table: "upgrade_plans",
                column: "FromBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_plans_RequestId_Rank",
                table: "upgrade_plans",
                columns: new[] { "RequestId", "Rank" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_plans_ToBuildId",
                table: "upgrade_plans",
                column: "ToBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_requests_MarketId",
                table: "upgrade_requests",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_upgrade_requests_UserPcId",
                table: "upgrade_requests",
                column: "UserPcId");

            migrationBuilder.CreateIndex(
                name: "IX_use_cases_Code",
                table: "use_cases",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_pcs_BuildId",
                table: "user_pcs",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_user_pcs_UserId",
                table: "user_pcs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_Name",
                table: "vendors",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "build_activity");

            migrationBuilder.DropTable(
                name: "build_parts");

            migrationBuilder.DropTable(
                name: "build_recommendations");

            migrationBuilder.DropTable(
                name: "build_warnings");

            migrationBuilder.DropTable(
                name: "case_specs");

            migrationBuilder.DropTable(
                name: "cooler_specs");

            migrationBuilder.DropTable(
                name: "cpu_specs");

            migrationBuilder.DropTable(
                name: "gpu_specs");

            migrationBuilder.DropTable(
                name: "motherboard_specs");

            migrationBuilder.DropTable(
                name: "part_offers");

            migrationBuilder.DropTable(
                name: "psu_specs");

            migrationBuilder.DropTable(
                name: "ram_specs");

            migrationBuilder.DropTable(
                name: "storage_specs");

            migrationBuilder.DropTable(
                name: "upgrade_plan_changes");

            migrationBuilder.DropTable(
                name: "build_requests");

            migrationBuilder.DropTable(
                name: "vendors");

            migrationBuilder.DropTable(
                name: "parts");

            migrationBuilder.DropTable(
                name: "upgrade_plans");

            migrationBuilder.DropTable(
                name: "use_cases");

            migrationBuilder.DropTable(
                name: "part_types");

            migrationBuilder.DropTable(
                name: "upgrade_requests");

            migrationBuilder.DropTable(
                name: "markets");

            migrationBuilder.DropTable(
                name: "user_pcs");

            migrationBuilder.DropTable(
                name: "builds");

            migrationBuilder.DropTable(
                name: "app_users");
        }
    }
}
