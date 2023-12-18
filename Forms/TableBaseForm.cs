using System.Windows.Forms;

namespace hospitalsCoursework.Forms;

public partial class TableBaseForm : Form
{
    protected TableBaseForm()
    {
        InitializeComponent();
    }

    protected void FillComboBox<T>(List<T> items, ComboBox comboBox)
    {
        foreach (var item in items)
        {
            if (item != null) comboBox.Items.Add(item);
        }
    }

    protected void FillCheckedListBox<T>(List<T> items, CheckedListBox checkedListBox)
    {
        checkedListBox.Items.Clear();
        checkedListBox.Items.Add("Все");
        foreach (var item in items)
        {
            if (item != null) checkedListBox.Items.Add(item);
        }
    }

    protected List<string> FindCheckedItems(CheckedListBox checkedListBox)
    {
        var temp = checkedListBox.CheckedItems;

        return (from object? val in temp select val.ToString()).ToList();
    }

    protected List<DateOnly> FindCheckedItemsDateOnly(CheckedListBox checkedListBox)
    {
        var temp = checkedListBox.CheckedItems;
        return temp.Contains("Все") ? new List<DateOnly>() : temp.Cast<DateOnly>().ToList();
    }
}