using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Challenge.DataLayer
{
    /// <summary>
    /// Management data, query, insert, delete, update.
    /// </summary>
    public class ManagementData
    {
        /// <summary>
        /// Evalue if exist the win or no exists.
        /// </summary>
        /// <param name="firts"></param>
        /// <param name="second"></param>
        public static void ProcesInfo(string first, string second)
        {
            if (Exits(first))
            {
                UpdateInfo(first, 3);
            }
            else
            {
                InsertInfo(first, 3);
            }

            if (Exits(second))
            {
                UpdateInfo(second, 1);
            }
            else
            {
                InsertInfo(second, 1);
            }
        }

        /// <summary>
        /// Update data of champions with yours points.
        /// </summary>
        private static void UpdateInfo(string user, int points)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand command = new SqlCommand();
                string connString = DataConfiguration.GetConnection();



                using (conn)
                {
                    //using parametirized query
                    string sqlInserString =
                       " UPDATE tournaments  " + 
                       " SET NUM_POINTS = (SELECT SUM(NUM_POINTS) + @points " +
                                            " from tournaments " +
                                            " where des_user = @user ) " +
                    " where upper(des_user) = @user " ;

                    conn = new SqlConnection(connString);

                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlInserString;

                    SqlParameter userNameparam = new SqlParameter("@user", user.ToUpper() );
                    SqlParameter pointNameparam = new SqlParameter("@points", points);

                    command.Parameters.AddRange(new SqlParameter[]{
                                    userNameparam,pointNameparam});

                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }

        /// <summary>
        /// Insert data into table of champions with yours points.
        /// </summary>
        private static void InsertInfo(string user, int points)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand command = new SqlCommand();
                string connString = DataConfiguration.GetConnection();



                using (conn)
                {
                    //using parametirized query
                    string sqlInserString =
                       " INSERT INTO tournaments (des_user, num_points ) " + 
                       " VALUES (@user, @points)";

                    conn = new SqlConnection(connString);

                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlInserString;
                    
                    SqlParameter userNameparam = new SqlParameter("@user", user);
                    SqlParameter pointNameparam = new SqlParameter("@points",points);
                    
                    command.Parameters.AddRange(new SqlParameter[]{
                                    userNameparam,pointNameparam});
                    
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }


        /// <summary>
        /// Insert data into table of champions with yours points.
        /// </summary>
        public static void DeleteInfo()
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand command = new SqlCommand();
                string connString = DataConfiguration.GetConnection();



                using (conn)
                {
                    //using parametirized query
                    string sqlDeleteString =
                       " delete from tournaments ";

                    conn = new SqlConnection(connString);

                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlDeleteString;
                    
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }


        private static bool Exits(string user)
        {


            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand command = new SqlCommand();
                string connString = DataConfiguration.GetConnection();

                using (conn)
                {
               
               
                    string sqlQueryString =
                        "select COUNT(*) "  +
                        " from tournaments " +
                        " where upper( des_user) = '"  + user.ToUpper()  + "'";

                    conn = new SqlConnection(connString);

                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlQueryString;
                    
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable data = new DataTable();

                    data.Load(reader);
                    command.Connection.Close();

                    int count = Convert.ToInt32(data.Rows[0][0]);
                    return count == 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                //err.ErrorMessage = ex.Message.ToString();
                throw;
            }

            return false;
        }

        /// <summary>
        /// Get Informacion of Data Base for top ten.
        /// </summary>
        public static DataTable ObtenerInfo(int top)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                SqlCommand command = new SqlCommand();
                string connString = DataConfiguration.GetConnection();

                using (conn)
                {
                    //using parametirized query
                    string texttop = string.Empty;

                    if (top > 0) 
                        texttop = " top " + top.ToString();
                    else
                        texttop = string.Empty;

                    string sqlQueryString =
                        "select " + texttop + " des_user,sum(num_points) as num_points " +
                        " from tournaments " +
                        " group by des_user " +
                        " order by num_points desc,des_user desc";
                       
                    conn = new SqlConnection(connString);

                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlQueryString;
                    /*
                    SqlParameter firstNameparam = new SqlParameter("@firstName", emp.FirstName);
                    SqlParameter lastNameparam = new SqlParameter("@lastName", emp.LastName);
                    SqlParameter IDparam = new SqlParameter("@ID", emp.EmpCode);
                    SqlParameter designationParam = new SqlParameter("@designation", emp.Designation);
                    
                    command.Parameters.AddRange(new SqlParameter[]{
                                    firstNameparam,lastNameparam,IDparam,designationParam});
                    */
                    SqlDataReader reader = command.ExecuteReader();
                    
                    DataTable data = new DataTable();

                    data.Load(reader);
                    command.Connection.Close();
                    return data;
                }
            }
            catch (Exception ex)
            {
                //err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
    }
}
