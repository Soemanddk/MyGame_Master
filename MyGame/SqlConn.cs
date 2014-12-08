using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyGame
{
    public static class SqlConn
    {
        #region Private Metoder
        private static SqlConnection New_Sql_Connection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SecretMyGameConnectionString"].ToString());
        }
        private static int ExecuteNonQuery(SqlCommand Cmd)
        {
            Cmd.Connection = New_Sql_Connection();
            Cmd.Connection.Open();
            object newid = Cmd.ExecuteNonQuery();
            Cmd.Connection.Close();
            int returnId = 0;
            int.TryParse(newid.ToString(), out returnId);
            return returnId;
        }
        private static object ExecuteScarlar(SqlCommand Cmd)
        {
            Cmd.Connection = New_Sql_Connection();
            Cmd.Connection.Open();
            object newid = Cmd.ExecuteScalar();
            Cmd.Connection.Close();
            return newid;
        }
        private static DataTable Adapter_Fill_DataTable(SqlCommand Cmd)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            return datatable;
        }
        private static DataTable Build_New_DataTable(string commandtext)
        {
            SqlCommand Cmd = new SqlCommand(commandtext, New_Sql_Connection());
            return Adapter_Fill_DataTable(Cmd);
        }
        private static DataTable Build_New_DataTable(SqlCommand Cmd)
        {
            Cmd.Connection = New_Sql_Connection();
            return Adapter_Fill_DataTable(Cmd);
        }
        #endregion

        #region Public Metoder

        #region Select
        public static DataTable Select_All_From(string table)
        {
            return Build_New_DataTable("SELECT * FROM " + table);
        }
        public static DataTable Select(SqlCommand Cmd)
        {
            return Build_New_DataTable(Cmd);
        }
        // Den her skal konventeres til LinQ
        public static DataTable Select(string commandtext)
        {
            return Build_New_DataTable(commandtext);
        }
        public static int Count_Rows(string Table, string Column)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "SELECT COUNT(" + Column + ") AS items FROM " + Table;
            return Convert.ToInt32(ExecuteScarlar(Cmd));
        }
        // Den her skal konventeres til LinQ
        public static int Count_Rows(string Table)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "SELECT COUNT(*) AS items FROM " + Table;
            return Convert.ToInt32(ExecuteScarlar(Cmd));
        }
        public static DataTable Select_From_Table_With_Id(string Table, string Column, object Id)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "SELECT * FROM " + Table + " WHERE " + Column + " = @Id";
            Cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(Id);
            return Build_New_DataTable(Cmd);
        }
        public static string Select_Single_Entity_From_Table_With_Id(string Table, string Single_Entity, string Compare_Column, object Id)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "SELECT " + Single_Entity + " FROM " + Table + " WHERE " + Compare_Column + " = @id";
            Cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(Id);
            DataTable Dt = Build_New_DataTable(Cmd);
            return Dt.Rows[0][Single_Entity].ToString();
        }
        #endregion

        #region Delete
        public static void Delete(SqlCommand Cmd)
        {
            ExecuteNonQuery(Cmd);
        }
        public static void Delete_From_Table_With_Id(string Table, string Column, object Id)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "DELETE FROM " + Table + " WHERE " + Column + " = @Id";
            Cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(Id);
            ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Insert
        public static int Insert(SqlCommand Cmd)
        {
            Cmd.CommandText += ";SELECT SCOPE_IDENTITY()";
            object scope_identity = ExecuteScarlar(Cmd);
            int newid = 0;
            int.TryParse(scope_identity.ToString(), out newid);

            return newid;
        }
        #endregion

        #region Update
        public static int Update(SqlCommand Cmd)
        {
            return ExecuteNonQuery(Cmd);
        }
        #endregion

        #endregion
    }
}