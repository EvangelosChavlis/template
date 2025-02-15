// source
using server.src.Domain.Common.Models;
using server.src.Domain.Developer.Modules.Models;

namespace server.src.Domain.Developer.Aggreggates.Models;

public class Aggregate : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Module> Modules { get; set; }
}