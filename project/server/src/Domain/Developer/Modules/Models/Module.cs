// source
using server.src.Domain.Common.Models;
using server.src.Domain.Developer.Aggreggates.Models;
using server.src.Domain.Developer.Features.Models;

namespace server.src.Domain.Developer.Modules.Models;

public class Module : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public Guid AggregateId { get; set; }

    public virtual Aggregate Aggregate { get; set; }
    public virtual List<Feature> Features { get; set; }
}