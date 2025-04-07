using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations.Archive
{
    /// <inheritdoc />
    public partial class InitialCreate_archive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "metrics");

            migrationBuilder.EnsureSchema(
                name: "supprort");

            migrationBuilder.EnsureSchema(
                name: "geography_natural");

            migrationBuilder.EnsureSchema(
                name: "geography_administrative");

            migrationBuilder.EnsureSchema(
                name: "weather_collections");

            migrationBuilder.EnsureSchema(
                name: "weather_tools");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    IPAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AdditionalMetadata = table.Column<string>(type: "jsonb", nullable: true),
                    BeforeValues = table.Column<string>(type: "jsonb", nullable: true),
                    AfterValues = table.Column<string>(type: "jsonb", nullable: true),
                    IsSystemAction = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TelemetryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                schema: "supprort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionLog = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Changes",
                schema: "supprort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ChangeLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeTypeId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Changes_ChangeLogs_ChangeLogId",
                        column: x => x.ChangeLogId,
                        principalSchema: "supprort",
                        principalTable: "ChangeLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeTypes",
                schema: "supprort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClimateZones",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    AvgTemperatureC = table.Column<double>(type: "double precision", nullable: false),
                    AvgPrecipitationMm = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continents",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Capital = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    PhoneCode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    TLD = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Currency = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ContinentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalSchema: "geography_administrative",
                        principalTable: "Continents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    Code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MunicipalityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQCategories",
                schema: "supprort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                schema: "supprort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Question = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Answer = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FAQCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQs_FAQCategories_FAQCategoryId",
                        column: x => x.FAQCategoryId,
                        principalSchema: "supprort",
                        principalTable: "FAQCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                schema: "weather_collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TemperatureC = table.Column<int>(type: "integer", nullable: false),
                    FeelsLikeC = table.Column<int>(type: "integer", nullable: false),
                    Humidity = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedKph = table.Column<double>(type: "double precision", nullable: false),
                    WindDirection = table.Column<int>(type: "integer", nullable: false),
                    PressureHpa = table.Column<double>(type: "double precision", nullable: false),
                    PrecipitationMm = table.Column<double>(type: "double precision", nullable: false),
                    VisibilityKm = table.Column<double>(type: "double precision", nullable: false),
                    UVIndex = table.Column<int>(type: "integer", nullable: false),
                    AirQualityIndex = table.Column<int>(type: "integer", nullable: false),
                    CloudCover = table.Column<int>(type: "integer", nullable: false),
                    LightningProbability = table.Column<int>(type: "integer", nullable: false),
                    PollenCount = table.Column<int>(type: "integer", nullable: false),
                    Sunrise = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Sunset = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Summary = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    WarningId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoonPhaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthStasus",
                schema: "weather_tools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStasus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Altitude = table.Column<double>(type: "double precision", nullable: false),
                    Depth = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TimezoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurfaceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClimateZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    NaturalFeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_ClimateZones_ClimateZoneId",
                        column: x => x.ClimateZoneId,
                        principalSchema: "geography_natural",
                        principalTable: "ClimateZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogErrors",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Error = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    Instance = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExceptionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StackTrace = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Aggregate_AggregateId",
                        column: x => x.AggregateId,
                        principalTable: "Aggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoonPhases",
                schema: "weather_collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    IlluminationPercentage = table.Column<double>(type: "double precision", nullable: false),
                    PhaseOrder = table.Column<int>(type: "integer", nullable: false),
                    DurationDays = table.Column<double>(type: "double precision", nullable: false),
                    IsSignificant = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OccurrenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoonPhases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalFeatures",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    Zipcode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "geography_administrative",
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TwoFactorToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TwoFactorTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    InitialPassword = table.Column<string>(type: "text", nullable: false),
                    NeighborhoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    MobilePhoneNumber = table.Column<string>(type: "text", nullable: false),
                    MobilePhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalSchema: "geography_administrative",
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Capital = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "geography_administrative",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_States_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    NeighborhoodId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "geography_natural",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stations_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalSchema: "geography_administrative",
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Stations_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SurfaceTypes",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurfaceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurfaceTypes_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TelemetryRecords",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    ResponseTime = table.Column<long>(type: "bigint", nullable: false),
                    MemoryUsed = table.Column<long>(type: "bigint", nullable: false),
                    CPUusage = table.Column<double>(type: "double precision", nullable: false),
                    RequestBodySize = table.Column<long>(type: "bigint", nullable: false),
                    RequestTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseBodySize = table.Column<long>(type: "bigint", nullable: false),
                    ResponseTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientIp = table.Column<string>(type: "text", nullable: false),
                    UserAgent = table.Column<string>(type: "text", nullable: false),
                    ThreadId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemetryRecords_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelemetryRecords_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Timezones",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    UtcOffset = table.Column<double>(type: "double precision", nullable: false),
                    SupportsDaylightSaving = table.Column<bool>(type: "boolean", nullable: false),
                    DstOffset = table.Column<double>(type: "double precision", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timezones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timezones_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SourceAuditLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetAuditLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trails_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalSchema: "metrics",
                        principalTable: "AuditLogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trails_AuditLogs_SourceAuditLogId",
                        column: x => x.SourceAuditLogId,
                        principalSchema: "metrics",
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trails_AuditLogs_TargetAuditLogId",
                        column: x => x.TargetAuditLogId,
                        principalSchema: "metrics",
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trails_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "weather_tools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogouts",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogouts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warnings",
                schema: "weather_collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    RecommendedActions = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warnings_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "geography_administrative",
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regions_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                schema: "weather_collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TemperatureC = table.Column<int>(type: "integer", nullable: false),
                    Humidity = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedKph = table.Column<double>(type: "double precision", nullable: false),
                    WindDirection = table.Column<int>(type: "integer", nullable: false),
                    PressureHpa = table.Column<double>(type: "double precision", nullable: false),
                    PrecipitationMm = table.Column<double>(type: "double precision", nullable: false),
                    VisibilityKm = table.Column<double>(type: "double precision", nullable: false),
                    UVIndex = table.Column<int>(type: "integer", nullable: false),
                    AirQualityIndex = table.Column<int>(type: "integer", nullable: false),
                    CloudCover = table.Column<int>(type: "integer", nullable: false),
                    LightningProbability = table.Column<int>(type: "integer", nullable: false),
                    PollenCount = table.Column<int>(type: "integer", nullable: false),
                    MoonPhaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_MoonPhases_MoonPhaseId",
                        column: x => x.MoonPhaseId,
                        principalSchema: "weather_collections",
                        principalTable: "MoonPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observations_Stations_StationId",
                        column: x => x.StationId,
                        principalSchema: "geography_administrative",
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observations_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SourceTelemetryRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetTelemetryRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    TelemetryRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_TelemetryRecords_SourceTelemetryRecordId",
                        column: x => x.SourceTelemetryRecordId,
                        principalSchema: "metrics",
                        principalTable: "TelemetryRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Stories_TelemetryRecords_TargetTelemetryRecordId",
                        column: x => x.TargetTelemetryRecordId,
                        principalSchema: "metrics",
                        principalTable: "TelemetryRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Stories_TelemetryRecords_TelemetryRecordId",
                        column: x => x.TelemetryRecordId,
                        principalSchema: "metrics",
                        principalTable: "TelemetryRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                schema: "weather_tools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Manufacturer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SN = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    HealthStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_HealthStasus_HealthStatusId",
                        column: x => x.HealthStatusId,
                        principalSchema: "weather_tools",
                        principalTable: "HealthStasus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sensors_Stations_StationId",
                        column: x => x.StationId,
                        principalSchema: "geography_administrative",
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "weather_tools",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                schema: "weather_tools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SensorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "weather_tools",
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_FeatureId",
                table: "Action",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Action_LockedByUserId",
                table: "Action",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Aggregate_LockedByUserId",
                table: "Aggregate",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Aggregate_PermissionId",
                table: "Aggregate",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TelemetryId",
                schema: "metrics",
                table: "AuditLogs",
                column: "TelemetryId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                schema: "metrics",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserLockedId",
                schema: "metrics",
                table: "AuditLogs",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLog_Id",
                schema: "supprort",
                table: "ChangeLogs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLog_VersionLog_DateTime",
                schema: "supprort",
                table: "ChangeLogs",
                columns: new[] { "Id", "VersionLog", "DateTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_UserLockedId",
                schema: "supprort",
                table: "ChangeLogs",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Change_Id",
                schema: "supprort",
                table: "Changes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Change_Id_Name",
                schema: "supprort",
                table: "Changes",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ChangeLogId",
                schema: "supprort",
                table: "Changes",
                column: "ChangeLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ChangeTypeId",
                schema: "supprort",
                table: "Changes",
                column: "ChangeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ChangeTypeId1",
                schema: "supprort",
                table: "Changes",
                column: "ChangeTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_UserLockedId",
                schema: "supprort",
                table: "Changes",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeType_Id",
                schema: "supprort",
                table: "ChangeTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeType_Id_Name_IsActive",
                schema: "supprort",
                table: "ChangeTypes",
                columns: new[] { "Id", "Name", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeTypes_UserLockedId",
                schema: "supprort",
                table: "ChangeTypes",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_ClimateZone_Code",
                schema: "geography_natural",
                table: "ClimateZones",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClimateZone_Id",
                schema: "geography_natural",
                table: "ClimateZones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClimateZone_Id_Name_Code_AvgTemperatureC_AvgPrecipitationMm_IsActive",
                schema: "geography_natural",
                table: "ClimateZones",
                columns: new[] { "Id", "Name", "Code", "AvgTemperatureC", "AvgPrecipitationMm", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClimateZones_UserLockedId",
                schema: "geography_natural",
                table: "ClimateZones",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Continent_Code",
                schema: "geography_administrative",
                table: "Continents",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Continent_Id",
                schema: "geography_administrative",
                table: "Continents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Continent_Id_Code",
                schema: "geography_administrative",
                table: "Continents",
                columns: new[] { "Id", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Continent_Id_Name_Code_IsActive",
                schema: "geography_administrative",
                table: "Continents",
                columns: new[] { "Id", "Name", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Continents_UserLockedId",
                schema: "geography_administrative",
                table: "Continents",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId",
                schema: "geography_administrative",
                table: "Countries",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_UserLockedId",
                schema: "geography_administrative",
                table: "Countries",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                schema: "geography_administrative",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Id",
                schema: "geography_administrative",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Id_Code_ContinentId",
                schema: "geography_administrative",
                table: "Countries",
                columns: new[] { "Id", "Code", "ContinentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Id_Name_Code_Population_IsActive",
                schema: "geography_administrative",
                table: "Countries",
                columns: new[] { "Id", "Name", "Code", "Population", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Code",
                schema: "geography_administrative",
                table: "Districts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Id",
                schema: "geography_administrative",
                table: "Districts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Id_Code_MunicipalityId",
                schema: "geography_administrative",
                table: "Districts",
                columns: new[] { "Id", "Code", "MunicipalityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Id_Name_Population_Code_IsActive",
                schema: "geography_administrative",
                table: "Districts",
                columns: new[] { "Id", "Name", "Population", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_MunicipalityId",
                schema: "geography_administrative",
                table: "Districts",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_UserLockedId",
                schema: "geography_administrative",
                table: "Districts",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQCategories_UserLockedId",
                schema: "supprort",
                table: "FAQCategories",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQCategory_Id",
                schema: "supprort",
                table: "FAQCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQCategory_Id_Name_IsActive",
                schema: "supprort",
                table: "FAQCategories",
                columns: new[] { "Id", "Name", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_Id",
                schema: "supprort",
                table: "FAQs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_Id_Title_IsActive",
                schema: "supprort",
                table: "FAQs",
                columns: new[] { "Id", "Title", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQs_FAQCategoryId",
                schema: "supprort",
                table: "FAQs",
                column: "FAQCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQs_UserLockedId",
                schema: "supprort",
                table: "FAQs",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_LockedByUserId",
                table: "Feature",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ModuleId",
                table: "Feature",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_Id",
                schema: "weather_collections",
                table: "Forecasts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_Id_Date_TemperatureC_Humidity",
                schema: "weather_collections",
                table: "Forecasts",
                columns: new[] { "Id", "Date", "TemperatureC", "Humidity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_MoonPhaseId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "MoonPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_StationId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_UserLockedId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_WarningId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "WarningId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthStasus_UserLockedId",
                schema: "weather_tools",
                table: "HealthStasus",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatus_Code",
                schema: "weather_tools",
                table: "HealthStasus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatus_Id",
                schema: "weather_tools",
                table: "HealthStasus",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatus_Id_Name",
                schema: "weather_tools",
                table: "HealthStasus",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatus_Id_Name_Code_IsActive",
                schema: "weather_tools",
                table: "HealthStasus",
                columns: new[] { "Id", "Name", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id",
                schema: "geography_natural",
                table: "Locations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id_Longitude_Latitude_Altitude_Depth_IsActive",
                schema: "geography_natural",
                table: "Locations",
                columns: new[] { "Id", "Longitude", "Latitude", "Altitude", "Depth", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ClimateZoneId",
                schema: "geography_natural",
                table: "Locations",
                column: "ClimateZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_NaturalFeatureId",
                schema: "geography_natural",
                table: "Locations",
                column: "NaturalFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_SurfaceTypeId",
                schema: "geography_natural",
                table: "Locations",
                column: "SurfaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TimezoneId",
                schema: "geography_natural",
                table: "Locations",
                column: "TimezoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserLockedId",
                schema: "geography_natural",
                table: "Locations",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_LogError_Id",
                schema: "metrics",
                table: "LogErrors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogError_Id_Error_StatusCode_Timestamp",
                schema: "metrics",
                table: "LogErrors",
                columns: new[] { "Id", "Error", "StatusCode", "Timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogErrors_UserId",
                schema: "metrics",
                table: "LogErrors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrors_UserLockedId",
                schema: "metrics",
                table: "LogErrors",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_AggregateId",
                table: "Module",
                column: "AggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_LockedByUserId",
                table: "Module",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MoonPhase_Id",
                schema: "weather_collections",
                table: "MoonPhases",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoonPhase_Id_Name_Code",
                schema: "weather_collections",
                table: "MoonPhases",
                columns: new[] { "Id", "Name", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoonPhases_UserLockedId",
                schema: "weather_collections",
                table: "MoonPhases",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_RegionId",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_UserLockedId",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_Code",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_Id",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_Id_Code_RegionId",
                schema: "geography_administrative",
                table: "Municipalities",
                columns: new[] { "Id", "Code", "RegionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_Id_Name_Population_Code_IsActive",
                schema: "geography_administrative",
                table: "Municipalities",
                columns: new[] { "Id", "Name", "Population", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaturalFeature_Id",
                schema: "geography_natural",
                table: "NaturalFeatures",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaturalFeature_Id_Name_Code_IsActive",
                schema: "geography_natural",
                table: "NaturalFeatures",
                columns: new[] { "Id", "Name", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaturalFeatures_UserLockedId",
                schema: "geography_natural",
                table: "NaturalFeatures",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Code",
                schema: "geography_administrative",
                table: "Neighborhoods",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Id",
                schema: "geography_administrative",
                table: "Neighborhoods",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Id_Code_DistrictId",
                schema: "geography_administrative",
                table: "Neighborhoods",
                columns: new[] { "Id", "Code", "DistrictId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhood_Id_Name_Zipcode_Code_IsActive",
                schema: "geography_administrative",
                table: "Neighborhoods",
                columns: new[] { "Id", "Name", "Zipcode", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_DistrictId",
                schema: "geography_administrative",
                table: "Neighborhoods",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_UserLockedId",
                schema: "geography_administrative",
                table: "Neighborhoods",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_Id",
                schema: "weather_collections",
                table: "Observations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observation_Id_Timestamp_TemperatureC_Humidity",
                schema: "weather_collections",
                table: "Observations",
                columns: new[] { "Id", "Timestamp", "TemperatureC", "Humidity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observations_MoonPhaseId",
                schema: "weather_collections",
                table: "Observations",
                column: "MoonPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_StationId",
                schema: "weather_collections",
                table: "Observations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_UserLockedId",
                schema: "weather_collections",
                table: "Observations",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_LockedByUserId",
                table: "Permission",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_Code",
                schema: "geography_administrative",
                table: "Regions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Id",
                schema: "geography_administrative",
                table: "Regions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Id_Code_StateId",
                schema: "geography_administrative",
                table: "Regions",
                columns: new[] { "Id", "Code", "StateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Id_Name_AreaKm2_Code_IsActive",
                schema: "geography_administrative",
                table: "Regions",
                columns: new[] { "Id", "Name", "AreaKm2", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_StateId",
                schema: "geography_administrative",
                table: "Regions",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UserLockedId",
                schema: "geography_administrative",
                table: "Regions",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Id",
                schema: "auth",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Id_Name_Description_IsActive",
                schema: "auth",
                table: "Roles",
                columns: new[] { "Id", "Name", "Description", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserLockedId",
                schema: "auth",
                table: "Roles",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_Id",
                schema: "weather_tools",
                table: "Sensors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_Id_Name_SN_IsActive",
                schema: "weather_tools",
                table: "Sensors",
                columns: new[] { "Id", "Name", "SN", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_HealthStatusId",
                schema: "weather_tools",
                table: "Sensors",
                column: "HealthStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_StationId",
                schema: "weather_tools",
                table: "Sensors",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UnitId",
                schema: "weather_tools",
                table: "Sensors",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UserLockedId",
                schema: "weather_tools",
                table: "Sensors",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Serie_Id",
                schema: "weather_tools",
                table: "Series",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Serie_Id_Value_Timestamp",
                schema: "weather_tools",
                table: "Series",
                columns: new[] { "Id", "Value", "Timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_SensorId",
                schema: "weather_tools",
                table: "Series",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_UserLockedId",
                schema: "weather_tools",
                table: "Series",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_State_Code",
                schema: "geography_administrative",
                table: "States",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Id",
                schema: "geography_administrative",
                table: "States",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Id_Code_CountryId",
                schema: "geography_administrative",
                table: "States",
                columns: new[] { "Id", "Code", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Id_Name_Population_Code_IsActive",
                schema: "geography_administrative",
                table: "States",
                columns: new[] { "Id", "Name", "Population", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                schema: "geography_administrative",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_States_UserLockedId",
                schema: "geography_administrative",
                table: "States",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Code",
                schema: "geography_administrative",
                table: "Stations",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Station_Id",
                schema: "geography_administrative",
                table: "Stations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Station_Id_Code_LocationId",
                schema: "geography_administrative",
                table: "Stations",
                columns: new[] { "Id", "Code", "LocationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Station_Id_Name_Code_IsActive",
                schema: "geography_administrative",
                table: "Stations",
                columns: new[] { "Id", "Name", "Code", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_LocationId",
                schema: "geography_administrative",
                table: "Stations",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_NeighborhoodId",
                schema: "geography_administrative",
                table: "Stations",
                column: "NeighborhoodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_UserLockedId",
                schema: "geography_administrative",
                table: "Stations",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_SourceTelemetryRecordId",
                schema: "metrics",
                table: "Stories",
                column: "SourceTelemetryRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_TargetTelemetryRecordId",
                schema: "metrics",
                table: "Stories",
                column: "TargetTelemetryRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_TelemetryRecordId",
                schema: "metrics",
                table: "Stories",
                column: "TelemetryRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserId",
                schema: "metrics",
                table: "Stories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurfaceType_Id",
                schema: "geography_natural",
                table: "SurfaceTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurfaceType_Id_Name_Description_IsActive",
                schema: "geography_natural",
                table: "SurfaceTypes",
                columns: new[] { "Id", "Name", "Description", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurfaceTypes_UserLockedId",
                schema: "geography_natural",
                table: "SurfaceTypes",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecord_Id_Method_StatusCode_ResponseTime_RequestTimestamp",
                schema: "metrics",
                table: "TelemetryRecords",
                columns: new[] { "Id", "Method", "Path", "StatusCode", "ResponseTime", "RequestTimestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_UserId",
                schema: "metrics",
                table: "TelemetryRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryRecords_UserLockedId",
                schema: "metrics",
                table: "TelemetryRecords",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Timezone_Code",
                schema: "geography_natural",
                table: "Timezones",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timezone_Id",
                schema: "geography_natural",
                table: "Timezones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timezone_Id_Name_Code_UtcOffset_IsActive",
                schema: "geography_natural",
                table: "Timezones",
                columns: new[] { "Id", "Name", "Code", "UtcOffset", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timezones_UserLockedId",
                schema: "geography_natural",
                table: "Timezones",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_AuditLogId",
                schema: "metrics",
                table: "Trails",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_SourceAuditLogId",
                schema: "metrics",
                table: "Trails",
                column: "SourceAuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_TargetAuditLogId",
                schema: "metrics",
                table: "Trails",
                column: "TargetAuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_UserLockedId",
                schema: "metrics",
                table: "Trails",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Id",
                schema: "weather_tools",
                table: "Units",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Id_Name",
                schema: "weather_tools",
                table: "Units",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Id_Name_Symbol_IsActive",
                schema: "weather_tools",
                table: "Units",
                columns: new[] { "Id", "Name", "Symbol", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_Symbol",
                schema: "weather_tools",
                table: "Units",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_UserLockedId",
                schema: "weather_tools",
                table: "Units",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "auth",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_Id",
                schema: "auth",
                table: "UserLogins",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_Id_LoginProvider_ProviderDisplayName_Date",
                schema: "auth",
                table: "UserLogins",
                columns: new[] { "Id", "LoginProvider", "ProviderDisplayName", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "auth",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogout_Id",
                schema: "auth",
                table: "UserLogouts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogout_Id_LoginProvider_ProviderDisplayName_Date",
                schema: "auth",
                table: "UserLogouts",
                columns: new[] { "Id", "LoginProvider", "ProviderDisplayName", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogouts_UserId",
                schema: "auth",
                table: "UserLogouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Id",
                schema: "auth",
                table: "UserRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Id_UserId_RoleId_Date",
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "Id", "UserId", "RoleId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "auth",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                schema: "auth",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Id_FirstName_LastName_Email_UserName_PhoneNumber_MobilePhoneNumber",
                schema: "auth",
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "UserName", "PhoneNumber", "MobilePhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NeighborhoodId",
                schema: "auth",
                table: "Users",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLockedId",
                schema: "auth",
                table: "Users",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Warning_Id",
                schema: "weather_collections",
                table: "Warnings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warning_Id_Name_Description",
                schema: "weather_collections",
                table: "Warnings",
                columns: new[] { "Id", "Name", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warnings_UserLockedId",
                schema: "weather_collections",
                table: "Warnings",
                column: "UserLockedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_Feature_FeatureId",
                table: "Action",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Action_Users_LockedByUserId",
                table: "Action",
                column: "LockedByUserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aggregate_Permission_PermissionId",
                table: "Aggregate",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aggregate_Users_LockedByUserId",
                table: "Aggregate",
                column: "LockedByUserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_TelemetryRecords_TelemetryId",
                schema: "metrics",
                table: "AuditLogs",
                column: "TelemetryId",
                principalSchema: "metrics",
                principalTable: "TelemetryRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                schema: "metrics",
                table: "AuditLogs",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserLockedId",
                schema: "metrics",
                table: "AuditLogs",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLogs_Users_UserLockedId",
                schema: "supprort",
                table: "ChangeLogs",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ChangeTypes_ChangeTypeId",
                schema: "supprort",
                table: "Changes",
                column: "ChangeTypeId",
                principalSchema: "supprort",
                principalTable: "ChangeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ChangeTypes_ChangeTypeId1",
                schema: "supprort",
                table: "Changes",
                column: "ChangeTypeId1",
                principalSchema: "supprort",
                principalTable: "ChangeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Users_UserLockedId",
                schema: "supprort",
                table: "Changes",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeTypes_Users_UserLockedId",
                schema: "supprort",
                table: "ChangeTypes",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimateZones_Users_UserLockedId",
                schema: "geography_natural",
                table: "ClimateZones",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Continents_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Continents",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Countries",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Municipalities_MunicipalityId",
                schema: "geography_administrative",
                table: "Districts",
                column: "MunicipalityId",
                principalSchema: "geography_administrative",
                principalTable: "Municipalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Districts",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQCategories_Users_UserLockedId",
                schema: "supprort",
                table: "FAQCategories",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQs_Users_UserLockedId",
                schema: "supprort",
                table: "FAQs",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Module_ModuleId",
                table: "Feature",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Users_LockedByUserId",
                table: "Feature",
                column: "LockedByUserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_MoonPhases_MoonPhaseId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "MoonPhaseId",
                principalSchema: "weather_collections",
                principalTable: "MoonPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_Stations_StationId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "StationId",
                principalSchema: "geography_administrative",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_Users_UserLockedId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_Warnings_WarningId",
                schema: "weather_collections",
                table: "Forecasts",
                column: "WarningId",
                principalSchema: "weather_collections",
                principalTable: "Warnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthStasus_Users_UserLockedId",
                schema: "weather_tools",
                table: "HealthStasus",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_NaturalFeatures_NaturalFeatureId",
                schema: "geography_natural",
                table: "Locations",
                column: "NaturalFeatureId",
                principalSchema: "geography_natural",
                principalTable: "NaturalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_SurfaceTypes_SurfaceTypeId",
                schema: "geography_natural",
                table: "Locations",
                column: "SurfaceTypeId",
                principalSchema: "geography_natural",
                principalTable: "SurfaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Timezones_TimezoneId",
                schema: "geography_natural",
                table: "Locations",
                column: "TimezoneId",
                principalSchema: "geography_natural",
                principalTable: "Timezones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Users_UserLockedId",
                schema: "geography_natural",
                table: "Locations",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrors_Users_UserId",
                schema: "metrics",
                table: "LogErrors",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LogErrors_Users_UserLockedId",
                schema: "metrics",
                table: "LogErrors",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Users_LockedByUserId",
                table: "Module",
                column: "LockedByUserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoonPhases_Users_UserLockedId",
                schema: "weather_collections",
                table: "MoonPhases",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Regions_RegionId",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "RegionId",
                principalSchema: "geography_administrative",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Municipalities",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NaturalFeatures_Users_UserLockedId",
                schema: "geography_natural",
                table: "NaturalFeatures",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhoods_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Neighborhoods",
                column: "UserLockedId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continents_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Continents");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Neighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Users_UserLockedId",
                schema: "geography_administrative",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UserLockedId",
                schema: "geography_administrative",
                table: "States");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Changes",
                schema: "supprort");

            migrationBuilder.DropTable(
                name: "FAQs",
                schema: "supprort");

            migrationBuilder.DropTable(
                name: "Forecasts",
                schema: "weather_collections");

            migrationBuilder.DropTable(
                name: "LogErrors",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Observations",
                schema: "weather_collections");

            migrationBuilder.DropTable(
                name: "Series",
                schema: "weather_tools");

            migrationBuilder.DropTable(
                name: "Stories",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Trails",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserLogouts",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "ChangeLogs",
                schema: "supprort");

            migrationBuilder.DropTable(
                name: "ChangeTypes",
                schema: "supprort");

            migrationBuilder.DropTable(
                name: "FAQCategories",
                schema: "supprort");

            migrationBuilder.DropTable(
                name: "Warnings",
                schema: "weather_collections");

            migrationBuilder.DropTable(
                name: "MoonPhases",
                schema: "weather_collections");

            migrationBuilder.DropTable(
                name: "Sensors",
                schema: "weather_tools");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "HealthStasus",
                schema: "weather_tools");

            migrationBuilder.DropTable(
                name: "Stations",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "weather_tools");

            migrationBuilder.DropTable(
                name: "TelemetryRecords",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Aggregate");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "ClimateZones",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "NaturalFeatures",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "SurfaceTypes",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "Timezones",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Neighborhoods",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Municipalities",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "States",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Continents",
                schema: "geography_administrative");
        }
    }
}
