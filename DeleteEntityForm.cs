using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterUtilityDispatcher.Domain.Common;

namespace WaterUtilityDispatcher;
public partial class DeleteEntityForm : Form
{
    private Guid? _entityId { get; set; }
    public DeleteEntityForm(Guid? entityId)
    {
        _entityId = entityId;
        InitializeComponent();
        label2.Text = _entityId.ToString();
    }

    private void YesButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
        Close();
    }

    private void NoButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }
}
