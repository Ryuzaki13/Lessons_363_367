using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Style.Models;
using System.Collections.ObjectModel;

namespace Style
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TimetableRow> timetables;
        public TimetableRow timetableRow { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            timetables = new ObservableCollection<TimetableRow>()
            {
                new TimetableRow() {
                    Number = 1,
                    Discipline = "ФЫВ",
                    Teacher = "ASD",
                    LectureHall = "21"
                },
                new TimetableRow() {
                    Number = 2,
                    Discipline = "ФЫВ",
                    Teacher = "ASD",
                    LectureHall = "21"
                },
                new TimetableRow() {
                    Number = 3,
                    Discipline = "ФЫВ",
                    Teacher = "ASD",
                    LectureHall = "21"
                },
            };

            lbTimetable.ItemsSource = timetables;

            timetableRow = new TimetableRow();
            timetableRow.Number = GetPairNumber();
        }

        private bool IsValid()
        {
            return timetableRow.Discipline != null &&
                timetableRow.Teacher != null &&
                timetableRow.LectureHall != null;
        }

        private int GetPairNumber()
        {
            return lbTimetable.Items.Count + 1;
        }

        private void CheckDiscipline()
        {
            if (cbDiscipline.SelectedItem == null)
            {
                timetableRow.Discipline = null;
            }
            else
            {
                timetableRow.Discipline = cbDiscipline.SelectedItem.ToString();
            }
        }

        private void CheckTeacher()
        {
            if (cbTeacher.SelectedItem == null)
            {
                timetableRow.Teacher = null;
            }
            else
            {
                timetableRow.Teacher = cbTeacher.SelectedItem.ToString();
            }
        }

        private void CheckLectureHall()
        {
            if (cbLectureHall.SelectedItem == null)
            {
                timetableRow.LectureHall = null;
            }
            else
            {
                timetableRow.LectureHall = cbLectureHall.SelectedItem.ToString();
            }
        }

        private void OnTimetableRowChange(object sender, SelectionChangedEventArgs e)
        {
            bRemove.IsEnabled = lbTimetable.SelectedItem != null;
        }

        private void OnDisciplineChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckDiscipline();
            bCreate.IsEnabled = IsValid();
        }

        private void OnTeacherChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckTeacher();
            bCreate.IsEnabled = IsValid();
        }

        private void OnLectureHallChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckLectureHall();
            bCreate.IsEnabled = IsValid();
        }

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            timetables.Add(timetableRow);

            timetableRow = new TimetableRow();
            timetableRow.Number = GetPairNumber();

            CheckDiscipline();
            CheckTeacher();
            CheckLectureHall();
        }

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            lbTimetable.Items.Remove(lbTimetable.SelectedItem);

            int number = 1;
            foreach (var item in lbTimetable.Items)
            {
                var timetableRow = item as TimetableRow;
                if (timetableRow != null)
                {
                    timetableRow.Number = number++;
                }
            }

            lbTimetable.Items.Refresh();
        }

        #region Create a new Discipline

        private void OnInputDiscipline(object sender, TextChangedEventArgs e)
        {
            bCreateDiscipline.IsEnabled = tbDiscipline.Text.Trim().Length > 2;
        }

        private void OnCreateDiscipline(object sender, RoutedEventArgs e)
        {
            string disciplineName = tbDiscipline.Text.Trim();
            disciplineName = disciplineName.Substring(0, 1).ToUpper() + disciplineName.Substring(1).ToLower();
            if (cbDiscipline.Items.IndexOf(disciplineName) == -1)
            {
                cbDiscipline.Items.Add(disciplineName);
                tbDiscipline.Clear();
            }
        }

        #endregion

        #region Create a new Teacher
        private void OnInputTeacher(object sender, TextChangedEventArgs e)
        {
            bCreateTeacher.IsEnabled = GetTeacherName() != null;
        }

        private void OnCreateTeacher(object sender, RoutedEventArgs e)
        {
            string teacherName = GetTeacherName();
            if (cbTeacher.Items.IndexOf(teacherName) == -1)
            {
                cbTeacher.Items.Add(teacherName);
                tbTeacher.Clear();
            }
        }

        private string GetTeacherName()
        {
            string[] words = tbTeacher.Text.Trim().Split(' ');

            if (words.Length > 2)
            {
                string[] teacherName = new string[3];
                int index = 0;

                foreach (var wordOf in words)
                {
                    string word = wordOf.Trim();
                    if (word.Length > 1)
                    {
                        word = word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                        teacherName[index++] = word;
                    }

                    if (index > 2)
                    {
                        return string.Join(" ", teacherName);
                    }
                }
            }

            return null;
        }
        #endregion

        #region Create a new Lecture hall
        private void OnInputLectureHall(object sender, TextChangedEventArgs e)
        {
            bCreateLectureHall.IsEnabled = tbLectureHall.Text.Trim().Length > 0;
        }

        private void OnCreateLectureHall(object sender, RoutedEventArgs e)
        {
            string lectureHall = tbLectureHall.Text.Trim();
            if (cbLectureHall.Items.IndexOf(lectureHall) == -1)
            {
                cbLectureHall.Items.Add(lectureHall);
                tbLectureHall.Clear();
            }
        }
        #endregion

        private void numberIncriment_Click(object sender, RoutedEventArgs e)
        {
            var row = lbTimetable.SelectedItem as TimetableRow;
            if (row == null) return;

            row.Number++;
        }
    }
}
