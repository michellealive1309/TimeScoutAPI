using System;

namespace TimeScout.API.Entity;

public abstract class BaseEntity : ISoftDelete
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
}
