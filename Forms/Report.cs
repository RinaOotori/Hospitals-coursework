using System.Windows.Forms;

namespace hospitalsCoursework.Forms;

public partial class Report : Form
{
    public Report()
    {
        InitializeComponent();
        Load += Report_Load;
    }

    private void Report_Load(object? sender, EventArgs e)
    {
        Text = "Создать отчёт";
        Size = new Size(300, 300);

        var label = new Label();
        label.Text = "Выберите таблицу для отчёта:";
        label.Location = new Point(10, 0);
        label.AutoSize = true;
        
        var table = new ComboBox();
        table.Location = new Point(10, label.Height + 5);
        table.Items.AddRange(new object[]{"Должности", "Больницы", "Подразделения", "Диагнозы", "Врачи", "Пациенты"});
        table.DropDownStyle = ComboBoxStyle.DropDownList;
        table.AutoSize = true;
        table.SelectedIndex = 0;

        var buttonContinue = new Button();
        buttonContinue.Text = "Продолжить";
        buttonContinue.Location = new Point(10, table.Location.Y + table.Height + 5);
        buttonContinue.AutoSize = true;
        
        Controls.AddRange(new Control[]{label, table, buttonContinue});
    }

    private void table_Click(object? sender, EventArgs e)
    {
        Controls.Clear();
        var label = new Label();
        label.Text = "Выберите все нужные столбцы:";
        label.Location = new Point(10, 0);
        label.AutoSize = true;
        
        var items = new CheckBox();
        items.Location = new Point(10, label.Height + 5);
        items.AutoSize = true;
    }
}