using MonthCalendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TogetherIsBetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TIB_Model db;
        private User user; // check if admin: user.Role.equals("admin")

        public MainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private List<Appointment> _myAppointmentsList = new List<Appointment>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rand = new Random(System.DateTime.Now.Second);

            for (int i = 1; i <= 20; i++)
            {
                Appointment apt = new Appointment();
                apt.AppointmentID = i;
                apt.StartTime = new System.DateTime(System.DateTime.Now.Year, 5, i);
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

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
}
