using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WWProject
{
    public partial class StartUp : Form
    {
        // Key: Database name   Value: Database file path
        private Dictionary<string, string> dictDatabases = new Dictionary<string, string>();

        public StartUp()
        {
            InitializeComponent();
            SetUpProgram();
            ListViewDatabases.Font = new Font("Segoe UI", 14);
            ListViewDatabases.SelectedIndexChanged += ListViewDatabases_SelectedChange;
            //AppSetting.ClearConnectionString();
        }

        private void SetUpProgram()
        {
            SaveDatabases();
        }

        // Reads and saves the names and file paths of each .db in a select folder
        private void SaveDatabases()
        {
            string[] filePaths = FileManagementHelper.GetDatabases();
            if (filePaths != null)
            {
                dictDatabases.Clear();
                foreach (string filePath in filePaths)
                {
                    dictDatabases.Add(Path.GetFileNameWithoutExtension(filePath), filePath);
                }
                DisplayDatabases();
            }
        }// !!

        // Displays all databases in ListView
        private void DisplayDatabases()
        {
            ListViewDatabases.Items.Clear();
            // Add DB names from dictDatabases to ListViewDatabases
            foreach(string name in dictDatabases.Keys)
            {
                ListViewDatabases.Items.Add(name);
            }
        }// !!

        // 
        public bool CheckForDuplicateName(string name)
        {
            string[] filePaths = FileManagementHelper.GetDatabases();
            if (filePaths != null)
            {
                foreach (string filePath in filePaths)
                {
                    if(Path.GetFileName(filePath) == (name + ".db"))
                    {
                        return true;
                    }
                }
                return false;
            } else return false;
        }// !!

        //
        private void MakeNewDatabase(string name)
        {
            // Copy default DB to 'Databases' directory
            FileManagementHelper.CreateNewDatabase(name);
            // Refresh ListViewDatabases and dictDatabases
            SaveDatabases();
        }// !!

        // Returns text of selected ListView item
        private string GetSelectedDatabase()
        {
            if (ListViewDatabases.SelectedItems.Count > 0)
            {
                return ListViewDatabases.SelectedItems[0].Text;
            }
            else return null;
        }// !!

        // 
        private void DeleteSelectedDatabase()
        {
            // Checks currently selected/highlighted DB,
            // Creates a MessageBox asking if user is sure they want to delete DB. Includes all text files.
            string selectedDB = GetSelectedDatabase();
            if (selectedDB == null || selectedDB == "")
                return;

            string message = "Are you sure you want to delete the selected database?\n\n" +
               "Doing so will also delete all entries and text files associated with this database.";
            string caption = "Delete Current Database";
            // Create MessageBox prompting user
            // If Yes, delete selected database and file directory
            if (UserInputHelper.YesNoMessage(message, caption))
            {
                if (dictDatabases.ContainsKey(selectedDB))
                {
                    // if all database parts were successfully deleted,
                    if (FileManagementHelper.DeleteDatabase(dictDatabases[selectedDB], selectedDB))
                    {
                        dictDatabases.Remove(selectedDB);
                        // Update ListViewDatabases.
                        SaveDatabases();
                    }
                }
                else MessageBox.Show("Database does not exist.");
            }
        }

        // 
        private void ChangeDatabaseConnectionString(string name)
        {
            if (File.Exists(dictDatabases[name]))
            {
                AppSetting.ChangeConnectionString(name);
            }
        }

        // Creates new Editor form. Created on selection of database
        private void StartEditor(string chosenDB)
        {
            Editor ed = new Editor(chosenDB, this);
            ed.Show();
            this.Enabled = false;
            this.Visible = false;
        }

        // *********************************************************************************************
        // =============================================================================================
        // COMPONENT EVENTS
        // =============================================================================================
        // *********************************************************************************************

        // 
        private void ButtonNewDatabase_Click(object sender, EventArgs e)
        {
            // Check for special characters
            if (UserInputHelper.CheckUserInput(TextboxNewDB.Text) && UserInputHelper.CheckDBNameSize(TextboxNewDB.Text))
            {
                if (CheckForDuplicateName(TextboxNewDB.Text))
                {
                    MessageBox.Show("Database name is already in use.");
                    TextboxNewDB.Text = null;
                    return;
                }
                if(TextboxNewDB.Text == "")
                {
                    MessageBox.Show("Text box cannot be left empty");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Improper name given. Letters and Numbers only.");
                return;
            }
            // If no duplicate, create new database
            MakeNewDatabase(TextboxNewDB.Text);
            TextboxNewDB.Text = null;
        }// !!

        // 
        private void ButtonDeleteDatabase_Click(object sender, EventArgs e)
        {
                DeleteSelectedDatabase();
        }

        // On click, changes connectionString to selected ListView item, then opens up the Editor
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string selectedDB = GetSelectedDatabase();
            if (selectedDB == null || selectedDB == "")
                return;
            
            // Edit App.config
            ChangeDatabaseConnectionString(selectedDB);
            
            StartEditor(selectedDB); // check that it contains data type
        }

        // Enable/Disable delete button
        private void ListViewDatabases_SelectedChange(object sender, EventArgs e)
        {
            if(ListViewDatabases.SelectedItems.Count > 0)
            {
                ButtonDeleteDatabase.Enabled = true;
            } else ButtonDeleteDatabase.Enabled = false;
        }

        private void StartUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            //AppSetting.ClearConnectionString();
        }
    }
}
