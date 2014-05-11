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
    /// Interaction logic for ManageClientsFrm.xaml
    /// </summary>
    public partial class CompanyManagementFrm : Window
    {
        public CompanyManagementFrm()
        {
            InitializeComponent();
            loadCompanies();
        }

        // reloads all companies and refreshes ListBox
        public void loadCompanies()
        {
            Global.companies = Companies.getCompanies();

            lbClients.Items.Clear();

            foreach (Company company in Global.companies)
            {
                lbClients.Items.Add(company.Name);
            }
        }


        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            Company nCompany = new Company();

            int index = lbClients.SelectedIndex;
            saveUpdateCompany(nCompany);

        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            int index = lbClients.SelectedIndex;
            if (index == -1) return;

            saveUpdateCompany(Global.companies[index]);
        }

        private void lbClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbClients.SelectedIndex;
            saveUpdateCompany(Global.companies[index]);
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            int index = lbClients.SelectedIndex;
            if (index == -1) return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this company?", "Are you sure?", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Companies.deleteCompany(Global.companies[index]);
                    MessageBox.Show("The company was removed successfully", "Company removed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem removing this company from the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
                // reload companies and refresh ListBox
                loadCompanies();
            }

            
        }

        public void saveUpdateCompany(Company company)
        {
            CompanyFrm cFrm = new CompanyFrm(company);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {
                try
                {
                    Companies.saveCompany(company);
                    MessageBox.Show("The company was saved successfully", "Company saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem saving this company to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // reload companies and refresh ListBox
                loadCompanies();
            }
        }

       
    }
}
