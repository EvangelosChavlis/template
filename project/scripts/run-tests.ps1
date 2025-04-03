# PowerShell script to run multiple test projects with filters

# Exit on error
$ErrorActionPreference = "Stop"

# Move to the parent directory
Set-Location ..

# Running Domain.Unit.Tests
Write-Host "Running Domain.Unit.Tests"
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Auth.RolesDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Auth.UsersDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.CommandResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.ItemResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.ListResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.PaginatedListTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Metrics.ErrorsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Metrics.TelemetryDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Weather.Collections.ForecastsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Weather.Collections.WarningsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Extensions.DateTimeExtensionsTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.RoleTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.UserRoleTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.UserTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.EnvelopeTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.IncludeThenIncludeTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.UrlQueryTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.CustomExceptionTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.ErrorDetailsTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.LogErrorTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Metrics.TelemetryTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Weather.ForecastTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Weather.WarningTests

# Running Persistence.Unit.Tests
Write-Host "Running Persistence.Unit.Tests"
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.RoleConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.UserConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.UserRoleConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Metrics.LogErrorConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Metrics.TelemetryConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Weather.ForecastConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Weather.WarningConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Contexts.DataContextTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Extensions.DbContextExtensionsTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Repositories.CommonRepositoryTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.DependencyInjectionTests

# Running Application.Unit.Tests
Write-Host "Running Application.Unit.Tests"
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Auth.UserFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Metrics.ErrorsFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Metrics.TelemetryFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Weather.ForecastFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Weather.WarningFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Auth.UserIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Metrics.ErrorsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Metrics.TelemetryIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Weather.Collections.ForecastsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Weather.Collections.WarningsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Auth.RolesMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Auth.UsersMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Metrics.ErrorsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Metrics.TelemetryMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Weather.Collections.ForecastsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Weather.Collections.WarningsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Auth.RoleServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Auth.UserServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Metrics.ErrorsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Metrics.TelemetryServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Weather.Collections.ForecastsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Weather.Collections.WarningsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.Enable2FAValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.ForgotPasswordValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.ResetPasswordValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.RoleValidatorsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.UserLoginValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.UserValidatorsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.Verify2FAValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Weather.Collections.ForecastsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Weather.Collections.WarningsServiceTests


# Running Api.Integration.Tests
Write-Host "Running Api.Integration.Tests"
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Weather.Collections.ForecastsControllerTests
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Weather.Collections.WarningsControllerTests
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Metrics.ErrorsControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Metrics.TelemetryControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Auth.RolesControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Auth.UsersControllerTests;

Write-Host "All specified tests have been executed." -ForegroundColor Green
