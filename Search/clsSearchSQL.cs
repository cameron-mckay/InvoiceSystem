using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Reflection;
using System.Xml.Linq;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Queries necessary for the search window
    /// </summary>
    public class clsSearchSQL
    {

        /// <summary>
        /// Selects all rows found in the Invoices Table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoices()
        {
            try
            {
                return "SELECT* FROM Invoices";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all rows found in the Invoices Table that match the same invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoicesOnInvoiceNum(int invoiceNum)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all rows found in the Invoices Table that match the given invoice number AND invoice date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoicesOnInvoiceNumAndInvoiceDate(int invoiceNum, string invoiceDate)
        {
            try
            {
                return "SELECT* FROM Invoices WHERE InvoiceNum = " + invoiceNum.ToString() + " AND InvoiceDate = #" + invoiceDate + "#";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }


        /// <summary>
        /// Selects all rows found in the Invoices table that match the given invoice number, invoice date, and total cost
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoiceOnAllColumns(int invoiceNum, string invoiceDate, string totalCost)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum.ToString() + " AND InvoiceDate = #" + invoiceDate + "# AND TotalCost = " + totalCost;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects 
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoicesOnTotalCost(string totalCost)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE TotalCost = " + totalCost;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects rows from the Invoices Table that match the given total cost and invoice date
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoicesOnTotalCostAndInvoiceDate(string totalCost, string invoiceDate)
        {
            try
            {
                return "SELECT* FROM Invoices WHERE TotalCost = " + totalCost + " and InvoiceDate = #" + invoiceDate + "#";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects rows from the invoices table that match the given invoice date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectInvoicesOnInvoiceDate(string invoiceDate)
        {
            try
            {
                return "SELECT * FROM Invoices WHERE InvoiceDate = #" + invoiceDate + "#";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all distinct invoice numbers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectDistinctInvoiceNum()
        {
            try
            {
                return "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all distinct invoice dates 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectDistinctInvoiceDate()
        {
            try
            {
                return "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all distinct total cost values
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string selectDistinctTotalCost()
        {
            try
            {
                return "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

    }
}


