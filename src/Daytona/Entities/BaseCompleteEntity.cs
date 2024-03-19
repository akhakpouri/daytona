using System;

namespace Daytona.Entities;

public class BaseCompleteEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}