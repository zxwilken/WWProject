﻿using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace WWProject
{
    internal class SqliteDataAccess
    {

        // Get DB connection string
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        // ###################################################################################################


        // Takes a Table name as a parameter, Returns the columns names of given table
        public static List<string> GetColumnAmount(string tableName)
        {
            List<string> columnNames = new List<string>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "PRAGMA table_info('" + tableName + "');";
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                columnNames.Add(dataReader.GetString(1));
            }

            dataReader.Close();
            cmd.Dispose();
            cnn.Close();
            return columnNames;
        }
        // ###################################################################################################


        // Get all entry Names from a table
        public static List<string> GetAllTableEntries(string tableName)
        {
            List<string> tableEntries = new List<string>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
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
        // ###################################################################################################


        // Gets all Database Table Names for dropdown list
        // if parameter is false, will exclude non-category tables
        public static List<string> GetAllTables(bool allTables)
        {
            List<string> tables = new List<string>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            DataTable t = cnn.GetSchema("Tables");

            // exclude non-category tables
            if (!allTables)
            {
                foreach (DataRow row in t.Rows)
                {
                    // Ignore non-category tables
                    // Temp solution, Fix later
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
        // ###################################################################################################


        // Get all entry IDs and Names from a table
        public static Dictionary<string, int> GetTableEntryIdAndName(string tableName)
        {
            Dictionary<string, int> dictCategoryEntries = new Dictionary<string, int>();
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT " + tableName + "ID,Name FROM " + tableName;
           dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                dictCategoryEntries.Add(dataReader.GetString(1), dataReader.GetInt32(0));
                //ListViewEntries.Items.Add(dataReader.GetString(1));
            }

            dataReader.Close();
            cnn.Close();
            return dictCategoryEntries;
        }
        // ###################################################################################################


        // Get EntryID of category table entry
        public static int GetEntryID(string tableName,string entryName)
        {
            int entryID = 0;
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT EntryID FROM " + tableName + " WHERE Name = '" + entryName + "';";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                entryID = dataReader.GetInt32(0);
            }

            cmd.Dispose();
            cnn.Close();
            return entryID;
        }
        // ###################################################################################################


        // Gets a category table's ID from LUTable
        public static int GetTableID(string tableName)
        {
            int id = 0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
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
        // ###################################################################################################


        // Handles adding a new entry into the DB. 
        // 1. Creates new entry in Entries table. Fills in Name, TableID, and TextfileAddress if there is one. Returns the EntryID
        // 2. Creates new entry in the associated Category table. Returns the [Table]ID to add to the linked Entries entry
        // 3. Updates the Entries entry with the newly created [Table]ID
        public static int AddTableEntry(string categoryName, string newEntryName,string txtFileAddress=null )
        {
            int entryID = AddToEntriesStart(newEntryName,GetTableID(categoryName),txtFileAddress);
            int tableEntryID = AddToCategoryTable(entryID,newEntryName,categoryName);
            AddToEntriesEnd(entryID,tableEntryID);
            return tableEntryID;
        }
        // ###################################################################################################


        // Creates most of a new entry in the main table Entries. Will need to be updated with the category ID
        // Returns the created entry's EntryID for creating the category table entry
        private static int AddToEntriesStart(string entryName,int tableID,string fileAddress)
        {
            int entryID=0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
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
        // ###################################################################################################


        // Updates the new entry with the category table ID
        private static void AddToEntriesEnd(int entryID,int tableEntryID)
        {
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "UPDATE Entries SET TableEntryID = " + tableEntryID + " WHERE EntryID = " + entryID + ";";
            dataReader = cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################


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
        // ###################################################################################################


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
                    cmd.CommandText += $"{kvp.Key} = '{kvp.Value}' ";
                else
                    cmd.CommandText += $"{kvp.Key} = '{kvp.Value}', ";                
            }
            cmd.CommandText += "WHERE Name = '" + entryName + "';";

            dataReader = cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################


        // Return string of an entry's file address from main table
        public static string GetFileAddress(string categoryName,string entryName)
        {
            string address = null;
            int id = 0;
            // Create and open connection to local SQLite DB
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT EntryID FROM " + categoryName + " WHERE Name = '" + entryName + "';";
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                id = dataReader.GetInt32(0);
            }
            cmd.Dispose();
            // Dispose of current SQL command

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
        // ###################################################################################################

        // Gets table name from given EntryID
        // Currently used when searching all DB entries
        public static string GetTableNameFromEntryID(int entryID)
        {
            string tableName = null;

            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT Name FROM LUTables WHERE TableID = (SELECT TableID FROM Entries WHERE EntryID = " + entryID + ");";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                tableName = dataReader.GetString(0);
            }

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
            return tableName;
        }
        // ###################################################################################################

        // If a text file is added to an entry in post, updates the Entries entry with the file address
        public static void UpdateEntryFileAddress(int entryID,string textAddress)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "UPDATE Entries SET FileAddress = '" + textAddress + "' WHERE EntryID = " + entryID + ";";
            dataReader = cmd.ExecuteReader();

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
        }
        // ###################################################################################################


        // Creates a new Table with given table name and list of column names
        public static void AddNewTable(string tableName,List<string> columnNames)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "CREATE TABLE '" + tableName + "' ( '" + tableName + "ID' INTEGER NOT NULL UNIQUE, ";
            cmd.CommandText += "'EntryID' INTEGER NOT NULL, ";
            cmd.CommandText += "'Name' TEXT NOT NULL, ";

            foreach(string column in columnNames)
            {
                cmd.CommandText += "'" + column + "' TEXT, ";
            }
            cmd.CommandText += "PRIMARY KEY('" + tableName + "ID' AUTOINCREMENT));";
            dataReader=cmd.ExecuteReader();

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();

            AddToLUTables(tableName);
        }
        // ###################################################################################################

        // When a new table is created, add new entry into LUTables w/ its name
        private static void AddToLUTables(string tableName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "INSERT INTO LUTABLES (NAME) VALUES ('" + tableName + "');";
            dataReader = cmd.ExecuteReader();

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
        }
        // ###################################################################################################


        // Change a table name, primary key name, and related entry in LUTABLES
        public static void EditTableName(string oldName, string newName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "ALTER TABLE " + oldName + " RENAME TO " + newName + ";";
            cmd.ExecuteReader();
            cmd.Dispose();

            cmd.CommandText = "ALTER TABLE " + newName + " RENAME COLUMN " + oldName + "ID TO " + newName + "ID;";
            cmd.ExecuteReader();
            cmd.Dispose();

            cmd.CommandText = "UPDATE LUTABLES SET NAME = '" + newName + "' WHERE NAME = '" + oldName + "';";
            cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################


        // Alters the names of a table's columns
        public static void EditTableColumns(Dictionary<string,string> columns,string tableName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            string last = columns.Keys.Last();
            foreach(KeyValuePair<string, string> column in columns)
            {
                cmd.CommandText = "ALTER TABLE " + tableName + " RENAME COLUMN " + column.Key + " TO " + column.Value + ";";
                cmd.ExecuteReader();
                cmd.Dispose();
            }

            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################

        // Adds new column(s) to a table
        public static void AddNewColumns(List<string> newColumns,string tableName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            string last = newColumns.Last();

            cmd.CommandText = "ALTER TABLE " + tableName + " ADD COLUMN ";
            foreach(string column in newColumns)
            {
                if (column == last)
                    cmd.CommandText += column + " TEXT;";
                else
                    cmd.CommandText += column + " TEXT,";
            }
            dataReader = cmd.ExecuteReader();

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
        }
        // ###################################################################################################

        // Deletes a table column
        // SQLite cannot delete just delete a table column. A new table needs to be made without the specific column(s)
        // With the entries being copied over
        public static void DeleteTableColumn(List<string> chosenColumns,string tableName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            string last = chosenColumns.Last();
            cmd.CommandText = "PRAGMA foreign_keys=off; " +
                "CREATE TABLE IF NOT EXISTS new_table(" +
                tableName + "ID INTEGER NOT NULL UNIQUE, " +
                "EntryID INTEGER NOT NULL," +
                "Name  TEXT NOT NULL, ";
            foreach(string col in chosenColumns)
            {
                cmd.CommandText += col + "   TEXT, ";
            }
            cmd.CommandText += "PRIMARY KEY(" + tableName + "ID AUTOINCREMENT));" +
                "INSERT INTO new_table(" + tableName + "ID, EntryID,Name,";
            foreach(string col in chosenColumns)
            {
                if (col == last)
                    cmd.CommandText += col + ") ";
                else 
                    cmd.CommandText += col + ",";
            }
            cmd.CommandText += "SELECT " + tableName + "ID,EntryID,Name,";
            foreach(string col in chosenColumns)
            {
                if (col == last)
                    cmd.CommandText += col + " FROM " + tableName + ";";
                else
                    cmd.CommandText += col + ",";
            }
            cmd.CommandText += "DROP TABLE " + tableName + "; ";
            cmd.CommandText += "ALTER TABLE new_table RENAME TO " + tableName + "; ";
            cmd.CommandText += "PRAGMA foreign_keys=on;";

            dataReader = cmd.ExecuteReader();

            cmd.Dispose();
            dataReader.Close();
            cnn.Close();
        }
        // ###################################################################################################
        
        // Calls multiple functions containing SQL commands to delete a Table and related entries
        public static void RemoveTable(int tableID,string tableName)
        {
            DeleteEntriesByTableID(tableID);
            DeleteLUTablesEntry(tableID);
            DropTable(tableName);
        }
        // ###################################################################################################

        // Deletes entries in Entries table by TableID
        private static void DeleteEntriesByTableID(int tableID)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "DELETE FROM Entries WHERE TableID = " + tableID + ";";
            cmd.ExecuteReader();

            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################

        // Deletes a LUTables entry
        private static void DeleteLUTablesEntry(int tableID)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "DELETE FROM LUTables WHERE TableID = " + tableID + ";";
            cmd.ExecuteReader();

            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################

        // Drop a table
        private static void DropTable(string tableName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "DROP TABLE " + tableName + ";";
            cmd.ExecuteReader();

            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################

        // Delete an entries text file address from DB
        public static void RemoveTextFileAddress(string tableName,string entryName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "UPDATE Entries SET FileAddress = null WHERE EntryID = (SELECT EntryID FROM " + tableName + " WHERE Name = '" + entryName + "');";
            cmd.ExecuteReader();

            cmd.Dispose();
            cnn.Close();
        }

        // Delete entry from selected table and Entries
        public static void DeleteTableEntry(string tableName, string entryName)
        {
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "DELETE FROM Entries WHERE EntryID = (" +
                "SELECT EntryID FROM " + tableName + " WHERE Name = '" + entryName + "');";
            cmd.ExecuteReader();
            cmd.Dispose ();

            cmd.CommandText = "DELETE FROM " + tableName + " WHERE Name = '" + entryName + "';";
            cmd.ExecuteReader ();
            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################

        // Checks if given string matches existing table name
        public static bool CheckIfTableExists(string name)
        {
            foreach(string n in GetAllTables(true))
            {
                if (name.ToLower().Equals(n.ToLower()))
                {
                    System.Windows.Forms.MessageBox.Show("Table Name: " + name + "  Is Already Taken.");
                    return false;
                }
            }
            return true;
        }
        // ###################################################################################################

        // For Editor searchbar. Searches for entry names containing string given by user
        public static Dictionary<string, int> SearchBarGetEntries(string userEntry)
        {
            Dictionary<string, int> entries = new Dictionary<string, int>();

            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = "SELECT Name, EntryID FROM Entries WHERE Name Like '%" + userEntry + "%';";
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                entries.Add(dataReader.GetString(0),dataReader.GetInt32(1));
            }
            cnn.Close();
            return entries;
        }

        // Return an entries data
        public static List<string> GetEntryDataByCategoryID(string categoryName,int tableID)
        {
            List<string> entryData = new List<string>();
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM " + categoryName + " WHERE " + categoryName + "ID = " + tableID;
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    entryData.Add(dataReader.GetValue(i).ToString());
                }
            }
            

            cmd.Dispose();
            cnn.Close();
            return entryData;
        }

        // return entry data
        public static List<string> GetEntryDataByEntryID(string categoryName, int entryID)
        {
            List<string> entryData = new List<string>();
            int count = 0;
            SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString());
            SQLiteDataReader dataReader;
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM " + categoryName + " WHERE EntryID = " + entryID;
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    entryData.Add(dataReader.GetValue(i).ToString());
                }
            }
            cmd.Dispose();
            cnn.Close();
            return entryData;
        }
    }
}
