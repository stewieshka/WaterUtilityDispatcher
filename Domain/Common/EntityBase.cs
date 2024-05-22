using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterUtilityDispatcher.Domain.Common;
public abstract class EntityBase
{
    public EntityBase()
    {

    }

    public EntityBase(Guid id)
    {
        Id = id;
    }

    [Browsable(false)]
    public Guid? Id { get; set; }
}