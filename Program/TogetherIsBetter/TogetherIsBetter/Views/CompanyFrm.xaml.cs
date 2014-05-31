using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            // initialize values
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
            // get values
            String error = "";
            String name = tbName.Text.Trim();
            String street = tbStreet.Text.Trim();
            String zipcode = tbZipcode.Text.Trim();
            String city = tbCity.Text.Trim();
            String country = tbCountry.Text.Trim();
            String email = tbEmail.Text.Trim();
            String phone = tbPhone.Text.Trim();
            String employeesString = tbEmployees.Text.Trim();
            int employees = -1;

            phone = phone.Replace(" ", "");
            phone = phone.Replace("-", "");
            phone = phone.Replace(".", "");

            // validate values

            if (Val.isEmpty(name))
            {
                error += "Name can't be empty. \n";
            }
            else if (Val.isLongerThan(name, 12))
            {
                error += "Name can't be more than 12 characters long. \n";
            }

            if (Val.isEmpty(street))
            {
                error += "Street can't be empty. \n";
            }
            else if (Val.isLongerThan(street, 20))
            {
                error += "Street can't be more than 20 characters long. \n";
            }

            if (Val.isEmpty(zipcode))
            {
                error += "Zipcode can't be empty. \n";
            }
            else if (Val.isLongerThan(zipcode, 6))
            {
                error += "City can't be more than 6 characters long. \n";
            }
            else if(!Val.isNumber(zipcode))
            {
                error += "Zipcode must be a number. \n";
            }

            if (Val.isEmpty(city))
            {
                error += "City can't be empty. \n";
            }
            else if (Val.isLongerThan(city, 20))
            {
                error += "City can't be more than 20 characters long. \n";
            }

            if (Val.isEmpty(country))
            {
                error += "Country can't be empty. \n";
            }
            else if (Val.isLongerThan(country, 20))
            {
                error += "Country can't be more than 20 characters long. \n";
            }

            if (Val.isEmpty(email))
            {
                error += "Email can't be empty. \n";
            }
            else if (Val.isLongerThan(email, 50))
            {
                error += "Email can't be more than 50 characters long. \n";
            }
            else if (!Val.isEmail(email))
            {
                error += "Email must be valid. \n";
            }


            if (Val.isEmpty(phone) &&  Val.isLongerThan(phone, 12))
            {
                error += "Phone can't be more than 12 characters long. \n";
            }

            if (!Val.isEmpty(employeesString) && !Val.isNumber(employeesString))
            {
                error += "Employees must be a number. \n";
            }
            else
            {
                Int32.TryParse(employeesString, out employees);
            }

            // check if there was an error
            if (error != "")
            {
                // show error
                error = "Please fix this first: \n\n " + error;
                MessageBox.Show(error, "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // proceed to save company
                company.Name = name;
                company.Street = street;
                company.Zipcode = zipcode;
                company.City = city;
                company.Country = country;
                company.Email = email;
                company.Phone = phone;

                if (!Val.isEmpty(employeesString) && employees != -1)
                {
                    company.Emplyees = employees;
                }


                this.DialogResult = true;
                this.Hide();
            }   

        }
    }
}
