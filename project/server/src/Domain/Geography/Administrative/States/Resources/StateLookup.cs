namespace server.src.Domain.Geography.Administrative.States.Resources;

public record StateLookup
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid CountryId { get; set; }
}