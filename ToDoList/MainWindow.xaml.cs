using System.Windows;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToDoList toDoList = new ToDoList();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = toDoList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var itemDescription = TextItemDesc.Text.Trim();
            if (!string.IsNullOrEmpty(itemDescription))
            {
                toDoList.AddItem(itemDescription);
            }

            TextItemDesc.Text = "";
            ListViewToDo.Items.Refresh();
        }
    }
}
