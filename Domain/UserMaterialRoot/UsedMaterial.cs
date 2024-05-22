using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.IncidentRoot;

namespace WaterUtilityDispatcher.Domain.UserMaterialRoot;
public class UsedMaterial : EntityBase
{
    public UsedMaterial()
    {

    }
    public string Name { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    [Browsable(false)]

    public Incident? Incident { get; set; }
}