using GroupFinal3280.Common;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace GroupFinal3280.Main
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A window to display the Items table
        /// </summary>
        private wndItems ItemWindow;
        /// <summary>
        /// A window to display the Search interface
        /// </summary>
        private wndSearch SearchWindow = new wndSearch();
        /// <summary>
        /// The ID for the currently selected/displayed invoice. Initial value is empty
        /// to represent a new invoice being created.
        /// </summary>
        public static string InvoiceID = "";
        /// <summary>
        /// The list of items for the current invoice
        /// </summary>
        private ObservableCollection<clsItems> invoiceItems = new ObservableCollection<clsItems>();
        /// <summary>
        /// Hold a reference to the item currently selected in the items combo box
        /// </summary>
        private clsItems selectedItem;
        /// <summary>
        /// Hold the total cost for the current invoice
        /// </summary>
        private double total;
        /// <summary>
        /// Hold the string representation of the selected invoice date
        /// </summary>
        private string selectedDate;


        
        /// <summary>
        /// Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                SetItemSelect();

                SetItemsGrid();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                this.Closed += MainWindow_Closed;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Set the combo box items for the Item Select
        /// </summary>
        private void SetItemSelect()
        {
            try
            {
                cmbItemSelect.ItemsSource = clsMainLogic.GetAllItems();
                cmbItemSelect.DisplayMemberPath = "Description";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Set the rows for the items data grid for the current selected invoice
        /// </summary>
        private void SetItemsGrid()
        {
            try
            {
                if(InvoiceID != "")
                {
                    invoiceItems = new ObservableCollection<clsItems>(clsMainLogic.GetItemsByInvoiceNum(InvoiceID));
                }

                gridItems.ItemsSource = invoiceItems;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        /// <summary>
        /// Called when the ItemWindow is closed. Reset the items in items combo box
        /// in case of any changes made in ItemWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Called when the ItemWindow is closed. Reset the items in items combo box
        /// in case of any changes made in ItemWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemWindow_Closed(object? sender, CancelEventArgs e)
        {
            try
            {
                SetItemSelect();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Called when the SearchWindow is closed. If an invoice has been selected, sets all UI elements
        /// to match the selected invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchWindow_Closed(object? sender, CancelEventArgs e)
        {
            try
            {
                if(InvoiceID != "")
                {
                    // set Invoice Number label to current invoice
                    lblInvoiceNumber.Content = "Invoice Number: " + InvoiceID;

                    InvoiceEditable(false);

                    invoiceItems = new ObservableCollection<clsItems>(clsMainLogic.GetItemsByInvoiceNum(InvoiceID));

                    btnViewInvoice.Visibility = Visibility.Collapsed;
                    cvsCreateInvoice.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Visible;
                    btnCreateInvoice.Margin = new Thickness(666, 384, 0, 0);
                    btnCreateInvoice.Height = 30;
                    btnCreateInvoice.Width = 100;
                    btnCreateInvoice.FontSize = 12;

                    clsInvoice invoice = clsMainLogic.GetInvoiceByNum(InvoiceID);

                    lblTotal.Content = "Total: $" + invoice.getCost();

                    string sCost = invoice.getCost();
                    double cost = 0.0;
                    if (double.TryParse(sCost, out cost))
                    {
                        total = cost;
                    }
                    else
                    {
                        throw new Exception("Cost parse failed.");
                    }

                    string sDate = invoice.GetDateTime();

                    if(DateTime.TryParse(sDate, out DateTime date))
                    {
                        dateInvoice.SelectedDate = date;
                    }
                    else
                    {
                        throw new Exception("Date parse failed.");
                    }

                    cmbItemSelect.SelectedItem = null;
                }
                SetItemsGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Called on click of Items button. Hides the main window and opens a new
        /// ItemWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                ItemWindow = new wndItems();
                ItemWindow.Closing += ItemWindow_Closed;
                ItemWindow.ShowDialog();
                this.Show();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Called on click of Search button. Hides the main window and opens a new
        /// SearchWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                SearchWindow = new wndSearch();
                SearchWindow.Closing += SearchWindow_Closed;
                SearchWindow.ShowDialog();
                this.Show();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Called on click of Create Invoice button. Moves the location of the Create
        /// Invoice button and makes UI for creating the invoice visible. Reset selected item,
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceEditable(true);

                btnEdit.Visibility = Visibility.Hidden;
                btnViewInvoice.Visibility=Visibility.Collapsed;
                cvsCreateInvoice.Visibility = Visibility.Visible;
                btnCreateInvoice.Margin = new Thickness(666, 384, 0, 0);
                InvoiceID = "";  // blank will indicate a new invoice being inputted
                btnCreateInvoice.Height = 30;
                btnCreateInvoice.Width = 100;
                btnCreateInvoice.FontSize = 12;

                invoiceItems = new ObservableCollection<clsItems>();

                lblInvoiceNumber.Content = "Invoice Number: TBD";

                lblTotal.Content = "Total: $0.00";
                total = 0.00;

                lblItemCost.Content = "$0.00";
                dateInvoice.SelectedDate = null;

                cmbItemSelect.SelectedItem = null;
                SetItemsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Called when the selected item is changed. Sets the cost label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItemSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
                // Set the selected item
                selectedItem = (clsItems)cmbItemSelect.SelectedItem;
                if(selectedItem != null)
                {
                    // Set the cost label to the selected item's cost
                    lblItemCost.Content = "$" + selectedItem.Cost;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Called on click of Add button. Adds the item to the invoice items data grid. 
        /// Calculate and display the new total.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cmbItemSelect.SelectedItem != null)
                {
                    lblItemErr.Visibility = Visibility.Hidden;
                    invoiceItems.Add(selectedItem);
                    string sCost = selectedItem.Cost.Substring(0);
                    double cost;
                    if (double.TryParse(sCost, out cost))
                    {
                        total += cost;

                        lblTotal.Content = "Total: $" + total.ToString();

                        lblItemCost.Content = "$0.00";
                        cmbItemSelect.SelectedItem = null;
                    }
                    else
                    {
                        throw new Exception("Cost parse failed.");
                    }
                } else
                {
                    lblItemErr.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Called on change of invoice date picker. Sets selectedDate to inputted date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateInvoice_SelectedDateChanged(object sender, EventArgs e)
        {
            try
            {
                selectedDate = dateInvoice.SelectedDate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Called on click of the delete item button. Removes the selected item from
        /// the items data grid from the items list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            clsItems selectedGridItem = (clsItems)gridItems.SelectedItem;

            if(selectedGridItem != null)
            {
                foreach(clsItems item in invoiceItems)
                {
                    if (item.Code == selectedGridItem.Code)
                    {
                        invoiceItems.Remove(item);
                        break;
                    }
                    
                }

                string sCost = selectedGridItem.Cost.Substring(0);
                double cost;
                if (double.TryParse(sCost, out cost))
                {
                    total -= cost;

                    lblTotal.Content = "Total: $" + total.ToString();
                }
                else
                {
                    throw new Exception("Cost parse failed.");
                }
            }
        }

        /// <summary>
        /// Called on click of the save invoice button. Saves invoice and line items and 
        /// resets the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedDate != null)
                {
                    if(total != 0)
                    {
                        if(InvoiceID != "")
                        {
                            clsMainLogic.UpdateInvoiceCost(total, InvoiceID);
                            clsMainLogic.UpdateInvoiceDate(selectedDate, InvoiceID);
                            // delete line items to re-add
                            clsMainLogic.DeleteLineItems(InvoiceID);
                        }
                        else
                        {
                            clsMainLogic.AddInvoice(selectedDate, total);
                        }
                            

                        for(int i = 0; i < invoiceItems.Count; i++)
                        {
                            if (InvoiceID == "")
                            {
                                InvoiceID = clsMainLogic.GetMaxInvoiceNum();
                            }

                            clsMainLogic.AddLineItem(InvoiceID, i + 1, invoiceItems.ElementAt(i).Code);
                        }

                        btnEdit.Visibility = Visibility.Visible;
                        InvoiceEditable(false);
                    }
                    else
                    {
                        lblTotalErr.Visibility = Visibility.Visible;
                    }
                    
                } else
                {
                    lblDateErr.Visibility = Visibility.Visible;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Set fields to enabled
        /// </summary>
        /// <param name="editable"></param>
        private void InvoiceEditable(bool editable)
        {
            dateInvoice.IsEnabled = editable;
            cmbItemSelect.IsEnabled = editable;
            btnAddItem.IsEnabled = editable;
            btnDeleteItem.IsEnabled = editable;
            btnSaveInvoice.IsEnabled = editable;
            gridItems.IsEnabled = editable;
        }

        /// <summary>
        /// Called on click of edit invoice button. Sets fields to enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            InvoiceEditable(true);
        }
    }
}