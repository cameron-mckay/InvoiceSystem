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
using InvoiceSystem.Common;

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
        /// 
        /// </summary>
        clsInvoice currentInvoice;
        /// <summary>
        /// All the possible modes for the window
        /// </summary>
        enum Mode
        {
            NO_INVOICE,
            VIEW,
            CREATE,
            EDIT,
        }
        /// <summary>
        /// Initializes the main window
        /// </summary>
        public MainWindow()
        {
            try
            {
                // Close other windows when main window is closed
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                // Initialize other windows
                itemWindow = new ItemWindow();
                searchWindow = new SearchWindow();
                // Initialize business logic
                logic = new clsMainLogic();
                // Create an empty invoice
                currentInvoice = new clsInvoice();
                // Initialize UI
                InitializeComponent();
                // Set mode
                currentMode = Mode.NO_INVOICE;
                // Get items
                cbChooseItem.ItemsSource = logic.getAllItems();
                // Set date
                dtInvoiceDate.Text = DateTime.Now.ToShortDateString();
                // Update the UI
                updateUI();
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
                currentInvoice = logic.getInvoiceByID("5017");
                currentMode = Mode.VIEW;
                dtInvoiceDate.Text = currentInvoice.InvoiceDate.ToShortDateString();
                updateUI();
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
                // Ignore if in create or edit mode
                if (currentMode == Mode.CREATE || currentMode == Mode.EDIT)
                    return;
                itemWindow.ShowDialog();
                // Check if items were updated on the static bool variable on the Item window
                // If they were, reload the items list
                cbChooseItem.ItemsSource = logic.getAllItems();
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
                if (currentInvoice != null && currentInvoice.InvoiceNum != "TBD")
                    currentMode = Mode.EDIT;
                // If loaded, enable edit mode and update UI
                updateUI();
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
                clsItem item = (clsItem)cbChooseItem.SelectedItem;
                if (item == null)
                    return;
                // Add it to the datagrid
                currentInvoice.Items.Add(item);
                // UI update will handle the stupid clear and copy
                updateUI();
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
                // If an item is selected
                if (cbChooseItem.SelectedItem != null)
                    // Update the cost text
                    txtCost.Content = $"{((clsItem)cbChooseItem.SelectedValue).ItemCost:C}";
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
                // Check the mode
                if (!(currentMode == Mode.CREATE || currentMode == Mode.EDIT))
                    return;
                // Validate the current invoice
                updateUI();
                // If invoice is valid - call logic to save it
                if(logic.invoiceValid(currentInvoice))
                {
                    // If in create mode
                    if(currentMode == Mode.CREATE)
                    {
                        // Create new invoice and set the invoice number
                        currentInvoice.InvoiceNum = logic.createNewInvoice(currentInvoice);
                    } else
                    {
                        // Update an existing invoice
                        logic.updateInvoice(currentInvoice);
                    }
                    // Get invoice from DB
                    currentInvoice = logic.getInvoiceByID(currentInvoice.InvoiceNum);
                    // Set view mode
                    currentMode = Mode.VIEW;
                    // Update the UI
                    updateUI();
                }
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
                // If in create mode
                if (currentMode == Mode.CREATE)
                {
                    // Set invoice to an empty invoice object
                    currentInvoice = new clsInvoice();
                    // Set no invoice mode
                    currentMode = Mode.NO_INVOICE;
                    // Update the UI
                    updateUI();
                }
                else if (currentMode == Mode.EDIT)
                {
                    // Reload invoice from DB
                    currentInvoice = logic.getInvoiceByID(currentInvoice.InvoiceNum);
                    // Enter view mode
                    currentMode = Mode.VIEW;
                    // Update the UI
                    updateUI();
                }
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
                // Early return if not creating or editing
                if (!(currentMode == Mode.CREATE || currentMode == Mode.EDIT))
                    return;
                // Get selected item / items from datagrid
                while (dgItemsOnInvoice.SelectedItems.Count > 0)
                {
                    // This remove at function would not work with datagrid binding
                    // Which is why I'm doing this weird clear and copy routine
                    dgItemsOnInvoice.Items.RemoveAt(dgItemsOnInvoice.SelectedIndex);
                }
                // Clear the items on current invoice
                currentInvoice.Items.Clear();
                // Loop through UI items and add them to the invoice object
                for(int i = 0; i < dgItemsOnInvoice.Items.Count; i++)
                {
                    currentInvoice.Items.Add((clsItem)dgItemsOnInvoice.Items[i]);
                }
                // Update the UI
                updateUI();
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
                    // If no invoice is loaded
                    case Mode.NO_INVOICE:
                        cbChooseItem.SelectedItem = null;
                        btnOpenInvoices.IsEnabled = true;
                        btnOpenItems.IsEnabled = true;
                        btnCreateInvoice.IsEnabled = true;
                        dtInvoiceDate.IsEnabled = false;
                        btnEditInvoice.IsEnabled = false;
                        cbChooseItem.IsEnabled = false;
                        btnAddToInvoice.IsEnabled = false;
                        btnSave.IsEnabled = false;
                        btnCancel.IsEnabled = false;
                        btnRemoveItem.IsEnabled = false;
                        break;
                    // If window is in view mode
                    case Mode.VIEW:
                        cbChooseItem.SelectedItem = null;
                        btnOpenInvoices.IsEnabled = true;
                        btnOpenItems.IsEnabled = true;
                        btnCreateInvoice.IsEnabled = true;
                        dtInvoiceDate.IsEnabled = false;
                        btnEditInvoice.IsEnabled = true;
                        cbChooseItem.IsEnabled = false;
                        btnAddToInvoice.IsEnabled = false;
                        btnSave.IsEnabled = false;
                        btnCancel.IsEnabled = false;
                        btnRemoveItem.IsEnabled = false;
                        break;
                    // If window is in edit mode
                    case Mode.EDIT:
                        btnOpenInvoices.IsEnabled = false;
                        btnOpenItems.IsEnabled = false;
                        btnCreateInvoice.IsEnabled = false;
                        dtInvoiceDate.IsEnabled = false;
                        btnEditInvoice.IsEnabled = false;
                        cbChooseItem.IsEnabled = true;
                        btnAddToInvoice.IsEnabled = true;
                        btnSave.IsEnabled = true;
                        btnCancel.IsEnabled = true;
                        btnRemoveItem.IsEnabled = currentInvoice.Items.Count > 0;
                        break;
                    // If window is in create mode
                    case Mode.CREATE:
                        btnOpenInvoices.IsEnabled = false;
                        btnOpenItems.IsEnabled = false;
                        btnCreateInvoice.IsEnabled = false;
                        dtInvoiceDate.IsEnabled = true;
                        btnEditInvoice.IsEnabled = false;
                        cbChooseItem.IsEnabled = true;
                        btnAddToInvoice.IsEnabled = true;
                        btnSave.IsEnabled = true;
                        btnCancel.IsEnabled = true;
                        btnRemoveItem.IsEnabled = currentInvoice.Items.Count > 0;
                        break;
                    // Shouldn't be possible
                    default: 
                        // But fix it just in case
                        currentMode = Mode.VIEW;
                        updateUI();
                        return;
                }
                clsItem selected = (clsItem)cbChooseItem.SelectedItem;
                decimal cost;
                if (selected != null)
                    cost = selected.ItemCost;
                else
                    cost = 0;
                txtCost.Content = $"{cost:C}";
                // Update the invoice number
                txtInvoiceNum.Text = currentInvoice.InvoiceNum;
                // Update the total cost
                txtInvoiceCost.Text = $"{currentInvoice.getTotalCost():C}";
                // Store the date from the picker box
                getDate();
                // Set the date in the picker box
                dtInvoiceDate.Text = currentInvoice.InvoiceDate.ToString();
                // Clear the visible items
                dgItemsOnInvoice.Items.Clear();
                // Copy the items back over from the invoice object
                for(int i = 0; i < currentInvoice.Items.Count; i++)
                {
                    dgItemsOnInvoice.Items.Add(currentInvoice.Items[i]);
                }
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Enter create invoice mode when create invoice is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure we are in the correct modes
                if (!(currentMode == Mode.VIEW || currentMode == Mode.NO_INVOICE))
                    return;
                // Create empty invoice
                currentInvoice = new clsInvoice();
                // Set create mode
                currentMode = Mode.CREATE;
                // Update the UI
                updateUI();
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Copy the date from the textbox to the invoice object
        /// </summary>
        private void getDate()
        {
            try
            {
                currentInvoice.InvoiceDate = DateTime.Parse(dtInvoiceDate.Text);
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
