using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToDoList ToDoList = new ToDoList();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ToDoList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask();
        }

        private void TextItemDesc_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TextItemDesc_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!textBox.IsKeyboardFocusWithin)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private void AddNewTask()
        {
            var itemDescription = TextItemDesc.Text.Trim();
            if (!string.IsNullOrEmpty(itemDescription))
            {
                ToDoList.AddItem(itemDescription);
            }

            TextItemDesc.Text = "";
            ListViewToDo.Items.Refresh();
        }

        private void TextItemDesc_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddNewTask();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ToDoList.Save();
        }

        private void MenuItem_OnClickDelete(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void DeleteTask()
        {
            if (ListViewToDo.SelectedItem == null) return;
            var deleteBoxResult = MessageBox.Show("Delete this item?", "Delete?",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (deleteBoxResult == MessageBoxResult.Yes)
            {
                ToDoList.DeleteItem(ListViewToDo.SelectedItem as ToDoItem);
            }

            ListViewToDo.Items.Refresh();
        }

        private void ListViewToDo_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteTask();
            }
        }

        private void MenuItem_OnClickDone(object sender, RoutedEventArgs e)
        {
            MarkAsDone();
        }

        private void MarkAsDone()
        {
            if (ListViewToDo.SelectedItem == null) return;
            var doneBoxResult = MessageBox.Show("Mark this as done ? ", "Done ? ", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            if (doneBoxResult == MessageBoxResult.Yes)
            {
                ((ToDoItem) ListViewToDo.SelectedItem).DoneDateTime = DateTime.Now.ToString("g");
            }

            ListViewToDo.Items.Refresh();
        }

        private void ListViewToDo_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkAsDone();
        }

        private void MenuItem_OnClickEdit(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewToDo.SelectedItem;

            if (selectedItem != null)
            {
                var description = ((ToDoItem)selectedItem).Description;
                TextItemDesc.Text = description;
                ToDoList.DeleteItem((ToDoItem)selectedItem);
                ListViewToDo.Items.Refresh();
                TextItemDesc.Focus();

            }
        }
    }
}
