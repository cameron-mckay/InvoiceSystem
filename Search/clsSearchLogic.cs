using InvoiceSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.Common;
using System.Reflection;
using System.Windows;
using System.Diagnostics;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Contains all the business logic for the SearchWindow
    /// </summary>
    public class clsSearchLogic
    {
        //Create new clsDataAccess object and dataset
        clsDataAccess db = new clsDataAccess();
        DataSet ds = new DataSet();
        int iRet = 0;

        /// <summary>
        /// List of strings of invoice numbers
        /// </summary>
        List<string> listInvoiceNums;

        /// <summary>
        /// List of strings for invoice dates
        /// </summary>
        List<string> listInvoiceDates;

        /// <summary>
        /// List of strings for invoice costs
        /// </summary>
        List<string> listInvoiceCosts;

        /// <summary>
        /// List of objects from clsInvoices
        /// </summary>
        List<clsInvoice> listInvoices;

        /// <summary>
        /// Method that returns a list of invoice numbers for the combo box in search window
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<string> getDistinctInvoiceNums()
        {
            try
            {
                //Create new list of strings 
                listInvoiceNums = new List<string>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectDistinctInvoiceNum(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    String temp;
                    temp = dr[0].ToString();
                    listInvoiceNums.Add(temp);
                }

                //return list of invoiceNums
                return listInvoiceNums;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Method that returns list of invoice dates for the combo box in search window
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<string> getDistinctInvoiceDates()
        {
            try
            {
                //Create new list of strings 
                listInvoiceDates = new List<string>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice dates
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectDistinctInvoiceDate(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    String temp;
                    temp = dr[0].ToString().Substring(0,9);
                    listInvoiceDates.Add(temp);
                }

                //return list of invoiceNums
                return listInvoiceDates;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Method that returns list of costs that will be used to populate the combo boxes in search window
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<string> getDistinctTotalCosts()
        {
            try
            {
                //Create new list of strings 
                listInvoiceCosts = new List<string>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice costs
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectDistinctTotalCost(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    String temp;
                    temp = "$" + dr[0].ToString() + ".00";
                    listInvoiceCosts.Add(temp);
                }

                //return list of invoiceNums
                return listInvoiceCosts;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Public method that returns a list of all Invoices 
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> getInvoices()
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoices(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }
        /// <summary>
        /// Method that returns a list of invoices based on invoiceNum
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnNum(string invoiceNumber)
        {
            try
            {
                //Parse string into int
                int invoice = Int32.Parse(invoiceNumber);
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoicesOnInvoiceNum(invoice), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }
                
                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Method that returns a list of invoices based on date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnDate(string invoiceDate)
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoicesOnInvoiceDate(invoiceDate), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Method that returns a list of invoices based on total cost seleted
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnCost(string cost)
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //edit cost
                cost = cost.Substring(1, cost.Length - 3);

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoicesOnTotalCost(cost), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Method that returns a list of invoices based on number and date selected
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnNum_Date(string num, string date)
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //parse string into an integer
                int invoice = Int32.Parse(num);

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoicesOnInvoiceNumAndInvoiceDate(invoice, date), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Method that returns a list of invocies based on all columns
        /// </summary>
        /// <param name="num"></param>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnAll(string num, string date, string cost)
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //edit cost
                cost = cost.Substring(1, cost.Length - 3);

                //parse string into an integer
                int invoice = Int32.Parse(num);

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoiceOnAllColumns(invoice, date, cost), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Method that returns a list of invoices based on date and cost selected
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> updateInvoicesOnDate_Cost(string date, string cost)
        {
            try
            {
                //Create new list of invoice objects
                listInvoices = new List<clsInvoice>();

                //edit cost
                cost = cost.Substring(1, cost.Length - 3);

                //Clear dataset
                ds.Clear();
                // Set dataset to include all distinct invoice numbers
                ds = db.ExecuteSQLStatement(clsSearchSQL.selectInvoicesOnTotalCostAndInvoiceDate(cost, date), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.InvoiceNum = dr[0].ToString();
                    newInvoice.InvoiceDate = DateTime.Parse(dr[1].ToString());
                    newInvoice.InvoiceTotalCost = Int32.Parse(dr[2].ToString());
                    listInvoices.Add(newInvoice);
                }

                //Return list
                return listInvoices;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

    }
}
         /*
            public getDistinctInvoiceDates{
        
                *Will use queries to find distinct invoice dates which will then be put into a list of string the method will return*

            }

            public getDistinctTotalCosts {
        
                *Will use queries to find distinct total cost values which will then be put into a list of string the method will return*

            }

            public List<clsInvoice> getInvoices() {
                
                
                //Will first check to see what, if any, of the combox boxes have been selected, depending on which of the filters were chosen 
                //it will then call another method that corresponds with that filter which will return a list of invoice objects
            }
            
            //Other methods that will return the invoices depending on the filter choices will be written here*

    }
} */
