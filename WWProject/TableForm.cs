using System;
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

    
    // Create New Table
    //  Put all additional column names in  TableColumns  list
    //  Put textbox.text in another list that will be sent to SQL command
    //  for creating new table
    
    // SQL order for New Table
    //  1. create new table
    //  2. Add new table name into LUTables
    //  3. update/refresh Editor dropdown

    // Edit Existing Table
    //  Display each existing Column name in a TextBox, set as disabled
    //  On submittion, put each undisabled original column that has been edited in a Dictionary.
    //  Do one UPDATE SQL making those changes.
    //  2nd UPDATE with any new table columns


    public partial class TableForm : Form
    {
        Editor editorForm;

        // Editing existing table
        private Dictionary<CheckBox, TextBox> originalTableColumns;
        private List<Button> RemoveOriginalColumnButtonList;
        private List<string> originalColumnNames;
        private List<string> originalColumnsToDelete;
        string originalTableName;
        private ComboBox dropdownTables;
        private TextBox tableNameToEdit;


        private List<TextBox> newTableColumns = new List<TextBox>();
        private int contentY;
        private bool isNewTable;
        private TextBox newTableName;
        private int textBoxMaxNumber;

        //
        public TableForm(Editor ed,bool creatingTable)
        {
            InitializeComponent();

            contentY = LabelCategoryName.Bottom + 10;
            textBoxMaxNumber = 8;
            isNewTable = creatingTable;
            editorForm = ed;
            if (isNewTable)
            {
                this.Text = "Table Creation";
                NewTableStartup();
            }
            else
            {
                this.Text = "Edit Table";
                originalTableColumns = new Dictionary<CheckBox, TextBox>();
                RemoveOriginalColumnButtonList = new List<Button>();
                originalColumnNames = new List<string>();
                originalColumnsToDelete = new List<string>();
                originalTableName = "";
                EditTableStartup();
            }
            PanelName.Show();
            PanelContent.Show();
        }
        // ###################################################################################################


        // // Sets up the Form to create new database table
        private void NewTableStartup()
        {
            AddTableNameTextBox();
        }
        // ###################################################################################################


        // Sets up the Form to edit database tables
        private void EditTableStartup()
        {
            dropdownTables = new ComboBox();
            dropdownTables.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
            dropdownTables.Size = new Size(160, 28);
            dropdownTables.Font = new Font("Microsoft Sans Serif", 12);
            //categoryNames.SelectedIndex = 0;
            dropdownTables.Location = new Point(PanelName.Width / 2 - dropdownTables.Width / 2, LabelTableName.Bottom + 10);
            dropdownTables.TextChanged += ComboBoxCategryNames_TextChanged;

            Button buttonEditName = new Button();
            buttonEditName.Font = new Font("Microsoft Sans Serif", 12);
            buttonEditName.Text = "Edit Name";
            buttonEditName.Location = new Point(dropdownTables.Left - buttonEditName.Width - 20,LabelTableName.Bottom + 10);
            buttonEditName.Height = 27;
            buttonEditName.Click += ButtonEditTableName_Click;

            tableNameToEdit = new TextBox();
            tableNameToEdit.Font = new Font("Microsoft Sans Serif", 12);
            tableNameToEdit.Location = dropdownTables.Location;
            tableNameToEdit.Size = dropdownTables.Size;
            tableNameToEdit.Visible = false;
            tableNameToEdit.Enabled = false;
            
            // Add Controls to panel
            PanelName.Controls.Add(dropdownTables);
            PanelName.Controls.Add(buttonEditName);
            PanelName.Controls.Add(tableNameToEdit);

        }
        // ###################################################################################################


        // Adds text box containing to hold table name
        private void AddTableNameTextBox(string tableName = "")
        {
            Font newFont = new Font("Microsoft Sans Serif", 12);
            TextBox richText = new TextBox();
            richText.Font = newFont;
            richText.Text = tableName;
            richText.MinimumSize = new Size(160, 0);
            richText.MaximumSize = new Size(160, 28);
            richText.Anchor = AnchorStyles.None;
            richText.Location = new Point(PanelName.Width/2 - richText.Width/2, LabelTableName.Bottom + 10);

            PanelName.Controls.Add(richText);
            newTableName = richText;
        }
        // ###################################################################################################


        //
        private TextBox AddColumnToList(string columnlName = "",bool disableBox = false )
        {
            Font newFont = new Font("Microsoft Sans Serif", 12);

            // TextBox Creation --------
            TextBox richText = new TextBox();
            richText.Text = columnlName;
            richText.Font = newFont;
            richText.MinimumSize = new Size(160, 0);
            richText.MaximumSize = new Size(160, 28);
            richText.Anchor = AnchorStyles.None;
            richText.Location = new Point(PanelContent.Width / 2 - richText.Width / 2, contentY);

            if (disableBox) // Disable existing 
            {
                richText.Enabled = false;
                // create checkbox object
                CreateCheckBox(richText);
            }else
            {
                newTableColumns.Add(richText);
            }
            // -------------------------

            PanelContent.Controls.Add(richText);

            contentY += richText.Height + 10;

            return richText;
        }
        // ###################################################################################################

        //
        private void RemoveNewColumnFromList()
        {
            if (newTableColumns.Count == 10 || (!isNewTable && (newTableColumns.Count + originalColumnNames.Count) == 10))
                ButtonColumnAdd.Enabled = true;

            if (newTableColumns.Count > 0)
            {
                contentY -= newTableColumns[newTableColumns.Count - 1].Height + 10;
                PanelContent.Controls.Remove(newTableColumns[newTableColumns.Count - 1]);
                newTableColumns[newTableColumns.Count - 1].Dispose();
                newTableColumns.RemoveAt(newTableColumns.Count - 1);
            }
            
        }
        // ###################################################################################################

        //
        private void CreateCheckBox(TextBox richText)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = "";
            checkBox.Width = 15;
            checkBox.Location = new Point(richText.Right + checkBox.Width / 2, richText.Top);
            checkBox.Checked = true;

            checkBox.CheckedChanged += TextBoxStatus_CheckedChanged;
            PanelContent.Controls.Add(checkBox);
            originalTableColumns.Add(checkBox,richText);

        }
        // ###################################################################################################


        //
        private bool CheckSubmittedColumns()
        {
            List<string> badInputs = new List<string>();

            for (int i = 0; i < newTableColumns.Count; i++)
            {
                if (!SqliteDataAccess.CheckUserInput(newTableColumns[i].Text) || newTableColumns[i].Text == "")
                {
                    badInputs.Add(newTableColumns[i].Text);
                    newTableColumns.RemoveAt(i);
                    continue;
                }

                if(newTableColumns[i].Text.ToLower() == "name")
                {
                    MessageBox.Show("'Name' is automatically added. Please change.");
                    return false;
                }

                if (i != newTableColumns.Count - 1)
                {
                    for (int j = i + 1; j < newTableColumns.Count; j++)
                    {
                        if (newTableColumns[j].Text.ToLower() == newTableColumns[i].Text.ToLower() || newTableColumns[j].Text.ToLower() == newTableName.Text.ToLower())
                        {
                            MessageBox.Show("Cannot have multiplies of the same name.");
                            return false;
                        }
                    }
                }
            }

            if (badInputs.Count > 0)
            {
                string badList = "List of bad user inputs for new column names:\n";
                foreach (string s in badInputs)
                {
                    badList += s + "\n";
                }
                MessageBox.Show(badList);
                return false;
            }

            return true;
        }
        // ###################################################################################################

        // 
        private bool SubmitNewTable()
        {
            // If problem with edited columns, return false
            if (!CheckSubmittedColumns())
            {
                return false;
            }

            // check that Table Name already exists
            // CHANGE PARAMETER TO DEFUALT STRING SO IT CAN BE USED BY BOTH FORMS
            if (!SqliteDataAccess.CheckIfTableExists(newTableName.Text))
                return false;

            List<string> goodColumns = new List<string>();
            foreach(TextBox text in newTableColumns)
            {
                goodColumns.Add(text.Text);
            }
            // Create new table with given name and column name list
            editorForm.AddNewTable(newTableName.Text, goodColumns);
            return true;
        }
        // ###################################################################################################


        //
        private bool SubmitEditedTable()
        {
            bool tableNameEdited = false;
            bool originalColumnsEdited = false;
            bool originalColumnDeleted = false;
            bool newColumnsEdited = false;

            List<string> badInputs = new List<string>();
            List<string> newGoodInputs = new List<string>();

            Dictionary<string,string> changedColumnNames = new Dictionary<string,string>();

            // if Table Name is in Edit mode & has been edited
            // if Table Name has been edited, it doesn't match any existing Table Names
            if (tableNameToEdit.Enabled && tableNameToEdit.Text.ToLower() != originalTableName.ToLower())
            {
                if (!SqliteDataAccess.CheckIfTableExists(tableNameToEdit.Text))
                {
                    return false;
                }
                if(tableNameToEdit.Text.ToLower() == "name")
                {
                    MessageBox.Show("Cannot use 'Name' as a Category name.");
                    return false;
                }
                if(tableNameToEdit.Text == "")
                {
                    MessageBox.Show("Text boxes cannot be left empty.");
                    return false;
                }
                tableNameEdited = true;
            }
            
            // if an Original column has been edited or empty
            for(int i = 0; i < originalTableColumns.Count; i++)
            {
                if (!SqliteDataAccess.CheckUserInput(originalTableColumns.ElementAt(i).Value.Text) || originalTableColumns.ElementAt(i).Value.Text == "")
                {
                    badInputs.Add(originalTableColumns.ElementAt(i).Value.Text);
                    continue;
                }
                if(originalTableColumns.ElementAt(i).Value.Text != originalColumnNames[i])
                {
                    originalColumnsEdited = true;
                    // Add edited columns to dictionary w/ the original column name
                    changedColumnNames.Add(originalColumnNames[i], originalTableColumns.ElementAt(i).Value.Text);
                }
            }
            // if an Original column has been deleted
            if(originalColumnsToDelete.Count > 0)
                originalColumnDeleted = true;

            // if a new column has been added & not blank
            // check that all column names consist of proper characters
            // Check that there are no duplicate column names (and don't match Table Name) and none are given the name 'Name'/'name'
            if (newTableColumns.Count != 0) {
                if (!CheckSubmittedColumns())
                    newColumnsEdited = true; // if true, scrap user attempt
                else
                {
                    foreach(TextBox column in newTableColumns)
                    {
                        newGoodInputs.Add(column.Text);
                    }
                }

            }

            // if old columns contain bad inputs
            if(badInputs.Count != 0)
            {
                string msg = "List of bad user inputs for old column names:\n";
                foreach (string columnName in badInputs) msg += columnName + "\n";
                MessageBox.Show(msg);
                return false;
            }
            // if new columns contain bad inputs
            if (newColumnsEdited)
            {
                return false;
            }

            // if original column was deleted, will need to remake entire table
            if (originalColumnDeleted)
            {

            }
            // Acceptable changes
            if (originalColumnsEdited)
            {
                SqliteDataAccess.EditTableColumns(changedColumnNames,originalTableName);
            }
            if (!newColumnsEdited)
            {
                SqliteDataAccess.AddNewColumns(newGoodInputs,originalTableName);
            }
            if (tableNameEdited)
            {
                SqliteDataAccess.EditTableName(originalTableName, tableNameToEdit.Text);
                // change Table's directory name
                string path = Application.StartupPath + editorForm.databaseName + @"\";
                System.IO.Directory.Move(path + originalTableName, path + tableNameToEdit.Text);
            }


            

            return true;
        }
        // ###################################################################################################



        // ---------------------------------------------------------------------------------------------------
        // CONTROL EVENTS below ------------------------------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------

        //
        private void ComboBoxCategryNames_TextChanged(object sender, EventArgs e)
        {
            newTableColumns.Clear();
            originalColumnNames.Clear();
            originalTableColumns.Clear();

            ComboBox table = sender as ComboBox;
            PanelContent.Controls.Clear();
            contentY = LabelCategoryName.Bottom + 10;
            originalColumnNames = SqliteDataAccess.GetColumnAmount(table.Text);
            originalColumnNames.RemoveRange(0, 3);
            //foreach (string column in originalColumnNames)
            //{
            //    AddColumnToList(column, true);
            //}
            for(int i = 0; i < originalColumnNames.Count; i++)
            {
                AddColumnToList(originalColumnNames[i], true);
            }

            if (!isNewTable && (newTableColumns.Count + originalColumnNames.Count) < textBoxMaxNumber)
            {
                ButtonColumnAdd.Enabled = true;
            }
            originalTableName = dropdownTables.Text;
        }
        // ###################################################################################################


        //
        private void ButtonEditTableName_Click(object sender, EventArgs e)
        {
            if (dropdownTables.Enabled)
            {
                if (dropdownTables.Text == "")
                {
                    return;
                }
                dropdownTables.Visible = false;
                dropdownTables.Enabled = false;

                tableNameToEdit.Enabled = true;
                tableNameToEdit.Visible = true;
                tableNameToEdit.Text = dropdownTables.Text;
                originalTableName = tableNameToEdit.Text;
            }
            else
            {
                dropdownTables.Visible = true;
                dropdownTables.Enabled = true;
                tableNameToEdit.Visible = false;
                tableNameToEdit.Enabled = false;
                //originalTableName = "";
            }

        }
        // ###################################################################################################


        //
        private void TextBoxStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (!checkBox.Checked)
            {
                // Enable linked TextBox
                originalTableColumns[checkBox].Enabled = true;
            }
            else
            {
                // Disable linked TextBox
                originalTableColumns[checkBox].Enabled = false;
            }
        }
        // ###################################################################################################

        //
        private void ButtonColumnAdd_Click(object sender, EventArgs e)
        {
            if(!isNewTable && dropdownTables.Text == "")
            {
                return;
            }
            
            AddColumnToList();
            if (isNewTable && newTableColumns.Count == textBoxMaxNumber)
            {
                ButtonColumnAdd.Enabled = false;
            } else if(!isNewTable && (newTableColumns.Count + originalColumnNames.Count) == textBoxMaxNumber)
            {
                ButtonColumnAdd.Enabled = false;
            }
        }
        // ###################################################################################################


        //
        private void ButtonColumnRemove_Click(object sender, EventArgs e)
        {
            RemoveNewColumnFromList();
        }
        // ###################################################################################################


        // Event for the submit button
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (isNewTable)
            {
                if (!SubmitNewTable())
                    return;
            }
            else
            {
                if (!SubmitEditedTable())
                    return;
            }

            this.Close();
        }
        // ###################################################################################################


        // On Form close, enable Editor
        private void TableForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            editorForm.UpdateDropdownAndDirectories();
            editorForm.Enabled = true;
        }
        // ###################################################################################################
    }
}
