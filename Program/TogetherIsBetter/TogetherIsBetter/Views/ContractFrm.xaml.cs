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
    /// Interaction logic for ContractFrm.xaml
    /// </summary>
    public partial class ContractFrm : Window
    {

        public Contract contract;

        public ContractFrm(Contract contract)
        {
            InitializeComponent();
            this.contract = contract;

            // this is a new contract
            if (contract.Id == 0)
            {
                // preselect start date
                dateStart.SelectedDate = DateTime.Today;
            }
            else
            {
                dateStart.SelectedDate = contract.StartDate;
                dateEnd.SelectedDate = contract.EndDate;
            }

            // set initial values
            if (contract.Number != 0)
                tboNumber.Text = contract.Number.ToString();

            // set combobox binding
            cboCompany.ItemsSource = Global.companies;
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "id";

            cboContractFormula.ItemsSource = Global.contractFormula;
            cboContractFormula.DisplayMemberPath = "Description";
            cboContractFormula.SelectedValuePath = "id";

            if (!Global.user.IsAdmin)
                cboCompany.IsEnabled = false;

            // preselect company and formula
            if (contract.CompanyId != 0 && contract.ContractFormulaId != 0)
            {
                for (int i = 0; i < Global.companies.Count; i++)
                {
                    if (Global.companies[i].Id == contract.CompanyId)
                        cboCompany.SelectedIndex = i;
                }

                for (int i = 0; i < Global.contractFormula.Count; i++)
                {
                    if (Global.contractFormula[i].Id == contract.ContractFormulaId)
                        cboContractFormula.SelectedIndex = i;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            String number = tboNumber.Text.Trim();
            DateTime ?startDate = dateStart.SelectedDate;
            DateTime ?endDate = dateEnd.SelectedDate;
            int companyIndex = cboCompany.SelectedIndex;
            int formulaIndex = cboContractFormula.SelectedIndex;
            String error = "";

            if (Val.isEmpty(number)) {
                error += "Number can't be empty. \n";
            } else if(!Val.isNumber(number)){
                error += "Number can only contain numbers. \n";
            }

            if (startDate == null) {
                error += "Start date must be selected. \n";
            } else if(startDate < new DateTime()){
                error += "Start date must be in the future. \n";
            }

            if (endDate == null)
            {
                error += "End date must be selected. \n";
            } else if(endDate < startDate){
                error += "End date must be after the start date. \n";
            }

            if(companyIndex == -1)
                error += "A company must be selected. \n";

            if (formulaIndex == -1)
            {
                error += "A contract formula must be selected. \n";
            }
            
            // check if start and end date add up to the length specified by the contract formula (allow error margin of 3 days) 
            if(formulaIndex != -1 && endDate != null && startDate != null)
            {
                ContractFormula formula = Global.contractFormula[formulaIndex];
                if (formula.PeriodInMonths != null)
                {
                    int formulaLengthDays = (int)formula.PeriodInMonths * 30;
                    TimeSpan difference = ((DateTime)endDate) - ((DateTime)startDate);
                    
                    int daysDifferent = (int)difference.TotalDays - formulaLengthDays;
                    if (daysDifferent > 3)
                    {
                        error += String.Format("Incorrect end date. This contract should last for {0} months.", formulaLengthDays/30);
                    }
                }
            }



            if (error != "")
            {
                error = "Please fix this first: \n\n " + error;
                MessageBox.Show(error, "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // check for overlapping contracts
            foreach (Contract contract in Global.contracts)
                if (contract.CompanyId == Global.companies[companyIndex].Id && startDate <= contract.EndDate && this.contract.Id != contract.Id)
                {
                    MessageBoxResult result = MessageBox.Show("There is another contract that overlaps with this one. Do you want to continue?", "Overlapping contract", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                        break;
                    else
                        return;
                        
                }

            this.contract.Number = Int32.Parse(number);
            this.contract.StartDate = startDate;
            this.contract.EndDate = endDate;
            this.contract.CompanyId = Global.companies[companyIndex].Id;
            this.contract.ContractFormulaId = Global.contractFormula[formulaIndex].Id; 

            this.DialogResult = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void cboContractFormula_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateStart.SelectedDate != null && cboContractFormula.SelectedIndex != -1)
            {
                // update end date according to selected formula
                ContractFormula formula = (ContractFormula)cboContractFormula.SelectedItem;

                int numMonths = (int)formula.PeriodInMonths;
                DateTime start = (DateTime)dateStart.SelectedDate;
                dateEnd.SelectedDate = start.AddMonths(numMonths);
            }
        }

        private void dateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int formulaIndex = cboContractFormula.SelectedIndex;
            if (formulaIndex == -1) return;

            ContractFormula formula = Global.contractFormula[formulaIndex];

            if (formula.PeriodInMonths != null && dateStart.SelectedDate != null)
            {
                int months = (int)formula.PeriodInMonths;
                DateTime start = (DateTime)dateStart.SelectedDate;
                // update end date 
                dateEnd.SelectedDate = start.AddMonths(months);
            }
        }
    }
}
