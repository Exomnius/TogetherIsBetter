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
    /// Interaction logic for ContractManagementFrm.xaml
    /// </summary>
    public partial class ManagementFrm : Window
    {
        private bool showAllContracts = false;
        public ManagementFrm()
        {
            InitializeComponent();
            loadCompanies();
            loadContracts();
            loadContractFormula();
        }

        // reloads all companies and refreshes ListBox
        public void loadCompanies()
        {
            // load companies from db
            Generic<Company> generic = new Generic<Company>();
            Global.companies = generic.GetAll().ToList();
            generic.Dispose();

            // clear listbox and add companies
            lbCompanies.Items.Clear();
            foreach (Company company in Global.companies)
            {
                lbCompanies.Items.Add(company.Name);
            }
        }

        private void btnNewCompany_Click(object sender, RoutedEventArgs e)
        {
            Company nCompany = new Company();

            int index = lbCompanies.SelectedIndex;
            saveUpdateCompany(nCompany);
        }

        private void btnEditCompany_Click(object sender, RoutedEventArgs e)
        {
            int index = lbCompanies.SelectedIndex;
            if (index == -1) return;

            saveUpdateCompany(Global.companies[index]);
        }

        private void lbCompanies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbCompanies.SelectedIndex;
            saveUpdateCompany(Global.companies[index]);
        }

        private void btnDeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            // return if no selection made
            int index = lbCompanies.SelectedIndex;
            if (index == -1) return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this company?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Generic<Company> generic = new Generic<Company>();
                    generic.Delete(Global.companies[index]);
                    generic.Dispose();

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
                    Generic<Company> gen = new Generic<Company>();
                    if (company.Id == 0)
                        gen.Add(company);
                    else
                        gen.Update(company, company.Id);
                    
                    gen.Dispose();
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
        
        private void btnNewContract_Click(object sender, RoutedEventArgs e)
        {
            Contract nContract = new Contract();

            int index = lvContracts.SelectedIndex;
            saveUpdateContract(nContract);
            loadContracts();
        }

        private void btnEditContract_Click(object sender, RoutedEventArgs e)
        {
            int index = lvContracts.SelectedIndex;
            if (index == -1) return;

            saveUpdateContract(Global.contracts[index]);
            loadContracts();
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
                    MessageBox.Show("There was a problem saving this company to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // reload companies and refresh ListBox
                loadCompanies();
            }
        }

        

        private void btnDeleteContract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = lbCompanies.SelectedIndex;
        }

        private void btnNewFormula_Click(object sender, RoutedEventArgs e)
        {
            //Formula nFormula = new Formula();

            //int index = lbFormula.SelectedIndex;
            //saveUpdateFormula(nFormula);
        }

        private void btnEditFormula_Click(object sender, RoutedEventArgs e)
        {
            //int index = lbFormula.SelectedIndex;
            //if (index == -1) return;

            //saveUpdateFormula(Global.Formula[index]);
        }

        private void btnDeleteFormula_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void loadContracts()
        {
            // load companies from db
            Generic<Contract> generic = new Generic<Contract>();
            Global.contracts = generic.GetAll().ToList();
            generic.Dispose();

            lvContracts.Items.Clear();
            foreach (Contract contract in Global.contracts)
            {
                if (showAllContracts)
                {
                    lvContracts.Items.Add(contract);   
                }
                else
                {
                    int res = ((DateTime)contract.EndDate).CompareTo(DateTime.Today);
                    if (res >= 0)
                        lvContracts.Items.Add(contract);
                }
                
            }
        }

        private void loadContractFormula()
        {
            // load companies from db
            Generic<ContractFormula> generic = new Generic<ContractFormula>();
            Global.contractFormula = generic.GetAll().ToList();
            generic.Dispose();

            lbContractFormula.Items.Clear();
            foreach (ContractFormula contractFormula in Global.contractFormula)
            {
                lbContractFormula.Items.Add(String.Format("{0}", contractFormula.Description));
            }
        }

        private void lbContractFormula_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void lvContracts_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            int index = lvContracts.SelectedIndex;
            if (index == -1) return;

            saveUpdateContract(Global.contracts[index]);
        }

        private void cboxShowAllContracts_Checked(object sender, RoutedEventArgs e)
        {
            lblContracts.Content = "All contracts";
            showAllContracts = true;
            loadContracts();
        }

        private void cboxShowAllContracts_Unchecked(object sender, RoutedEventArgs e)
        {
            lblContracts.Content = "Active contracts";
            showAllContracts = false;
            loadContracts();
            
        }





        
        
    }
}
