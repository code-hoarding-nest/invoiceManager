
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace GroupFinal3280.Main
{
    /// <summary>
    /// Database interaction for the Main Window
    /// <author>Anna Jacobs</author>
    /// </summary>
    internal class clsMainSQL
    {
        /// <summary>
        /// Instance of a class that holds methods to connect with the 
        /// database and execute SQL queries
        /// </summary>
        private clsDataAccess dataAccess = new clsDataAccess();

        /// <summary>
        /// Execute a SQL statement to update an invoice date
        /// </summary>
        /// <param name="date">The new date to be set</param>
        /// <param name="invoiceNum">The id of the invoice to be updated</param>
        /// <returns>The count of the rows affected</returns>
        public int UpdateInvoiceDate(string date, string invoiceNum)
        {
            return dataAccess.ExecuteNonQuery(
                "UPDATE Invoices SET InvoiceDate = #" + date + "# WHERE InvoiceNum = " + invoiceNum
            );
        }

        /// <summary>
        /// Execute a SQL statement to update an invoice total cost
        /// </summary>
        /// <param name="totalCost">The new total cost to be set</param>
        /// <param name="invoiceNum">The id of the invoice to be updated</param>
        /// <returns>The count of the rows affected</returns>
        public int UpdateInvoiceCost(double totalCost, string invoiceNum)
        {
            return dataAccess.ExecuteNonQuery(
                "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + invoiceNum
            );
        }

        /// <summary>
        /// Execute a SQL statement to add a new row into the LineItems table
        /// </summary>
        /// <param name="invoiceNum">The id number of the invoice the item is on</param>
        /// <param name="lineItemNum">The line number the item is located at in the invoice</param>
        /// <param name="ItemCode">The code that represents the item</param>
        /// <returns>The count of the rows affected</returns>
        public int InsertIntoLineItems(string invoiceNum, int lineItemNum, string ItemCode)
        {
            return dataAccess.ExecuteNonQuery(
                "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) VALUES(" + invoiceNum +
                ", " + lineItemNum + ", " + "'" + ItemCode + "'" +
                ")"
            );
        }

        /// <summary>
        /// Execute a SQL statement to select the maximum InvoiceNum
        /// </summary>
        /// <returns></returns>
        public string SelectLastInvoiceNum()
        {
            return dataAccess.ExecuteScalarSQL("SELECT MAX(InvoiceNum) FROM Invoices");
        }

        /// <summary>
        /// Execute a SQL statement to add a new row into Invoices table
        /// </summary>
        /// <param name="invoiceDate">The date the invoice was made</param>
        /// <param name="totalCost">The total owed for the invoice</param>
        /// <returns>The count of the rows affected</returns>
        public int InsertIntoInvoices(string invoiceDate, double totalCost)
        {
            return dataAccess.ExecuteNonQuery(
                "INSERT INTO Invoices(InvoiceDate, TotalCost) VALUES(#"
                + invoiceDate + "#, " + totalCost + 
                ")"
            );
        }

        /// <summary>
        /// Execute a SQL statement to select an Invoice by its InvoiceNum
        /// </summary>
        /// <param name="invoiceNum">The id number of the target invoice</param>
        /// <param name="rowsCount">Variable passed by ref to hold the number of rows affected
        ///     by the query (should only be one).</param>
        /// <returns>DataSet that holds the row returned by the query</returns>
        public DataSet SelectInvoiceByNum(string invoiceNum, ref int rowsCount)
        {
            return dataAccess.ExecuteSQLStatement(
                "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum, ref rowsCount);
        }

        /// <summary>
        /// Execute a SQL statement to select all the rows in the ItemDesc table
        /// </summary>
        /// <param name="rowsCount">Variable passed by ref to hold the count of rows 
        ///     returned by the query</param>
        /// <returns>DataSet to hold the rows returned by the query</returns>
        public DataSet SelectAllItemDesc(ref int rowsCount)
        {
            return dataAccess.ExecuteSQLStatement(
                "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc", ref rowsCount
            );
        }

        /// <summary>
        /// Execute a SQL statement to join LineItems and ItemDesc tables to display item 
        /// costs for a particular invoice selected by InvoiceNum
        /// </summary>
        /// <param name="invoiceNum">The id for the target Invoice</param>
        /// <param name="rowsCount">Variable passed by ref to hold count of rows returned 
        /// by the query</param>
        /// <returns>Dataset that holds result rows from the query</returns>
        public DataSet SelectJoinedItemsByInvoiceNum(string invoiceNum, ref int rowsCount)
        {
            return dataAccess.ExecuteSQLStatement(
                "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + invoiceNum, ref rowsCount
            );
        }

        /// <summary>
        /// Execute a SQL statement to delete all LineItems rows from a certain invoice
        /// </summary>
        /// <param name="invoiceNum">Invoice id number for target invoice to delete all
        /// LineItems from</param>
        /// <returns>Number of rows affected by the statement</returns>
        public int DeleteLineItems(string invoiceNum)
        {
            return dataAccess.ExecuteNonQuery(
                "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum
            );
        }
    }
}
