using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Data;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher.Domain;
public class CustomTabPage<T> : TabPage, IRefresh
    where T : EntityBase
{
    private Label TabName { get; set; }
    private Button AddButton { get; set; }
    private DataGridView EntityGridView { get; set; }
    private Button PreviousPageButton { get; set; }
    private Button NextPageButton { get; set; }
    private Label PageLabel { get; set; }
    private Label CountLabel { get; set; }

    private int _page { get; set; } = 1;
    private const int _pageSize = 10;
    private int _totalCount { get; set; } = 0;
    private int _maxPage { get; set; } = 0;

    public List<T> Entities { get; set; } = new(10);

    public CustomTabPage()
    {
        //
        TabName = new Label
        {
            Text = typeof(T).Name,
            Location = new Point(0, 0),
            Size = new Size(100, 15)
        };
        Controls.Add(TabName);
        //
        AddButton = new Button
        {
            Text = "Добавить",
            Location = new Point(0, 18),
            Size = new Size(75, 23)
        };
        AddButton.Click += AddButton_Click;
        Controls.Add(AddButton);
        //
        EntityGridView = new DataGridView
        {
            Location = new Point(0, 76),
            Size = new Size(685, 304)
        };
        Controls.Add(EntityGridView);
        //
        PreviousPageButton = new Button
        {
            Location = new Point(0, 386),
            Size = new Size(38, 23),
            Text = "<<"
        };
        PreviousPageButton.Click += PrevPage_Click;
        Controls.Add(PreviousPageButton);
        //
        NextPageButton = new Button
        {
            Location = new Point(44, 386),
            Size = new Size(38, 23),
            Text = ">>"
        };
        NextPageButton.Click += NextPage_Click;
        Controls.Add(NextPageButton);
        //
        PageLabel = new Label
        {
            Location = new Point(88, 390),
            Size = new Size(50, 15),
            Text = "1/1"
        };
        Controls.Add(PageLabel);
        //
        CountLabel = new Label
        {
            Location = new Point(640, 390),
            Size = new Size(50, 15),
            Text = "0"
        };
        Controls.Add(CountLabel);

        AddButtonColumns();
        RefreshList();
    }

    public void RefreshList()
    {
        using var context = new AppDbContext();
        IQueryable<T> entities = context.Set<T>();

        if (typeof(T) == typeof(Worker))
        {
            entities = entities.Include("Brigade");
        }
        else if (typeof(T) == typeof(Brigade))
        {
            entities = entities.Include("Workers");
        }
        else if (typeof(T) == typeof(Incident))
        {
            entities = entities.Include("UsedMaterials");
            entities = entities.Include("Brigade");
        }
        else if (typeof(T) == typeof(UsedMaterial))
        {
            entities = entities.Include("Incident");
        }

        _totalCount = entities.Count();

        CountLabel.Text = _totalCount.ToString();

        _maxPage = (_totalCount + _pageSize - 1) / _pageSize;

        if (_maxPage == 0)
        {
            _maxPage = 1;
        }

        PageLabel.Text = $"{_page}/{_maxPage}";

        var entitiesList = entities
            .Skip((_page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        EntityGridView.DataSource = null;

        EntityGridView.DataSource = entitiesList;
    }

    private void NextPage_Click(object sender, EventArgs e)
    {
        if (_page == _maxPage)
        {
            return;
        }
        _page++;
        RefreshList();
    }

    private void PrevPage_Click(object sender, EventArgs e)
    {
        if (_page == 1)
        {
            return;
        }
        _page--;
        RefreshList();
    }

    private void AddButtonColumns()
    {
        var editButtonColumn = new DataGridViewButtonColumn
        {
            Name = "editButtonColumn",
            Text = "Изменить",
            UseColumnTextForButtonValue = true,
        };
        EntityGridView.Columns.Add(editButtonColumn);

        var deleteButtonColumn = new DataGridViewButtonColumn
        {
            Name = "deleteButtonColumn",
            Text = "Удалить",
            UseColumnTextForButtonValue = true
        };
        EntityGridView.Columns.Add(deleteButtonColumn);

        EntityGridView.CellClick += EntityGridView_CellClick;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var form = new EntityForm(this);
        form.GenerateForm<T>(null);
        form.Show();
    }

    private void EntityGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        var grid = (DataGridView)sender;


        if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && grid[e.ColumnIndex, e.RowIndex].Value != null)
        {
            var entity = (T)grid.Rows[e.RowIndex].DataBoundItem;

            if (grid.Columns[e.ColumnIndex].Name == "editButtonColumn")
            {
                EditEntity(entity);
            }
            else if (grid.Columns[e.ColumnIndex].Name == "deleteButtonColumn")
            {
                DeleteEntity(entity);
            }
        }
    }

    private void EditEntity(T entity)
    {
        var form = new EntityForm(this);
        form.GenerateForm<T>(entity);
        form.Show();
    }

    private void DeleteEntity(T entity)
    {
        using var form = new DeleteEntityForm(entity.Id);

        var result = form.ShowDialog();

        if (result != DialogResult.Yes)
        {
            return;
        }

        using var context = new AppDbContext();
        context.Set<T>().Remove(entity);
        context.SaveChanges();

        RefreshList();
    }
}
