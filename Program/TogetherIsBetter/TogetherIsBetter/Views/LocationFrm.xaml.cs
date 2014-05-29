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
    /// Interaction logic for locationFrm.xaml
    /// </summary>
    public partial class LocationFrm : Window
    {
        public Location location;
        public LocationFrm(Location location)
        {
            InitializeComponent();
            this.location = location;

            tbName.Text = location.Name;
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

            // validate values

            if (Val.isEmpty(name))
            {
                error += "Name can't be empty. \n";
            }
            else if (Val.isLongerThan(name, 12))
            {
                error += "Name can't be more than 12 characters long. \n";
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
                // proceed to save location
                location.Name = name;
                //location.Street = street;
                //location.Zipcode = zipcode;
                //location.City = city;
                //location.Country = country;            

                this.DialogResult = true;
                this.Hide();
            }            

        }
    }
}
