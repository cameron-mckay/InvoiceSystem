using InvoiceSystem.Common;
using InvoiceSystem.Item;
using InvoiceSystem.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    /// <summary>
    /// Logic for the main window
    /// </summary>
    public class clsMainLogic
    {
        /// <summary>
        /// Database connection
        /// </summary>
        clsDataAccess db;
        /// <summary>
        /// Constructor for main logic
        /// </summary>
        /// <exception cref="Exception"></exception>
        public clsMainLogic()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        /// <summary>
        /// Fetches an invoice object by ID
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public clsInvoice getInvoiceByID(string invoiceID)
        {

            try
            {
                int rowsEffected = 0;
                // Get line items from db
                DataSet invoice = db.ExecuteSQLStatement(clsMainSQL.selectInvoice(invoiceID), ref rowsEffected);
                // Create the list of items
                if(rowsEffected == 0)
                {
                    throw new Exception("Failed to fetch invoice with ID: "+invoiceID);
                }
                string id = invoice.Tables[0].Rows[0][0].ToString();
                DateTime invoiceDate = (DateTime)invoice.Tables[0].Rows[0][1];
                // Skip total cost - add items and use method instead
                // Get the items on invoice
                DataSet lineItems = db.ExecuteSQLStatement(clsMainSQL.selectLineItemsOnInvoice(invoiceID),ref rowsEffected);
                // Create the list
                List<clsItem> items = new List<clsItem>();
                // Loop through items and add them to list
                for (int i = 0; i < lineItems.Tables[0].Rows.Count; i++)
                {
                    string itemCode = (string)lineItems.Tables[0].Rows[i][0];
                    string itemDesc = (string)lineItems.Tables[0].Rows[i][1];
                    decimal cost = (decimal)lineItems.Tables[0].Rows[i][2];
                    items.Add(new clsItem(itemCode, itemDesc, cost));
                }
                // Create the invoice object and return
                return new clsInvoice(id, invoiceDate, items);
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        /// <summary>
        /// Creates a new invoice in the database and returns the ID
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string createNewInvoice(clsInvoice invoice)
        {
            try
            {
                Trace.WriteLine(invoice.InvoiceDate.ToString());
                Trace.WriteLine(invoice.Items.Count.ToString());
                Trace.WriteLine(invoice.getTotalCost().ToString());
                // Insert the invoice
                int rowsEffected = db.ExecuteNonQuery(clsMainSQL.insertInvoice(invoice.InvoiceDate.ToString(), invoice.getTotalCost()));
                Trace.WriteLine("TEST");
                // If failed, throw error
                if(rowsEffected == 0)
                {
                    throw new Exception("Failed to create invoice");
                }
                string invoiceNum = (string)db.ExecuteScalarSQL(clsMainSQL.getNewInvoiceNum());
                Trace.WriteLine(invoiceNum);
                // Loop through items
                for(int i = 0; i < invoice.Items.Count; i++)
                {
                    // Insert line item
                    Trace.WriteLine(invoiceNum);
                    Trace.WriteLine(invoice.Items[i].ItemCode);
                    Trace.WriteLine(i);
                    Trace.WriteLine(clsMainSQL.insertLineItem(invoiceNum, i, invoice.Items[i].ItemCode));

                    rowsEffected = db.ExecuteNonQuery(clsMainSQL.insertLineItem(invoiceNum, i, invoice.Items[i].ItemCode));
                    Trace.WriteLine("TEST");
                    // If failed throw error
                    if(rowsEffected == 0)
                    {
                        throw new Exception("Failed to insert line item");
                    }
                }
                return invoiceNum;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        /// <summary>
        /// Updates an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <exception cref="Exception"></exception>
        public void updateInvoice(clsInvoice invoice)
        {
            try
            {
                // Delete existing line items
                int rowsEffected = db.ExecuteNonQuery(clsMainSQL.deleteLineItems(invoice.InvoiceNum));
                if (rowsEffected == 0) {
                    throw new Exception("Could not delete line items");
                }
                // create new line items
                for (int i = 0; i < invoice.Items.Count; i++)
                {
                    // Insert the line item
                    rowsEffected = db.ExecuteNonQuery(clsMainSQL.insertLineItem(invoice.InvoiceNum, i+1, invoice.Items[i].ItemCode));
                    // If failed
                    if (rowsEffected == 0) {
                        // Delete line items
                        db.ExecuteNonQuery(clsMainSQL.deleteLineItems(invoice.InvoiceNum));
                        throw new Exception("Could not insert line items");
                    }
                }
                // Update the total cost
                rowsEffected = db.ExecuteNonQuery(clsMainSQL.updateTotalCost(invoice.InvoiceNum, invoice.getTotalCost()));
                // Throw an error if unsuccessful
                if (rowsEffected == 0) {
                    throw new Exception("Could not update total cost");
                }
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        /// <summary>
        /// Returns true if an invoice is valid
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool invoiceValid(clsInvoice invoice)
        {
            try
            {
                // Make sure date is valid and invoice has items
                return invoice.InvoiceDate < DateTime.Now &&
                    invoice.Items.Count > 0;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        public List<clsItem> getAllItems()
        {
            try
            {
                // Get list of items
                List<clsItem> items = new List<clsItem>();
                // Create ref for num rows returned
                int numRows = 0;
                // Execute get items query
                DataSet itemsFromDB = db.ExecuteSQLStatement(clsItemSQL.GetItems(), ref numRows);
                // Get the item table
                DataTable itemsTable = itemsFromDB.Tables[0];
                // Loop through all the rows and create the item objects
                for (int i = 0; i < itemsTable.Rows.Count; i++)
                {
                    string itemCode = (string)itemsTable.Rows[i][0];
                    string itemDesc = (string)itemsTable.Rows[i][1];
                    decimal itemCost = (decimal)itemsTable.Rows[i][2];
                    items.Add(new clsItem(itemCode, itemDesc, itemCost));
                }
                // Return items list
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
