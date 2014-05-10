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
    public partial class CompanyManageFrm : Window
    {
        public CompanyManageFrm()
        {
            InitializeComponent();
            
            // reload companies
            Global.companies = Companies.getCompanies();

            foreach (Company company in Global.companies)
            {
                lbClients.Items.Add(company.Name);
            }
        }


        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
            Company nCompany = new Company();

            CompanyFrm cFrm = new CompanyFrm(nCompany);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {

            }

        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            int index = lbClients.SelectedIndex;

            CompanyFrm cFrm = new CompanyFrm(Global.companies[index]);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {

            }
        }

        private void lbClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbClients.SelectedIndex;

            CompanyFrm cFrm = new CompanyFrm(Global.companies[index]);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {

            }
        }
    }
}
