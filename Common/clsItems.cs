using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupFinal3280.Common
{
    class clsItems
    {
        /// <summary>
        /// Will hold the code of the item as a string
        /// </summary>
        private string sCode;

        /// <summary>
        /// Will hold the Description of the item 
        /// </summary>
        private string sDescription;

        /// <summary>
        /// Will hold the cost of the item as a string
        /// </summary>
        private string sCost;

        /// <summary>
        /// A static variable the will keep track if something has been changed in items
        /// </summary>
         public static bool itemsChanged;

        /// <summary>
        /// Get and set item code
        /// </summary>
        public string Code
        {

            get {
                try { return sCode; }
                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
            set {
                try { sCode = value; }

                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Get and set item description
        /// </summary>
        public string Description
        {
            get {
                try { return sDescription; }
                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
            set { 
                try { sDescription = value; }
                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Get and set item cost
        /// </summary>
        public string Cost
        {
            get { 
                try { return sCost; }
                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
            set {
                try { sCost = value; }
                catch (Exception ex)
                {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }
    }
}
