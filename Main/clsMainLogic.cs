using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using GroupFinal3280.Common;

namespace GroupFinal3280.Main
{
    class clsMainLogic
    {

        /// <summary>
        /// Instance of clsMainSQL to execute SQL statements
        /// </summary>
        private static clsMainSQL mainSQL = new clsMainSQL();

        /// <summary>
        /// Get a DataSet of all ItemDesc rows and return as a list of clsItem objects
        /// </summary>
        /// <returns>List of clsItem objects</returns>
        public static List<clsItems> GetAllItems()
        {
            List<clsItems> itemsList = new List<clsItems>();
            int rowsCount = 0;

            DataSet itemsDataSet = mainSQL.SelectAllItemDesc(ref rowsCount);

            DataTable itemsTable = itemsDataSet.Tables[0];

            foreach (DataRow row in itemsTable.Rows)
            {
                string itemCode = row["ItemCode"].ToString();
                string itemDesc = row["ItemDesc"].ToString();
                string cost = row["Cost"].ToString();

                clsItems item = new clsItems();
                item.Code = itemCode;
                item.Description = itemDesc;
                item.Cost = cost;
                itemsList.Add(item);
            }
            return itemsList;
        }

        /// <summary>
        /// Get DataSet of all ItemDesc for a particular invoice and return as a list
        /// of clsItem objects
        /// </summary>
        /// <param name="InvoiceNum">The target invoice number</param>
        /// <returns>List of clsItem objects from the given invoice</returns>
        public static List<clsItems> GetItemsByInvoiceNum(string invoiceNum)
        {
            List<clsItems> itemsList = new List<clsItems>();
            int rowsCount = 0;

            DataSet itemsDataSet = mainSQL.SelectJoinedItemsByInvoiceNum(invoiceNum, ref rowsCount);

            DataTable itemsTable = itemsDataSet.Tables[0];

            foreach (DataRow row in itemsTable.Rows)
            {
                string itemCode = row["ItemCode"].ToString();
                string itemDesc = row["ItemDesc"].ToString();
                string cost = row["Cost"].ToString();

                clsItems item = new clsItems();
                item.Code = itemCode;
                item.Description = itemDesc;
                item.Cost = cost;
                itemsList.Add(item);
            }
            return itemsList;
        }

        /// <summary>
        /// Add a new invoice with the given date and total.
        /// </summary>
        /// <param name="invoiceDate">The date for the invoice</param>
        /// <param name="totalCost">The total cost for the invoice</param>
        public static void AddInvoice(string invoiceDate, double totalCost)
        {
            mainSQL.InsertIntoInvoices(invoiceDate, totalCost);
        }

        /// <summary>
        /// Add a line item entry.
        /// </summary>
        /// <param name="invoiceNum">The invoice the item belongs to</param>
        /// <param name="lineItemNum">The line of the item in the invoice</param>
        /// <param name="ItemCode">The item's code</param>
        public static void AddLineItem(string invoiceNum, int lineItemNum, string ItemCode)
        {
            mainSQL.InsertIntoLineItems(invoiceNum, lineItemNum, ItemCode);
        }

        /// <summary>
        /// Change an invoice's total
        /// </summary>
        /// <param name="totalCost">The invoice's new total</param>
        /// <param name="invoiceNum">The invoice the total belongs to</param>
        public static void UpdateInvoiceCost(double totalCost, string invoiceNum)
        {
            mainSQL.UpdateInvoiceCost(totalCost, invoiceNum);
        }

        /// <summary>
        /// Change an invoice's date
        /// </summary>
        /// <param name="date">The invoice's new date</param>
        /// <param name="invoiceNum">The invoice the date belongs to</param>
        public static void UpdateInvoiceDate(string date, string invoiceNum)
        {
            mainSQL.UpdateInvoiceDate(date, invoiceNum);
        }

        /// <summary>
        /// Delete all the line items for a give invoice
        /// </summary>
        /// <param name="invoiceNum">The invoice whose line items are to be deleted</param>
        public static void DeleteLineItems(string invoiceNum)
        {
            mainSQL.DeleteLineItems(invoiceNum);
        }

        /// <summary>
        /// Get the maximum invoice num
        /// </summary>
        /// <returns>The maximum InvoiceNum</returns>
        public static string GetMaxInvoiceNum()
        {
            return mainSQL.SelectLastInvoiceNum();
        }

        /// <summary>
        /// Get an object of clsInvoice with a given invoice number
        /// </summary>
        /// <param name="invoiceNum">the invoice number for the invoice</param>
        /// <returns>the object of clsInvoice</returns>
        public static clsInvoice GetInvoiceByNum(string invoiceNum)
        {
            int rowsCount = 0;
            DataSet invoiceDs = mainSQL.SelectInvoiceByNum(invoiceNum, ref rowsCount);

            DataTable invoiceTable = invoiceDs.Tables[0];

            DataRow row = invoiceTable.Rows[0];

            string invoiceDate = row["InvoiceDate"].ToString();
            string total = row["TotalCost"].ToString();

            clsInvoice invoice = new clsInvoice();
            invoice.sInvoiceNum = invoiceNum;
            invoice.SetDate(invoiceDate);
            invoice.SetCost(total);

            return invoice;   
        }
    }
}
