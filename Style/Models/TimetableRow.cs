using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style.Models
{
    public class TimetableRow : INotifyPropertyChanged
    {
        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Number"));
            }
        }

        private string _discipline;
        public string Discipline
        {
            get { return _discipline; }
            set
            {
                _discipline = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("_Discipline"));
            }
        }

        private string _teacher;
        public string Teacher
        {
            get { return _teacher; }
            set
            {
                _teacher = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Teacher"));
            }
        }

        private string _lectureHall;
        public string LectureHall
        {
            get { return _lectureHall; }
            set
            {
                _lectureHall = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Teacher"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }
    }
}
