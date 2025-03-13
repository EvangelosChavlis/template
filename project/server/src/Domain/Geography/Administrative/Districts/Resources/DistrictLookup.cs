namespace server.src.Domain.Geography.Administrative.Districts.Resources;

public class DistrictLookup
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid MunicipalityId { get; set; }
}