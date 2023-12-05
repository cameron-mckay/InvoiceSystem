using InvoiceSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Documents;

namespace InvoiceSystem.Item
{
    public class clsItemLogic
    {
        /// <summary>
        /// database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// int for storing the invoice num if an item is on an invoice
        /// </summary>
        public static string invoicenum;
           

        /// <summary>
        /// creating an object to access  the database
        /// </summary>
        public clsItemLogic() 
        { 
            db = new clsDataAccess();
        }

        /// <summary>
        /// Getting data from the db, looping through it and putting that data into a list
        /// </summary>
        /// <returns></returns>
        public List<clsItem> GetAllItems()
        {
            try
            {
                DataSet ds = new DataSet();
                List<clsItem> lstItems = new List<clsItem>();
                int iRet = 0;

                string sSQL = clsItemSQL.GetItems();
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsItem item = new clsItem();
                    item.ItemCode = dr[0].ToString();
                    item.ItemDesc = dr[1].ToString();
                    item.ItemCost = Convert.ToDecimal(dr[2]);

                    lstItems.Add(item);
                }

                return lstItems;
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }

        }

        /// <summary>
        /// Adding an item to the db
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(clsItem item)
        { 
            try
            {
                int iRet = 0;

                string sSQL = clsItemSQL.InsertItemDesc(item.ItemCode, item.ItemDesc, item.ItemCost);
                iRet = db.ExecuteNonQuery(sSQL);
            }
            

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }


        /// <summary>
        /// Editing an existing item in the db
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        public void EditItem(clsItem oldItem, clsItem newItem)
        {
            try
            {
                int iRet = 0;

                string sSQL = clsItemSQL.UpdateItemDesc(oldItem.ItemCode, newItem.ItemCost, newItem.ItemDesc);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Deleting an existing item from the db
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItem(string itemcode)
        {
            try
            {
                int iRet = 0;

                string sSQL = clsItemSQL.DeleteItemDesc(itemcode);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Checking if an item in the db is on an existing invoice
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsItemOnInvoice(clsItem item)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsItemSQL.SelectInvoicefromItemCode(item.ItemCode);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }

                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        invoicenum = dr[0].ToString();

                    }
                    
                    return true;
                }
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }

        }
        
    }
}
