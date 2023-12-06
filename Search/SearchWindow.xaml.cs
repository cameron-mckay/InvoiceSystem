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
using InvoiceSystem.Search;
using InvoiceSystem.Common;
using System.ComponentModel;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : MetroWindow
    {

        /// <summary>
        /// Boolean that tracks whether user has selected an invoice
        /// </summary>
        public static Boolean HasInvoiceBeenSelected;

        /// <summary>
        /// String that will hold invoice number, if non selected will be set to null on default
        /// </summary>
        public static string sInvoiceNumber;

        /// <summary>
        /// Instantiates the search window
        /// </summary>
        public SearchWindow()
        {
            try
            {
                InitializeComponent();
                //Call method that populates combo boxes and data grid
                populateWindow();
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
        /// <summary>
        /// Method that populates the DataGrid with a list of Invoice objects and the Combo boxes with values
        /// </summary>
        private void populateWindow()
        {
            try
            {
                //Populate combo boxes with invoice nums, dates, and costs
                clsSearchLogic searchLogic = new clsSearchLogic();
                cboInvoiceNumberComboBox.ItemsSource = searchLogic.getDistinctInvoiceNums();
                cboInvoiceDateComboBox.ItemsSource = searchLogic.getDistinctInvoiceDates();
                cboTotalChargeComboBox.ItemsSource = searchLogic.getDistinctTotalCosts();

                //Populate data grid with list of invoice objects
                InvoicesDataGrid.ItemsSource = searchLogic.getInvoices();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method that takes care of sorting list of invoices based on selections from user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Reset hasBeenSelected boolean and invoiceNum int
                HasInvoiceBeenSelected = false;
                sInvoiceNumber = null;

                //Reset selectedItems label and reset selection from datagrid
                InvoicesDataGrid.SelectedItem = null;
                lbSelectedItemLabel.Content = "Selected Item: None";

                clsSearchLogic searchLogic = new clsSearchLogic();

                //Based on which combo boxes are selected, a method will be called to update the data grid based on selections
                if (cboInvoiceNumberComboBox.SelectedItem != null && cboInvoiceDateComboBox.SelectedItem == null && cboTotalChargeComboBox.SelectedItem == null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnNum(cboInvoiceNumberComboBox.SelectedItem.ToString());
                }
                if (cboInvoiceNumberComboBox.SelectedItem == null && cboInvoiceDateComboBox.SelectedItem != null && cboTotalChargeComboBox.SelectedItem == null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnDate(cboInvoiceDateComboBox.SelectedItem.ToString());
                }
                if (cboInvoiceNumberComboBox.SelectedItem == null && cboInvoiceDateComboBox.SelectedItem == null && cboTotalChargeComboBox.SelectedItem != null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnCost(cboTotalChargeComboBox.SelectedItem.ToString());
                }
                if (cboInvoiceNumberComboBox.SelectedItem != null && cboInvoiceDateComboBox.SelectedItem != null && cboTotalChargeComboBox.SelectedItem == null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnNum_Date(cboInvoiceNumberComboBox.SelectedItem.ToString(), cboInvoiceDateComboBox.SelectedItem.ToString());
                }
                if (cboInvoiceNumberComboBox.SelectedItem != null && cboInvoiceDateComboBox.SelectedItem != null && cboTotalChargeComboBox.SelectedItem != null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnAll(cboInvoiceNumberComboBox.SelectedItem.ToString(), cboInvoiceDateComboBox.SelectedItem.ToString(), cboTotalChargeComboBox.SelectedItem.ToString());
                }
                if (cboInvoiceNumberComboBox.SelectedItem == null && cboInvoiceDateComboBox.SelectedItem != null && cboTotalChargeComboBox.SelectedItem != null)
                {
                    InvoicesDataGrid.ItemsSource = searchLogic.updateInvoicesOnDate_Cost(cboInvoiceDateComboBox.SelectedItem.ToString(), cboTotalChargeComboBox.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method that clears all the selection in combo boxes, and rests list of invoices in data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                resetWindow();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method that handles the event in which an invoice is selected from data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoicesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (InvoicesDataGrid.SelectedItem != null)
                {
                    //Select new item, update label, and set boolean to true;
                    clsInvoice selectedInvoice = (clsInvoice)InvoicesDataGrid.SelectedItem;
                    lbSelectedItemLabel.Content = "Selected Item: Invoice Number " + selectedInvoice.InvoiceNum.ToString();
                    HasInvoiceBeenSelected = true;
                    sInvoiceNumber = selectedInvoice.InvoiceNum;
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Button that is pressed when user has selected an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InvoicesDataGrid.SelectedItem != null)
                {
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Resets the contents of window back to default
        /// </summary>
        public void resetWindow()
        {
            try
            {
                //Reset hasBeenSelected boolean and invoiceNum int
                HasInvoiceBeenSelected = false;
                sInvoiceNumber = null;

                clsSearchLogic searchLogic = new clsSearchLogic();

                //Reset selectedItems label and reset selection from datagrid
                InvoicesDataGrid.SelectedItem = null;
                lbSelectedItemLabel.Content = "Selected Item: None";

                //Resets datagrid and sets all combo boxes to null
                cboTotalChargeComboBox.SelectedItem = null;
                cboInvoiceNumberComboBox.SelectedItem = null;
                cboInvoiceDateComboBox.SelectedItem = null;

                //Populate window again
                populateWindow();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Override the OnClosing Event to set HasInvoiceBeenSelected to false if the user closes out of the window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                HasInvoiceBeenSelected = false;
                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
