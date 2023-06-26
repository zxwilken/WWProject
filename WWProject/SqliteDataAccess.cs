using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace WWProject
{
    internal class SqliteDataAccess
    {
        // Temporary hardcoding DB name
        private string databaseName;

        public SqliteDataAccess()
        {
            databaseName = "WorldDB";
        }

        public string GetDatabaseName()
        {
            return databaseName;
        }

        /*public static List<string> LoadPP()
        {
            // Need to do  Ctrl+. on 'IdbConnection' and 'SQLiteConnection' to add a using statement for each
            // This using statement makes sure that the Database connection is closed,
            // whether it reaches the end of the statement or a crash
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("SELECT * FROM NPC",new DynamicParameters());
                return output.ToList();
            }
        }*/

        /*public static void SavePerson(List<string> entry)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO NPC ( Name,)", entry);
            }
        }*/

        // Takes a Table name as a parameter, Returns the amount of columns that table has
        // Mainly used for displaying the right amount of labels & Rich-Textboxes
        public static List<string> GetColumnAmount(string tableName)
        {
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();
            //cmd.CommandText = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('" + tableName + "');";
            cmd.CommandText = "PRAGMA table_info('" + tableName + "');";
            dataReader = cmd.ExecuteReader();

            List<string> columnNames = new List<string>();
            while (dataReader.Read())
            {
                columnNames.Add(dataReader.GetString(1));
            }

            dataReader.Close();
            cmd.Dispose();
            cnn.Close();

            return columnNames;
        }

        // Gets all Database Table Names for dropdown list
        // if parameter is false, will exclude non-category tables
        public static List<string> GetAllTables(bool allTables)
        {
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            DataTable t = cnn.GetSchema("Tables");
            List<string> tables = new List<string>();

            // exclude non-category tables
            if (!allTables)
            {
                foreach (DataRow row in t.Rows)
                {
                    // Ignore non-category tables
                    // Temp solution
                    // Fix later
                    if ((string)row[2] != "sqlite_sequence")
                    {
                        if ((string)row[2] != "Entries")
                        {
                            if ((string)row[2] != "LUTables")
                            {
                                // must be row[2] as table name is 3rd/[2] position
                                tables.Add((string)row[2]);
                            }
                        }
                    }
                }
            }
            else // include all tables
            {
                foreach (DataRow row in t.Rows)
                {
                    tables.Add((string)row[2]);
                }
            }
            cnn.Close();
            tables.Sort();
            return tables;
        }

        // Get all entry Names from a table
        public static List<string> GetAllTableEntries(string tableName)
        {
            List<string> tableEntries = new List<string>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT Name FROM " + tableName;
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                tableEntries.Add(dataReader.GetString(0));
            }

            cmd.Dispose();
            cnn.Close();
            return tableEntries;
        }

        // Get all entry IDs and Names from a table
        public static Dictionary<string, int> GetEntryIdAndName(string tableName)
        {
            Dictionary<string, int> dictCategoryEntries = new Dictionary<string, int>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            // NEED TO FINISH
            cnn.Close();
            return dictCategoryEntries;
        }


        // Gets a category table's ID from LUTable
        private static int GetTableID(string tableName)
        {
            int id = 0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT TableID FROM LUTables WHERE Name='" + tableName + "';";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                id = dataReader.GetInt32(0);
            }
            cmd.Dispose();
            cnn.Close();
            return id;
        }


        // 
        public static int AddTableEntry(string categoryName, string newEntryName,string txtFileAddress=null )
        {
            int entryID = AddToEntriesStart(newEntryName,GetTableID(categoryName),txtFileAddress);
            int tableEntryID = AddToCategoryTable(entryID,newEntryName,categoryName);
            AddToEntriesEnd(entryID,tableEntryID);

            return tableEntryID;
        }

        // Creates most of a new entry in the main table Entries. Will need to be updated with the category ID
        // Returns the created entry's EntryID for creating the category table entry
        private static int AddToEntriesStart(string entryName,int tableID,string fileAddress)
        {
            int entryID=0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            if (fileAddress != null)
            {
                cmd.CommandText = "INSERT INTO Entries (Name,TableID,FileAddress) VALUES ('" + entryName + "'," + tableID + ",'" + fileAddress + "') RETURNING EntryID;";
            } else
            {
                cmd.CommandText = "INSERT INTO Entries (Name,TableID) VALUES ('" + entryName +"'," + tableID +") RETURNING EntryID;";
            }

            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                entryID = dataReader.GetInt32(0);
            }
            cmd.Dispose();
            cnn.Close();

            return entryID;
        }

        // Updates the new entry with the category table ID
        private static void AddToEntriesEnd(int entryID,int tableEntryID)
        {
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "UPDATE Entries SET TableEntryID = " + tableEntryID + " WHERE EntryID = " + entryID + ";";
            dataReader = cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Close();
        }

        // Creates entry in given category table
        private static int AddToCategoryTable(int entryID,string entryName,string categoryName)
        {
            int tableEntryID=0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            //
            cmd.CommandText = "INSERT INTO " + categoryName + " (EntryID,Name) VALUES (" + entryID + ",'" + entryName + "') RETURNING " + categoryName + "ID;";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                tableEntryID = dataReader.GetInt32(0);
            }
            cmd.Dispose();
            cnn.Close();
            return tableEntryID;
        }


        // Updates table entry's values with values from textbox
        public static void UpdateSelectedDBEntry(string categoryName,string entryName, Dictionary<string, string> dictDBEntryData)
        {
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "UPDATE " + categoryName + " SET ";

            string last = dictDBEntryData.Keys.Last();

            foreach(KeyValuePair<string, string> kvp in dictDBEntryData)
            {
                if (kvp.Key == last)
                {
                    cmd.CommandText += $"{kvp.Key} = '{kvp.Value}' ";
                }
                else
                {
                    cmd.CommandText += $"{kvp.Key} = '{kvp.Value}', ";
                }
            }
            cmd.CommandText += "WHERE Name = '" + entryName + "';";

            dataReader = cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Close();
        }

        // Return string of an entry's file address from main table
        public static string GetFileAddress(string categoryName,string entryName)
        {
            string address = null;
            int id = 0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT EntryID FROM " + categoryName + " WHERE Name = '" + entryName + "';";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                id = dataReader.GetInt32(0);
            }
            cmd.Dispose();

            cmd.CommandText = "SELECT FileAddress FROM Entries WHERE EntryID = '" + id + "';";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    address = dataReader.GetString(0);
                }
            }
            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
            return address;
        }

        // Get DB connection string
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
