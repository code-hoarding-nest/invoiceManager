using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GroupFinal3280.Common
{
    public class clsHandleError
    {

        /// <summary>
        /// Handle Error
        /// </summary>
        /// <param name="sClass"> The class the error Occured in </param>
        /// <param name="sMethod"> The method the error occured in</param>
        /// <param name="sMessage"></param>
        public static void HandleError(string sClass, String sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {

                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
