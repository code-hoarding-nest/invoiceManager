using GroupFinal3280.Common;
using GroupFinal3280.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupFinal3280.Items
{
    class clsItemsLogic
    {
        /// <summary>
        /// Hold a list of item objects from clsItems
        /// </summary>
        public List<clsItems> itemsList;

        /// <summary>
        /// Hold a list of item codes as strings
        /// </summary>
        public List<string> codeList;

        /// <summary>
        /// used to generate the new item code 
        /// </summary>
        int newCode;


        /// <summary>
        /// Will create, populate the itemsList and return
        /// </summary>
        /// <returns></returns>
        public List<clsItems> GetAllItems()
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                itemsList = new List<clsItems>();
                ds = db.ExecuteSQLStatement(clsItemsSQL.GetItems(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsItems item = new clsItems();
                    item.Code = dr[0].ToString();
                    item.Description = dr[1].ToString();
                    item.Cost = dr[2].ToString();
                    itemsList.Add(item);
                }

                return itemsList;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<string> GetAllCodes()
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRet = 0;
                codeList = new List<string>();
                ds = db.ExecuteSQLStatement(clsItemsSQL.GetItems(), ref iRet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    codeList.Add(dr[0].ToString());
                }
                codeList.Sort();
                return codeList;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add an item to the database with the given description and cost, generates a unique item code
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void addItem( string desc, string cost)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsItemsSQL itemSQL = new clsItemsSQL();
                int iRet;
                newCode = itemsList.Count + 1;
                //generate new item code using newCode counter and converting to letters
                string code = (((char)((newCode / 26) + 64)).ToString()) + (((char)((newCode % 26) + 64)).ToString());

                //make sure the generated code is unique
                foreach (clsItems item in itemsList)
                {
                    if (item.Code == code)
                    {
                        newCode++;
                        code = (((char)((newCode / 26) + 64)).ToString()) + (((char)((newCode % 26) + 64)).ToString());
                    }
                }

                iRet = db.ExecuteNonQuery(itemSQL.AddItem(cost, desc, code));
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Check if the item with the given code is in use on any invoices
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ItemInUse(string code)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsItemsSQL itemSQL = new clsItemsSQL();
                DataSet ds = new DataSet();
                int iRet = 0;
                ds = db.ExecuteSQLStatement(itemSQL.GetInvoice(code), ref iRet);
                //if any rows are returned then the item is in use, return the first invoice number found
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    return dr[0].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Use the code to delete the item from the database
        /// </summary>
        /// <param name="code"></param>
        public void deleteItem(string code) 
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsItemsSQL itemSQL = new clsItemsSQL();
                int iRet = db.ExecuteNonQuery(itemSQL.DeleteItem(code));
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Using the Code to find and update the item description and cost
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public void updateItem(string cost, string desc, string code)
        {
            try
            {
                clsDataAccess db = new clsDataAccess();
                clsItemsSQL itemSQL = new clsItemsSQL();
                int iRet = db.ExecuteNonQuery(itemSQL.UpdateItemDesc(cost, desc, code));
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
