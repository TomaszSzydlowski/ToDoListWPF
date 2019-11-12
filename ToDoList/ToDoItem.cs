using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace ToDoListApp
{
    [Serializable]
    public class ToDoItem : INotifyPropertyChanged
    {
        private const string Donedatetime = "donedatetime";
        private XElement _todo;
        internal XElement ToDo { get { return _todo; } }
        internal ToDoItem(XElement todo)
        {
            _todo = todo;
        }

        public string Description
        {

            get => ToDo.Element("description")?.Value;
            set
            {
                if (ToDo != null) ToDo.Element("description").Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public string DoneDateTime
        {
            get => ToDo.Element(Donedatetime) == null ? "" : ToDo.Element(Donedatetime)?.Value;
            set
            {
                if (ToDo.Element(Donedatetime) == null)
                {
                    ToDo.Add(new XElement(Donedatetime));
                }

                ToDo.Element(Donedatetime).Value = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DoneDateTime"));
            }
        }

        public override string ToString()
        {
            string result;
            if (!string.IsNullOrEmpty(DoneDateTime))
            {
                var doneAt = DateTime.Parse(DoneDateTime);
                result = $"{Description} - Done at: {doneAt:yyyy-MM-dd hh:mm}";
            }
            else
            {
                result = Description;
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
