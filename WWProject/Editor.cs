using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WWProject
{
    public partial class Editor : Form
    {
        SqliteDataAccess sqliteData;

        // Dictionary with Column name as the key for the RichTextBox holding data
        private Dictionary<string, RichTextBox> dictDBEntryData = new Dictionary<string, RichTextBox>();
        // Dictionary for 
        private Dictionary<string, int> dictCategoryEntries = new Dictionary<string, int>();

        // current values
        private string currentTxtFileAddress = "";
        private string currentTableName = "";
        private string currentEntryName = "";

        public Editor()
        {
            InitializeComponent();
            sqliteData = new SqliteDataAccess();

            EditorStartUp();
        }
        // ---------------------------------------------------------------------------------------------------

        // May need parameter holding Server / Database info
        // Manages all of the setup required of the Editor
        //      Save server / database information in variables
        //      Get all Table names
        // 
        public void EditorStartUp()
        {
            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            RichTextBoxMain.Enabled = false;

            CheckFileSystem();
        }
        // ---------------------------------------------------------------------------------------------------

        // Checks if the root directory for text files exists
        // if any directories are missing, a new one is created
        private void CheckFileSystem()
        {
            string path = Application.StartupPath + sqliteData.GetDatabaseName() + @"\";

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Project's Root Directory\nDoes Not Exist\nCreating New Root Directory");
                Directory.CreateDirectory(path);
            } 
            CreateFileSystem(path);
        }
        // ---------------------------------------------------------------------------------------------------

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
        // ---------------------------------------------------------------------------------------------------


        // Called by NewEntryForm when creating a new table entry
        // Calls a SqliteDataAccess method to start the process of creating new entry
        // Will display new entry in Editor
        public void GetNewEntryValues(bool addTextBox,string categoryName, string newEntryName)
        {
            int tableID = 0;
            if (addTextBox)
            {
                string path = Application.StartupPath + sqliteData.GetDatabaseName() + @"\";
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
            }
            else
            {
                tableID = SqliteDataAccess.AddTableEntry(categoryName, newEntryName);
                RichTextBoxMain.Enabled = false;
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
            // ------------------------ Will eventually need to ask user if they want to save any updated data before creation of new entry -----------------
        }
        // ---------------------------------------------------------------------------------------------------

        // If the selected drop down text changes, clear ListViewEntries
        // and fill it with entries from newely selected category
        private void ComboBoxCategories_TextChanged(object sender, EventArgs e)
        {
            ListViewEntries.Clear();
            dictCategoryEntries.Clear();

            SQLiteConnection cnn = new SQLiteConnection(SqliteDataAccess.LoadConnectionString());
            cnn.Open();

            SQLiteDataReader dataReader;
            SQLiteCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT " + ComboBoxCategories.Text + "ID,Name FROM " + ComboBoxCategories.Text;
            dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                dictCategoryEntries.Add(dataReader.GetString(1), dataReader.GetInt32(0));
                ListViewEntries.Items.Add(dataReader.GetString(1));
            }

            dataReader.Close();
            cmd.Dispose();
            cnn.Close();
        }
        // ---------------------------------------------------------------------------------------------------

        // If a entry in ListViewEntries is double clicked,
        // show it's data in the Database panel on right
        private void ListViewEntries_DoubleClick(object sender, EventArgs e)
        {

            List<string> columnNames = SqliteDataAccess.GetColumnAmount(ComboBoxCategories.Text);

            LabelEntryName.Text = ListViewEntries.SelectedItems[0].SubItems[0].Text;
            currentEntryName = ListViewEntries.SelectedItems[0].SubItems[0].Text;
            currentTableName = ComboBoxCategories.Text;

            // Gets selected entries text file address if there is one
            currentTxtFileAddress = "";
            string address = null;
            address = SqliteDataAccess.GetFileAddress(currentTableName, currentEntryName);
            if(address == null)
            {
                RichTextBoxMain.Clear();
                RichTextBoxMain.Enabled = false;
            } else
            {
                RichTextBoxMain.Enabled = true;
                currentTxtFileAddress = Application.StartupPath + sqliteData.GetDatabaseName() + @"\" + address;
                RichTextBoxMain.Text = File.ReadAllText(currentTxtFileAddress);
            }

            SQLiteConnection cnn = new SQLiteConnection(SqliteDataAccess.LoadConnectionString());
            cnn.Open();
            SQLiteCommand cmd = cnn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM " + ComboBoxCategories.Text + " WHERE " + ComboBoxCategories.Text + "ID = " + dictCategoryEntries[ListViewEntries.SelectedItems[0].SubItems[0].Text];

            // Send SQLite command
            DynamTableEntries(columnNames, cmd);

            cmd.Dispose();
            cnn.Close();
        }
        // ---------------------------------------------------------------------------------------------------

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
                for(int i = 3; i < columnNames.Count; i++)
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
        // ---------------------------------------------------------------------------------------------------

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
        // ---------------------------------------------------------------------------------------------------

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
        // ---------------------------------------------------------------------------------------------------

        // Save textbox content to currently open text file
        private void ButtonSaveTextFile_Click(object sender, EventArgs e)
        {
            File.WriteAllText(currentTxtFileAddress, RichTextBoxMain.Text);
        }
        // ---------------------------------------------------------------------------------------------------
    }
}
