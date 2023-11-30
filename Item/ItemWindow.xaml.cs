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
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : MetroWindow
    {
        /// <summary>
        /// Instantiates the item window object
        /// </summary>
        public ItemWindow()
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
        // static bool bHasItemsBeenChanged;  //Set to true when an item has been added/edited/deleted.  Used by main window to know if needs to  refresh items list
        // bool HasItemsBeenChanged; //Property
    }
}
