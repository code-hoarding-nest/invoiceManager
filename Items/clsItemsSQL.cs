using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GroupFinal3280.Items
{
    class clsItemsSQL
    {

        /// <summary>
        /// Will return sql that will return all items from the ItemDesc table
        /// </summary>
        /// <returns></returns>
        public static string GetItems()
        {
            try
            {
                string sSQL = "select ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns sql thatwill return a distinct invoice number that matches the given item code
        /// </summary>
        /// <param name="code"></param>
        public string GetInvoice(string code)
        {
            try
            {
                string sSQL = "select distinct(InvoiceNum) from LineItems where ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns sql that Updates an item description and cost based on the given item code, can't change item code
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public string UpdateItemDesc(string cost, string desc, string code)
        {
            try
            {
                string sSQL = "Update ItemDesc Set ItemDesc = '" + desc + "', Cost = " + cost + " where ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns sql that adds an item to the database with given cost, description, and code
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public string AddItem(string cost, string desc, string code)
        {
            try
            {
                string sSQL = "Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + code + "', '" + desc + "', " + cost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// returns sql that deletes an item from the database based on the given item code
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        public string DeleteItem(string code)
        {
            try
            {
                string sSQL = "Delete from ItemDesc Where ItemCode = '" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                               MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
