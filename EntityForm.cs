using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterUtilityDispatcher.Domain;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher;
public partial class EntityForm : Form
{
    public IRefresh Page;
    public EntityForm(IRefresh page)
    {
        InitializeComponent();
        Page = page;
    }

    public void GenerateForm<T>(T? entity)
    {
        switch (typeof(T).Name)
        {
            case "Worker":
                var workerBuilder = new FormBuilder<Worker>(this, entity as Worker, Page);
                break;
            case "Brigade":
                var brigadeBuilder = new FormBuilder<Brigade>(this, entity as Brigade, Page);
                break;
            case "Incident":
                var incidentBuilder = new FormBuilder<Incident>(this, entity as Incident, Page);
                break;
            case "UsedMaterial":
                var usedMaterialBuilder = new FormBuilder<UsedMaterial>(this, entity as UsedMaterial, Page);
                break;
        }
    }
}
