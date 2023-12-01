using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Reference to the item window
        /// </summary>
        ItemWindow itemWindow;
        /// <summary>
        /// Reference to the search window
        /// </summary>
        SearchWindow searchWindow;
        /// <summary>
        /// Reference to the main logic object
        /// </summary>
        clsMainLogic logic;
        /// <summary>
        /// Current mode the window is in
        /// </summary>
        Mode currentMode;
        /// <summary>
        /// All the possible modes for the window
        /// </summary>
        enum Mode
        {
            VIEW,
            CREATE,
            EDIT
        }
        /// <summary>
        /// Initializes the main window
        /// </summary>
        public MainWindow()
        {
            try
            {
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                itemWindow = new ItemWindow();
                searchWindow = new SearchWindow();
                logic = new clsMainLogic();
                InitializeComponent();
                currentMode = Mode.VIEW;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Opens the search invoice window when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenInvoices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchWindow.ShowDialog();
                // Check if an invoice was selected, and what ID it was from static variables on Search window
                // If both conditions are met, open the invoice for editing
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Opens the items window when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                itemWindow.ShowDialog();
                // Check if items were updated on the static bool variable on the Item window
                // If they were, reload the items list
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Enables edit mode on the current invoice with the edit invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure program is in view mode
                if (currentMode != Mode.VIEW)
                    return;
                // Check if an invoice is loaded

                // If loaded, enable edit mode and update UI
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Adds the currently selected item to the invoice when the add to invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the currently selected item from the combo box

                // Add it to the datagrid

            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// This function runs when the choose item combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Saves the current invoice when the save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Shouldn't be possible but just in case
                if (currentMode == Mode.VIEW)
                    return;
                // Validate the current invoice

                // Call logic to save it

                // Go back to view mode
                currentMode = Mode.VIEW;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Cancels the changes on the current invoice when the cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Shouldn't be possible but just in case
                if (currentMode == Mode.VIEW)
                    return;
                // Revert fields back to the state they were in before
                // Empty if in creation mode
                // Back to original inoice if in edit mode

                // 
                currentMode = Mode.VIEW;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Removes the currently selected item from the invoice when the remove item button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Shouldn't be possible but just in case
                if (currentMode == Mode.VIEW)
                    return;
                // Get selected item / items from datagrid

                // Remove them from the invoice object
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Updates UI to reflect the current mode and variables of the program
        /// </summary>
        private void updateUI()
        {
            try
            {
                // Switch on the current mode
                switch (currentMode)
                {
                    // If window is in view mode
                    case Mode.VIEW:
                        break;
                    // If window is in edit mode
                    case Mode.EDIT:
                        break;
                    // If window is in create mode
                    case Mode.CREATE:
                        break;
                    // Shouldn't be possible
                    default: 
                        // But fix it just in case
                        currentMode = Mode.VIEW;
                        updateUI();
                        break;
                }
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
