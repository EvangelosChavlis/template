namespace server.src.Domain.Geography.Administrative.Municipalities.Resources;

public record MunicipalityLookup
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid RegionId { get; set; }
}