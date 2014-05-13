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
using TogetherIsBetter.Model;
using TogetherIsBetter.Views;

namespace TogetherIsBetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user; // check if admin: user.Role == "admin"
        private List<Appointment> _myAppointmentsList = new List<Appointment>();

        public MainWindow(User user)
        {
            InitializeComponent();
            this.user = user;

            if (user.Role != "admin")
                this.btnManagement.Visibility = Visibility.Hidden;   
        }      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = Companies.getReservations();

            for (int i = 0; i < reservations.Count; i++)
            {
                Appointment apt = new Appointment();
                apt.AppointmentID = i;
                apt.StartTime = reservations[i].StartDate;
                apt.EndTime = reservations[i].EndDate;
                apt.Subject = Global.companies.Find(c => c.Id == reservations[i].CompanyId).Name;
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void btnManagement_Click(object sender, RoutedEventArgs e)
        {
            ManagementFrm contractFrm = new ManagementFrm();
            contractFrm.Show();
        }


  
        
    }
}
