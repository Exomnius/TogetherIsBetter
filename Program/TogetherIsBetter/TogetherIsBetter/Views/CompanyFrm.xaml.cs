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

namespace TogetherIsBetter.Views
{
    /// <summary>
    /// Interaction logic for CompanyFrm.xaml
    /// </summary>
    public partial class CompanyFrm : Window
    {
        public Company company;
        public CompanyFrm(Company company)
        {
            InitializeComponent();
            this.company = company;

            tbName.Text = company.Name;
            tbStreet.Text = company.Street;
            tbZipcode.Text = company.Zipcode;
            tbCity.Text = company.City;
            tbCountry.Text = company.Country;
            tbEmail.Text = company.Email;
            tbPhone.Text = company.Phone;
            tbEmployees.Text = company.Emplyees.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            // add validation

            this.DialogResult = true;
            this.Hide();

        }
    }
}
