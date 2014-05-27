using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Challenge.DataLayer;
using System.Data;

namespace Challenge.BusinessLogic
{
    /// <summary>
    ///Process and return information 
    /// </summary>
    public class InfoData
    {
        /// <summary>
        /// return array with info.
        /// </summary>
        /// <returns></returns>
        public static String[] GetTop(int top)
        {
            
           DataTable dt = ManagementData.ObtenerInfo(top);

           String[] myString = new string[dt.Rows.Count];

           int count = 0;
           foreach (DataRow item in dt.Rows)
           {
               myString[count] = item["DES_USER"].ToString();
               count++;
           }

           return myString;
        }

        /// <summary>
        /// Set The First and second en the database.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void SetFirstSecond(string first, string second)
        {
            ManagementData.ProcesInfo(first, second);
        }

        /// <summary>
        /// Evalute all information about winner.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<string> ProcessData(string[][] data)
        {
            string strategy1 = data[0][1];
            string strategy2 = data[1][1];

            string strategyWinner = ManagementLogic.EvaluteOptions(strategy1, strategy2);
            string first = string.Empty;
            string second = string.Empty;
            

            if (strategyWinner == data[0][1])
            {
                first = data[0][0];
                second = data[1][0];

                
            }
            else
            {
                second = data[0][0];
                first = data[1][0];
            }

            SetFirstSecond(first, second);

            string[] returnInfo = { first, strategyWinner };

            return returnInfo;
        }

        

        /// <summary>
        /// Reset info data base to empty, delete all data.
        /// </summary>
        public static void ResetDataBase()
        {
            ManagementData.DeleteInfo();
        }
    }
}
