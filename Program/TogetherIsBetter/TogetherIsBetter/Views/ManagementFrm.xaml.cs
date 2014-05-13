﻿using System;
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
            Global.companies = Companies.getCompanies();

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
            int index = lbCompanies.SelectedIndex;
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
        
        private void btnNewContract_Click(object sender, RoutedEventArgs e)
        {
            //Contract nContract = new Contract();

            //int index = lbContract.SelectedIndex;
            //saveUpdateContract(nContract);

        }

        private void btnEditContract_Click(object sender, RoutedEventArgs e)
        {
            //int index = lbContract.SelectedIndex;
            //if (index == -1) return;

            //saveUpdateContract(Global.Contract[index]);
        }

        private void btnDeleteContract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            Global.contracts = Contracts.getContracts();

            lbContracts.Items.Clear();

            foreach (Contract contract in Global.contracts)
            {
                lbContracts.Items.Add(String.Format("{0} \t({1:dd/MM/yy} - {2:dd/MM/yy})", Global.companies.Find(c => c.Id == contract.CompanyId).Name, contract.StartDate, contract.EndDate));
            }
        }

        private void loadContractFormula()
        {
            Global.contractFormula = Contracts.getContractForumla();

            lbContractFormula.Items.Clear();

            foreach (ContractFormula contractFormula in Global.contractFormula)
            {
                lbContractFormula.Items.Add(String.Format("{0}", contractFormula.Description));
            }
        }

        private void lbContractFormula_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        
        
    }
}
