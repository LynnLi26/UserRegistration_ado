using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using UserRegistration_ado.Models;
using System.Configuration;

namespace UserRegistration_ado.DAL
{
    public class DAL_Layer
    {
        string connString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
        public DataSet getUser()
        {
            DataSet ds = new DataSet();

            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand("select * from [User]", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(ds);
            connection.Close();

            return ds;
        }

        public int AddUser(User user)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand("insert into [User] (Name, Email, Password) values(@Name, @Email, @Password)", connection);

            SqlParameter[] parameters = new SqlParameter[3];

            parameters[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
            parameters[1] = new SqlParameter("@Email", SqlDbType.NVarChar);
            parameters[2] = new SqlParameter("@Password", SqlDbType.Money);

            parameters[0].Value = user.Name;
            parameters[1].Value = user.Email;
            parameters[2].Value = user.Password;

            //command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                SqlParameter p = null;
                foreach (SqlParameter sqlP in parameters)
                {
                    p = sqlP;
                    if (p != null)
                    {
                        if (p.Direction == ParameterDirection.InputOutput ||
                           p.Direction == ParameterDirection.Input && p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
            var result = command.ExecuteNonQuery();
            command.CommandTimeout = 6000;
            connection.Close();
            return result;
        }
    }
}