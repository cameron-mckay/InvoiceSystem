using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static string GetItems()
        {
            try 
            {
            string sSQL = "select ItemCode, ItemDesc, Cost from ItemDesc";
        
            return sSQL;
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        
        /// <summary>
        /// Gets an invoice num based on itemcode input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public static string  SelectInvoicefromItemCode(string sItemCode)
        {
            try 
            {
            string sSQL = "select distinct(InvoiceNum) from LineItems where ItemCode = '" + sItemCode + "'";
        
            return sSQL;
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
        
        /// <summary>
        /// updates an item description and cost based  on input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <returns></returns>
        public static string UpdateItemDesc(string sItemCode, decimal sItemCost, string sItemDesc)
        {
            try 
            {
            string sSQL = "Update ItemDesc Set ItemDesc = '"+ sItemDesc + "', Cost = "+ sItemCost +" where ItemCode = '" + sItemCode.ToString() +"'";
        
            return sSQL;
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }  
        
        /// <summary>
        /// adds a new  item to the itemdesc table
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="sItemCost"></param>
        /// <returns></returns>
        public static string InsertItemDesc(string sItemCode, string sItemDesc, decimal sItemCost)
        {

            try 
            {
                
            string sSQL = "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('"+ sItemCode +"', '" + sItemDesc + "', "+ sItemCost.ToString() +")";
        
            return sSQL;
            }
            
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        
        }
        
        /// <summary>
        /// deletes an item based on itemcode input
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public static string DeleteItemDesc(string sItemCode)
        {
            try 
            {
            string sSQL = "Delete from ItemDesc Where ItemCode = '"+ sItemCode +"'";
        
            return sSQL;
            }

            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + 
                                    MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
