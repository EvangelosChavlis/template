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
                name: "geography_natural");

            migrationBuilder.EnsureSchema(
                name: "geography_administrative");

            migrationBuilder.EnsureSchema(
                name: "weather");

            migrationBuilder.EnsureSchema(
                name: "auth");

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
                    Address = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
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
                        name: "FK_Users_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClimateZone",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AvgTemperatureC = table.Column<double>(type: "double precision", nullable: false),
                    AvgPrecipitationMm = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateZone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClimateZone_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Continents",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Continents_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LogErrors",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Error = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    Instance = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ExceptionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StackTrace = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogErrors_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MoonPhases",
                schema: "weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_MoonPhases_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NaturalFeature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaturalFeature_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "TerrainTypes",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerrainTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerrainTypes_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeZones",
                schema: "geography_natural",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UtcOffset = table.Column<double>(type: "double precision", nullable: false),
                    SupportsDaylightSaving = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DstOffset = table.Column<double>(type: "double precision", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeZones_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
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
                schema: "weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RecommendedActions = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warnings_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Capital = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Countries_Users_UserLockedId",
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
                name: "AuditLogs",
                schema: "metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    IPAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AdditionalMetadata = table.Column<string>(type: "jsonb", nullable: true),
                    BeforeValues = table.Column<string>(type: "jsonb", nullable: true),
                    AfterValues = table.Column<string>(type: "jsonb", nullable: true),
                    IsSystemAction = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TelemetryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_TelemetryRecords_TelemetryId",
                        column: x => x.TelemetryId,
                        principalSchema: "metrics",
                        principalTable: "TelemetryRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Aggregate_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aggregate_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AreaKm2 = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Module_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Municipalities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "geography_administrative",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Municipalities_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    table.ForeignKey(
                        name: "FK_Feature_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feature_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Districts_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalSchema: "geography_administrative",
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Districts_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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
                    table.ForeignKey(
                        name: "FK_Action_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                schema: "geography_administrative",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    Zipcode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TimezoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    TerrainTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClimateZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    NaturalFeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    NeighborhoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_ClimateZone_ClimateZoneId",
                        column: x => x.ClimateZoneId,
                        principalSchema: "geography_natural",
                        principalTable: "ClimateZone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_NaturalFeature_NaturalFeatureId",
                        column: x => x.NaturalFeatureId,
                        principalTable: "NaturalFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Neighborhoods_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalSchema: "geography_administrative",
                        principalTable: "Neighborhoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_TerrainTypes_TerrainTypeId",
                        column: x => x.TerrainTypeId,
                        principalSchema: "geography_natural",
                        principalTable: "TerrainTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_TimeZones_TimezoneId",
                        column: x => x.TimezoneId,
                        principalSchema: "geography_natural",
                        principalTable: "TimeZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Users_LockedByUserId",
                        column: x => x.LockedByUserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                schema: "weather",
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
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "geography_natural",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forecasts_MoonPhases_MoonPhaseId",
                        column: x => x.MoonPhaseId,
                        principalSchema: "weather",
                        principalTable: "MoonPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forecasts_Users_UserLockedId",
                        column: x => x.UserLockedId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Forecasts_Warnings_WarningId",
                        column: x => x.WarningId,
                        principalSchema: "weather",
                        principalTable: "Warnings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                schema: "weather",
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
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    LockUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserLockedId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "geography_natural",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observations_MoonPhases_MoonPhaseId",
                        column: x => x.MoonPhaseId,
                        principalSchema: "weather",
                        principalTable: "MoonPhases",
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
                name: "IX_\r                ClimateZone_\r                Id_\r                Name_\r                AvgTemperatureC_\r                AvgPrecipitationMm_\r                IsActive",
                schema: "geography_natural",
                table: "ClimateZone",
                columns: new[] { "Id", "Name", "AvgTemperatureC", "AvgPrecipitationMm", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClimateZone_LockedByUserId",
                schema: "geography_natural",
                table: "ClimateZone",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Continent_\r                Id_\r                Name_\r                Code_\r                IsActive",
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
                name: "IX_\r                Country_\r                Id_\r                Name_\r                Code_\r                Population_\r                IsActive",
                schema: "geography_administrative",
                table: "Countries",
                columns: new[] { "Id", "Name", "Code", "Population", "IsActive" },
                unique: true);

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
                name: "IX_\r                District_\r                Id_\r                Name_\r                Population_\r                IsActive",
                schema: "geography_administrative",
                table: "Districts",
                columns: new[] { "Id", "Name", "Population", "IsActive" },
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
                name: "IX_Feature_LockedByUserId",
                table: "Feature",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ModuleId",
                table: "Feature",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Forecast_\r                Id_\r                Date_\r                TemperatureC_\r                Humidity",
                schema: "weather",
                table: "Forecasts",
                columns: new[] { "Id", "Date", "TemperatureC", "Humidity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_LocationId",
                schema: "weather",
                table: "Forecasts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_MoonPhaseId",
                schema: "weather",
                table: "Forecasts",
                column: "MoonPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_UserLockedId",
                schema: "weather",
                table: "Forecasts",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_WarningId",
                schema: "weather",
                table: "Forecasts",
                column: "WarningId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Location_\r                Id_\r                Longitude_\r                Latitude_\r                Altitude_\r                IsActive",
                schema: "geography_natural",
                table: "Locations",
                columns: new[] { "Id", "Longitude", "Latitude", "Altitude", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id",
                schema: "geography_natural",
                table: "Locations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ClimateZoneId",
                schema: "geography_natural",
                table: "Locations",
                column: "ClimateZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LockedByUserId",
                schema: "geography_natural",
                table: "Locations",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_NaturalFeatureId",
                schema: "geography_natural",
                table: "Locations",
                column: "NaturalFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_NeighborhoodId",
                schema: "geography_natural",
                table: "Locations",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TerrainTypeId",
                schema: "geography_natural",
                table: "Locations",
                column: "TerrainTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TimezoneId",
                schema: "geography_natural",
                table: "Locations",
                column: "TimezoneId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                LogError_\r                Id_\r                Error_\r                StatusCode_\r                Timestamp",
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
                name: "IX_Module_AggregateId",
                table: "Module",
                column: "AggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_LockedByUserId",
                table: "Module",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                MoonPhase_\r                Id_\r                Name_\r                Description",
                schema: "weather",
                table: "MoonPhases",
                columns: new[] { "Id", "Name", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoonPhases_UserLockedId",
                schema: "weather",
                table: "MoonPhases",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Municipality_\r                Id_\r                Name_\r                Population_\r                IsActive",
                schema: "geography_administrative",
                table: "Municipalities",
                columns: new[] { "Id", "Name", "Population", "IsActive" },
                unique: true);

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
                name: "IX_NaturalFeature_LockedByUserId",
                table: "NaturalFeature",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Neighborhood_\r                Id_\r                Name_\r                Zipcode_\r                IsActive",
                schema: "geography_administrative",
                table: "Neighborhoods",
                columns: new[] { "Id", "Name", "Zipcode", "IsActive" },
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
                name: "IX_\r                Observation_\r                Id_\r                Timestamp_\r                TemperatureC_\r                Humidity",
                schema: "weather",
                table: "Observations",
                columns: new[] { "Id", "Timestamp", "TemperatureC", "Humidity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observations_LocationId",
                schema: "weather",
                table: "Observations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_MoonPhaseId",
                schema: "weather",
                table: "Observations",
                column: "MoonPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_UserLockedId",
                schema: "weather",
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
                name: "IX_\r                Region_\r                Id_\r                Name_\r                AreaKm2_\r                Code_\r                IsActive",
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
                name: "IX_\r                Role_\r                Id_\r                Name_\r                Description_\r                IsActive",
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
                name: "IX_\r                State_\r                Id_\r                Name_\r                Population_\r                Code_\r                IsActive",
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
                name: "IX_\r                TelemetryRecord_\r                Id_\r                Method_\r                StatusCode_\r                ResponseTime_\r                RequestTimestamp",
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
                name: "IX_\r                TerrainType_\r                Id_\r                Name_\r                Description_\r                IsActive",
                schema: "geography_natural",
                table: "TerrainTypes",
                columns: new[] { "Id", "Name", "Description", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TerrainTypes_LockedByUserId",
                schema: "geography_natural",
                table: "TerrainTypes",
                column: "LockedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Timezone_\r                Id_\r                Name_\r                UtcOffset_\r                IsActive",
                schema: "geography_natural",
                table: "TimeZones",
                columns: new[] { "Id", "Name", "UtcOffset", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeZones_LockedByUserId",
                schema: "geography_natural",
                table: "TimeZones",
                column: "LockedByUserId");

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
                name: "IX_UserClaims_UserId",
                schema: "auth",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                UserLogin_\r                Id_\r                LoginProvider_\r                ProviderDisplayName_\r                Date",
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
                name: "IX_\r                UserLogout_\r                Id_\r                LoginProvider_\r                ProviderDisplayName_\r                Date",
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
                name: "IX_\r                UserRole_\r                Id_\r                UserId_\r                RoleId_\r                Date",
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
                name: "IX_\r                User_\r                Id_\r                FirstName_\r                LastName_\r                Email_\r                UserName_\r                PhoneNumber_\r                MobilePhoneNumber",
                schema: "auth",
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "UserName", "PhoneNumber", "MobilePhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLockedId",
                schema: "auth",
                table: "Users",
                column: "UserLockedId");

            migrationBuilder.CreateIndex(
                name: "IX_\r                Warning_\r                Id_\r                Name_\r                Description",
                schema: "weather",
                table: "Warnings",
                columns: new[] { "Id", "Name", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warnings_LockedByUserId",
                schema: "weather",
                table: "Warnings",
                column: "LockedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Forecasts",
                schema: "weather");

            migrationBuilder.DropTable(
                name: "LogErrors",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Observations",
                schema: "weather");

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
                name: "Warnings",
                schema: "weather");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "MoonPhases",
                schema: "weather");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "ClimateZone",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "NaturalFeature");

            migrationBuilder.DropTable(
                name: "Neighborhoods",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "TerrainTypes",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "TimeZones",
                schema: "geography_natural");

            migrationBuilder.DropTable(
                name: "TelemetryRecords",
                schema: "metrics");

            migrationBuilder.DropTable(
                name: "Aggregate");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Municipalities",
                schema: "geography_administrative");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth");

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

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");
        }
    }
}
