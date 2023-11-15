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

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        //static boolean variable HasInvoiceBeenSelected, if an invoice is selected change to true
        //variable to hold InvoiceNumber, default to null

        //Populate the Date grid with invoices
        //Populate the combo boxes with distinct invoicenums, dates, and total charges
    }
}
