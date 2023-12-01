using System;
using System.Collections.Generic;
using System.Reflection;

namespace InvoiceSystem.Common
{
    // Pulled this from the database using the db driver since I don't have MS access
    // InvoiceNum: System.Int32
    // InvoiceDate: System.DateTime
    // TotalCost: System.Int32
    // ItemCode: System.String
    // ItemDesc: System.String
    // Cost: System.Decimal
    // InvoiceNum: System.Int32
    // LineItemNum: System.Int32
    // ItemCode: System.String

    /// <summary>
    /// Object used to represent an invoice
    /// </summary>
    public class clsInvoice
    {
        /// <summary>
        /// Unique invoice ID
        /// </summary>
        public int InvoiceNum { get; set; }
        /// <summary>
        /// Date of the invoice
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// Items on the invoice
        /// </summary>
        public List<clsItem> Items { get; set; }
        /// <summary>
        /// Constructor for the invoice object
        /// </summary>
        public clsInvoice()
        {
            try
            {
                InvoiceNum = 0;
                InvoiceDate = DateTime.Now;
                Items = new List<clsItem>();
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Constructor for the invoice object
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <param name="items"></param>
        public clsInvoice(int invoiceNum, DateTime invoiceDate, List<clsItem> items)
        {
            try
            {
                InvoiceNum = invoiceNum;
                InvoiceDate = invoiceDate;
                Items = items;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Returns the total cost of the items on the invoice
        /// </summary>
        /// <returns></returns>
        public float getTotalCost()
        {
            try
            {
                float totalCost = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    totalCost += Items[i].ItemCost;
                }
                return totalCost;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return 0;
            }
        }
    }
}
