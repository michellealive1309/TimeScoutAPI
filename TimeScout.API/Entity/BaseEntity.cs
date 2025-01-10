using System;

namespace TimeScout.API.Entity;

public abstract class BaseEntity
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; } = DateTime.Now;
}
