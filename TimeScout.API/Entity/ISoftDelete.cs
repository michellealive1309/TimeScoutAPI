using System;

namespace TimeScout.API.Entity;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}
