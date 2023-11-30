using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : MetroWindow
    {
        /// <summary>
        /// Instantiates the search window
        /// </summary>
        public SearchWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Overrides the window close event to hide the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Hide();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        //static boolean variable HasInvoiceBeenSelected, if an invoice is selected change to true
        //variable to hold InvoiceNumber, default to null

        //Populate the Date grid with invoices
        //Populate the combo boxes with distinct invoicenums, dates, and total charges
    }
}
