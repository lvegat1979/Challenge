using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenge.DataLayer
{
    /// <summary>
    /// Set Information of Connection String, for use in all aplication
    /// </summary>
    public class DataConfiguration
    {
        public static string GetConnection()
        {
            return @"Data Source=LVEGAT-LAPTOP\SQLEXPRESS;Initial Catalog=Challenge;Persist Security Info=True;User ID=sa;Password=nehemias";
        }   
    }
}
