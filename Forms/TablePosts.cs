using hospitalsCoursework.UseCase;
using hospitalsCoursework.UseCasePosts;

namespace hospitalsCoursework.Forms;

public partial class TablePosts : TableBaseForm
{
    private readonly ComboBox _comboBoxCode = new ComboBox();
    private readonly TextBox _textBoxName = new TextBox();
    private readonly TextBox _textBoxBranch = new TextBox();
    private readonly DataGridView _grid = new DataGridView();
    private Post _currentPost = new Post();
    private readonly CheckedListBox _checkedListBoxBranch = new CheckedListBox();

    public TablePosts()
    {
        InitializeComponent();
        Load += TablePost_Load;
    }

    private void TablePost_Load(object? sender, EventArgs e)
    {
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Size = new Size(900, 400);
        FillDataGrid();
        _grid.CellClick += grid_Click;
        _comboBoxCode.SelectedIndexChanged += ComboBoxCodeClick;
        Text = "Должности врачей";
        FillComboBoxPostCode();

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
        labelCode.Text = "Код должности:";
        labelCode.AutoSize = true;
        labelCode.Location = new Point(_grid.Size.Width + 10, labelAddUpdate.Height + 10);

        var labelName = new Label();
        labelName.Text = "Название:";
        labelName.AutoSize = true;
        labelName.Location = new Point(_grid.Size.Width + 10, labelCode.Location.Y + labelCode.Size.Height + 5);

        var labelBranch = new Label();
        labelBranch.Text = "Отделение:";
        labelBranch.AutoSize = true;
        labelBranch.Location = new Point(_grid.Size.Width + 10, labelName.Location.Y + labelName.Size.Height + 5);

        _comboBoxCode.Location = new Point(labelCode.Width + labelCode.Location.X, labelAddUpdate.Height + 10);
        _textBoxName.Location = new Point(labelName.Location.X + labelName.Width,
            labelCode.Location.Y + labelCode.Size.Height + 5);
        _textBoxBranch.Location = new Point(labelBranch.Location.X + labelBranch.Width,
            labelName.Location.Y + labelName.Size.Height + 5);
        _textBoxName.AutoSize = true;
        _textBoxBranch.AutoSize = true;
        _comboBoxCode.AutoSize = true;

        _checkedListBoxBranch.MaximumSize = new Size(150, 150);

        var labelFilter = new Label();
        labelFilter.Text = "Фильтр значений";
        labelFilter.AutoSize = true;
        labelFilter.Location = new Point(Width - _grid.Width, _textBoxBranch.Location.Y + 80);

        var labelFilterBranch = new Label();
        labelFilterBranch.Text = labelBranch.Text;
        labelFilterBranch.AutoSize = true;
        labelFilterBranch.Location = new Point(_grid.Size.Width + 10, labelFilter.Location.Y + labelFilter.Height);

        _checkedListBoxBranch.Location =
            new Point(_grid.Size.Width + 10, labelFilterBranch.Location.Y + labelFilterBranch.Height + 5);
        FillCheckedListBoxBranch();

        var buttonFind = new Button();
        buttonFind.Text = "Найти значения";
        buttonFind.AutoSize = true;
        buttonFind.Location = new Point(this.Width - buttonAdd.Size.Width * 2 + 20,
            labelFilter.Location.Y + labelFilter.Height + 10);
        buttonFind.Click += FindRow_Click;

        Controls.AddRange(new Control[] { buttonAdd, buttonUpdate, buttonFind });
        Controls.AddRange(new Control[]
            { labelCode, labelName, labelAddUpdate, labelFilter, labelFilterBranch, labelBranch });
        Controls.AddRange(new Control[] { _comboBoxCode, _textBoxName, _textBoxBranch });
        Controls.AddRange(new Control[] { _checkedListBoxBranch });
    }

    private void grid_Click(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = _grid.Rows[e.RowIndex];
        var handle = new GetPostsDataHandler();
        _currentPost = handle.Handle(new List<string> { row.Cells[0].Value.ToString() }, new List<string>())
            .FirstOrDefault();
        _comboBoxCode.Text = _currentPost?.PostCode;
        _textBoxName.Text = _currentPost?.Name;
        if (_grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
        {
            RemoveRow_Click();
        }
    }

    private void ComboBoxCodeClick(object? sender, EventArgs e)
    {
        var currentCode = _comboBoxCode.SelectedItem?.ToString()?.Split()[0];
        var handle = new GetPostsDataHandler();
        if (currentCode != null)
        {
            var currentItem = handle.Handle(new List<string> { currentCode }, new List<string>()).FirstOrDefault();
            _textBoxName.Text = currentItem?.Name;
            _textBoxBranch.Text = currentItem?.Branch;
        }
    }

    private void FillDataGrid()
    {
        _grid.AutoSize = true;
        _grid.Location = new Point(0, 0);
        _grid.BorderStyle = BorderStyle.None;
        _grid.BackgroundColor = this.BackColor;
        Controls.Add(_grid);
        var postsColumns = new[] { "Код должности", "Должность", "Отделение" };
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

    private void FillComboBoxPostCode()
    {
        var handler = new GetPostsDataHandler();
        var posts = handler.Handle(new List<string>(), new List<string>()).Select(p => p.PostCode).ToList();
        FillComboBox(posts, _comboBoxCode);
    }

    private void ShowTablePost()
    {
        _grid.Rows.Clear();
        var handler = new GetPostsDataHandler();
        var posts = handler.Handle(new List<string>(), FindCheckedItems(_checkedListBoxBranch));
        for (var i = 0; i < posts.Count; i++)
        {
            _grid.Rows.Add();
            _grid.Rows[i].Cells[0].Value = posts[i].PostCode;
            _grid.Rows[i].Cells[1].Value = posts[i].Name;
            _grid.Rows[i].Cells[2].Value = posts[i].Branch;
            _grid.Rows[i].Cells[3].Value = "Удалить";
        }
    }

    private void FillCheckedListBoxBranch()
    {
        var branches = new GetPostsDataHandler().Handle(new List<string>(), new List<string>()).Select(p => p.Branch).Distinct()
            .ToList();
        FillCheckedListBox(branches, _checkedListBoxBranch);
    }

    private void AddRow_Click(object? sender, EventArgs e)
    {
        try
        {
            var handler = new AddPostDataHandler();
            handler.Handle(_comboBoxCode.Text, _textBoxName.Text, _textBoxBranch.Text);
            UpdateControls();
            _comboBoxCode.Items.Add(_comboBoxCode.Text);
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
        var handler = new RemovePostDataHandler();
        handler.Handle(_currentPost.Id.ToString());
        _comboBoxCode.Items.Remove(_currentPost.PostCode);
        UpdateControls();
    }

    private void UpdateRow_Click(object? sender, EventArgs e)
    {
        try
        {
            _comboBoxCode.Items.Remove(_currentPost.PostCode);
            var handler = new UpdatePostDataHandler();
            handler.Handle(_currentPost, _comboBoxCode.Text, _textBoxName.Text, _textBoxBranch.Text);
            UpdateControls();
            _comboBoxCode.Items.Add(_comboBoxCode.Text);
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
        FillCheckedListBoxBranch();
    }
}