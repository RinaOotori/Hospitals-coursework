using hospitalsCoursework.UseCaseHospitals;

namespace hospitalsCoursework.Forms;

public partial class TableHospitals : TableBaseForm
{
    private readonly ComboBox _comboBoxCode = new ComboBox();
    private readonly TextBox _textBoxName = new TextBox();
    private readonly TextBox _textBoxAddress = new TextBox();
    private readonly ComboBox _comboBoxTin = new ComboBox();
    private readonly TextBox _textBoxAge = new TextBox();
    private readonly DataGridView _grid = new DataGridView();
    private Hospital _currentHospital = new Hospital();
    private readonly CheckedListBox _checkedListBoxAge = new CheckedListBox();

    public TableHospitals()
    {
        InitializeComponent();
        Load += TableHospital_Load;
    }

    private void TableHospital_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        FillDataGrid();
        Size = new Size(1300, 500);
        Text = "Больницы";
        _grid.CellClick += grid_Click;
        _comboBoxCode.SelectedIndexChanged += ComboBoxCodeClick;
        _comboBoxTin.SelectedIndexChanged += ComboBoxTinClick;
        FillComboBoxes();

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
        labelCode.Text = "Код больницы:";
        labelCode.AutoSize = true;
        labelCode.Location = new Point(_grid.Size.Width + 10, labelAddUpdate.Height + 10);

        var labelName = new Label();
        labelName.Text = "Название:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10, labelCode.Location.Y + labelCode.Size.Height + 5);

        var labelAddress = new Label();
        labelAddress.Text = "Адрес:";
        labelAddress.AutoSize = true;
        labelAddress.Location = new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        var labelTin = new Label();
        labelTin.Text = "ИНН:";
        labelTin.AutoSize = true;
        labelTin.Location = new Point(_grid.Size.Width + 10, labelAddress.Location.Y + labelAddress.Size.Height + 5);

        var labelAge = new Label();
        labelAge.Text = "Возраст:";
        labelAge.AutoSize = true;
        labelAge.Location = new Point(_grid.Size.Width + 10, labelTin.Location.Y + labelTin.Size.Height + 5);

        _comboBoxCode.Location = new Point(labelCode.Width + labelCode.Location.X, labelAddUpdate.Height + 10);
        _textBoxName.Location = new Point(labelName.Location.X + labelName.Width,
            labelCode.Location.Y + labelCode.Size.Height + 5);
        _textBoxAddress.Location = new Point(labelAddress.Location.X + labelAddress.Width,
            labelName.Location.Y + labelName.Size.Height + 5);
        _comboBoxTin.Location = new Point(labelTin.Location.X + labelTin.Width,
            labelAddress.Location.Y + labelAddress.Size.Height + 5);
        _textBoxAge.Location = new Point(labelAge.Location.X + labelAge.Width,
            labelTin.Location.Y + labelTin.Size.Height + 5);
        _textBoxName.AutoSize = true;
        _comboBoxCode.AutoSize = true;

        _checkedListBoxAge.MaximumSize = new Size(150, 150);

        var labelFilter = new Label();
        labelFilter.Text = "Фильтр значений";
        labelFilter.AutoSize = true;
        labelFilter.Location = new Point(Width - _grid.Width, _comboBoxTin.Location.Y + 80);

        _checkedListBoxAge.Location =
            new Point(_grid.Size.Width + 10, labelFilter.Location.Y + labelFilter.Height + 30);
        FillCheckedListBoxAges();

        var labelFilterAge = new Label();
        labelFilterAge.Text = labelAge.Text;
        labelFilterAge.AutoSize = true;
        labelFilterAge.Location = new Point(_checkedListBoxAge.Location.X + 1,
            labelFilter.Location.Y + labelFilter.Height + 10);

        var buttonFind = new Button();
        buttonFind.Text = "Найти значения";
        buttonFind.AutoSize = true;
        buttonFind.Location = new Point(this.Width - buttonAdd.Size.Width * 2 + 20,
            labelFilter.Location.Y + labelFilter.Height + 10);
        buttonFind.Click += FindRow_Click;

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate, buttonFind });
        Controls.AddRange(new Control[]
        {
            labelCode, labelName, labelAddress, labelTin, labelAddUpdate, labelFilter, labelFilterAge, labelAge
        });
        Controls.AddRange(new Control[] { _comboBoxCode, _textBoxName, _textBoxAddress, _comboBoxTin, _textBoxAge });
        Controls.AddRange(new Control[]
            { _checkedListBoxAge });
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetHospitalsDataHandler();
        _currentHospital = handle.Handle(new List<string>() { row.Cells[0].Value.ToString() }, new List<string>(),
                new List<string>(), new List<string>(), new List<string>())
            .FirstOrDefault();
        _comboBoxCode.Text = _currentHospital?.HospitalCode;
        _textBoxName.Text = _currentHospital?.Name;
        _textBoxAddress.Text = _currentHospital?.Address;
        _comboBoxTin.Text = _currentHospital?.Tin;
        _textBoxAge.Text = _currentHospital?.AgePatients;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void ComboBoxCodeClick(object? sender, EventArgs e)
    {
        var currentCode = _comboBoxCode.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetHospitalsDataHandler();
        if (currentCode != null)
        {
            var currentItem = handle.Handle(new List<string> { currentCode }, new List<string>(), new List<string>(),
                new List<string>(), new List<string>()).FirstOrDefault();
            _textBoxName.Text = currentItem?.Name;
            _textBoxAge.Text = currentItem?.AgePatients;
            _textBoxAddress.Text = currentItem?.Address;
            _comboBoxTin.Text = currentItem?.Tin;
        }
    }

    private void ComboBoxTinClick(object? sender, EventArgs e)
    {
        var currentTin = _comboBoxTin.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetHospitalsDataHandler();
        if (currentTin != null)
        {
            var currentItem = handle.Handle(new List<string>(), new List<string>(), new List<string>(),
                new List<string> { currentTin }, new List<string>()).FirstOrDefault();
            _textBoxName.Text = currentItem?.Name;
            _textBoxAge.Text = currentItem?.AgePatients;
            _textBoxAddress.Text = currentItem?.Address;
            _comboBoxCode.Text = currentItem?.HospitalCode;
        }
    }

    private void FillComboBoxes()
    {
        var handler = new GetHospitalsDataHandler();
        var hospitals = handler.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            new List<string>());
        FillComboBox(hospitals.Select(h => h.Tin).ToList(), _comboBoxTin);
        FillComboBox(hospitals.Select(h => h.HospitalCode).ToList(), _comboBoxCode);
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var postsColumns = new[] { "Код больницы", "Название", "Адрес", "ИНН", "Возраст" };
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

    private void ShowTablePost()
    {
        _grid.Rows.Clear();
        var handler = new GetHospitalsDataHandler();
        var hospitals = handler.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            FindCheckedItems(_checkedListBoxAge));
        for (var i = 0; i < hospitals.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = hospitals[i].HospitalCode;
            _grid.Rows[i].Cells[1].Value = hospitals[i].Name;
            _grid.Rows[i].Cells[2].Value = hospitals[i].Address;
            _grid.Rows[i].Cells[3].Value = hospitals[i].Tin;
            _grid.Rows[i].Cells[4].Value = hospitals[i].AgePatients;
            _grid.Rows[i].Cells[5].Value = "Удалить";
        }
    }

    private void FillCheckedListBoxAges()
    {
        var ages = new GetHospitalsDataHandler().Handle(new List<string>(), new List<string>(), new List<string>(),
            new List<string>(),
            new List<string>()).Select(h => h.AgePatients).Distinct().ToList();
        FillCheckedListBox(ages, _checkedListBoxAge);
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddHospitalDataHandler();
            handler.Handle(_comboBoxCode.Text, _textBoxName.Text, _textBoxAddress.Text,
                _comboBoxTin.Text, _textBoxAge.Text);
            _comboBoxCode.Items.Add(_comboBoxCode.Text);
            _comboBoxTin.Items.Add(_comboBoxTin.Text);
            UpdateControls();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException exceptionNpsql)
        {
            if (exceptionNpsql.InnerException != null && exceptionNpsql.InnerException.Message.Contains("22001"))
            {
                MessageBox.Show("Значение слишком велико!");
            }

            if (exceptionNpsql.InnerException != null && exceptionNpsql.InnerException.Message.Contains("23505"))
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
        var handler = new RemoveHospitalDataHandler();
        handler.Handle(_currentHospital.Id.ToString());
        _comboBoxCode.Items.Remove(_comboBoxCode.SelectedItem);
        _comboBoxTin.Items.Remove(_comboBoxTin.SelectedItem);
        UpdateControls();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            _comboBoxCode.Items.Remove(_currentHospital.HospitalCode);
            _comboBoxTin.Items.Remove(_currentHospital.Tin);
            var handler = new UpdateHospitalDataHandler();
            handler.Handle(_currentHospital, _comboBoxCode.Text, _textBoxName.Text, _textBoxAddress.Text,
                _comboBoxTin.Text,
                _textBoxAge.Text);
            _comboBoxCode.Items.Add(_comboBoxCode.Text);
            _comboBoxTin.Items.Add(_comboBoxTin.Text);
            UpdateControls();
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
        FillCheckedListBoxAges();
    }
}