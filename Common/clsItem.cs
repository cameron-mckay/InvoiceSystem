using System;
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
    /// Object to represent an item
    /// </summary>
    public class clsItem
    {
        /// <summary>
        /// Name/ID of the item
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// Cost of the item
        /// </summary>
        public decimal ItemCost { get; set; }
        /// <summary>
        /// Constructor for the item object
        /// </summary>
        public clsItem() {
            try
            {
                ItemCode = string.Empty;
                ItemDesc = string.Empty;
                ItemCost = 0;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Constructor for the item object
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        public clsItem(string itemCode, string itemDesc, decimal itemCost)
        {
            try
            {
                ItemCode = itemCode;
                ItemDesc = itemDesc;
                ItemCost = itemCost;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Override the default tostring method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try {
                return this.ItemCode + " - " + this.ItemDesc;
            }
            catch (Exception ex)
            {
                Common.clsCommonUtil.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return "";
            }
        }
    }
}
