using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;

namespace WaterUtilityDispatcher.Domain.IncidentRoot;
public class Incident : EntityBase
{
    public Incident()
    {

    }

    public string Type { get; set; }
    public string Address { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    [Browsable(false)]
    public Brigade? Brigade { get; set; }
    public string Priority { get; set; }
    [Browsable(false)]
    public List<UsedMaterial> UsedMaterials { get; set; } = [];
    public string Status { get; set; }
}
