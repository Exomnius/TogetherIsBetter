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
using System.Windows.Shapes;
using TogetherIsBetter.Model;

namespace TogetherIsBetter.Views
{
    /// <summary>
    /// Interaction logic for ReservationFrm.xaml
    /// </summary>
    public partial class ReservationFrm : Window
    {
        public Reservation reservation;
        public ReservationFrm(Reservation reservation)
        {
            InitializeComponent();
            this.reservation = reservation;

            //binding
            if (reservation.Number != null && reservation.StartDate != null && reservation.EndDate != null)
            {
                lblCompany.Content = Global.companies.Find(c => c.Id == reservation.CompanyId).Name;
                tboNumber.Text = reservation.Number.ToString();
                dateStart.Value = (DateTime)reservation.StartDate;
                dateEnd.Value = (DateTime)reservation.EndDate;
            }
            else
            {
                lblCompany.Content = Global.user.Company.Name;
                dateStart.Value = (DateTime)reservation.StartDate;
                dateEnd.Value = (DateTime)reservation.EndDate;
            }

            //add locations to combobox
            loadLocations();
            cboLocation.ItemsSource = Global.locations;
            cboLocation.DisplayMemberPath = "Name";
            cboLocation.SelectedValuePath = "id";

            if (reservation.Location != null)
            {
                // preselect company and formula
                if (reservation.Location.Id != 0)
                {
                    for (int i = 0; i < Global.locations.Count; i++)
                    {
                        if (Global.locations[i].Id == reservation.Location.Id)
                            cboLocation.SelectedIndex = i;
                    }
                }
            }
            else if (cboLocation.Items.Count > 0)
            {
                cboLocation.SelectedIndex = 0;
            }
                
        }

        private void loadLocations()
        {
            Generic<Location> generic = new Generic<Location>();
            Global.locations = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void loadContracts()
        {
            Generic<Contract> generic = new Generic<Contract>();
            Global.contracts = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void loadContractFormulas()
        {
            Generic<ContractFormula> generic = new Generic<ContractFormula>();
            Global.contractFormula = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void loadReservations()
        {
            Generic<Reservation> generic = new Generic<Reservation>();
            Global.reservations = generic.GetAll().ToList();
            generic.Dispose();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            loadContracts();
            loadContractFormulas();
            loadReservations();

            String error = "";
            String number = tboNumber.Text.Trim();
            DateTime startDate = (DateTime)dateStart.Value;
            DateTime endDate = (DateTime)dateEnd.Value;
            Location location = (Location)cboLocation.SelectedItem;

            TimeSpan counter = new TimeSpan();
            foreach (var reser in Global.reservations)
            {
                if (reser.CompanyId == Global.user.Company.Id)
                {
                    counter.Add((TimeSpan)(reser.EndDate - reser.StartDate));
                }
            }

            List<Contract> temp = Global.contracts.FindAll(c => c.CompanyId == Global.user.Company.Id);
            Contract contract = null;
            bool validDate = true; ;

            foreach (Contract contr in temp)
            {
                if (contr.EndDate >= endDate && contr.StartDate <= startDate)
                {
                    validDate = true;
                    contract = contr;
                }
            }

            if (!validDate)
            {
                error += "Your contract has expired at the time of the reservation. \n";
            }

            if (Val.isEmpty(number))
            {
                error += "Number can't be empty. \n";
            }

            if (Val.isLongerThan(number, 50))
            {
                error = "Number can't be longer than 50 charachters. \n";
            }

            if (!Val.isNumber(number))
            {
                error += "Number must be a number. \n";
            }

            if (contract != null)
            {
                ContractFormula contractFormula = Global.contractFormula.Find(f => f.Id == contract.ContractFormulaId);

                if (contractFormula.MaxUsageHoursPerPeriod != null && counter.TotalMinutes > contractFormula.MaxUsageHoursPerPeriod * 60)
                {
                    error = "You exceeded your hour limit of your contract \n";
                }
            }

            if (startDate < DateTime.Now)
            {
                error += "The start date can not be in the past. \n";
            }

            if (endDate <= startDate)
            {
                error += "The end date cannot be smaller than the start date. \n";
            }

            foreach (Reservation res in Global.reservations)
            {
                if (res.LocationId == location.Id && res.StartDate >= startDate && res.EndDate <= startDate)
                {
                    error += "There is already another meeting in place at that time. \n";
                    break;
                }
            }

            if (error != "")
            {
                error = "Please Correct this first: \n\n" + error;
                MessageBox.Show(error, "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 

            try
            {
                reservation.Number = int.Parse(tboNumber.Text);
                reservation.StartDate = startDate;
                reservation.EndDate = endDate;
                reservation.LocationId = location.Id;
                reservation.CompanyId = Global.user.Company.Id;
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occurred. Please contact your sysadmin.", "Unknown error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            this.DialogResult = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this reservation?", "Are you sure?", MessageBoxButton.YesNo))
            {
                try
                {
                    Generic<Reservation> generic = new Generic<Reservation>();
                    generic.Delete(reservation);
                    generic.Dispose();

                    MessageBox.Show("The reservation was removed successfully", "Reservation removed", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem removing this reservation from the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }   
        } 
    }
}
