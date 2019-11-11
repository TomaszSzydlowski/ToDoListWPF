using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace ToDoListApp
{
    public class ToDoList : INotifyPropertyChanged
    {
        [NonSerialized]
        private XDocument _toDoList;

        [NonSerialized]
        private List<ToDoItem> _items;

        public List<ToDoItem> Items
        {
            get { return _items; }
        }

        public ToDoList()
        {
            var asr = new AppSettingsReader();
            var toDoSaveFile = asr.GetValue("toDoSaveFile", typeof(string)).ToString();
            if (File.Exists(toDoSaveFile))
            {
                _toDoList = XDocument.Load(toDoSaveFile);
                _items = (from e in _toDoList.Root?.Elements()
                          select new ToDoItem(e)).ToList();
            }
            else
            {
                _toDoList=new XDocument();
                _toDoList.Add(new XElement("todolist"));
                _items=new List<ToDoItem>();
            }
        }

        public void AddItem(string desciption)
        {
            var item = new XElement("todo");
            var createAt = new XElement("createDateTime") {Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms")};
            item.Add(createAt);
            var descriptionElement = new XElement("description") {Value = desciption};
            item.Add(descriptionElement);

            _toDoList.Root?.Add(item);
            _items.Add(new ToDoItem(item));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save()
        {
            var asr = new AppSettingsReader();
            var toDoSaveFile = asr.GetValue("toDoSaveFile", typeof(string)).ToString();
            _toDoList.Save(toDoSaveFile);
        }

        public void DeleteItem(ToDoItem item)
        {
            _items.Remove(item);
            item.ToDo.Remove();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }
    }
}
