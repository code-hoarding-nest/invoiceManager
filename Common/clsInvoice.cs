using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupFinal3280.Common
{
     class clsInvoice
    {
       public string sInvoiceDate { get; set; }
        public string sInvoiceNum { get; set; }
        public string sTotalCost { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public clsInvoice() { }


        /// <summary>
        /// getter for DateTime attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
       public string GetDateTime()
        {
            try
            {
                return sInvoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// getter for invoiceNum attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetInvoiceNum()
        {
            try
            {
                return sInvoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// getter for totalCost attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getCost()
        {
            try
            {
                return sTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// setter  for invoiceDate attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void SetDate(string inDateTime)
        {
            try
            {
                sInvoiceDate = inDateTime;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// setter  for invoiceNumber attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void SetNumber(string inNum)
        {
            try
            {
                sInvoiceNum = inNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// setter  for invoiceCost attribute
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void SetCost(string inCost)
        {
            try
            {
                sTotalCost = inCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// overloaded constructor with paramters
        /// </summary>
        /// <param name="sInNum"></param>
        /// <param name="sInDate"></param>
        /// <param name="sInCost"></param>
        public clsInvoice(string sInNum, string sInDate, string sInCost)
        {
            try
            {
                sInvoiceDate = sInDate;
                sInvoiceNum = sInNum;
                sTotalCost = sInCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// displays invoice iformation as text
        /// </summary>
        /// <returns></returns>
        override public string  ToString()
        {
            try
            {
                string sReturn = sInvoiceNum + " " + sInvoiceDate + " " + sTotalCost;
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
