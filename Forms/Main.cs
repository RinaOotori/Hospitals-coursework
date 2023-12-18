namespace hospitalsCoursework.Forms;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        Load += Main_Load;
    }

    private void Main_Load(object? sender, EventArgs e)
    {
        Size = new Size(400, 400);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Text = "Меню";
        var label = new Label();
        label.Text = "Выберите таблицу для изменения";
        label.AutoSize = true;
        label.Location = new Point(200 - label.Size.Width, 0);
        Controls.Add(label);

        var buttonPost = new Button();
        buttonPost.Text = "Должности врачей";
        buttonPost.AutoSize = true;
        buttonPost.Location = new Point(10, label.Size.Height + 5);
        buttonPost.Click += (_, _) =>
        {
            new TablePosts().Show();
        };

        var buttonHospitals = new Button();
        buttonHospitals.Text = "Больницы";
        buttonHospitals.AutoSize = true;
        buttonHospitals.Location = new Point(10, buttonPost.Location.Y + buttonPost.Size.Height + 5);
        buttonHospitals.Click += (_, _) =>
        {
            new TableHospitals().Show();
        };

        var buttonDepartments = new Button();
        buttonDepartments.Text = "Подразделения";
        buttonDepartments.AutoSize = true;
        buttonDepartments.Location = new Point(10, buttonHospitals.Location.Y + buttonHospitals.Size.Height + 5);
        buttonDepartments.Click += (_, _) =>
        {
            new TableDepartments().Show();
        };

        var buttonDiagnoses = new Button();
        buttonDiagnoses.Text = "Диагнозы";
        buttonDiagnoses.AutoSize = true;
        buttonDiagnoses.Location = new Point(10, buttonDepartments.Location.Y + buttonDepartments.Size.Height + 5);
        buttonDiagnoses.Click += (_, _) =>
        {
            new TableDiagnoses().Show();
        };

        var buttonDoctors = new Button();
        buttonDoctors.Text = "Доктора";
        buttonDoctors.AutoSize = true;
        buttonDoctors.Location = new Point(10, buttonDiagnoses.Location.Y + buttonDiagnoses.Size.Height + 5);
        buttonDoctors.Click += (_, _) =>
        {
            new TableDoctors().Show();
        };

        var buttonPatients = new Button();
        buttonPatients.Text = "Пациенты";
        buttonPatients.AutoSize = true;
        buttonPatients.Location = new Point(10, buttonDoctors.Location.Y + buttonDoctors.Size.Height + 5);
        buttonPatients.Click += (_, _) =>
        {
            new TablePatients().Show();
        };
        
        var buttonReport = new Button();
        buttonReport.Text = "Создать отчёт";
        buttonReport.AutoSize = true;
        buttonReport.Location = new Point(10, buttonPatients.Location.Y + buttonPatients.Size.Height + 5);
        buttonReport.Click += (_, _) =>
        {
            new Report().Show();
        };

        Controls.AddRange(new Control[]
            { buttonPost, buttonHospitals, buttonDiagnoses, buttonDoctors, buttonDepartments, buttonPatients, buttonReport });
    }
}