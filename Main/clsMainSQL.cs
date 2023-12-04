using System;
using System.Reflection;

namespace InvoiceSystem
{
    /// <summary>
    /// SQL queries for the main window
    /// </summary>
    public class clsMainSQL
    {
        /// <summary>
        /// Updates the total cost on an invoice
        /// Ex: UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public static string updateTotalCost(string invoiceNum, float totalCost)
        {
            try
            {
                return "UPDATE Invoices SET TotalCost = " + totalCost.ToString() + " WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Inserts line items
        /// Ex: INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (123, 1, 'AA')
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static string insertLineItem(string invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (" + invoiceNum + ", " + lineItemNum.ToString() + ", '" + itemCode + "')";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Inserts a new invoice
        /// Ex: INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/13/2018#, 100)
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public static string insertInvoice(string invoiceDate, float totalCost)
        {
            try
            {
                return "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#" + invoiceDate + "#, " + totalCost.ToString() + ")";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        
        /// <summary>
        /// Selects an invoice by ID
        /// Ex: SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string selectInvoice(string invoiceNum)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects all item descriptions
        /// Ex: SELECT ItemCode, ItemDesc, Cost from ItemDesc
        /// </summary>
        /// <returns></returns>
        public static string selectItemDesc()
        {
            try
            {
                return "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Selects the line items associated with an invoice ID
        /// Ex: SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = 5000
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string selectLineItemsOnInvoice(string invoiceNum)
        {
            try
            {
                return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNum;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Deletes all the line items associated with an invoice ID
        /// Ex: DELETE FROM LineItems WHERE InvoiceNum = 5000
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string deleteLineItems(string invoiceNum)
        {
            try
            {
                return "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        public static string getNewInvoiceNum()
        {
            try
            {
                return "SELECT MAX(InvoiceNum) FROM Invoices";
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
