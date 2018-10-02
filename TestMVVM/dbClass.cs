using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TestMVVM
{
    public static class dbClass
    {
        public static SqlConnection GetDBConnection()
        {
            string connection_string = Properties.Settings.Default.connection_string;

            SqlConnection connection = new SqlConnection(connection_string);

            if (connection.State != ConnectionState.Open) connection.Open();

            return connection;
        }

        public static void CloseDBConnection()
        {
            string connection_string = Properties.Settings.Default.connection_string;

            SqlConnection connection = new SqlConnection(connection_string);

            if (connection.State != ConnectionState.Closed) connection.Close();
        }

        public static DataTable Execute_Proc(string procedureName)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var dataReader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }



        #region Tag

        public static int InsertTagToDB(int? SelectedParentCategoryID, string name, bool? IsFellingGoodBad)    
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.SaveTag", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@ParentCategoryID", (object)SelectedParentCategoryID ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@TagName", name));
            command.Parameters.Add(new SqlParameter("@IsFeeling", (object)IsFellingGoodBad ?? DBNull.Value));

            return (int) command.ExecuteScalar();            
        }

        public static void DeleteTagFromDB_AtTagID(int tagID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.DeleteTag", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@TagID", tagID));

            command.ExecuteNonQuery();
        }

        #endregion
        #region FoodEntry

        public static int InsertFoodEntryToDB(DateTime DateTime, string Description, bool? IsFellingGoodBad)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.SaveFoodEntry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@DateTime", DateTime));
            command.Parameters.Add(new SqlParameter("@Description", Description));
            command.Parameters.Add(new SqlParameter("@IsFeeling", (object)IsFellingGoodBad ?? DBNull.Value));

            return (int)command.ExecuteScalar();
        }        

        public static void DeleteFoodEntryFromDB_AtFoodEntryID(int FoodEntryID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.DeleteFoodEntry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@FoodEntryID", FoodEntryID));

            command.ExecuteNonQuery();
        }

        #endregion
        #region FoodEntry_Tag
        public static DataTable GetFoodEntry_Tags(string procedureName, int FoodEntryID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter(("@FoodEntryID"), FoodEntryID));

            var dataReader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        public static void InsertFoodEntry_TagToDB(int FoodEntryID, int TagID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.SaveFoodEntry_Tag", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@FoodEntryID", FoodEntryID));
            command.Parameters.Add(new SqlParameter("@TagID", TagID));

            command.ExecuteNonQuery();
        }

        public static void DeleteFoodEntry_TagFromDB_AtTagID(int FoodEntryID, int TagID)
        {
            SqlConnection connection = GetDBConnection();

            SqlCommand command = new SqlCommand("dbo.DeleteFoodEntry_Tag", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@FoodEntryID", FoodEntryID));
            command.Parameters.Add(new SqlParameter("@TagID", TagID));

            command.ExecuteNonQuery();
        }

        #endregion
    }
}
