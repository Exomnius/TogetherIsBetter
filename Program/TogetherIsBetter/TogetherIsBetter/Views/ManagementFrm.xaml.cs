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

            Company company = Global.companies[index];

            // check for existing contracts
            if (Global.contracts.Find(contract => contract.CompanyId == company.Id) != null)
            {
                MessageBox.Show("You can't delete this company because it has 1 or more existing contracts.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

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
            saveUpdateContract(nContract);
            loadContracts();
        }

        private void btnEditContract_Click(object sender, RoutedEventArgs e)
        {
            if (lvContracts.SelectedIndex == -1)
                return;

            Contract contract = (Contract)lvContracts.SelectedItem;
            saveUpdateContract(Global.contracts.Find(c => c.Id == contract.Id));
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
            // return if no selection made
            int index = lvContracts.SelectedIndex;
            if (index == -1) return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this contract?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Generic<Contract> generic = new Generic<Contract>();
                    generic.Delete(Global.contracts[index]);
                    generic.Dispose();

                    MessageBox.Show("The contract was removed successfully", "Contract removed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem removing this contract from the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                loadContracts();
            }
        }

        private void lvContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = lbCompanies.SelectedIndex;
        }

        private void btnNewFormula_Click(object sender, RoutedEventArgs e)
        {
            ContractFormula nFormula = new ContractFormula();

            int index = lbContractFormula.SelectedIndex;
            saveUpdateFormula(nFormula);
            loadContractFormula();
        }

        private void btnEditFormula_Click(object sender, RoutedEventArgs e)
        {
            int index = lbContractFormula.SelectedIndex;
            if (index == -1) return;

            saveUpdateFormula(Global.contractFormula[index]);
            loadContractFormula();
        }

        private void btnDeleteFormula_Click(object sender, RoutedEventArgs e)
        {
            // return if no selection made
            int index = lbContractFormula.SelectedIndex;
            if (index == -1) return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this contract formula?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Generic<ContractFormula> generic = new Generic<ContractFormula>();
                    generic.Delete(Global.contractFormula[index]);
                    generic.Dispose();

                    MessageBox.Show("The contract formula was removed successfully", "Company removed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem removing this contract formula from the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                loadContractFormula();
            }
        }

        

        private void loadContracts()
        {
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
            if (lvContracts.SelectedIndex == -1)
                return;

            Contract contract = (Contract)lvContracts.SelectedItem;
            saveUpdateContract(Global.contracts.Find(c => c.Id == contract.Id));
            loadContracts();
        }

        private void cboxShowAllContracts_Checked(object sender, RoutedEventArgs e)
        {
            lblContracts.Content = "Showing: all contracts";
            showAllContracts = true;
            loadContracts();
        }

        private void cboxShowAllContracts_Unchecked(object sender, RoutedEventArgs e)
        {
            lblContracts.Content = "Showing: active contracts";
            showAllContracts = false;
            loadContracts();
            
        }


        public void saveUpdateFormula(ContractFormula contractFormula)
        {
            ContractFormulaFrm cFrm = new ContractFormulaFrm(contractFormula);
            bool result = (bool)cFrm.ShowDialog();
            cFrm.Close();

            if (result)
            {
                try
                {
                    Generic<ContractFormula> gen = new Generic<ContractFormula>();
                    if (contractFormula.Id == 0)
                        gen.Add(contractFormula);
                    else
                        gen.Update(contractFormula, contractFormula.Id);

                    gen.Dispose();
                    MessageBox.Show("The contract formula was saved successfully", "Contract formula saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem saving this contract formula to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void lbContractFormula_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbContractFormula.SelectedIndex;
            if (index == -1) return;

            saveUpdateFormula(Global.contractFormula[index]);
        }

        private void btnStopContract_Click(object sender, RoutedEventArgs e)
        {
            if (lvContracts.SelectedIndex == -1)
                return;

            Contract contract = (Contract)lvContracts.SelectedItem;
            ContractFormula formula = Global.contractFormula.Find(f => f.Id == contract.ContractFormulaId);
            int? monthsNotice = formula.NoticePeriodInMonths;
            DateTime endDate = (DateTime)contract.EndDate;
           
            if (monthsNotice == null)
            {
                if (endDate < DateTime.Today)
                {
                    MessageBox.Show("This contract has already endend", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else if (DateTime.Today.AddMonths((int)monthsNotice) < endDate)
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
                MessageBox.Show("The contract has been stopped successfully", "Contract saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                MessageBox.Show("There was a problem stopping this contract. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            loadContracts();
        }




        
        
    }
}
