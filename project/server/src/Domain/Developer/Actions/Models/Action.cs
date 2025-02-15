// source
using server.src.Domain.Common.Models;
using server.src.Domain.Developer.Features.Models;

namespace server.src.Domain.Developer.Actions.Models;

public class Action : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public Guid FeatureId { get; set; }

    public virtual Feature Feature { get; set; }
}