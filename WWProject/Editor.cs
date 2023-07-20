using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WWProject
{
    public partial class Editor : Form
    {

        public string databaseName;

        // Dictionary with Column name as the key for the RichTextBox holding data
        private Dictionary<string, RichTextBox> dictDBEntryData = new Dictionary<string, RichTextBox>();
        private Dictionary<string, int> dictCategoryEntries = new Dictionary<string, int>();

        // current values
        private string currentTxtFileAddress = "";
        private string currentTableName = "";
        private string currentEntryName = "";

        private bool searchAll = false;

        public Editor()
        {
            InitializeComponent();

            EditorStartUp();
        }
        // ###################################################################################################


        // May need parameter holding Server / Database info
        // Manages all of the setup required of the Editor
        //      Save server / database information in variables
        //      Get all Table names
        // 
        public void EditorStartUp()
        {
            databaseName = "WorldDB";

            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            RichTextBoxMain.Enabled = false;
            ButtonAddTextFile.Enabled = false;

            TextboxSearch.Text = "Search Database here...";
            TextboxSearch.GotFocus += Textbox_RemovePlaceholderText;
            TextboxSearch.LostFocus += Textbox_AddPlaceholderText;

            CheckFileSystem();
        }
        // ###################################################################################################

        // Checks if the root directory for text files exists
        // if any directories are missing, a new one is created
        private void CheckFileSystem()
        {
            string path = Application.StartupPath + databaseName + @"\";

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Project's Root Directory\nDoes Not Exist\nCreating New Root Directory");
                Directory.CreateDirectory(path);
            } 
            CreateFileSystem(path);
        }
        // ###################################################################################################

        // Checks if category table directories exist,
        // if not, create a new one
        private void CreateFileSystem(string path)
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

        // Called by NewEntryForm when creating a new table entry
        // Calls a SqliteDataAccess method to start the process of creating new entry
        // Will display new entry in Editor
        public void GetNewEntryValues(bool addTextBox,string categoryName, string newEntryName)
        {
            int tableID = 0;
            if (addTextBox)
            {
                string path = Application.StartupPath + databaseName + @"\";
                // txtFileAddress will only contain
                string txtFileAddress = categoryName + @"\" + newEntryName + ".txt";

                // check that category directory exists
                if (!Directory.Exists(path + categoryName))
                {
                    Directory.CreateDirectory(path + categoryName);
                }

                // create text file + save address
                FileStream fs = File.Create(path + txtFileAddress);
                // make sure new text file exists
                if(!File.Exists(path + txtFileAddress))
                {
                    MessageBox.Show("File was not created.");
                }
                fs.Close();
                // With Text File Address
                tableID = SqliteDataAccess.AddTableEntry(categoryName, newEntryName, txtFileAddress);
                currentTxtFileAddress = path + txtFileAddress;
                ButtonAddTextFile.Enabled = false;
                RichTextBoxMain.Enabled = true;
            }
            else
            {
                tableID = SqliteDataAccess.AddTableEntry(categoryName, newEntryName);
                RichTextBoxMain.Enabled = false;
                ButtonAddTextFile.Enabled = true;
            }

            // Display new Entry in Editor
            // clear Rich Text Box
            RichTextBoxMain.Clear();

            List<string> columnNames = SqliteDataAccess.GetColumnAmount(categoryName);

            LabelEntryName.Text = newEntryName;
            currentEntryName = newEntryName;
            currentTableName = categoryName;

            SQLiteConnection cnn = new SQLiteConnection(SqliteDataAccess.LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM " + categoryName + " WHERE " + categoryName + "ID = " + tableID;
            
            DynamTableEntries(columnNames, cmd);

            cmd.Dispose();
            cnn.Close();

            // If new entry belongs to currently selected category,
            // update the entry list
            if (categoryName == ComboBoxCategories.Text)
            {
                UpdateEntryList(ComboBoxCategories.Text);
            }
            // ------------------------ Will eventually need to ask user if they want to save any updated data before creation of new entry -----------------
        }
        // ###################################################################################################

        // clear ListViewEntries and fill it with entries from newely selected category
        private void UpdateEntryList(string categoryName)
        {
            ListViewEntries.Clear();
            dictCategoryEntries.Clear();

            dictCategoryEntries = SqliteDataAccess.GetTableEntryIdAndName(categoryName);

            foreach (string nm in dictCategoryEntries.Keys)
            {
                ListViewEntries.Items.Add(nm);
            }
        }
        // ###################################################################################################

        // Method to display all data from relevant DB entry
        private void DynamTableEntries(List<string> columnNames, SQLiteCommand cmd)
        {
            SQLiteDataReader dataReader = cmd.ExecuteReader();

            int labelX = 100;
            int textBoxX = 200;       // Find a better way than just hard coding it
            int contentY = 40;
            Font newFont = new Font("Microsoft Sans Serif", 12);

            // Clears dictionary before adding new set
            dictDBEntryData.Clear();
            // Clears panel of old displayed data
            PanelDatabase.Controls.Clear();

            // Creates a label for each table column name
            // and textbox for each column value 
            while (dataReader.Read())
            {
                // i needs to be   3   in normal cases
                for (int i = 3; i < columnNames.Count; i++)
                {
                    Label label = new Label();
                    label.Font = newFont;
                    label.Text = columnNames[i];
                    label.Location = new Point(labelX, contentY);

                    RichTextBox richText = new RichTextBox();
                    richText.Font = newFont;
                    richText.Text = dataReader.GetValue(i).ToString();
                    richText.Location = new Point(textBoxX, contentY);
                    richText.RightMargin = 200;                     // This sort-of solves the issue of text not wrapping.
                    richText.MaximumSize = new Size(200, 50);
                    richText.MinimumSize = new Size(200, 0);

                    richText.ContentsResized += (object sender, ContentsResizedEventArgs e) =>
                    {
                        var richTextBox = (RichTextBox)sender;
                        richTextBox.Width = e.NewRectangle.Width;
                        richTextBox.Height = e.NewRectangle.Height + 7;
                        richText.Width += richText.Margin.Horizontal + SystemInformation.HorizontalResizeBorderThickness;
                    };

                    // Add label Text and rich textbox to dictionary
                    dictDBEntryData.Add(label.Text, richText);

                    PanelDatabase.Controls.Add(label);
                    PanelDatabase.Controls.Add(richText);
                    PanelDatabase.Show();

                    contentY = richText.Bottom;
                }
            }
        }
        // ###################################################################################################

        public void AddNewTable(string tableName, List<string> newColumns)
        {
            SqliteDataAccess.AddNewTable(tableName, newColumns);
            UpdateDropdownAndDirectories();
        }
        
        public void UpdateDropdownAndDirectories()
        {
            ComboBoxCategories.Items.Clear();
            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            CheckFileSystem();
        }




        // *********************************************************************************************
        // =============================================================================================
        // COMPONENT EVENTS
        // =============================================================================================
        // *********************************************************************************************

        // If the selected drop down text changes, clear ListViewEntries
        // and fill it with entries from newely selected category
        private void ComboBoxCategories_TextChanged(object sender, EventArgs e)
        {
            //UpdateEntryList(ComboBoxCategories.Text);
        }
        // ###################################################################################################


        // If a entry in ListViewEntries is double clicked,
        // show it's data in the Database panel on right
        private void ListViewEntries_DoubleClick(object sender, EventArgs e)
        {
            if (!searchAll)
            {
                currentTableName = ComboBoxCategories.Text;

            } else
            {
                //currentTableName = SqliteDataAccess.
                currentTableName = SqliteDataAccess.GetTableNameFromEntryID(dictCategoryEntries[ListViewEntries.SelectedItems[0].SubItems[0].Text]);
            }
            List<string> columnNames = SqliteDataAccess.GetColumnAmount(currentTableName);
            LabelEntryName.Text = ListViewEntries.SelectedItems[0].SubItems[0].Text;
            currentEntryName = ListViewEntries.SelectedItems[0].SubItems[0].Text;

            // Gets selected entries text file address if there is one
            currentTxtFileAddress = "";
            string address = null;
            address = SqliteDataAccess.GetFileAddress(currentTableName, currentEntryName);
            if (address == null)
            {
                RichTextBoxMain.Clear();
                RichTextBoxMain.Enabled = false;
                ButtonAddTextFile.Enabled = true;
            } else
            {
                RichTextBoxMain.Enabled = true;
                ButtonAddTextFile.Enabled = false;
                currentTxtFileAddress = Application.StartupPath + databaseName + @"\" + address;
                RichTextBoxMain.Text = File.ReadAllText(currentTxtFileAddress);
            }

            SQLiteConnection cnn = new SQLiteConnection(SqliteDataAccess.LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();
            // create SQL statement to get all values from selected table

            // The 'value' of each 'key' contained in the Dictionary dictCategoryEntries is different
            // depending on whether the Dictionary was filled based on ComboBoxCategories_SelectedIndexChanged or from TextboxSearch_KeyPress
            // ComboBoxCategories_SelectedIndexChanged returns Entry Name and its [Table]ID
            // while TextboxSearch_KeyPress returns its Entry Name and EntryID
            // Using Different command text is simple way instead of trying to do another SQL query
            if (!searchAll) { 
            cmd.CommandText = @"SELECT * FROM " + currentTableName + " WHERE " + currentTableName + "ID = " + dictCategoryEntries[currentEntryName];
            } else
            {
                cmd.CommandText = @"SELECT * FROM " + currentTableName + " WHERE EntryID = " + dictCategoryEntries[currentEntryName];
            }

            // Send SQLite command
            DynamTableEntries(columnNames, cmd);

            cmd.Dispose();
            cnn.Close();
        }
        // ###################################################################################################


        // Brings up a form to let user make new table entry
        private void ButtonNewDBEntry_Click(object sender, EventArgs e)
        {
            // --------- Prompt user asking if they want to attach a text file ------------
            // bool includeTextFile = PromptTextFile();

            // Create new NewEntryForm
            // Disable/Make Uninteractable Editor
            // End of this method
            NewEntryForm form = new NewEntryForm(this);
            this.Enabled = false;
            form.Show();

            // After user clicks Submit disable form
            // take values from form, give to SqliteDataAccess
            // reactivate Editor form
            // delete NewEntryForm form
        }
        // ###################################################################################################

        // Update currently selected DB entry with the current textbox values
        private void ButtonSaveDBEntry_Click(object sender, EventArgs e)
        {
            Dictionary<string,string> dict = new Dictionary<string,string>();

            foreach(KeyValuePair<string,RichTextBox> kvp in dictDBEntryData)
            {
                dict.Add(kvp.Key, kvp.Value.Text);
            }

            SqliteDataAccess.UpdateSelectedDBEntry(currentTableName,currentEntryName,dict);
        }
        // ###################################################################################################

        // Save textbox content to currently open text file
        private void ButtonSaveTextFile_Click(object sender, EventArgs e)
        {
            File.WriteAllText(currentTxtFileAddress, RichTextBoxMain.Text);
        }

        // 
        private void ButtonAddTextFile_Click(object sender, EventArgs e)
        {
            //currentTxtFileAddress = Application.StartupPath + SqliteDataAccess.GetDatabaseName() + @"\" + currentTableName + @"\" + currentEntryName + ".txt";
            currentTxtFileAddress = Application.StartupPath + databaseName + @"\" + currentTableName + @"\" + currentEntryName + ".txt";
            string entryAddress = currentTableName + @"\" + currentEntryName + ".txt";
            // Create new text file
            FileStream fs = File.Create(currentTxtFileAddress);
            // make sure new text file exists
            if (!File.Exists(currentTxtFileAddress))
            {
                MessageBox.Show("File was not created.");
                return;
            }
            fs.Close();
            int entryID = SqliteDataAccess.GetEntryID(currentTableName, currentEntryName);
            SqliteDataAccess.UpdateEntryFileAddress(entryID, entryAddress);
            //Enable main text box, disable button to create new file
            RichTextBoxMain.Enabled = true;
            ButtonAddTextFile.Enabled = false;
        }
        // ###################################################################################################

        //
        private void ButtonNewCategory_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            TableForm tableForm = new TableForm(this,true);
            tableForm.Show();
        }

        private void ButtonEditTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            TableForm tableForm = new TableForm(this, false);
            tableForm.Show();
        }
        // ###################################################################################################

        // 
        private void Textbox_RemovePlaceholderText(object sender,EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox.Text == "Search Database here...")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
        // ###################################################################################################

        // 
        private void Textbox_AddPlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = "Search Database here...";
                textBox.ForeColor = Color.Gray;
            }
        }
        // ###################################################################################################

        // If the selected drop down text changes, clear ListViewEntries
        // and fill it with entries from newely selected category
        private void ComboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEntryList(ComboBoxCategories.Text);
            searchAll = false;
            TextboxSearch.Clear();
        }
        // ###################################################################################################

        //// If text is empty on keypress, clear combobox and listview
        private void TextboxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchAll = true;

            if (TextboxSearch.Text != "")
            {
                ComboBoxCategories.Text = "";
                ListViewEntries.Clear();
                dictCategoryEntries.Clear();

                dictCategoryEntries = SqliteDataAccess.SearchBarGetEntries(TextboxSearch.Text);
                foreach (string nm in dictCategoryEntries.Keys)
                {
                    ListViewEntries.Items.Add(nm);
                }
                /*if (TextboxSearch.Text == "")
                {
                    searchAll = false;
                    ListViewEntries.Clear();
                    dictCategoryEntries.Clear();
                }*/
            } else if (TextboxSearch.Text == "")
            {
                searchAll = false;
                ListViewEntries.Clear();
                dictCategoryEntries.Clear();
            }

        }
        // ###################################################################################################




    }
}
