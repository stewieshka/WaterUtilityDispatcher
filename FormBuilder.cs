using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Data;
using WaterUtilityDispatcher.Domain;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.Common;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;

namespace WaterUtilityDispatcher;
public class FormBuilder<T> 
    where T : EntityBase, new()
{
    public Form Form;
    public Button SubmitButton;
    public T Entity;
    private int _currentPosition = 0;
    public IRefresh Page;
    private string submitButtonText;

    private ListBox materialsListBox;
    private TextBox materialNameTextBox;
    private TextBox materialAmountTextBox;
    private TextBox materialUnitTextBox;
    private List<UsedMaterial> usedMaterials;

    public delegate void MaterialAddedHandler(UsedMaterial material);
    public delegate void MaterialRemovedHandler(UsedMaterial material);

    public event MaterialAddedHandler? MaterialAdded;
    public event MaterialRemovedHandler? MaterialRemoved;

    public FormBuilder(Form form, T? entity, IRefresh page)
    {
        Entity = entity ?? new T();
        Form = form;
        Page = page;
        usedMaterials = Entity is Incident incident ? incident.UsedMaterials : new List<UsedMaterial>();

        submitButtonText = entity == null ? "Добавить" : "Изменить";

        Form.AutoSize = true;
        Form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Form.StartPosition = FormStartPosition.CenterScreen;

        CreateForm();
    }

    public void CreateForm()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.Name == "Id" ||
                property.Name == "Workers" ||
                property.Name == "WorkersAmount" ||
                property.Name == "BrigadeName")
            {
                continue;
            }

            if (property.Name == "UsedMaterials")
            {
                CreateMaterials(property);
                continue;
            }

            CreateLabel(property.Name);
            CreateTextBox(property);
        }
        CreateButton();
    }

    private void CreateMaterials(PropertyInfo property)
    {
        materialsListBox = new ListBox
        {
            Size = new Size(224, 94),
            Location = new Point(12, _currentPosition),
            DataSource = usedMaterials.ToList(),
            DisplayMember = "Name"
        };
        Form.Controls.Add(materialsListBox);

        materialsListBox.SelectedIndexChanged += (s, e) => UpdateMaterialDetails();

        _currentPosition += 100;

        materialNameTextBox = new TextBox { Location = new Point(12, _currentPosition), Width = 100 };
        Form.Controls.Add(materialNameTextBox);

        materialAmountTextBox = new TextBox { Location = new Point(118, _currentPosition), Width = 50 };
        Form.Controls.Add(materialAmountTextBox);

        materialUnitTextBox = new TextBox { Location = new Point(174, _currentPosition), Width = 50 };
        Form.Controls.Add(materialUnitTextBox);

        var addButton = new Button
        {
            Text = "Add",
            Location = new Point(230, _currentPosition)
        };
        addButton.Click += AddMaterial;
        Form.Controls.Add(addButton);

        var removeButton = new Button
        {
            Text = "Remove",
            Location = new Point(310, _currentPosition)
        };
        removeButton.Click += RemoveMaterial;
        Form.Controls.Add(removeButton);

        _currentPosition += 30;

        RefreshMaterialsListBox();
    }

    private void UpdateMaterialDetails()
    {
        if (materialsListBox.SelectedItem is UsedMaterial selectedMaterial)
        {
            materialNameTextBox.Text = selectedMaterial.Name;
            materialAmountTextBox.Text = selectedMaterial.Amount.ToString();
            materialUnitTextBox.Text = selectedMaterial.Unit;
        }
    }

    private void AddMaterial(object sender, EventArgs e)
    {
        var newMaterial = new UsedMaterial
        {
            Name = materialNameTextBox.Text,
            Amount = int.TryParse(materialAmountTextBox.Text, out var amount) ? amount : 0,
            Unit = materialUnitTextBox.Text
        };
        usedMaterials.Add(newMaterial);
        RefreshMaterialsListBox();
        MaterialAdded?.Invoke(newMaterial); // Raise the MaterialAdded event
    }

    private void RemoveMaterial(object sender, EventArgs e)
    {
        if (materialsListBox.SelectedItem is UsedMaterial selectedMaterial)
        {
            usedMaterials.Remove(selectedMaterial);
            RefreshMaterialsListBox();
            MaterialRemoved?.Invoke(selectedMaterial); // Raise the MaterialRemoved event
        }
    }

    private void RefreshMaterialsListBox()
    {
        materialsListBox.DataSource = null;
        materialsListBox.DataSource = usedMaterials.ToList();
        materialsListBox.DisplayMember = "Name";
    }

    private void CreateLabel(string propertyName)
    {
        var label = new Label
        {
            Text = propertyName,
            Top = _currentPosition
        };
        Form.Controls.Add(label);
        _currentPosition += label.Height;
    }

    private void CreateTextBox(PropertyInfo property)
    {
        var textBox = new TextBox
        {
            Top = _currentPosition,
            Tag = property
        };

        var value = property.GetValue(Entity);
        if (value is Brigade brigade)
        {
            textBox.Text = brigade.Name;
        }
        else if (value is not null)
        {
            textBox.Text = value.ToString();
        }

        Form.Controls.Add(textBox);
        _currentPosition += textBox.Height + 20;
    }

    private void CreateButton()
    {
        SubmitButton = new Button
        {
            Text = submitButtonText,
            Top = _currentPosition
        };
        SubmitButton.Click += SubmitButton_Click;
        Form.Controls.Add(SubmitButton);
        _currentPosition += SubmitButton.Height + 20;
    }

    private void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            using var context = new AppDbContext();
            foreach (var control in Form.Controls)
            {
                if (control is TextBox textBox && textBox.Tag is PropertyInfo property)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        if (!DateTime.TryParse(textBox.Text, out var myDateTime))
                        {
                            throw new Exception($"В поле '{property.Name}' должна быть дата");
                        }

                        var utcDateTime = DateTime.SpecifyKind(myDateTime, DateTimeKind.Utc);
                        property.SetValue(Entity, utcDateTime);
                        continue;
                    }

                    if (property.Name == "Brigade")
                    {
                        var brigade = context.Brigades.FirstOrDefault(x => x.Name == textBox.Text)
                            ?? throw new Exception("Бригады с таким названием не существует");

                        property.SetValue(Entity, brigade);
                        continue;
                    }

                    property.SetValue(Entity, Convert.ChangeType(textBox.Text, property.PropertyType));
                }
            }

            if (Entity.Id == Guid.Empty)
            {
                Entity.Id = Guid.NewGuid();
                context.Set<T>().Add(Entity);
            }
            else
            {
                context.Set<T>().Update(Entity);
            }

            context.SaveChanges();
            Page.RefreshList();
            Form.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла ошибка: {ex.Message}");
        }
    }
}

