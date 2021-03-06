﻿using MonthCalendar;
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
        public bool logout { get; set; } // used when logout button is clicked

        public MainWindow(User user)
        {
            InitializeComponent();
            this.user = user;

            if (user.IsAdmin){
                btnManagementText.Text = "Management";
            }
            else {
                btnManagementText.Text = "Settings";
                ActionButtons.Width = 267;
            }

            loadCalendar();
        }

        private void loadCalendar()
        {
            loadReservations();
            _myAppointmentsList.Clear();

            for (int i = 0; i < Global.reservations.Count; i++)
            {
                Appointment apt = new Appointment();
                apt.AppointmentID = Global.reservations[i].Id;
                apt.StartTime = Global.reservations[i].StartDate;
                apt.EndTime = Global.reservations[i].EndDate;

                apt.Subject = Global.companies.Find(c => c.Id == Global.reservations[i].CompanyId).Name;
                _myAppointmentsList.Add(apt);
            }

            SetAppointments();

        }

        private void DayBoxDoubleClicked_event(NewAppointmentEventArgs e)
        {

            if (e.StartDate < DateTime.Today)
            {
                MessageBox.Show("Sorry. No reservations can be made in the past.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //MessageBox.Show("You double-clicked on day " + Convert.ToDateTime(e.StartDate).ToShortDateString(), "Calendar Event", MessageBoxButton.OK);
            Reservation reservation = new Reservation();
            reservation.StartDate = Convert.ToDateTime(e.StartDate).Date + DateTime.Now.TimeOfDay;
            reservation.EndDate = Convert.ToDateTime(e.StartDate).Date + DateTime.Now.TimeOfDay;

            ReservationFrm reservationFrm = new ReservationFrm(reservation);
            bool result = (bool)reservationFrm.ShowDialog();

            if (result)
            {
                try
                {
                    Generic<Reservation> gen = new Generic<Reservation>();
                    if (reservation.Id == 0)
                        gen.Add(reservation);
                    else
                        gen.Update(reservation, reservation.Id);

                    gen.Dispose();
                    MessageBox.Show("The reservation was saved successfully", "Reservation saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem saving this Reservation to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // reload companies and refresh ListBox
                loadCalendar();
            }

        }

        private void loadReservations()
        {
            Generic<Reservation> generic = new Generic<Reservation>();
            Global.reservations = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void loadLocations()
        {
            Generic<Location> generic = new Generic<Location>();
            Global.locations = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void AppointmentDblClicked(int Appointment_Id)
        {
            //MessageBox.Show("You double-clicked on appointment with ID = " + Appointment_Id, "Calendar Event", MessageBoxButton.OK);

            //load reservations + locations
            loadReservations();
            loadLocations();

            //when clicked on a reservation
            Reservation reservation = Global.reservations.Find(r => r.Id == Appointment_Id);
            reservation.Location = Global.locations.Find(l => l.Id == reservation.LocationId);

            ReservationFrm reservationFrm = new ReservationFrm(reservation);
            bool result = (bool)reservationFrm.ShowDialog();

            loadCalendar();
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
            this.logout = true;
            this.Hide();
        }


        private void btnManagement_Click(object sender, RoutedEventArgs e)
        {

            if (user.IsAdmin)
            {
                ManagementFrm contractFrm = new ManagementFrm();
                contractFrm.ShowDialog();
                contractFrm.Close();
            }
            else
            {
                SettingsFrm settingsFrm = new SettingsFrm(user);
                bool result = (bool)settingsFrm.ShowDialog();
                settingsFrm.Close();

                if (result)
                    updateCompany(user.Company);
            }
        }

        public void updateCompany(Company company)
        {
            try
            {
                Generic<Company> gen = new Generic<Company>();
                gen.Update(company, company.Id);
                gen.Dispose();
                MessageBox.Show("Your company was saved successfully", "Company saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                MessageBox.Show("There was a problem saving your company to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileAdminFrm form = new ProfileAdminFrm();
            form.ShowDialog();
        }







    }
}
