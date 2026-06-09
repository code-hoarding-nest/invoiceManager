using GroupFinal3280.Common;
using GroupFinal3280.Items;
using GroupFinal3280.Main;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.XPath;

namespace GroupFinal3280
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// Will hold the items list
        /// </summary>
        clsItemsLogic itemLogic;

        /// <summary>
        /// Keeps track if the delete button has been clicked
        /// </summary>
        bool deleting = false;

        /// <summary>
        /// keeps track of the last selected item in the datagrid
        /// </summary>
        clsItems selected;

        public wndItems()
        {
            try
            {
                InitializeComponent();

                itemLogic = new clsItemsLogic();
                displayItems();
                
                ///a static variable to track if items were changed thoughout the program
                clsItems.itemsChanged = false;
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Will remove item from the items table 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    MessageBox.Show("Please select an item to delete.");
                    deleting = true;
                    cmdAdd.IsEnabled = false;
                    cmdEdit.IsEnabled = false;
                    return;
                }
                MessageBoxResult msgDelete;

                //check item is not used in any invoices
                string invoice = itemLogic.ItemInUse(selected.Code);
                if (invoice != "")
                {
                    MessageBox.Show("Item " + selected.Description.ToString() + " is in use on invoice " + invoice + " and cannot be deleted.");
                    deleting = false;
                    cmdAdd.IsEnabled = true;
                    cmdEdit.IsEnabled = true;
                    return;
                }

                //ask user to confirm deletion
                msgDelete = MessageBox.Show("Do you want to delete the item "
                                            + selected.Description.ToString()
                                            + " for $" + selected.Cost.ToString(),
                                            "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (msgDelete == MessageBoxResult.No)
                {
                    MessageBox.Show("Deletion Canceled");
                    deleting = false;
                    cmdAdd.IsEnabled = true;
                    cmdEdit.IsEnabled = true;
                }
                else if (msgDelete == MessageBoxResult.Yes)
                {
                    //check a valid items is selected
                    if (selected.Code != null)
                    {

                        //sql delete item
                        itemLogic.deleteItem(selected.Code);
                        clsItems.itemsChanged = true;
                        deleting = false;

                        //show updated items  datagrid
                        displayItems();

                        //ask if the user wants to delete another item
                        msgDelete = MessageBox.Show("Do you want to delete another item?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (msgDelete == MessageBoxResult.No)
                        {
                            cmdAdd.IsEnabled = true;
                            cmdEdit.IsEnabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Please select an item to delete.");
                            deleting = true;
                            cmdAdd.IsEnabled = false;
                            cmdEdit.IsEnabled = false;
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Will allow user to save edits on an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmdEdit.Visibility = Visibility.Collapsed;
                cmdSave.Visibility = Visibility.Visible;
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Will allow user to add an item to the items table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmdDelete.IsEnabled = false;
                cmdEdit.IsEnabled = false;

                //make sure the user entered data
                //get item description and cost from inputboxes
                if (txtDesc.Text == "" || txtCost.Text == "")
                {
                    MessageBox.Show("Please enter a description and cost for the new item.");
                    //change text red?
                    return;
                }
                if (txtDesc.Text.Length > 50)
                {
                    MessageBox.Show("Description cannot be longer than 50 characters.");
                    return;
                }
                itemLogic.addItem(txtDesc.Text, txtCost.Text);

                //check item was added successfully 

                clsItems.itemsChanged = true;

                //show updated items  datagrid
                displayItems();

                cmdDelete.IsEnabled = true;
                cmdEdit.IsEnabled = true;
                txtCost.Text = "";
                txtDesc.Text = "";
                cbCode.SelectedIndex = -1;
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// This will display the code, description, and cost of all the items in the database
        /// </summary>
        public void displayItems()
        {
            try
            {
                dgItems.ItemsSource = itemLogic.GetAllItems();
                cbCode.ItemsSource = itemLogic.GetAllCodes();
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Will close item window and return to Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdReturntoMain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// button to save any edits made to an item(s) to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected != null)
                {
                    string desc = txtDesc.Text;
                    if (desc.Length > 50)
                    {
                        MessageBox.Show("Description cannot be longer than 50 characters.");
                        return;
                    }
                    string cost = txtCost.Text;
                    if (desc != selected.Description || cost != selected.Cost)
                    {
                        itemLogic.updateItem(cost, desc, selected.Code);
                        //sql update item
                        clsItems.itemsChanged = true;

                        //show updated items  datagrid
                        displayItems();

                    }
                    else
                    {
                        MessageBox.Show("No changes were made to item " + selected.Description);
                    }

                    cmdEdit.Visibility = Visibility.Visible;
                    cmdSave.Visibility = Visibility.Collapsed;
                }
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// Binds the selected item in the datagrid to the textboxes, if edit button is clicked user can edit the textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem != null)
                {
                    selected = dgItems.SelectedItem as clsItems;
                    txtDesc.Text = selected.Description;
                    txtCost.Text = selected.Cost;
                    cbCode.SelectedItem = selected.Code;
                    //check if deleteing
                    if (deleting == true)
                    {
                        RoutedEventArgs d = new RoutedEventArgs();
                        cmdDelete_Click(sender, d);
                        txtDesc.Text = "";
                        txtCost.Text = "";
                    }

                }
                else
                {
                    txtDesc.Text = "";
                    txtCost.Text = "";

                }
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Allows user to select an item code from the combobox and have the datagrid scroll to that item, selecting it 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbCode.SelectedItem != null)
                {
                    string code = cbCode.SelectedItem.ToString();

                    if (code != null)
                    {
                        //uses the item variable created in get all invoices to find the index of the selected code
                        dgItems.SelectedIndex = itemLogic.itemsList.FindIndex(item => item.Code == code);
                        dgItems.ScrollIntoView(dgItems.SelectedItem);
                    }
                }
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Only allows numbers to be entered into the cost textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow numbers to be entered
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
                {  //Only allow numbers from number pad to be entered
                    if (!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                    {   //allow backspace, delete, tab, and enter keys
                        if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                        {
                            e.Handled = true;
                        }

                    }

                }
            }
            catch (System.Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
