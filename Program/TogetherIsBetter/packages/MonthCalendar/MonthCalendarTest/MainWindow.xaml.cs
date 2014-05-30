using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonthCalendar;

namespace MonthCalendarTest
{
    /// <summary>A very quick and dirty WPF Month-view calendar control that supports simple 1-day appointments.
    /// This is *NOT* meant to showcase best practices for WPF, or for .Net coding in general.  Please improve it, and post the
    /// improvements to CodeProject so that others can benefit, thanks!  Kirk Davis, February 2009, Bangkok, Thailand.
    /// </summary>
    /// <remarks>
    /// ''' This code is for anybody to use for any legal reason.  Given that I wrote this in about four hours, use it at your own risk.
    /// If your application crashes, a memory-leak brings down your entire country, or you hate it, you take full responsibility.</remarks>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window1_Loaded;
        }

        private List<Appointment> _myAppointmentsList = new List<Appointment>();

        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rand = new Random(System.DateTime.Now.Second);

            for (int i = 1; i <= 50; i++)
            {
                Appointment apt = new Appointment();
                apt.AppointmentID = i;
                apt.StartTime = new System.DateTime(System.DateTime.Now.Year, rand.Next(1, 12), rand.Next(1, System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month)));
                apt.EndTime = apt.StartTime;
                apt.Subject = "Random apt, blah blah";
                _myAppointmentsList.Add(apt);
            }

            SetAppointments();
        }

        private void DayBoxDoubleClicked_event(NewAppointmentEventArgs e)
        {
            MessageBox.Show("You double-clicked on day " + Convert.ToDateTime(e.StartDate).ToShortDateString(), "Calendar Event", MessageBoxButton.OK);
        }

        private void AppointmentDblClicked(int Appointment_Id)
        {
            MessageBox.Show("You double-clicked on appointment with ID = " + Appointment_Id, "Calendar Event", MessageBoxButton.OK);
        }

        private void DisplayMonthChanged(MonthChangedEventArgs e)
        {
            SetAppointments();
        }

        private void SetAppointments()
        {
            //-- Use whatever function you want to load the MonthAppointments list, I happen to have a list filled by linq that has
            //   many (possibly the past several years) of them loaded, so i filter to only pass the ones showing up in the displayed
            //   month.  Note that the "setter" for MonthAppointments also triggers a redraw of the display.
            AptCalendar.MonthAppointments = _myAppointmentsList.FindAll(new System.Predicate<Appointment>((Appointment apt) => apt.StartTime != null && Convert.ToDateTime(apt.StartTime).Month == this.AptCalendar.DisplayStartDate.Month && Convert.ToDateTime(apt.StartTime).Year == this.AptCalendar.DisplayStartDate.Year));
        }
    }
}

