using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace ToDoListApp
{
    [Serializable]
    public class ToDoItem:INotifyPropertyChanged
    {
        private XElement _todo;
        internal XElement ToDo { get { return _todo; } }
        internal ToDoItem(XElement todo)
        {
            _todo = todo;
        }

        public string Description
        {
            get { return ToDo.Element("description").Value; }
            set
            {
                ToDo.Element("description").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                }
            }
        }

        public string DoneDateTime
        {
            get { return ToDo.Element("donedatetime") == null ? "" : ToDo.Element("donedatetime").Value; }
            set
            {
                if (ToDo.Element("donedatetime") == null)
                {
                    ToDo.Add(new XElement("donedatetime"));
                }

                ToDo.Element("donedatetime").Value = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DoneDateTime"));
                }
            }
        }

        public override string ToString()
        {
            string result;
            if (DoneDateTime != "")
            {
                DateTime doneAt = DateTime.Parse(DoneDateTime);
                result = String.Format("{0} - Done at: {1:yyyy-MM-dd hh:mm}", Description, doneAt);
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
