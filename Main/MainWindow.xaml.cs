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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ItemWindow itemWindow;
        SearchWindow searchWindow;
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
    }
}
