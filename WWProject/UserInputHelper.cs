using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWProject
{
    // Helper class for user inputs
    internal static class UserInputHelper
    {
        // List of Column names that can't be used
        private static string[] HiddenColumnList()
        {
            string[] columnList = { "Name","EntryID","ID" };
            return columnList;
        }

        // Creates a Yes/No MessageBox with given message
        public static bool YesNoMessage(string message, string caption)
        {
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            // If Yes, delete selected database and file directory
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }
        // ###################################################################################################

        // Creates a Yes/No MessageBox /w default message
        public static bool YesNoMessage()
        {
            string message = "Are you sure you want to continue?";
            string caption = "User Responce";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            // If Yes, delete selected database and file directory
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }
        // ###################################################################################################

        // Checks If user input is made of standard characters.
        public static bool CheckUserInput(string userInput)
        {
            if (userInput.All(char.IsLetterOrDigit))
            {
                return true;
            }
            else return false;
        }
        // ###################################################################################################

        // Limits size of column names
        public static bool CheckColumnNameSize(string name)
        {
            if (name.Length > 12)
                return false;
            else 
                return true;
        }

        // Limits size of DB names
        public static bool CheckDBNameSize(string name)
        {
            if (name.Length > 20)
                return false;
            else
                return true;
        }

        // Checks if user created column name is the same as one of the not displayed columns
        public static bool CheckIfHiddenName(string name,string tableName)
        {
            foreach(string columnName in HiddenColumnList())
            {
                if (columnName == "ID")
                {
                    if (name.ToLower() == (tableName + columnName).ToLower())
                        return false;
                }
                if (columnName.ToLower() == tableName.ToLower())
                    return false;
            }
            return true;
        }

    }
}
