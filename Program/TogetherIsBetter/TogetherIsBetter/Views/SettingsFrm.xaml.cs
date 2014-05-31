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
    /// Interaction logic for SettingsFrm.xaml
    /// </summary>
    public partial class SettingsFrm : Window
    {
        private List<Contract> usersActiveContracts;
        private User user;
        private Company company;
        public SettingsFrm(User user)
        {
            InitializeComponent();

            this.user = user;
            this.company = user.Company;


            // initialize values
            tbName.Text = company.Name;
            tbStreet.Text = company.Street;
            tbZipcode.Text = company.Zipcode;
            tbCity.Text = company.City;
            tbCountry.Text = company.Country;
            tbEmail.Text = company.Email;
            tbPhone.Text = company.Phone;
            tbEmployees.Text = company.Emplyees.ToString();
            loadContracts();

            Generic<ContractFormula> generic = new Generic<ContractFormula>();
            Global.contractFormula = generic.GetAll().ToList();
            generic.Dispose();

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
            else if (!Val.isNumber(zipcode))
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


            if (Val.isEmpty(phone) && Val.isLongerThan(phone, 12))
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

        private void loadContracts()
        {
            Generic<Contract> generic = new Generic<Contract>();
            Global.contracts = generic.GetAll().ToList();
            generic.Dispose();

            List<Contract> tempUsersContracts = Global.contracts.FindAll(c => c.CompanyId == user.Company.Id);
            usersActiveContracts = new List<Contract>();

            lbContracts.Items.Clear();
            foreach (Contract contract in tempUsersContracts)
            {
                // add if enddate is today or after
                int res = ((DateTime)contract.EndDate).CompareTo(DateTime.Today);
                if (res >= 0)
                {
                    usersActiveContracts.Add(contract);
                    lbContracts.Items.Add(String.Format("{0:dd/MM/yy} - {1:dd/MM/yy}", contract.StartDate, contract.EndDate));
                }
            }
        }

        


        private void saveUpdateContract(Contract contract)
        {
            ContractFrm cFrm = new ContractFrm(contract);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {
                try
                {
                    Generic<Contract> gen = new Generic<Contract>();
                    if (contract.Id == 0)
                        gen.Add(contract);
                    else
                        gen.Update(contract, contract.Id);

                    gen.Dispose();
                    MessageBox.Show("The contract was saved successfully", "Contract saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem saving this contract to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                loadContracts();
            }
        }

        private void btnNewContract_Click_1(object sender, RoutedEventArgs e)
        {

            Contract nContract = new Contract();
            nContract.CompanyId = company.Id;
            saveUpdateContract(nContract);
            loadContracts();
        }

        private void btnStopContract_Click(object sender, RoutedEventArgs e)
        {
            if (lbContracts.SelectedIndex == -1)
                return;

            Contract contract = usersActiveContracts[lbContracts.SelectedIndex];

            ContractFormula formula = Global.contractFormula.Find(f => f.Id == contract.ContractFormulaId);
            int? monthsNotice = formula.NoticePeriodInMonths;
            DateTime endDate = (DateTime)contract.EndDate;

            if (endDate < DateTime.Today)
            {
                MessageBox.Show("This contract has already endend", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (monthsNotice != null && DateTime.Today.AddMonths((int)monthsNotice) < endDate)
            {
                MessageBox.Show(String.Format("This contract can't be stopped. The contract formula requires a {0} month notice and this contract ends {1:dd-mm-yy}. ", (int)monthsNotice, endDate), "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (monthsNotice == null)
            {
                contract.EndDate = DateTime.Today.AddDays(-1);
            }
            else
            {
                contract.EndDate = DateTime.Today.AddMonths((int)monthsNotice);
            }


            try
            {
                Generic<Contract> gen = new Generic<Contract>();
                gen.Update(contract, contract.Id);
                gen.Dispose();

                if (monthsNotice == null)
                    MessageBox.Show("The contract has been stopped successfully.", "Contract saved", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(String.Format("The contract has a {0} months notice period. The end date has been adjusted accordingly.", (int)monthsNotice), "Contract saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                MessageBox.Show("There was a problem stopping this contract. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            loadContracts();
        }

        private void lbContracts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
