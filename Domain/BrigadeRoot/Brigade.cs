using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher.Domain.BrigadeRoot;
public sealed class Brigade : EntityBase
{
    public Brigade()
    {

    }

    public string Name { get; set; }
    [Browsable(false)]
    public List<Worker> Workers { get; set; } = [];
    public int WorkersAmount => Workers.Count;
}
