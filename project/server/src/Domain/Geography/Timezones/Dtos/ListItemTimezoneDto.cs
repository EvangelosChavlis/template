using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.src.Domain.Geography.Timezones.Dtos;

public record ListItemTimezoneDto(
    string Name,
    double UtcOffset,
    bool IsActive
);