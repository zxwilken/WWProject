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
        StartUp startUP;
        bool fullClose;
        // Dictionary with Column name as the key for the RichTextBox holding data
        private Dictionary<string, RichTextBox> dictDBEntryData = new Dictionary<string, RichTextBox>();
        // Dictionary holding Entry name(key) and its [Table]ID (value)
        private Dictionary<string, int> dictCategoryEntries = new Dictionary<string, int>();

        // current values
        private string currentTxtFileAddress = "";
        private string currentTableName = "";
        private string currentEntryName = "";
        // true if searching through all DB entries
        private bool searchAll = false;
        private string startUpPath = Application.StartupPath + @"\";

        public Editor(string name,StartUp stUp)
        {
            InitializeComponent();
            startUP = stUp;
            fullClose = true;
            databaseName = name;
            this.Text = "Editor: " + name;
            EditorStartUp();
            PanelDatabase.BringToFront();
        }
        // ###################################################################################################


        // May need parameter holding Server / Database info
        // Manages all of the setup required of the Editor
        //      Save server / database information in variables
        //      Get all Table names
        // 
        public void EditorStartUp()
        {
            // setup search bar
            TextboxSearch.Text = "Search Database here...";
            TextboxSearch.GotFocus += Textbox_RemovePlaceholderText;
            TextboxSearch.LostFocus += Textbox_AddPlaceholderText;

            FileManagementHelper.CheckFileSystem(databaseName);

            // Fill ComboBoxCategories with DB's category tables
            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            RichTextBoxMain.Enabled = false;
            ButtonAddTextFile.Enabled = false;
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
                string path = startUpPath + databaseName + @"\";
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
            // Get names of table columns
            List<string> columnNames = SqliteDataAccess.GetColumnAmount(categoryName);
            // set current entry and table names to their globals
            LabelEntryName.Text = newEntryName;
            currentEntryName = newEntryName;
            currentTableName = categoryName;

            // Store entry data
            List<string> entryData = SqliteDataAccess.GetEntryDataByCategoryID(categoryName, tableID);

            // Clears dictionary before adding new set
            dictDBEntryData.Clear();
            // Clears panel of old displayed data
            PanelDatabase.Controls.Clear();
            dictDBEntryData = DataDisplayHelper.DynamDisplayEntries(columnNames, entryData, PanelDatabase);
    
            // If new entry belongs to currently selected category,
            // update the entry list
            if (categoryName == ComboBoxCategories.Text)
            {
                UpdateEntryList(ComboBoxCategories.Text);
            }
        }
        // ###################################################################################################


        // clear ListViewEntries and fill it with entries from newly selected category
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


        // REMOVE AFTER CONFIRMING EVERYTHING ELSE WORKS
        // Method to display all data from relevant DB entry
        private void DynamTableEntries(List<string> columnNames, SQLiteCommand cmd)
        {
            SQLiteDataReader dataReader = cmd.ExecuteReader();

            int labelX = 50;
            int textBoxX = 150;       // Find a better way than just hard coding it
            int contentY = 40;
            Font newFont = new Font("Segoe UI", 12);

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
                    label.Size = new Size(100,50);
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

                    contentY = richText.Bottom + 35;

                }
            }
        }
        // ###################################################################################################
        // REMOVE AFTER CONFIRMING EVERYTHING ELSE WORKS

        // Add new table to DB. Update category dropdown
        public void AddNewTable(string tableName, List<string> newColumns)
        {
            SqliteDataAccess.AddNewTable(tableName, newColumns);
            UpdateDropdownAndDirectories();
        }
        // ###################################################################################################


        // Updates category dropdown and checks filesystem
        public void UpdateDropdownAndDirectories()
        {
            ComboBoxCategories.Items.Clear();
            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            FileManagementHelper.CheckFileSystem(databaseName);
        }
        // ###################################################################################################


        // Delete currently selected text file
        private void RemoveTextFile()
        {
            // remove text file address stored in DB
            SqliteDataAccess.RemoveTextFileAddress(currentTableName, currentEntryName);
            // delete text file
            if (File.Exists(currentTxtFileAddress))
            {
                File.Delete(currentTxtFileAddress);
            }
            // Refresh RichTextBox
            RichTextBoxMain.Clear();
            RichTextBoxMain.Enabled = false;
            ButtonAddTextFile.Enabled = true;
        }
        // ###################################################################################################


        // Clear Editor panels related to entry data display. If parameter is true, clear RichTextboxMain too.
        private void ClearDataPanels(bool clearRichTextboxMain)
        {
            PanelDatabase.Controls.Clear();
            LabelEntryName.Text = "        ";
            dictDBEntryData.Clear();
            currentEntryName = "";
            currentTxtFileAddress = "";
            if (clearRichTextboxMain)
            {
                RichTextBoxMain.Clear();
                RichTextBoxMain.Enabled = false;
                ButtonAddTextFile.Enabled = false;
            }
        }
        // ###################################################################################################


        // Update currently selected DB entry
        private void UpdateSelectedEntry()
        {
            // Need to transfer dictDBentryData's elements to a dictionary compatible w/ SqliteDataAccess function
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (KeyValuePair<string, RichTextBox> kvp in dictDBEntryData)
            {
                dict.Add(kvp.Key, kvp.Value.Text);
            }
            SqliteDataAccess.UpdateSelectedDBEntry(currentTableName, currentEntryName, dict);
        }
        // ###################################################################################################




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
            try
            {
                if (!searchAll)
                {
                    currentTableName = ComboBoxCategories.Text;

                }
                else
                {
                    //currentTableName = SqliteDataAccess.
                    currentTableName = SqliteDataAccess.GetTableNameFromEntryID(dictCategoryEntries[ListViewEntries.SelectedItems[0].SubItems[0].Text]);
                }
                List<string> columnNames = SqliteDataAccess.GetColumnAmount(currentTableName);
                if (ListViewEntries.SelectedItems[0].SubItems[0].Text == "")
                    return;

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
                }
                else
                {
                    RichTextBoxMain.Enabled = true;
                    ButtonAddTextFile.Enabled = false;
                    currentTxtFileAddress = startUpPath + databaseName + @"\" + address;
                    RichTextBoxMain.Text = File.ReadAllText(currentTxtFileAddress);
                }

                List<string> entryData = new List<string>();
                // Clears dictionary before adding new set
                dictDBEntryData.Clear();
                // Clears panel of old displayed data
                PanelDatabase.Controls.Clear();
                // The 'value' of each 'key' contained in the Dictionary dictCategoryEntries is different
                // depending on whether the Dictionary was filled based on ComboBoxCategories_SelectedIndexChanged or from TextboxSearch_KeyPress
                // ComboBoxCategories_SelectedIndexChanged returns Entry Name and its [Table]ID
                // while TextboxSearch_KeyPress returns its Entry Name and EntryID
                // Using Different command text is simple way instead of trying to do another SQL query
                if (!searchAll)
                {
                    entryData = SqliteDataAccess.GetEntryDataByCategoryID(currentTableName, dictCategoryEntries[currentEntryName]);
                }
                else
                {
                    entryData = SqliteDataAccess.GetEntryDataByEntryID(currentTableName, dictCategoryEntries[currentEntryName]);
                }

                dictDBEntryData = DataDisplayHelper.DynamDisplayEntries(columnNames, entryData, PanelDatabase);

            } catch (Exception ex)
            {
                return;
            }
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
            UpdateSelectedEntry();
        }
        // ###################################################################################################

        // Save textbox content to currently open text file
        private void ButtonSaveTextFile_Click(object sender, EventArgs e)
        {
            File.WriteAllText(currentTxtFileAddress, RichTextBoxMain.Text);
        }
        // ###################################################################################################


        // Creates new text file and adds its address to DB
        private void ButtonAddTextFile_Click(object sender, EventArgs e)
        {
            currentTxtFileAddress = startUpPath + databaseName + @"\" + currentTableName + @"\" + currentEntryName + ".txt";
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


        //Opens New form to create a new category
        private void ButtonNewCategory_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            TableForm tableForm = new TableForm(this,true);
            tableForm.Show();
        }
        // ###################################################################################################


        //Opens new form to edit or delete a DB table
        private void ButtonEditTable_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            TableForm tableForm = new TableForm(this, false);
            tableForm.Show();
        }
        // ###################################################################################################


        // If TextboxSearch is clicked and is in foces, remove its placeholder text
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


        // If TextboxSearch text is removed, re-add placeholder text
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


        // Button that will call method to delete currently selected text file
        private void ButtonDeleteTextFile_Click(object sender, EventArgs e)
        {
            RemoveTextFile();
        }
        // ###################################################################################################


        // Deletes currently selected DB entry + related text file
        private void ButtonDeleteDBEntry_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete the selected entry?\n\n" +
                "Doing so will also delete any text file attached to this entry.";
            string caption = "Delete Current Entry";

            // If Yes, call SQLiteDataAccess function to delete table
            if (UserInputHelper.YesNoMessage(message, caption))
            {
                // Call
                SqliteDataAccess.DeleteTableEntry(currentTableName, currentEntryName);
                RemoveTextFile();
                // currently selected Category is the same as deleted entry's, refresh displayed entry list
                if(ComboBoxCategories.Text == currentTableName)
                {
                    UpdateEntryList(currentTableName);
                }
                ClearDataPanels(false);
            }
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(fullClose)
            startUP.Close();
        }

        private void ButtonToStartUp_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to continue? Any unsaved data will be lost.\n\nContinue?";
            string caption = "Close Editor";
            // Ask user if if they are sure they want to pick new DB
            if (UserInputHelper.YesNoMessage(msg, caption))
            {
                fullClose = false;
                startUP.Enabled = true;
                startUP.Visible = true;
                this.Close();
            }           
        }
        // ###################################################################################################
    }
}
