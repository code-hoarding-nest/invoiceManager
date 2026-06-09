using GroupFinal3280.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GroupFinal3280.Main; //deleteme


namespace GroupFinal3280

{

    internal class clsSearchSQL
    {
        clsDataAccess DataAccess = new clsDataAccess();


        public clsSearchSQL()
        {

        }

        /// <summary>
        /// Returns  an SQL formatted string to find all invoice data given an invoice ID
        /// </summary>
        /// <param name="invoiceNum">invoiceNum field of the invoice</param>
        /// <returns>SQL formatted string to find all invoice data given an invoice ID</returns>
        public string GetInvoiceInfoByNum(string invoiceNum)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// returns SQL formatted string to find all invoice data given invoiceNum and date
        /// </summary>
        /// <param name="invoiceNum">invoiceNum field of invoice</param>
        /// <param name="InvoiceDate">invoiceDate field of invoice</param>
        /// <returns>SQL formatted string to find all invoice data given invoice ID and date</returns>
        public string GetInvoiceInfoByNumDate(string invoiceNum, string InvoiceDate)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum
                    + " AND InvoiceDate = FORMAT(#" + InvoiceDate + "#,'MMM dd yyyy')";

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// returns SQL formatted string to find all invoice data given date and total cost
        /// </summary>
        /// <param name="invoiceNum">invoiceNum field of the invoice</param>
        /// <param name="InvoiceDate">invoiceDate field of the invoice</param>
        /// <param name="totalcost">totalCost field of the invoice</param>
        /// <returns>SQL formatted string to find all invoice data given date and total cost</returns>
        public string GetInvoiceInfoByNumDateCost(string invoiceNum, string InvoiceDate, string totalcost)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE invoiceNum = " + invoiceNum
                    + " AND InvoiceDate = #" + InvoiceDate + "# AND totalCost = " + totalcost;

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns SQL formatted string to find all invoice data given a total cost
        /// </summary>
        /// <param name="totalcost">totalCost field of invoice field</param>
        /// <returns>SQL formatted string to find all invoice data given a total cost</returns>
        public string GetInvoiceInfoByCost(string totalcost)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE totalCost = " + totalcost;

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns SQL formatted string to find all invoice data given a total cost and date
        /// </summary>
        /// <param name="totalcost">totalCost field of invoice field</param>
        /// <param name="invoiceDate">invoiceDate field of invoice field</param>
        /// <returns>SQL formatted string to find all invoice data given a total cost and date</returns>
        public string GetInvoiceInfoByCostDate(string totalcost, string invoiceDate)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE totalCost = " + totalcost
                    + " AND invoiceDate = #" + invoiceDate + "#";

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns SQL formatted string to find all invoices with 
        /// mathcing invoiceNum and invoiceCost
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetInvoiceInfoByNumCost(string invoiceNum, string invoiceCost)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE invoicenum = " + invoiceNum
                    + " AND totalCost = " + invoiceCost;

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns SQL formatted string to find all invoice data given a date
        /// </summary>
        /// <param name="invoiceDate">invoiceDate field of invoice field</param>
        /// <returns>SQL formatted string to find all invoice data given a date</returns>
        public string GetInvoiceInfoByDate(string invoiceDate)
        {
            try
            {
                string sReturn;
                sReturn = "SELECT * FROM Invoices WHERE invoiceDate = #" + invoiceDate + "#";

                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns SQL formatted string to return all invoices numbers low to high
        /// </summary>
        /// <returns>SQL formatted string to return all invoices numbers low to high </returns>
        public string OrderInvoicesByNum()
        {
            try
            {
                string sReturn;
                sReturn = "SELECT DISTINCT InvoiceNum FROM Invoices ORDER BY invoiceNum";
                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>//fixme check oldest to newest
        /// returns SQL formatted string to return all invoices dates oldest to newest
        /// </summary>
        /// <returns>SQL formatted string to return all invoices dates oldest to newest</returns>
        public string OrderInvoicesByDate()
        {
            try
            {
                string sReturn;
                sReturn = "SELECT DISTINCT InvoiceDate FROM Invoices ORDER BY invoiceDate";
                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns SQL formatted string to return all invoice total costs small to large
        /// </summary>
        /// <returns>SQL formatted string to return all invoice total costs small to large</returns>
        public string OrderInvoicesByCost()
        {
            try
            {
                string sReturn;
                sReturn = "SELECT DISTINCT TotalCost FROM Invoices ORDER BY TotalCost";
                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
