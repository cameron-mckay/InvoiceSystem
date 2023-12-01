using InvoiceSystem.Common;
using System;
using System.Collections.Generic;
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
        public clsInvoice getInvoiceByID(int invoiceID)
        {

            try
            {
                // Get line items from db

                // Create the list of items

                // Get the invoice from the db

                // Create the invoice object and return
                return new clsInvoice();
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
        public int createNewInvoice(clsInvoice invoice)
        {
            try
            {
                // Get line items from db

                // Create the list of items

                // Get the invoice from the db

                // Create the invoice object and return
                return 1;
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

                // update invoice

                // create new line items

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
                // Delete existing line items

                // update invoice

                // create new line items
                
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
