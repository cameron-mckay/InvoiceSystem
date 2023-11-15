using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Item
{
    public class clsItemSQL
    {
                /// <summary>
        /// Gets all item codes and and descriptions from the ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string GetItems()
        {
            string sSQL = "select ItemCode, ItemDesc, Cost from ItemDesc";
        
            return sSQL;
        }
        
        /// <summary>
        /// Gets an invoice num based on itemcode input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string  SelectInvoicefromItemCode(string sItemCode)
        {
            string sSQL = "select distinct(InvoiceNum) from LineItems where ItemCode = '" + sItemCode + "'";
        
            return sSQL;
        }
        
        /// <summary>
        /// updates an item description and cost based  on input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <returns></returns>
        public string UpdateItemDesc(string sItemCode, string sItemCost, string sItemDesc)
        {
            string sSQL = "Update ItemDesc Set ItemDesc = '"+ sItemDesc + " ', Cost = "+ sItemCost +" where ItemCode = '" + sItemCode +"'";
        
            return sSQL;
        }  
        
        /// <summary>
        /// adds a new  item to the itemdesc table
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="sItemCost"></param>
        /// <returns></returns>
        public string InsertItemDesc(string sItemCode, string sItemDesc, string sItemCost)
        {
        
            string sSQL = "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('"+ sItemCode +"', '" + sItemDesc + "', "+ sItemCost + ")";
        
            return sSQL;
        
        }
        
        /// <summary>
        /// deletes an item based on itemcode input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string DeleteItemDesc(string sItemCode)
        {
            string sSQL = "Delete from ItemDesc Where ItemCode = '"+ sItemCode +"'";
        
            return sSQL;
        }
    }
}
