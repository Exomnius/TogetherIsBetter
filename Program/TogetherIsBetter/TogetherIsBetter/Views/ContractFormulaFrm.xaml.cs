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
    /// Interaction logic for ContractFormulaFrm.xaml
    /// </summary>
    public partial class ContractFormulaFrm : Window
    {
        public ContractFormula contractFormula;
        public ContractFormulaFrm(ContractFormula contractFormula)
        {
            InitializeComponent();
            this.contractFormula = contractFormula;

            tboDescription.Text = contractFormula.Description;
            if (contractFormula.MaxUsageHoursPerPeriod != 0)
                tboMaxHoursPerPeriod.Text = contractFormula.MaxUsageHoursPerPeriod.ToString();
            if (contractFormula.NoticePeriodInMonths != 0)
                tboNoticePeriodInMonths.Text = contractFormula.NoticePeriodInMonths.ToString();
            if (contractFormula.PeriodInMonths != 0)
                tboPeriodInMonths.Text = contractFormula.PeriodInMonths.ToString();
            if (contractFormula.Price != 0)
                tboPrice.Text = contractFormula.Price.ToString();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            String error = "";
            String description = tboDescription.Text.Trim();
            String maxHoursPerPeriod = tboMaxHoursPerPeriod.Text.Trim();
            String noticePeriodInMonths = tboNoticePeriodInMonths.Text.Trim();
            String periodInMonths = tboPeriodInMonths.Text.Trim();
            String price = tboPrice.Text.Trim();

            if (Val.isEmpty(description))
            {
                error = "Description can't be empty. \n";
            }

            if (Val.isLongerThan(description, 50))
            {
                error = "Description can't be longer than 50 charachters. \n";
            }

            if (!Val.isDecimal(maxHoursPerPeriod))
            {
                error = "Max hours per period must be a number. \n";
            }

            if (!Val.isDecimal(noticePeriodInMonths))
            {
                error = "Notice period in months must be a number. \n";
            }

            if (!Val.isDecimal(periodInMonths))
            {
                error = "Period in months must be a number. \n";
            }

            if (!Val.isDecimal(price))
            {
                error = "Price must be a number. \n";
            }

            if (error != "")
            {
                error = "Please fix this first: \n\n " + error;
                MessageBox.Show(error, "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        } 
    }
}
