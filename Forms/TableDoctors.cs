using hospitalsCoursework.UseCase;
using hospitalsCoursework.UseCases.UseCasesDepartment;
using hospitalsCoursework.UseCases.UseCasesDoctors;

namespace hospitalsCoursework.Forms;

public partial class TableDoctors : TableBaseForm
{
    private readonly ComboBox _comboBoxHospitalCode = new ComboBox();
    private readonly TextBox _textBoxName = new TextBox();
    private readonly ComboBox _comboBoxDepartmentCode = new ComboBox();
    private readonly ComboBox _comboBoxPostCode = new ComboBox();
    private readonly ComboBox _comboBoxTin = new ComboBox();
    private readonly DataGridView _grid = new DataGridView();
    private Doctor _currentDoctor = new Doctor();
    private readonly CheckedListBox _checkedListBoxHospitalCode = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxDepartmentCode = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxPostCode = new CheckedListBox();

    public TableDoctors()
    {
        InitializeComponent();
        Load += TableDoctor_Load;
    }

    private void TableDoctor_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        FillDataGrid();
        Size = new Size(1200, 500);
        Text = "Доктора";
        _grid.CellClick += grid_Click;
        _comboBoxTin.SelectedIndexChanged += _comboBoxTin_Click;
        FillComboBoxes();
        _comboBoxDepartmentCode.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboBoxHospitalCode.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboBoxPostCode.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboBoxHospitalCode.SelectedIndex = 0;
        _comboBoxDepartmentCode.SelectedIndex = 0;
        _comboBoxHospitalCode.SelectedIndexChanged += _comboBoxHospitalCode_Click;
        //_comboBoxDepartmentCode.SelectedIndexChanged += _comboBoxDepartmentCode_Click;

        var labelAddUpdate = new Label();
        labelAddUpdate.Text = "Добавить или изменить значение:";
        labelAddUpdate.AutoSize = true;
        labelAddUpdate.Location = new Point(this.Width - _grid.Width / 2, 0);

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

        var labelHospitalCode = new Label();
        labelHospitalCode.Text = "Код больницы:";
        labelHospitalCode.AutoSize = true;
        labelHospitalCode.Location = new Point(_grid.Size.Width + 10, labelAddUpdate.Height + 10);

        var labelName = new Label();
        labelName.Text = "ФИО:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10,
            labelHospitalCode.Location.Y + labelHospitalCode.Size.Height + 5);

        var labelDepartmentCode = new Label();
        labelDepartmentCode.Text = "Код подраз-ия:";
        labelDepartmentCode.AutoSize = true;
        labelDepartmentCode.Location =
            new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        var labelTin = new Label();
        labelTin.Text = "ИНН:";
        labelTin.AutoSize = true;
        labelTin.Location = new Point(_grid.Size.Width + 10,
            labelDepartmentCode.Location.Y + labelDepartmentCode.Size.Height + 5);

        var labelPostCode = new Label();
        labelPostCode.Text = "Код должности:";
        labelPostCode.AutoSize = true;
        labelPostCode.Location = new Point(_grid.Size.Width + 10, labelTin.Location.Y + labelTin.Size.Height + 5);

        _comboBoxHospitalCode.Location = new Point(labelHospitalCode.Width + labelHospitalCode.Location.X,
            labelHospitalCode.Location.Y + 1);
        _textBoxName.Location = new Point(labelName.Location.X + labelName.Width,
            labelName.Location.Y + 1);
        _comboBoxDepartmentCode.Location = new Point(labelDepartmentCode.Location.X + labelDepartmentCode.Width,
            labelDepartmentCode.Location.Y + 1);
        _comboBoxTin.Location = new Point(labelTin.Location.X + labelTin.Width,
            labelTin.Location.Y + 1);
        _comboBoxPostCode.Location = new Point(labelTin.Location.X + labelTin.Width,
            labelPostCode.Location.Y + 1);

        _checkedListBoxHospitalCode.MaximumSize = new Size(150, 150);
        _checkedListBoxDepartmentCode.MaximumSize = new Size(150, 150);
        _checkedListBoxPostCode.MaximumSize = new Size(150, 150);
        
        var labelFilter = new Label();
        labelFilter.Text = "Фильтр значений";
        labelFilter.AutoSize = true;
        labelFilter.Location = new Point(Width - _grid.Width / 2, _comboBoxPostCode.Location.Y + 80);
        
        var buttonFind = new Button();
        buttonFind.Text = "Найти значения";
        buttonFind.AutoSize = true;
        buttonFind.Location = new Point(this.Width - buttonAdd.Size.Width * 2 + 20,
            labelFilter.Location.Y + labelFilter.Height + 10);
        buttonFind.Click += FindRow_Click;

        var labelFilterHospitalCode = new Label();
        labelFilterHospitalCode.Text = labelHospitalCode.Text;
        labelFilterHospitalCode.AutoSize = true;
        labelFilterHospitalCode.Location =
            new Point(_grid.Size.Width + 10, labelFilter.Location.Y + labelFilter.Height);
        
        var labelFilterDepartmentCode = new Label();
        labelFilterDepartmentCode.Text = labelDepartmentCode.Text;
        labelFilterDepartmentCode.AutoSize = true;
        labelFilterDepartmentCode.Location =
            new Point(labelFilterHospitalCode.Width + labelFilterHospitalCode.Location.X + 30, labelFilter.Location.Y + labelFilter.Height);
        
        var labelFilterPostCode = new Label();
        labelFilterPostCode.Text = labelPostCode.Text;
        labelFilterPostCode.AutoSize = true;
        labelFilterPostCode.Location =
            new Point(labelFilterDepartmentCode.Width + labelFilterDepartmentCode.Location.X + 30, labelFilter.Location.Y + labelFilter.Height);

        _checkedListBoxHospitalCode.Location =
            new Point(labelFilterHospitalCode.Location.X + 1, labelFilterHospitalCode.Location.Y + labelFilterHospitalCode.Height + 5);        
        _checkedListBoxDepartmentCode.Location =
            new Point(labelFilterDepartmentCode.Location.X + 1, labelFilterDepartmentCode.Location.Y + labelFilterDepartmentCode.Height + 5);        
        _checkedListBoxPostCode.Location =
            new Point(labelFilterPostCode.Location.X + 1, labelFilterPostCode.Location.Y + labelFilterPostCode.Height + 5);
        FillCheckedListBoxes();

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate, buttonFind });
        Controls.AddRange(new Control[]
        {
            labelHospitalCode, labelName, labelPostCode, labelTin, labelAddUpdate, labelDepartmentCode, labelFilter,
            labelFilterHospitalCode, labelFilterDepartmentCode, labelFilterPostCode
        });
        Controls.AddRange(new Control[]
            { _comboBoxHospitalCode, _textBoxName, _comboBoxPostCode, _comboBoxTin, _comboBoxDepartmentCode });
        Controls.AddRange(new Control[] { _checkedListBoxHospitalCode, _checkedListBoxDepartmentCode, _checkedListBoxPostCode });
    }

    private void FillCheckedListBoxes()
    {
        var handler = new GetDoctorsDataHandler();
        var doctors = handler.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            new List<string>());
        FillCheckedListBox(doctors.Select(d => d.HospitalCode).Distinct().ToList(), _checkedListBoxHospitalCode);
        FillCheckedListBox(doctors.Select(d => d.DepartmentCode).Distinct().ToList(), _checkedListBoxDepartmentCode);
        FillCheckedListBox(doctors.Select(d => d.PostCode).Distinct().ToList(), _checkedListBoxPostCode);
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetDoctorsDataHandler();
        _currentDoctor = handle.Handle(new List<string>(), new List<string>(), new List<string>(),
                new List<string> { row.Cells[3].Value.ToString() }, new List<string>())
            .FirstOrDefault();        
        _comboBoxHospitalCode.Text = _currentDoctor?.HospitalCode;
        _comboBoxTin.Text = _currentDoctor?.Tin;
        _textBoxName.Text = _currentDoctor?.Name;
        _comboBoxPostCode.Text = _currentDoctor?.PostCode;
        _comboBoxDepartmentCode.Text = _currentDoctor?.DepartmentCode;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void _comboBoxHospitalCode_Click(object? sender, EventArgs e)
    {
        _comboBoxDepartmentCode.Items.Clear();
        _comboBoxDepartmentCode.Items.Add("Все");
        var departmentsCode = new GetDepartmentsDataHandler().Handle(new List<string>() { _comboBoxHospitalCode.Text }, new List<string>(),
                new List<string>(), new List<string>()).Select(d => d.DepartmentCode).Distinct().ToList();
        FillComboBox(departmentsCode, _comboBoxDepartmentCode);
    }

    private void _comboBoxDepartmentCode_Click(object? sender, EventArgs e)
    {
        var hc = _comboBoxHospitalCode.Text;
        if (_comboBoxDepartmentCode.Text == "Все")
        {
            _comboBoxHospitalCode.Items.Clear();
            _comboBoxHospitalCode.Items.Add("Все");
            var handler = new GetDepartmentsDataHandler();
            var hospitalsCode = handler.Handle(new List<string>(), new List<string>(),
                new List<string>(), new List<string>()).Select(d => d.HospitalCode).Distinct();
            foreach (var code in hospitalsCode)
            {
                _comboBoxHospitalCode.Items.Add(code);
            }
        }
        else
        {
            var handler = new GetHopsitalsCodesByDepartmentCodeDataHandler();
            var hospitalsCode = handler.Handle(_comboBoxDepartmentCode.Text).Distinct();
            _comboBoxHospitalCode.Items.Clear();
            _comboBoxHospitalCode.Items.Add("Все");
            foreach (var hospital in hospitalsCode)
            {
                _comboBoxHospitalCode.Items.Add(hospital);
            }
        }

        _comboBoxHospitalCode.Text = hc;
    }


    private void _comboBoxTin_Click(object? sender, EventArgs e)
    {
        var currentTin = _comboBoxTin.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetDoctorsDataHandler();
        var currentItem = handle.Handle(new List<string>(), new List<string>(), new List<string>(),
            new List<string> { currentTin }, new List<string>()).FirstOrDefault();
        _textBoxName.Text = currentItem?.Name;
        _comboBoxPostCode.Text = currentItem?.PostCode;
        _comboBoxHospitalCode.Text = currentItem?.HospitalCode;
        _comboBoxDepartmentCode.Text = currentItem?.DepartmentCode;
        _comboBoxTin.Text = currentItem?.Tin;
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var postsColumns = new[] { "Код больницы", "Код подразделения", "Имя", "ИНН", "Код должности" };
        foreach (var value in postsColumns)
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
        UpdateControls();
    }

    private void FillComboBoxes()
    {
        var doctorsTin = new GetDoctorsDataHandler().Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            new List<string>()).Select(d => d.Tin).ToList();
        FillComboBox(doctorsTin, _comboBoxTin);

        var handlerDepartments = new GetDepartmentsDataHandler();
        var departments = handlerDepartments.Handle(new List<string>(), new List<string>(), new List<string>(),
            new List<string>());
        _comboBoxHospitalCode.Items.Add("Все");
        _comboBoxDepartmentCode.Items.Add("Все");
        FillComboBox(departments.Select(d => d.DepartmentCode).ToList(), _comboBoxDepartmentCode);
        FillComboBox(departments.Select(d => d.HospitalCode).Distinct().ToList(), _comboBoxHospitalCode);

        var postsCode = new GetPostsDataHandler().Handle(new List<string>(), new List<string>()).Select(p => p.PostCode).ToList();
        FillComboBox(postsCode, _comboBoxPostCode);
    }

    private void ShowTablePost()
    {
        _grid.Rows.Clear();

        var handler = new GetDoctorsDataHandler();
        var posts = handler.Handle(FindCheckedItems(_checkedListBoxHospitalCode), new List<string>(), FindCheckedItems(_checkedListBoxDepartmentCode), new List<string>(),
            FindCheckedItems(_checkedListBoxPostCode));
        for (var i = 0; i < posts.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = posts[i].HospitalCode;
            _grid.Rows[i].Cells[1].Value = posts[i].DepartmentCode;
            _grid.Rows[i].Cells[2].Value = posts[i].Name;
            _grid.Rows[i].Cells[3].Value = posts[i].Tin;
            _grid.Rows[i].Cells[4].Value = posts[i].PostCode;
            _grid.Rows[i].Cells[5].Value = "Удалить";
        }
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddDoctorDataHandler();
            handler.Handle(_comboBoxHospitalCode.Text, _textBoxName.Text, _comboBoxDepartmentCode.Text,
                _comboBoxTin.Text,
                _comboBoxPostCode.Text);
            UpdateControls();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exceptionNpsql)
        {
            if (exceptionNpsql.InnerException.Message.Contains("22001"))
            {
                MessageBox.Show("Значение слишком велико!");
            }

            if (exceptionNpsql.InnerException.Message.Contains("23505"))
            {
                MessageBox.Show("Значение с таким кодом уже есть!");
            }
        }
        catch (FormatException exception)
        {
            MessageBox.Show($"{exception.Message}");
        }
    }

    private void RemoveRow_Click()
    {
        var handler = new RemoveDoctorDataHandler();
        handler.Handle(_currentDoctor.Id.ToString());
        _comboBoxTin.Items.Remove(_currentDoctor.PostCode);
        UpdateControls();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            _comboBoxTin.Items.Remove(_currentDoctor.Tin);
            var handler = new UpdateDoctorDataHandler();
            handler.Handle(_currentDoctor, _comboBoxHospitalCode.Text, _textBoxName.Text, _comboBoxDepartmentCode.Text,
                _comboBoxTin.Text, _comboBoxPostCode.Text);
            UpdateControls();
            _comboBoxTin.Items.Add(_comboBoxTin.Text);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exceptionNpsql)
        {
            if (exceptionNpsql.InnerException.Message.Contains("22001"))
            {
                MessageBox.Show("Значение слишком велико!");
            }

            if (exceptionNpsql.InnerException.Message.Contains("23505"))
            {
                MessageBox.Show("Значение с таким кодом уже есть!");
            }
        }
        catch (FormatException exception)
        {
            MessageBox.Show($"{exception.Message}");
        }
    }
    
    private void FindRow_Click(object? sender, EventArgs e)
    {
        ShowTablePost();
    }

    private void UpdateControls()
    {
        ShowTablePost();
        FillCheckedListBoxes();
    }
}