using System;
using System.IO;
using System.Windows.Forms;

namespace WWProject
{
    internal class FileManagementHelper
    {

        // **************************************************************
        // ##############################################################
        // DIRECTORY/TEXT-FILE SECTION ==================================
        // ##############################################################
        // **************************************************************

        // Checks if the root directory for text files exists
        // if any directories are missing, a new one is created
        public static void CheckFileSystem(string databaseName)
        {
            //string path = Application.StartupPath + @"\" + databaseName + @"\";
            string path = Application.StartupPath + @"\" + databaseName + @"\";
            if (!Directory.Exists(path))
            {
                //MessageBox.Show("Project's Root Directory\nDoes Not Exist\nCreating New Root Directory");
                Directory.CreateDirectory(path);
            }
            CreateFileSystem(path);
        }
        // ###################################################################################################

        // Checks if category table directories exist,
        // if not, create a new one
        private static void CreateFileSystem(string path)
        {
            foreach (string dir in SqliteDataAccess.GetAllTables(false))
            {
                if (!Directory.Exists(path + dir))
                {
                    Directory.CreateDirectory(path + dir);
                }
            }
        }
        // ###################################################################################################


        public static bool DeleteTextDirectoryMain(string databaseName)
        {
            string path = Application.StartupPath + @"\" + databaseName + @"\";
            try
            {
                if (Directory.Exists(path) && databaseName != "")
                {
                    Directory.Delete(path, true);
                    return true;
                }
                else {
                    //MessageBox.Show("File directory not found.");
                    return true;
                }
            } catch (UnauthorizedAccessException e) { 
                MessageBox.Show($"Error deleting {path}: {e.Message}");
                return DeleteTextDirectorySub(path,databaseName);
            }
        }

        // Tries to delete a database directory by individually deleting the text files
        private static bool DeleteTextDirectorySub(string path,string databaseName)
        {
            try
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    if(Directory.GetFiles(directory).Length == 0)
                    {
                        Directory.Delete(directory, true);
                    } else
                    {
                        foreach(string file in Directory.GetFiles(directory))
                        {
                            File.Delete(file);
                        }
                        Directory.Delete(directory, true);
                    }
                }
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Error: {e.Message}");
                return false;
            }
        }

        // **************************************************************
        // ##############################################################
        // DATABASE SECTION =============================================
        // ##############################################################
        // **************************************************************

        public static string[] GetDatabases()
        {
            string startUpPath = Application.StartupPath + @"\Databases\";
            if (Directory.Exists(startUpPath))
            {
                return Directory.GetFiles(startUpPath, "*.db");
            }
            else
            {
                Directory.CreateDirectory(startUpPath);
                return null;
            }
        }// !!

        // Copy default DB to 'Databases' directory
        public static void CreateNewDatabase(string name)
        {
            string startingPath = Application.StartupPath + @"\..\..\DefaultDatabaseCopy\Default.db";
            if(!File.Exists(startingPath)){
                MessageBox.Show("The default database file has been moved, deleted, or is unreachable.\nThe creation of a new database is not possible.");
                return;
            }
            string newDatabaseName = name + ".db";
            string destinationPath = Application.StartupPath + @"\Databases\" + newDatabaseName;
            File.Copy(startingPath, destinationPath, false);
        }

        public static bool DeleteDatabase(string dbPath,string dbName)
        {
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
                if (DeleteTextDirectoryMain(dbName))
                {
                    return true;
                } else return false;
            }
            else
            {
                MessageBox.Show("Database file does not exist.");
                return false;
            }
        }
    }
}
