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

    public partial class TableForm : Form
    {
        Editor editorForm;

        // Editing existing table
        private Dictionary<CheckBox, TextBox> originalTableColumns;
        private List<Button> removeOriginalColumnButtonList;
        private List<string> originalColumnNames;
        private List<int> originalColumnsToDelete;
        //private bool originalColumnDeleted = false;
        string originalTableName;
        private ComboBox dropdownTables;
        private TextBox tableNameToEdit;


        private List<TextBox> newTableColumns = new List<TextBox>();
        private int contentY;
        private bool isNewTable;
        private TextBox newTableName;
        private const int TEXTBOX_MAX_NUMBER = 8;

        //
        public TableForm(Editor ed,bool creatingTable)
        {
            InitializeComponent();

            contentY = LabelCategoryName.Bottom + 10;
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
                removeOriginalColumnButtonList = new List<Button>();
                originalColumnNames = new List<string>();
                originalColumnsToDelete = new List<int>();
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
            dropdownTables.SelectedIndexChanged += ComboBoxCategoryNames_SelectedIndexChanged;

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
                CreateDeleteColumnButton(richText);
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
        private void CreateCheckBox(TextBox textBox)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = "";
            checkBox.Width = 15;
            checkBox.Location = new Point(textBox.Right + checkBox.Width / 2, textBox.Top);
            checkBox.Checked = true;

            checkBox.CheckedChanged += TextBoxStatus_CheckedChanged;
            PanelContent.Controls.Add(checkBox);
            originalTableColumns.Add(checkBox, textBox);

        }
        // ###################################################################################################

        // Add button for deleting a Table column 
        private void CreateDeleteColumnButton(TextBox textBox)
        {
            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Width = 50;
            deleteButton.Location = new Point(textBox.Left - deleteButton.Width - 15,textBox.Top);

            // add event method
            deleteButton.Click += ColumnDeleteButton_Click;
            PanelContent.Controls.Add(deleteButton);
            removeOriginalColumnButtonList.Add(deleteButton);
            // 
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


        // When editing a table.
        // After submit button is clicked, this will check for valid inputs and whether certain
        // parts of the table have been edited or deleted, changing the resulting SQL command
        private bool SubmitEditedTable()
        {
            bool tableNameEdited = false;
            bool originalColumnsEdited = false;
            bool newColumnsEdited = false;
            bool deletingColumns = false;

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
            for (int i = 0; i < originalTableColumns.Count; i++)
            {
                // checks if the current loops index matches one of the columns chosen to be deleted,
                if (originalColumnsToDelete.Contains(i))
                    continue; 

                
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
            if (originalColumnsToDelete.Count > 0)
            {
                List<string> chosenColumns = new List<string>();
                for(int i = 0; i < originalColumnNames.Count; i++)
                {
                    if(!originalColumnsToDelete.Contains(i))
                        chosenColumns.Add(originalColumnNames[i]);
                }
                // SQL function
                SqliteDataAccess.DeleteTable(chosenColumns, originalTableName);
            }
            // Acceptable changes
            if (originalColumnsEdited)
            {
                SqliteDataAccess.EditTableColumns(changedColumnNames,originalTableName);
            }
            if (!newColumnsEdited && newTableColumns.Count > 0)
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

        // On dropdown change, display Table's columns 
        private void ComboBoxCategoryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            newTableColumns.Clear();
            originalColumnNames.Clear();
            originalTableColumns.Clear();
            originalColumnsToDelete.Clear();

            ComboBox table = sender as ComboBox;
            PanelContent.Controls.Clear();
            contentY = LabelCategoryName.Bottom + 10;
            originalColumnNames = SqliteDataAccess.GetColumnAmount(table.Text);
            originalColumnNames.RemoveRange(0, 3);
            //foreach (string column in originalColumnNames)
            //{
            //    AddColumnToList(column, true);
            //}
            for (int i = 0; i < originalColumnNames.Count; i++)
            {
                AddColumnToList(originalColumnNames[i], true);
            }

            if (!isNewTable && (newTableColumns.Count + originalColumnNames.Count) < TEXTBOX_MAX_NUMBER)
            {
                ButtonColumnAdd.Enabled = true;
            }
            originalTableName = dropdownTables.Text;
        }
        // ###################################################################################################

        // On Button click, lets selected table name in dropdown be changed
        private void ButtonEditTableName_Click(object sender, EventArgs e)
        {
            // If 
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


        // Disables/enables original table columns when editing a table
        private void TextBoxStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (!checkBox.Checked) // Enable linked TextBox
                originalTableColumns[checkBox].Enabled = true;
            else // Disable linked TextBox
                originalTableColumns[checkBox].Enabled = false;
        }
        // ###################################################################################################


        // Deletes related table column
        private void ColumnDeleteButton_Click(object sender, EventArgs e)
        {
            // Do not remove dictionary elements, just add their index to List<int> originalColumnsToDelete
            // make sure skip indexes that have a copy in this List<int>
            // subtract the List<int> Count() from the current column count

            Button deleteButton = sender as Button;
            int index = removeOriginalColumnButtonList.IndexOf(deleteButton);
            //originalColumnNames.RemoveAt(index);
            // 1. store column information in order to remove from Table.
            originalColumnsToDelete.Add(index);

            //CheckBox checkBox = originalTableColumns.ElementAt(index).Key;
            originalTableColumns.ElementAt(index).Key.Enabled = false;
            originalTableColumns.ElementAt(index).Key.Visible = false;
            originalTableColumns.ElementAt(index).Value.Enabled = false;
            originalTableColumns.ElementAt(index).Value.Visible = false;
            //originalTableColumns.ElementAt(index).Value.Dispose();
            //originalTableColumns.Remove(checkBox);
            //removeOriginalColumnButtonList.Remove(deleteButton);
            removeOriginalColumnButtonList[index].Enabled = false;
            removeOriginalColumnButtonList[index].Visible = false;
            //checkBox.Dispose();
            //deleteButton.Dispose();
        }
        // ###################################################################################################


        // On Button click, if category is selected call function to add textbox for new column.
        private void ButtonColumnAdd_Click(object sender, EventArgs e)
        {
            if(!isNewTable && dropdownTables.Text == "")
            {
                return;
            }
            // add new column
            AddColumnToList();
            // if number of columns meets TEXTBOX_MAX_NUMBER, disable add column button
            if (isNewTable && newTableColumns.Count == TEXTBOX_MAX_NUMBER)
            {
                ButtonColumnAdd.Enabled = false;
            } else if(!isNewTable && (newTableColumns.Count + originalColumnNames.Count) == TEXTBOX_MAX_NUMBER)
            {
                ButtonColumnAdd.Enabled = false;
            }
        }
        // ###################################################################################################


        // On Button click, call function to remove newest added column
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
