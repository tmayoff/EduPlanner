using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EduPlanner {
    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class EditClassWindow : Window {

        private readonly List<ClassTime> _classTimes = new List<ClassTime>();

        private readonly Class _class;

        public EditClassWindow(Class _class) {
            InitializeComponent();

            this._class = _class;
            txtClassName.Text = _class.className;

            for (int i = 0; i < _class.classTimes.Length; i++) {
                if (_class.classTimes[i][0] != null && _class.classTimes[i][1] != null) {

                    ClassTime time = ClassTimeExist(_class.classTimes[i][0]);

                    if (time == null)
                        time = new ClassTime();

                    time.tpStartTime.SelectedTime = _class.classTimes[i][0];
                    time.tpEndTime.SelectedTime = _class.classTimes[i][1];

                    //Loop through class times
                    WrapPanel wrap = time.FindName("wpDays") as WrapPanel;
                    for (int j = 0; j < wrap.Children.Count; j++) {

                        if (wrap.Children[j] is CheckBox) {
                            CheckBox check = wrap.Children[j] as CheckBox;

                            if (check.Content.ToString() == ((DayOfWeek)i).ToString()) {
                                check.IsChecked = true;
                            }
                        }
                    }

                    time.ClassTimeChanged += Handler;
                    _classTimes.Add(time);
                    if (!spClassTimesViewer.Children.Contains(time))
                        spClassTimesViewer.Children.Add(time);
                }
            }
        }

        private ClassTime ClassTimeExist(DateTime? start) {
            for (int i = 0; i < _classTimes.Count; i++) {
                if (_classTimes[i].tpStartTime.SelectedTime == start)
                    return _classTimes[i];
            }

            return null;
        }

        private void ClassName_TextChanged(object sender, TextChangedEventArgs e) {
            Handler();
        }

        private void AddTime_Click(object sender, RoutedEventArgs e) {
            ClassTime time = new ClassTime();
            time.ClassTimeChanged += Handler;
            _classTimes.Add(time);
            spClassTimesViewer.Children.Add(time);

            Handler();
        }

        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e) {
            foreach (Day day in DataManager.Schedule.days) {
                day.classes.Remove(_class);
            }

            DataManager.Schedule.classes.Remove(_class);

            Close();
        }

        private void SaveClass_Click(object sender, RoutedEventArgs e) {



            Close();
        }

        //private void SaveClass_Click(object sender, RoutedEventArgs e) {
        //    foreach (Day day in DataManager.Schedule.days) {
        //        day.classes.Remove(_class);
        //    }

        //    _class.ResetTimes();

        //    //Loop through class times
        //    for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
        //        ClassTime time = spClassTimesViewer.Children[i] as ClassTime;

        //        WrapPanel panel = time.FindName("wpDays") as WrapPanel;
        //        Grid timeGrid = time.FindName("TimeGrid") as Grid;

        //        //Loop through checkboxes (days)
        //        for (int j = 0; j < panel.Children.Count; j++) {

        //            //Check the current checkbox
        //            if (panel.Children[j] is CheckBox) {
        //                CheckBox check = panel.Children[j] as CheckBox;
        //                if (check.IsChecked == true) {

        //                    //Get day as an enum
        //                    Enum.TryParse(check.Name, out DayOfWeek day);

        //                    DateTime? startTime = (timeGrid.Children[0] as TimePicker).SelectedTime;
        //                    DateTime? endTime = (timeGrid.Children[1] as TimePicker).SelectedTime;

        //                    _class.className = txtClassName.Text;

        //                    _class._classTimes[(int)day][0] = startTime;
        //                    _class._classTimes[(int)day][1] = endTime;

        //                    DataManager.Schedule.days[(int)day].classes.Add(_class);
        //                }
        //            }
        //        }
        //    }

        private void Handler() {
            bool dayChecked = false;
            bool timePicked = false;

            //Loop through class times
            for (int i = 0; i < spClassTimesViewer.Children.Count; i++) {
                ClassTime time = spClassTimesViewer.Children[i] as ClassTime;

                WrapPanel panel = time.FindName("wpDays") as WrapPanel;
                Grid timeGrid = time.FindName("TimeGrid") as Grid;

                //Loop through checkboxes
                for (int j = 0; j < panel.Children.Count; j++) {

                    if (panel.Children[i] is CheckBox) {
                        CheckBox check = panel.Children[j] as CheckBox;
                        if (check.IsChecked == true)
                            dayChecked = true;
                    }
                }

                //Loop through time pickers
                for (int j = 0; j < timeGrid.Children.Count; j++) {
                    if (timeGrid.Children[j] is TimePicker) {
                        TimePicker picker = timeGrid.Children[j] as TimePicker;
                        timePicked = picker.SelectedTime != null;
                    }
                }
            }

            if (dayChecked && timePicked && txtClassName.Text != "")
                btnSaveClass.IsEnabled = true;
            else
                btnSaveClass.IsEnabled = false;
        }
    }
}
