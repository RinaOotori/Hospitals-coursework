using hospitalsCoursework.UseCasesDiagnoses;

namespace hospitalsCoursework.Forms;

public partial class TableDiagnoses : TableBaseForm
{
    private readonly ComboBox _comboBoxCode = new ComboBox();
    private readonly TextBox _textBoxName = new TextBox();
    private readonly TextBox _textBoxMethod = new TextBox();
    private readonly DataGridView _grid = new DataGridView();
    private Diagnosis _currentDiagnosis = new Diagnosis();

    public TableDiagnoses()
    {
        InitializeComponent();
        Load += TableDiagnosis_Load;
    }

    private void TableDiagnosis_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        FillDataGrid();
        Size = new Size(1000, 500);
        Text = "Диагнозы";
        _grid.CellClick += grid_Click;
        _comboBoxCode.SelectedIndexChanged += ComboBoxCodeClick;
        FillComboBoxCode();

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
        labelCode.Text = "Код диагноза:";
        labelCode.AutoSize = true;
        labelCode.Location = new Point(_grid.Size.Width + 10, labelAddUpdate.Height + 10);

        var labelName = new Label();
        labelName.Text = "Название:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10, labelCode.Location.Y + labelCode.Size.Height + 5);

        var labelMethod = new Label();
        labelMethod.Text = "Метод лечения:";
        labelMethod.AutoSize = true;
        labelMethod.Location = new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        _comboBoxCode.Location = new Point(labelCode.Width + labelCode.Location.X, labelAddUpdate.Height + 10);
        _textBoxName.Location = new Point(labelName.Location.X + labelName.Width,
            labelCode.Location.Y + labelCode.Size.Height + 5);
        _textBoxMethod.Location = new Point(labelMethod.Location.X + labelMethod.Width,
            labelName.Location.Y + labelName.Size.Height + 5);
        _textBoxName.AutoSize = true;
        _comboBoxCode.AutoSize = true;

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate });
        Controls.AddRange(new Control[]
        {
            labelCode, labelName, labelMethod, labelAddUpdate
        });
        Controls.AddRange(new Control[] { _comboBoxCode, _textBoxName, _textBoxMethod });
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetDiagnosesDataHandler();
        _currentDiagnosis = handle.Handle(new List<string>() { row.Cells[0].Value.ToString() }, new List<string>(),
                new List<string>())
            .FirstOrDefault();
        _comboBoxCode.Text = _currentDiagnosis?.DiagnosisCode;
        _textBoxName.Text = _currentDiagnosis?.Name;
        _textBoxMethod.Text = _currentDiagnosis?.TreatmentMethod;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void ComboBoxCodeClick(object? sender, EventArgs e)
    {
        var currentCode = _comboBoxCode.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetDiagnosesDataHandler();
        if (currentCode != null)
        {
            var currentItem = handle.Handle(new List<string> { currentCode }, new List<string>(), new List<string>())
                .FirstOrDefault();
            _textBoxName.Text = currentItem?.Name;
            _textBoxMethod.Text = currentItem?.TreatmentMethod;
        }
    }

    private void FillComboBoxCode()
    {
        var diagnosisCodes = new GetDiagnosesDataHandler()
            .Handle(new List<string>(), new List<string>(), new List<string>()).Select(d => d.DiagnosisCode).ToList();
        FillComboBox(diagnosisCodes, _comboBoxCode);
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var postsColumns = new[] { "Код диагноза", "Название", "Метод лечения" };
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
        ShowTablePost();
    }

    private void ShowTablePost()
    {
        _grid.Rows.Clear();
        var handler = new GetDiagnosesDataHandler();
        var diagnoses = handler.Handle(new List<string>(), new List<string>(), new List<string>());
        for (var i = 0; i < diagnoses.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = diagnoses[i].DiagnosisCode;
            _grid.Rows[i].Cells[1].Value = diagnoses[i].Name;
            _grid.Rows[i].Cells[2].Value = diagnoses[i].TreatmentMethod;
            _grid.Rows[i].Cells[3].Value = "Удалить";
        }
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddDiagnosisDataHandler();
            handler.Handle(_comboBoxCode.Text, _textBoxName.Text, _textBoxMethod.Text);
            ShowTablePost();
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
        var handler = new RemoveDiagnosisDataHandler();
        handler.Handle(_currentDiagnosis.Id.ToString());
        _comboBoxCode.Items.Clear();
        ShowTablePost();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var newCode = _comboBoxCode.Text;
            var newName = _textBoxName.Text;
            var newMethod = _textBoxMethod.Text;
            var handler = new UpdateDiagnosisDataHandler();
            handler.Handle(_currentDiagnosis, newCode, newName, newMethod);
            ShowTablePost();
        }
        catch (FormatException exception)
        {
            MessageBox.Show($"{exception.Message}");
        }
    }
}