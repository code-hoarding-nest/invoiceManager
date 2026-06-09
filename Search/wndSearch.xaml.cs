using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GroupFinal3280.Common;
using GroupFinal3280.Main;


namespace GroupFinal3280

{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// logic portion of search window
        /// </summary>
        clsSearchLogic SearchLogic = new clsSearchLogic();

        /// <summary>
        /// String taken in constructor from main.
        /// Used to return selected invoice to main
        /// </summary>
        string InvoiceID;

        /// <summary>
        /// default window constructor
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();

                //populate window with information from database
                cmboInvoiceNum.ItemsSource = SearchLogic.GetInvoiceNums();
                cmboInvoiceCost.ItemsSource = SearchLogic.GetInvoiceCosts();
                cmboInvoiceDate.ItemsSource = SearchLogic.GetInvoiceDates();
                dtGridInvoices.ItemsSource = SearchLogic.GetInvoices();
                dtGridInvoices.UnselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// cancel looking up invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                     MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// select invoice and pass it to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsInvoice selectedInvoice = (clsInvoice)dtGridInvoices.SelectedItem;
                if (selectedInvoice == null)
                {
                    MessageBox.Show("Please select an invoice.", "Selection error", MessageBoxButton.OK);
                }
                else
                {
                    MainWindow.InvoiceID = selectedInvoice.GetInvoiceNum();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                     MethodInfo.GetCurrentMethod().Name, ex.Message);
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

        /// <summary>
        /// Display original list of invoices, ignoring any previous filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmboInvoiceCost.SelectedItem = null;
                cmboInvoiceDate.SelectedItem = null;
                cmboInvoiceNum.SelectedItem = null;
                dtGridInvoices.ItemsSource = SearchLogic.GetInvoices();
                dtGridInvoices.UnselectAll();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// updates datagrid to filter invoices based on selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                //get selected items from dropdown
                string selectedInvoiceNum = (string)cmboInvoiceNum.SelectedItem;
                string selectedInvoiceDate = (string)cmboInvoiceDate.SelectedItem;
                string selectedInvoiceCost = (string)cmboInvoiceCost.SelectedItem;

                //only invoiceNum selected
                if (selectedInvoiceNum != null && selectedInvoiceDate == null && selectedInvoiceCost == null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByNum(selectedInvoiceNum);
                    dtGridInvoices.UnselectAll();
                }

                //invoiceNum and invoiceDate selected
                else if (selectedInvoiceNum != null && selectedInvoiceDate != null && selectedInvoiceCost == null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByNumDate(selectedInvoiceNum, selectedInvoiceDate);
                    dtGridInvoices.UnselectAll();
                }

                //invoiceNum and invoiceCost selected
                else if (selectedInvoiceNum != null && selectedInvoiceDate == null && selectedInvoiceCost != null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByNumCost(selectedInvoiceNum, selectedInvoiceCost);
                    dtGridInvoices.UnselectAll();
                }

                //invoiceNum, invoiceDate, and invoiceCost all selected
                else if (selectedInvoiceNum != null && selectedInvoiceDate != null && selectedInvoiceCost != null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByNumDateCost(selectedInvoiceNum, selectedInvoiceDate, selectedInvoiceCost);
                    dtGridInvoices.UnselectAll();
                }

                //only invoiceDate selected
                else if (selectedInvoiceDate != null && selectedInvoiceCost == null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByDate(selectedInvoiceDate);
                    dtGridInvoices.UnselectAll();
                }

                //invoiceDate and invoiceCost selected
                else if (selectedInvoiceDate != null && selectedInvoiceCost != null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByCostDate(selectedInvoiceCost, selectedInvoiceDate);
                    dtGridInvoices.UnselectAll();
                }

                //only invoiceCost selected
                else if (selectedInvoiceCost != null)
                {
                    dtGridInvoices.ItemsSource = SearchLogic.GetInvoiceInfoByCost(selectedInvoiceCost);
                    dtGridInvoices.UnselectAll();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
           MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
