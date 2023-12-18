using hospitalsCoursework.UseCaseHospitals;
using hospitalsCoursework.UseCases.UseCasesDepartment;

namespace hospitalsCoursework.Forms;

public partial class TableDepartments : TableBaseForm
{
    private readonly ComboBox _comboBoxNewCode = new ComboBox();
    private readonly TextBox _textBoxNewName = new TextBox();
    private readonly ComboBox _comboBoxNewHospitalCode = new ComboBox();
    private readonly TextBox _textBoxNewManager = new TextBox();
    private readonly DataGridView _grid = new DataGridView();
    private Department _currentDepartment = new Department();

    public TableDepartments()
    {
        InitializeComponent();
        Load += TableDepartments_Load;
    }

    private void TableDepartments_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        FillDataGrid();
        Size = new Size(1200, 500);
        Text = "Подразделения";
        _grid.CellClick += grid_Click;
        FillComboBoxes();
        _comboBoxNewCode.SelectedIndexChanged += ComboBoxCode_Click;

        var labelAddUpdate = new Label();
        labelAddUpdate.Text = "Добавить или изменить значение:";
        labelAddUpdate.AutoSize = true;
        labelAddUpdate.Location = new Point(this.Width - _grid.Width, 0);

        var buttonAdd = new Button();
        buttonAdd.Text = "Добавить значение";
        buttonAdd.AutoSize = true;
        buttonAdd.Location = new Point(this.Width - buttonAdd.Size.Width * 2, labelAddUpdate.Height + 10);
        buttonAdd.Click += AddRow_Click;

        var buttonUpdate = new Button();
        buttonUpdate.Text = "Сохранить новое значение";
        buttonUpdate.AutoSize = true;
        buttonUpdate.Location = new Point(this.Width - buttonAdd.Size.Width * 2 - buttonUpdate.Size.Width + 31,
            buttonAdd.Location.Y + buttonAdd.Size.Height + 5);
        buttonUpdate.Click += UpdateRow_Click;

        var labelCode = new Label();
        labelCode.Text = "Код:";
        labelCode.AutoSize = true;
        labelCode.Location = new Point(_grid.Size.Width + 10, labelAddUpdate.Height + 10);

        var labelName = new Label();
        labelName.Text = "Название:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10, labelCode.Location.Y + labelCode.Size.Height + 5);

        var labelHospitalCode = new Label();
        labelHospitalCode.Text = "Код больницы:";
        labelHospitalCode.AutoSize = true;
        labelHospitalCode.Location = new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        var labelManager = new Label();
        labelManager.Text = "Заведующий:";
        labelManager.AutoSize = true;
        labelManager.Location = new Point(_grid.Size.Width + 10,
            labelHospitalCode.Location.Y + labelHospitalCode.Size.Height + 5);

        _comboBoxNewCode.Location = new Point(labelCode.Width + labelCode.Location.X, labelAddUpdate.Height + 10);
        _textBoxNewName.Location = new Point(labelName.Location.X + labelName.Width,
            labelCode.Location.Y + labelCode.Size.Height + 5);
        _comboBoxNewHospitalCode.Location = new Point(labelHospitalCode.Location.X + labelHospitalCode.Width,
            labelName.Location.Y + labelName.Size.Height + 5);
        _textBoxNewManager.Location = new Point(labelManager.Location.X + labelManager.Width,
            labelHospitalCode.Location.Y + labelHospitalCode.Size.Height + 5);
        _textBoxNewName.AutoSize = true;
        _comboBoxNewCode.AutoSize = true;

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate });
        Controls.AddRange(new Control[]
        {
            labelCode, labelName, labelHospitalCode, labelManager, labelAddUpdate
        });
        Controls.AddRange(new Control[]
            { _comboBoxNewCode, _textBoxNewName, _comboBoxNewHospitalCode, _textBoxNewManager });
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetDepartmentsDataHandler();
        _currentDepartment = handle.Handle(new List<string>(), new List<string>() { row.Cells[0].Value.ToString() },
                new List<string>(), new List<string>())
            .FirstOrDefault();
        _comboBoxNewCode.Text = _currentDepartment?.DepartmentCode;
        _textBoxNewName.Text = _currentDepartment?.Name;
        _comboBoxNewHospitalCode.Text = _currentDepartment?.HospitalCode;
        _textBoxNewManager.Text = _currentDepartment?.Manager;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void ComboBoxCode_Click(object? sender, EventArgs e)
    {
        var currentCode = _comboBoxNewCode.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetDepartmentsDataHandler();
        if (currentCode != null)
        {
            var currentItem = handle
                .Handle(new List<string>(), new List<string> { currentCode }, new List<string>(), new List<string>())
                .FirstOrDefault();
            _textBoxNewManager.Text = currentItem?.Manager;
            _textBoxNewName.Text = currentItem?.Name;
            _comboBoxNewHospitalCode.Text = currentItem?.HospitalCode;
        }
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var departmentsColumns = new[] { "Код подразделения", "Название", "Код больницы", "Заведующий" };
        foreach (var value in departmentsColumns)
        {
            _grid.Columns.Add(value, value);
        }

        var buttonColumn = new DataGridViewButtonColumn();
        buttonColumn.Text = "";
        _grid.Columns.Add(buttonColumn);
        _grid.RowHeadersVisible = false;
        _grid.AllowUserToAddRows = false;
        _grid.ReadOnly = true;
        _grid.AutoSize = true;
        ShowTablePost();
    }

    private void FillComboBoxes()
    {
        var hospitalCodes = new GetHospitalsDataHandler().Handle(new List<string>(), new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>()).Select(h => h.HospitalCode).ToList();
        FillComboBox(hospitalCodes, _comboBoxNewHospitalCode);

        var departmentCodes = new GetDepartmentsDataHandler()
            .Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>())
            .Select(d => d.DepartmentCode).ToList();
        FillComboBox(departmentCodes, _comboBoxNewCode);
    }

    private void ShowTablePost()
    {
        _grid.Rows.Clear();
        var handler = new GetDepartmentsDataHandler();
        var hospitals = handler.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>());
        for (var i = 0; i < hospitals.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = hospitals[i].DepartmentCode;
            _grid.Rows[i].Cells[1].Value = hospitals[i].Name;
            _grid.Rows[i].Cells[2].Value = hospitals[i].HospitalCode;
            _grid.Rows[i].Cells[3].Value = hospitals[i].Manager;
            _grid.Rows[i].Cells[4].Value = "Удалить";
        }
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddDepartmentDataHandler();
            handler.Handle(_comboBoxNewHospitalCode.Text, _comboBoxNewCode.Text, _textBoxNewName.Text,
                _textBoxNewManager.Text);
            ShowTablePost();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exceptionNpsql)
        {
            if (exceptionNpsql.InnerException != null && exceptionNpsql.InnerException.Message.Contains("22001"))
            {
                MessageBox.Show("Значение слишком велико!");
            }
        }
        catch (FormatException exception)
        {
            MessageBox.Show($"{exception.Message}");
        }
    }

    private void RemoveRow_Click()
    {
        var handler = new RemoveDepartmentDataHandler();
        handler.Handle(_currentDepartment.Id.ToString());
        _comboBoxNewCode.Items.Remove(_currentDepartment.DepartmentCode);
        ShowTablePost();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var newCode = _comboBoxNewCode.Text;
            var newName = _textBoxNewName.Text;
            var newHospitalCode = _comboBoxNewHospitalCode.Text;
            var newManager = _textBoxNewManager.Text;
            var handler = new UpdateDepartmentDataHandler();
            handler.Handle(_currentDepartment, newHospitalCode, newCode, newName, newManager);
            ShowTablePost();
        }
        catch (FormatException exception)
        {
            MessageBox.Show($"{exception.Message}");
        }
    }
}