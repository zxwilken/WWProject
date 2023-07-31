using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ListViewDatabases.Font = new Font("Microsoft Sans Serif", 14);
            ListViewDatabases.SelectedIndexChanged += ListViewDatabases_SelectedChange;
            ButtonDeleteDatabase.Enabled = false;
        }

        private void SetUpProgram()
        {
            SaveDatabases();
        }

        private string[] GetDatabases()
        {
            string startUpPath = Application.StartupPath + @"\Databases\";
            if (Directory.Exists(startUpPath))
            {
                return Directory.GetFiles(startUpPath, "*.db");
            } else
            {
                Directory.CreateDirectory(startUpPath);
                return null;
            }
        }// !!

        // Reads and saves the names and file paths of each .db in a select folder
        private void SaveDatabases()
        {
            string[] filePaths = GetDatabases();
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


        private void DisplayDatabases()
        {
            ListViewDatabases.Items.Clear();
            // Add DB names from dictDatabases to ListViewDatabases
            foreach(string name in dictDatabases.Keys)
            {
                ListViewDatabases.Items.Add(name);
            }
        }// !!


        private void StartEditor(string chosenDB)
        {
            // Create new Editor object
            // Send this form to Editor??
            Editor ed = new Editor(chosenDB,this);
            ed.Show();
            //this.Close();
            this.Enabled = false;
            this.Visible = false;
        }

        public bool CheckForDuplicateName(string name)
        {
            string[] filePaths = GetDatabases();
            if(filePaths != null)
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

        private void MakeNewDatabase(string name)
        {
            // Copy default DB to 'Databases' directory
            string newDatabaseName = name + ".db";
            string startingPath = Application.StartupPath + @"\..\..\DefaultDatabaseCopy\Default.db";
            string destinationPath = Application.StartupPath + @"\Databases\" + newDatabaseName;
            File.Copy(startingPath, destinationPath, false);

            // Refresh ListViewDatabases and dictDatabases
            SaveDatabases();
        }// !!

        private string GetSelectedDatabase()
        {
            if (ListViewDatabases.SelectedItems.Count > 0)
            {
                return ListViewDatabases.SelectedItems[0].Text;
            }
            else return null;
        }// !!

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
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            // If Yes, delete selected database and file directory
            if (result == DialogResult.Yes)
            {
                if (dictDatabases.ContainsKey(selectedDB))
                {
                    if (File.Exists(dictDatabases[selectedDB]))
                    {
                        File.Delete(dictDatabases[selectedDB]);
                        if(Directory.Exists(Application.StartupPath + @"\" + selectedDB))
                        {
                            Directory.Delete(Application.StartupPath + @"\" + selectedDB,true);
                        }
                        dictDatabases.Remove(selectedDB);
                    }
                    else MessageBox.Show("Database file does not exist.");
                }
                else MessageBox.Show("Database does not exist.");
            }
            // Update ListViewDatabases.
            SaveDatabases();
        }

        private void AddDatabaseConnectionString(string name)
        {
            if (File.Exists(dictDatabases[name]))
            {
                AppSetting.ChangeConnectionString(name);
            }
        }

        // *********************************************************************************************
        // =============================================================================================
        // COMPONENT EVENTS
        // =============================================================================================
        // *********************************************************************************************

        private void ButtonNewDatabase_Click(object sender, EventArgs e)
        {
            // Check for special characters
            if (SqliteDataAccess.CheckUserInput(TextboxNewDB.Text))
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

        private void ButtonDeleteDatabase_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + @"\" + GetSelectedDatabase()))
            {
                MessageBox.Show(Application.StartupPath + @"\" + GetSelectedDatabase());
                //Directory.Delete(Application.StartupPath + @"\" + GetSelectedDatabase(), true);
            }
            //DeleteSelectedDatabase();
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string selectedDB = GetSelectedDatabase();
            if (selectedDB == null || selectedDB == "")
                return;
            
            // Edit App.config
            AddDatabaseConnectionString(selectedDB);
            //AddDatabaseConnectionString("WorldDB");
            
            StartEditor(selectedDB); // check that it contains data type
        }

        private void ListViewDatabases_SelectedChange(object sender, EventArgs e)
        {
            if(ListViewDatabases.SelectedItems.Count > 0)
            {
                //ButtonDeleteDatabase.Enabled = true;
            } //else ButtonDeleteDatabase.Enabled = false;
        }
    }
}
