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
        private ToDoList toDoList = new ToDoList();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = toDoList;
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
                toDoList.AddItem(itemDescription);
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
            toDoList.Save();
        }

        private void MenuItem_OnClickDelete(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void DeleteTask()
        {
            if (ListViewToDo.SelectedItem != null)
            {
                var deleteBoxResult = MessageBox.Show("Delete this item?", "Delete?",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (deleteBoxResult == MessageBoxResult.Yes)
                {
                    toDoList.DeleteItem(ListViewToDo.SelectedItem as ToDoItem);
                }

                ListViewToDo.Items.Refresh();
            }
        }

        private void ListViewToDo_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteTask();
            }
        }
    }
}
