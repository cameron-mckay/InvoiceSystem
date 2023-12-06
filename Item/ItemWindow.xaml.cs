using InvoiceSystem.Common;
using InvoiceSystem.Item;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : MetroWindow
    {
        /// <summary>
        /// Set to true when an item has been added/edited/deleted.  Used by main window to know if needs to  refresh items list
        /// </summary>
        static public bool bHasItemsChanged;

        /// <summary>
        /// true or false on if a user is adding an item
        /// </summary>
        private bool bAddItemMode;

        /// <summary>
        /// true or false on if a user is editing an item
        /// </summary>
        private bool bEditItemMode;

        /// <summary>
        /// true or false on if an item is currently selected
        /// </summary>
        private bool bItemSelected;

        /// <summary>
        /// true or false on if a code already exists in the database
        /// </summary>
        private bool bIsCodeTaken;

        /// <summary>
        /// clsItemLogic object
        /// </summary>
        clsItemLogic MyItem;

        /// <summary>
        /// variable to store the currently selected item, for edit and delete purposes
        /// </summary>
        clsItem item;

        /// <summary>
        /// List to hold initial datagrid so we don't have to repeatedly call the database, refreshed from the db 
        /// whenever an item is added, deleted, or edited
        /// </summary>
        List<clsItem> Items;

        /// <summary>
        /// Instantiates the item window object
        /// </summary>
        public ItemWindow()
        {
            try
            {
                InitializeComponent();

                MyItem = new clsItemLogic();

                Items = new List<clsItem>();

                this.ItemList.ItemsSource = MyItem.GetAllItems();

                Items = MyItem.GetAllItems();

                item = new clsItem();

            }
            catch (Exception ex)
            {
                
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// show window
        /// </summary>
        public void showWindow()
        {
            try
            {
                this.ShowDialog();
            }

            catch (Exception ex)
            {
                
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
               
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void DataGridRefresh()
        {
            try
            {
                this.ItemList.ItemsSource = MyItem.GetAllItems();

                Items = MyItem.GetAllItems();
            }

            catch (Exception ex)
            {
                
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// How the window changes when you click an item in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            { 
                
                if (ItemList.CurrentItem != null) 
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Items[i].ToString() == ItemList.CurrentItem.ToString())
                        {
                            CodeTextbox.Text = Items[i].ItemCode.ToString();
                            CostTextbox.Text = "$" + Items[i].ItemCost.ToString();
                            DescTextbox.Text = Items[i].ItemDesc.ToString();
                            item = Items[i];
                            bItemSelected = true;
                        }
                    }
                }

                else
                {
                    CodeTextbox.Text = "";
                    CostTextbox.Text = "";
                    DescTextbox.Text = "";
                }
            }

            catch (Exception ex)
            {
               
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// clicking add item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {     
                bAddItemMode = true;
                CodeTextbox.Text = "";
                CostTextbox.Text = "";
                DescTextbox.Text = "";
                CodeTextbox.IsReadOnly = false;
                CostTextbox.IsReadOnly = false;
                DescTextbox.IsReadOnly = false;
                ItemList.IsEnabled = false;
                LockWindow();
                ErrorLabel.Content = "Insert your item info into the above textboxes!\n Code must only be letters and numbers \n4 characters in length";
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }



        }

        /// <summary>
        /// clicking edit item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bItemSelected == true)
                {
                    LockWindow();
                    bEditItemMode = true;
                    CostTextbox.IsReadOnly = false;
                    DescTextbox.IsReadOnly = false;
                    ItemList.IsEnabled = false;
                    ErrorLabel.Content = "Edit the cost and description in the above textboxes! Code cannot be changed";
                    CodeTextbox.Text = item.ItemCode.ToString();
                    CostTextbox.Text = item.ItemCost.ToString();
                    DescTextbox.Text = item.ItemDesc.ToString();
                }

                else
                {
                    ErrorLabel.Content = "Item must be selected!";
                }
                
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// clicking delete item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (bItemSelected == true)
                {
                    if (MyItem.IsItemOnInvoice(item) == true)
                    {
                        ErrorLabel.Content = "This item is on an invoice and cannot be deleted \n Item is on invoice: " + clsItemLogic.invoicenum;
                    }

                    else
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? This cannot be undone", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            MyItem.DeleteItem(item.ItemCode);

                            DataGridRefresh();

                            ErrorLabel.Content = "";

                            bItemSelected = false;

                            bHasItemsChanged = true;
                        }

                        else
                        {
                            ResetWindow();
                        }
                            
                    }
                    

                }

                else
                {
                    ErrorLabel.Content = "An item must be selected to delete!";
                }
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// clicking save item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// item that is  being created with our textbox input
                clsItem item2;
                item2 = new clsItem();
                bIsCodeTaken = false;
                

                if (bAddItemMode == true)
                {
                    if (CodeTextbox.Text == "" || CostTextbox.Text == "" || DescTextbox.Text == "")
                    {
                        ErrorLabel.Content = "All information must be provided!";
                    }

                    else
                    {
                        for (int i = 0; i < Items.Count; i++)
                        {
                            if (CodeTextbox.Text == Items[i].ItemCode)
                            {
                                ErrorLabel.Content = "This item code is already taken!";
                                bIsCodeTaken = true;
                            }
                        }

                        if (bIsCodeTaken == false)
                        {
                            item2.ItemCode = CodeTextbox.Text;
                            item2.ItemCost = Convert.ToDecimal(CostTextbox.Text);
                            item2.ItemDesc = DescTextbox.Text;

                            MyItem.AddItem(item2);
                            DataGridRefresh();

                            CodeTextbox.Text = "";
                            CostTextbox.Text = "";
                            DescTextbox.Text = "";
                            ErrorLabel.Content = "Item added!";
                            bAddItemMode = false;
                            bHasItemsChanged = true;
                            UnlockWindow();
                            ItemList.IsEnabled = true;
                        }
                    }
                    

                    

                }

                if (bEditItemMode == true)
                {
                    if (CostTextbox.Text == "" || DescTextbox.Text == "")
                    {
                        ErrorLabel.Content = "All information must be provided!";
                    }

                    else
                    {
                        item2.ItemCode = CodeTextbox.Text;
                        item2.ItemCost = Convert.ToDecimal(CostTextbox.Text);
                        item2.ItemDesc = DescTextbox.Text;

                        MyItem.EditItem(item, item2);
                        DataGridRefresh();

                        CodeTextbox.Text = "";
                        CostTextbox.Text = "";
                        DescTextbox.Text = "";
                        ErrorLabel.Content = "Item Edited!";
                        UnlockWindow();
                        ItemList.IsEnabled = true;
                        bEditItemMode = false;
                        bHasItemsChanged = true;
                    }
                    
                }
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// How the window changes when hitting add item or edit item
        /// </summary>
        private void LockWindow()
        {
            try
            {
                btnAddItem.IsEnabled = false;
                btnEditItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;
                btnSaveItem.IsEnabled = true;
                btnCancel.IsEnabled = true;
            }


            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// How the window changes after successfully adding or editing an item
        /// </summary>
        private void UnlockWindow()
        {
            try
            {
                btnAddItem.IsEnabled = true;
                btnEditItem.IsEnabled = true;
                btnDeleteItem.IsEnabled = true;
                btnSaveItem.IsEnabled = false;
                btnCancel.IsEnabled = false;

                CostTextbox.IsReadOnly = true;
                CodeTextbox.IsReadOnly = true;
                DescTextbox.IsReadOnly = true;

                bIsCodeTaken = false;
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Resetting the window back to its default state
        /// </summary>
        public void ResetWindow()
        {
            btnAddItem.IsEnabled = true;
            btnEditItem.IsEnabled = true;
            btnDeleteItem.IsEnabled = true;
            btnSaveItem.IsEnabled = false;
            btnCancel.IsEnabled = false;
            ItemList.IsEnabled = true;

            bAddItemMode = false;
            bEditItemMode = false;
            bItemSelected = false;
            bIsCodeTaken = false;

            CodeTextbox.Text = "";
            CostTextbox.Text = "";
            DescTextbox.Text = "";
            ErrorLabel.Content = "";

            CostTextbox.IsReadOnly = true;
            CodeTextbox.IsReadOnly = true;
            DescTextbox.IsReadOnly = true;

        }

        /// <summary>
        /// textbox that only allows numbers, used for cost textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// textbox that only allows numbers and letters, used for code textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoSymbolValidationTextbox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^a-zA-Z0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }

            catch (Exception ex)
            {

                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// Hitting the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
        }
    }
}
