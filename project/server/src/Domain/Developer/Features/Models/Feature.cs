// source
using server.src.Domain.Common.Models;
using Action = server.src.Domain.Developer.Actions.Models.Action;

namespace server.src.Domain.Developer.Features.Models;

public class Feature : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Action> Actions { get; set; }
}