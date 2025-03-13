namespace server.src.Domain.Geography.Administrative.Regions.Resources;

public record RegionLookup
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid StateId { get; set; }
}