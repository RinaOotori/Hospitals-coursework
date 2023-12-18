using hospitalsCoursework.UseCaseHospitals;
using hospitalsCoursework.UseCases.UseCasesDoctors;
using hospitalsCoursework.UseCases.UseCasesPatients;
using hospitalsCoursework.UseCasesDiagnoses;

namespace hospitalsCoursework.Forms;

public partial class TablePatients : TableBaseForm
{
    private readonly ComboBox _comboBoxTin = new ComboBox();
    private readonly TextBox _textBoxName = new TextBox();
    private readonly ComboBox _comboBoxHospitalCode = new ComboBox();
    private readonly ComboBox _comboBoxDoctorTin = new ComboBox();
    private readonly ComboBox _comboBoxDiagnosisCode = new ComboBox();
    private readonly TextBox _textBoxConditionDischarge = new TextBox();
    private readonly DateTimePicker _textBoxHospitalizationDate = new DateTimePicker();
    private readonly DateTimePicker _textBoxExtractDate = new DateTimePicker();
    private readonly DataGridView _grid = new DataGridView();
    private Patient _currentPatient = new Patient();
    private readonly CheckedListBox _checkedListBoxHospitalCode = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxDoctorTin = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxDiagnosisCode = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxTin = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxHospitalizationDate = new CheckedListBox();
    private readonly CheckedListBox _checkedListBoxExtractDate = new CheckedListBox();

    public TablePatients()
    {
        InitializeComponent();
        Load += TablePatients_Load;
    }

    private void TablePatients_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Size = new Size(1500, 750);
        FillDataGrid();
        _grid.CellClick += grid_Click;
        _comboBoxTin.SelectedIndexChanged += ComboBoxCodeClick;

        _comboBoxDoctorTin.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboBoxHospitalCode.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboBoxDiagnosisCode.DropDownStyle = ComboBoxStyle.DropDownList;
        Text = "Пациенты";
        FillComboBoxes();

        var labelAddUpdate = new Label();
        labelAddUpdate.Text = "Добавить или изменить значение:";
        labelAddUpdate.AutoSize = true;
        labelAddUpdate.Location = new Point(this.Width - _grid.Width / 3, 0);

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

        var labelDoctorTin = new Label();
        labelDoctorTin.Text = "ИНН доктора:";
        labelDoctorTin.AutoSize = true;
        labelDoctorTin.Location = new Point(_grid.Size.Width + 10,
            labelHospitalCode.Location.Y + labelHospitalCode.Size.Height + 5);

        var labelDiagnosisCode = new Label();
        labelDiagnosisCode.Text = "Код диагноза:";
        labelDiagnosisCode.AutoSize = true;
        labelDiagnosisCode.Location =
            new Point(_grid.Size.Width + 10, labelDoctorTin.Location.Y + labelDoctorTin.Size.Height + 5);

        var labelName = new Label();
        labelName.Text = "ФИО:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10,
            labelDiagnosisCode.Location.Y + labelDiagnosisCode.Size.Height + 5);

        var labelHospitalizationDate = new Label();
        labelHospitalizationDate.Text = "Дата госпит-ии:";
        labelHospitalizationDate.AutoSize = true;
        labelHospitalizationDate.Location =
            new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        var labelExtractDate = new Label();
        labelExtractDate.Text = "Дата выписки:";
        labelExtractDate.AutoSize = true;
        labelExtractDate.Location = new Point(_grid.Size.Width + 10,
            labelHospitalizationDate.Location.Y + labelHospitalizationDate.Size.Height + 5);

        var labelTin = new Label();
        labelTin.Text = "ИНН:";
        labelTin.AutoSize = true;
        labelTin.Location = new Point(_grid.Size.Width + 10,
            labelExtractDate.Location.Y + labelExtractDate.Size.Height + 5);

        var labelConditionDischarge = new Label();
        labelConditionDischarge.Text = "Рекомендации:";
        labelConditionDischarge.AutoSize = true;
        labelConditionDischarge.Location =
            new Point(_grid.Size.Width + 10, labelTin.Location.Y + labelTin.Size.Height + 5);

        _comboBoxTin.Location = new Point(labelTin.Width + labelTin.Location.X, labelTin.Location.Y + 1);
        _textBoxName.Location = new Point(labelName.Location.X + labelName.Width, labelName.Location.Y + 1);
        _comboBoxDoctorTin.Location =
            new Point(labelDoctorTin.Location.X + labelDoctorTin.Width, labelDoctorTin.Location.Y + 1);
        _comboBoxHospitalCode.Location = new Point(labelHospitalCode.Location.X + labelHospitalCode.Width,
            labelHospitalCode.Location.Y + 1);
        _comboBoxDiagnosisCode.Location = new Point(labelDiagnosisCode.Location.X + labelDiagnosisCode.Width,
            labelDiagnosisCode.Location.Y + 1);
        _textBoxHospitalizationDate.Location =
            new Point(labelHospitalizationDate.Location.X + labelHospitalizationDate.Width,
                labelHospitalizationDate.Location.Y + 1);
        _textBoxExtractDate.Location = new Point(labelExtractDate.Location.X + labelExtractDate.Width,
            labelExtractDate.Location.Y + 1);
        _textBoxConditionDischarge.Location =
            new Point(labelConditionDischarge.Location.X + labelConditionDischarge.Width,
                labelConditionDischarge.Location.Y + 1);

        _checkedListBoxTin.MaximumSize = new Size(150, 150);
        _checkedListBoxExtractDate.MaximumSize = new Size(150, 150);
        _checkedListBoxHospitalizationDate.MaximumSize = new Size(150, 150);
        _checkedListBoxDoctorTin.MaximumSize = new Size(150, 150);
        _checkedListBoxDiagnosisCode.MaximumSize = new Size(150, 150);
        _checkedListBoxHospitalCode.MaximumSize = new Size(150, 150);

        var labelFilter = new Label();
        labelFilter.Text = "Фильтр значений";
        labelFilter.AutoSize = true;
        labelFilter.Location = new Point(Width - _grid.Width / 3, _textBoxConditionDischarge.Location.Y + 80);

        var labelFilterHospitalCode = new Label();
        labelFilterHospitalCode.Text = labelHospitalCode.Text;
        labelFilterHospitalCode.AutoSize = true;
        labelFilterHospitalCode.Location =
            new Point(_grid.Size.Width + 10, labelFilter.Location.Y + labelFilter.Height);

        var labelFilterDoctorTin = new Label();
        labelFilterDoctorTin.Text = labelDoctorTin.Text;
        labelFilterDoctorTin.AutoSize = true;
        labelFilterDoctorTin.Location =
            new Point(labelFilterHospitalCode.Location.X + labelFilterHospitalCode.Width + 30,
                labelFilter.Location.Y + labelFilter.Height);

        var labelFilterDiagnosisCode = new Label();
        labelFilterDiagnosisCode.Text = labelDiagnosisCode.Text;
        labelFilterDiagnosisCode.AutoSize = true;
        labelFilterDiagnosisCode.Location = new Point(labelFilterDoctorTin.Location.X + labelFilterDoctorTin.Width + 30,
            labelFilter.Location.Y + labelFilter.Height);

        var labelFilterHospitalizationDate = new Label();
        labelFilterHospitalizationDate.Text = labelHospitalizationDate.Text;
        labelFilterHospitalizationDate.AutoSize = true;
        labelFilterHospitalizationDate.Location =
            new Point(_grid.Size.Width + 10, labelFilter.Location.Y + labelFilter.Height + 150);

        var labelFilterExtractDate = new Label();
        labelFilterExtractDate.Text = labelExtractDate.Text;
        labelFilterExtractDate.AutoSize = true;
        labelFilterExtractDate.Location =
            new Point(labelFilterHospitalizationDate.Location.X + labelFilterHospitalizationDate.Width + 30,
                labelFilter.Location.Y + labelFilter.Height + 150);

        var labelFilterTin = new Label();
        labelFilterTin.Text = labelTin.Text;
        labelFilterTin.AutoSize = true;
        labelFilterTin.Location = new Point(labelFilterExtractDate.Location.X + labelFilterExtractDate.Width + 30,
            labelFilter.Location.Y + labelFilter.Height + 150);

        _checkedListBoxHospitalCode.Location =
            new Point(_grid.Size.Width + 10, labelFilterHospitalCode.Location.Y + labelFilterHospitalCode.Height + 5);
        _checkedListBoxDoctorTin.Location =
            new Point(labelFilterDoctorTin.Location.X + 1,
                labelFilterDoctorTin.Location.Y + labelFilterDoctorTin.Height + 5);
        _checkedListBoxDiagnosisCode.Location =
            new Point(labelFilterDiagnosisCode.Location.X + 1,
                labelFilterDiagnosisCode.Location.Y + labelFilterDiagnosisCode.Height + 5);
        _checkedListBoxHospitalizationDate.Location =
            new Point(labelFilterHospitalizationDate.Location.X + 1,
                labelFilterHospitalizationDate.Location.Y + labelFilterHospitalizationDate.Height + 5);
        _checkedListBoxExtractDate.Location =
            new Point(labelFilterExtractDate.Location.X + 1,
                labelFilterExtractDate.Location.Y + labelFilterExtractDate.Height + 5);
        _checkedListBoxTin.Location =
            new Point(labelFilterTin.Location.X + 1, labelFilterTin.Location.Y + labelFilterTin.Height + 5);
        FillCheckedListBoxes();

        var buttonFind = new Button();
        buttonFind.Text = "Найти значения";
        buttonFind.AutoSize = true;
        buttonFind.Location = new Point(this.Width - buttonAdd.Size.Width * 2 + 20,
            labelFilter.Location.Y + labelFilter.Height + 10);
        buttonFind.Click += FindRow_Click;

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate, buttonFind });
        Controls.AddRange(new Control[]
        {
            labelHospitalCode, labelName, labelAddUpdate, labelDoctorTin, labelDiagnosisCode, labelHospitalizationDate,
            labelExtractDate, labelTin, labelConditionDischarge, labelFilter, labelFilterHospitalCode,
            labelFilterDoctorTin, labelFilterDiagnosisCode, labelFilterHospitalizationDate, labelFilterExtractDate,
            labelFilterTin
        });
        Controls.AddRange(new Control[]
        {
            _comboBoxTin, _textBoxName, _comboBoxDiagnosisCode, _textBoxConditionDischarge, _textBoxExtractDate,
            _textBoxHospitalizationDate, _comboBoxDoctorTin, _comboBoxHospitalCode, _checkedListBoxHospitalCode,
            _checkedListBoxDoctorTin, _checkedListBoxDiagnosisCode, _checkedListBoxHospitalizationDate,
            _checkedListBoxExtractDate, _checkedListBoxTin
        });
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetPatientsDataHandler();
        _currentPatient = handle.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
                new List<string> { row.Cells[6].Value.ToString() }, new List<string>(), new List<DateOnly>(),
                new List<DateOnly>())
            .FirstOrDefault();
        _comboBoxTin.Text = _currentPatient?.Tin;
        _textBoxName.Text = _currentPatient?.Name;
        _comboBoxHospitalCode.Text = _currentPatient?.HospitalCode;
        _comboBoxDiagnosisCode.Text = _currentPatient?.DiagnosisCode;
        _comboBoxDoctorTin.Text = _currentPatient?.DoctorTin;
        _textBoxExtractDate.Text = _currentPatient?.ExtractDate.ToString();
        _textBoxHospitalizationDate.Text = _currentPatient?.HospitalizationDate.ToString();
        _textBoxConditionDischarge.Text = _currentPatient?.ConditionDischarge;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void FillComboBoxes()
    {
        var patientTins = new GetPatientsDataHandler().Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            new List<string>(), new List<string>(), new List<DateOnly>(), new List<DateOnly>()).Select(p => p.Tin).ToList();
        FillComboBox(patientTins, _comboBoxTin);

        var hospitalsCode = new GetHospitalsDataHandler().Handle(new List<string>(), new List<string>(), new List<string>(),
            new List<string>(),
            new List<string>()).Select(h => h.HospitalCode).ToList();
        FillComboBox(hospitalsCode, _comboBoxHospitalCode);

        var diagnosesCode = new GetDiagnosesDataHandler().Handle(new List<string>(), new List<string>(), new List<string>())
            .Select(d => d.DiagnosisCode).ToList();
        FillComboBox(diagnosesCode, _comboBoxDiagnosisCode);

        var doctorsTin = new GetDoctorsDataHandler().Handle(new List<string>(), new List<string>(), new List<string>(),
            new List<string>(), new List<string>()).Select(d => d.Tin).ToList();
        FillComboBox(doctorsTin, _comboBoxDoctorTin);
    }

    private void ComboBoxCodeClick(object? sender, EventArgs e)
    {
        var currentTin = _comboBoxTin.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetPatientsDataHandler();
        if (currentTin != null)
        {
            var currentItem = handle.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
                    new List<string> { currentTin }, new List<string>(), new List<DateOnly>(), new List<DateOnly>())
                .FirstOrDefault();
            _textBoxName.Text = currentItem?.Name;
            _comboBoxHospitalCode.Text = currentItem?.HospitalCode;
            _comboBoxDiagnosisCode.Text = currentItem?.DiagnosisCode;
            _comboBoxDoctorTin.Text = currentItem?.DoctorTin;
            _textBoxExtractDate.Text = currentItem?.ExtractDate.ToString();
            _textBoxHospitalizationDate.Text = currentItem?.HospitalizationDate.ToString();
            _textBoxConditionDischarge.Text = currentItem?.ConditionDischarge;
        }
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var postsColumns = new[]
        {
            "Код больницы", "ИНН доктора", "Код диагноза", "Дата поступления", "Дата выписки", "ФИО", "ИНН",
            "Рекомендации"
        };
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
        var handler = new GetPatientsDataHandler();
        var patients = handler.Handle(FindCheckedItems(_checkedListBoxHospitalCode), FindCheckedItems(_checkedListBoxDoctorTin), FindCheckedItems(_checkedListBoxDiagnosisCode), new List<string>(),
            FindCheckedItems(_checkedListBoxTin), new List<string>(), FindCheckedItemsDateOnly(_checkedListBoxHospitalizationDate), FindCheckedItemsDateOnly(_checkedListBoxExtractDate));
        for (var i = 0; i < patients.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = patients[i].HospitalCode;
            _grid.Rows[i].Cells[1].Value = patients[i].DoctorTin;
            _grid.Rows[i].Cells[2].Value = patients[i].DiagnosisCode;
            _grid.Rows[i].Cells[3].Value = patients[i].HospitalizationDate;
            _grid.Rows[i].Cells[4].Value = patients[i].ExtractDate;
            _grid.Rows[i].Cells[5].Value = patients[i].Name;
            _grid.Rows[i].Cells[6].Value = patients[i].Tin;
            _grid.Rows[i].Cells[7].Value = patients[i].ConditionDischarge;
            _grid.Rows[i].Cells[8].Value = "Удалить";
        }
    }

    private void FillCheckedListBoxes()
    {
        var handler = new GetPatientsDataHandler();
        var patients = handler.Handle(new List<string>(), new List<string>(), new List<string>(), new List<string>(),
            new List<string>(), new List<string>(), new List<DateOnly>(), new List<DateOnly>());
        FillCheckedListBox(patients.Select(p => p.HospitalizationDate).Distinct().ToList(), _checkedListBoxHospitalizationDate);
        FillCheckedListBox(patients.Select(p => p.ExtractDate).Distinct().ToList(), _checkedListBoxExtractDate);
        FillCheckedListBox(patients.Select(p => p.DoctorTin).Distinct().ToList(), _checkedListBoxDoctorTin);
        FillCheckedListBox(patients.Select(p => p.HospitalCode).Distinct().ToList(), _checkedListBoxHospitalCode);
        FillCheckedListBox(patients.Select(p => p.DiagnosisCode).Distinct().ToList(), _checkedListBoxDiagnosisCode);
        FillCheckedListBox(patients.Select(p => p.Tin).Distinct().ToList(), _checkedListBoxTin);
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddPatientDataHandler();
            handler.Handle(_comboBoxHospitalCode.Text, _comboBoxDoctorTin.Text, _comboBoxDiagnosisCode.Text,
                DateOnly.Parse(_textBoxHospitalizationDate.Text),
                DateOnly.Parse(_textBoxExtractDate.Text), _textBoxName.Text, _comboBoxTin.Text,
                _textBoxConditionDischarge.Text);
            UpdateControls();
            _comboBoxTin.Items.Add(_comboBoxTin.Text);
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
        var handler = new RemovePatientDataHandler();
        handler.Handle(_currentPatient.Id.ToString());
        _comboBoxTin.Items.Remove(_currentPatient.Tin);
        UpdateControls();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            _comboBoxTin.Items.Remove(_currentPatient.Tin);
            var handler = new UpdatePatientDataHandler();
            handler.Handle(_currentPatient, _comboBoxHospitalCode.Text, _comboBoxDoctorTin.Text,
                _comboBoxDiagnosisCode.Text,
                DateOnly.Parse(_textBoxHospitalizationDate.Text),
                DateOnly.Parse(_textBoxExtractDate.Text), _textBoxName.Text, _comboBoxTin.Text,
                _textBoxConditionDischarge.Text);
            UpdateControls();
            _comboBoxTin.Items.Add(_comboBoxTin.Text);
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