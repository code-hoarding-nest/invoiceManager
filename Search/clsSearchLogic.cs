using GroupFinal3280.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GroupFinal3280.Main; //deleteme

namespace GroupFinal3280

{
    class clsSearchLogic
    {
        /// <summary>
        /// list of all invoices
        /// </summary>
        public ObservableCollection<clsInvoice> invoices;


        /// <summary>
        /// data access class to get database info
        /// </summary>
        clsDataAccess myDataAccess = new clsDataAccess();


        /// <summary>
        /// dataset holds information from database
        /// </summary>
        static DataSet searchDS = new DataSet();

        /// <summary>
        /// the invoice the user has selected
        /// </summary>
        public clsInvoice currentInvoice;

        /// <summary>
        /// used for querying database
        /// </summary>
        clsSearchSQL searchSQL = new clsSearchSQL();

        /// <summary>
        /// default constructor grabs info from database
        /// </summary>
        public clsSearchLogic()
        {
            try
            {
                clsInvoice test = GetSelectedInvoice();
                //put info from database into a dataset
                string sSQLcommand = "SELECT * FROM Invoices";
                int numRows = -1;
                searchDS = myDataAccess.ExecuteSQLStatement(sSQLcommand, ref numRows);


                //populate invoice list
                invoices = new ObservableCollection<clsInvoice>();
                foreach (DataRow dr in searchDS.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice();
                    newInvoice.SetNumber(dr[0].ToString());
                    newInvoice.SetDate(dr[1].ToString());
                    newInvoice.SetCost(dr[2].ToString());

                    invoices.Add(newInvoice);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// returns list of invoices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<clsInvoice> GetInvoices()
        {
            try
            {
                int iRet = 0;
                invoices = new ObservableCollection<clsInvoice>();
                searchDS = myDataAccess.ExecuteSQLStatement("SELECT * FROM Invoices", ref iRet);

                //read in data to invoiceList
                foreach (DataRow dr in searchDS.Tables[0].Rows)
                {
                    clsInvoice item = new clsInvoice();
                    item.sInvoiceNum = dr[0].ToString();
                    item.sInvoiceDate = dr[1].ToString();
                    item.sTotalCost = dr[2].ToString();
                    invoices.Add(item);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string list of all invoice numbers.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<string> GetInvoiceNums()
        {
            try
            {
                ObservableCollection<string> invoiceNums = new ObservableCollection<string>();
                int iRet = -1;
                string sSQLCommand = searchSQL.OrderInvoicesByNum();
                DataSet ds = myDataAccess.ExecuteSQLStatement(sSQLCommand, ref iRet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoiceNums.Add(dr[0].ToString());
                }
                return invoiceNums;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string list of all invoice dates.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<string> GetInvoiceDates()
        {
            try
            {
                ObservableCollection<string> invoiceDates = new ObservableCollection<string>();
                int iRet = -1;
                string sSQLCommand = searchSQL.OrderInvoicesByDate();
                DataSet ds = myDataAccess.ExecuteSQLStatement(sSQLCommand, ref iRet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoiceDates.Add(dr[0].ToString());
                }
                return invoiceDates;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string list of all invoice costs.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<string> GetInvoiceCosts()
        {
            try
            {
                ObservableCollection<string> invoiceCosts = new ObservableCollection<string>();
                int iRet = -1;
                string sSQLCommand = searchSQL.OrderInvoicesByCost();
                DataSet ds = myDataAccess.ExecuteSQLStatement(sSQLCommand, ref iRet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoiceCosts.Add(dr[0].ToString());
                }
                return invoiceCosts;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns currently selected invoice
        /// </summary>
        /// <returns></returns>
        public clsInvoice GetSelectedInvoice()
        {
            try
            {
                //dummy return invoice
                clsInvoice returnInvoice = new clsInvoice();

                if (currentInvoice == null)  //not a valid invoice so a blank invoice is returned
                {
                    return returnInvoice;
                }
                else
                {
                    return currentInvoice;
                }

            }
            catch (Exception ex)
            {
                //HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                // MethodInfo.GetCurrentMethod().Name, ex.Message);
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// returns a string containing the invoice that has specified number
        /// </summary>
        /// <param name="inNum">Number of the invoice</param>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByNum(string inNum)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByNum(inNum);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                ObservableCollection<clsInvoice> matchingInvoices = new ObservableCollection<clsInvoice>();

                //should never be two invoices with the same number. but ...
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    matchingInvoices.Add(invoice);
                }
                return matchingInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a string containing the invoice that has specified date
        /// </summary>
        /// <param name="inNum">date of the invoice</param>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByDate(string inDate)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByDate(inDate);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                clsInvoice invoice = new clsInvoice();
                ObservableCollection<clsInvoice> foundInvoices = new ObservableCollection<clsInvoice>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    foundInvoices.Add(invoice);
                }
                return foundInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns an observableCollection containing all invoices matching given number and date parameters
        /// </summary>
        /// <param name="inNum">Number of the invoice</param>
        /// <param name="inDate">Date of the invoice</param>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByNumDate(string inNum, string inDate)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByNumDate(inNum, inDate);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                ObservableCollection<clsInvoice> matchingInvoices = new ObservableCollection<clsInvoice>();
                clsInvoice invoice = new clsInvoice();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    matchingInvoices.Add(invoice);
                }
                return matchingInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns an observableCollection of all invoices with a number, date, and cost 
        /// that matches parameters
        /// </summary>
        /// <param name="inNum">invoiceNum of the invoice</param>
        /// <param name="inDate">invoiceDate of the invoice</param>
        /// <param name="inCost">invoiceCost of the invoice</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByNumDateCost(string inNum, string inDate, string inCost)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByNumDateCost(inNum, inDate, inCost);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                clsInvoice invoice = new clsInvoice();
                ObservableCollection<clsInvoice> foundInvoices = new ObservableCollection<clsInvoice>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    foundInvoices.Add(invoice);
                }
                return foundInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns list of all invoices with a matching invoiceNum and
        /// invoiceCost that match parameters
        /// </summary>
        /// <param name="inNum">invoiceNum to search for</param>
        /// <param name="inCost">invoiceCost to search for </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByNumCost(string inNum, string inCost)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByNumCost(inNum, inCost);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                clsInvoice invoice = new clsInvoice();
                ObservableCollection<clsInvoice> foundInvoices = new ObservableCollection<clsInvoice>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    foundInvoices.Add(invoice);
                }
                return foundInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a string containing all invoices matching given cost and date parameters
        /// </summary>
        /// <param name="inNum">cost of the invoice</param>
        /// <param name="inDate">Date of the invoice</param>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByCostDate(string inCost, string inDate)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByCostDate(inCost, inDate);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                clsInvoice invoice = new clsInvoice();
                ObservableCollection<clsInvoice> foundInvoices = new ObservableCollection<clsInvoice>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    foundInvoices.Add(invoice);
                }
                return foundInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns a string containing all invoices with matching cost of parameter
        /// </summary>
        /// <param name="inNum">Cost of the invoice</param>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> GetInvoiceInfoByCost(string inCost)
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.GetInvoiceInfoByCost(inCost);
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                clsInvoice invoice = new clsInvoice();
                ObservableCollection<clsInvoice> foundInvoices = new ObservableCollection<clsInvoice>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    invoice.SetNumber(dr[0].ToString());
                    invoice.SetDate(dr[1].ToString());
                    invoice.SetCost(dr[2].ToString());
                    foundInvoices.Add(invoice);
                }
                return foundInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns string containing all dates, ordered by date
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<string> OrderInvoicesByDate()
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.OrderInvoicesByDate();
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                ObservableCollection<string> sDates = new ObservableCollection<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sDates.Add(dr[0].ToString());
                }
                return sDates;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns string containing all invoice numbers, ordered by invoice number
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string OrderInvoicesByNum()
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.OrderInvoicesByNum();
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                string sReturn = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sReturn += (string)dr[0].ToString() + " ";
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns string containing all invoice costs, ordered by cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string OrderInvoicesByCost()
        {
            try
            {
                int iRet = -1;
                string sCommand = searchSQL.OrderInvoicesByCost();
                DataSet ds = new DataSet();
                ds = myDataAccess.ExecuteSQLStatement(sCommand, ref iRet);
                string sReturn = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sReturn += (string)dr[0].ToString() + " ";
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //method from Professor Cowder
        /// <summary>
        /// handles thrown errors
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
