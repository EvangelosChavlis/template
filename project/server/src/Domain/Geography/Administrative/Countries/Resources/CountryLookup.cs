namespace server.src.Domain.Geography.Administrative.Countries.Resources;

public record CountryLookup
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid ContinentId { get; set; }
}