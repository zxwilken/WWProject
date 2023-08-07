using System;
using System.Data.SQLite;
using System.Windows.Forms;


// Form used to start making new Database Entry
namespace WWProject
{
    public partial class NewEntryForm : Form
    {

        Editor editor;

        public NewEntryForm(Editor ed)
        {
            InitializeComponent();

            editor = ed;
            ComboBoxCategories.Items.AddRange(SqliteDataAccess.GetAllTables(false).ToArray());
        }

        // Sends information needed to make a new Database entry
        // Whether a text file will be connected
        // What category will it belong to
        // New Entry Name
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if(ComboBoxCategories.Text == "")
            {
                MessageBox.Show("Category Must Be Selected");
                return;
            }
            if(TextBoxEntryName.Text == "")
            {
                MessageBox.Show("Entry Name Must Be Entered");
                return;
            }
            bool badTypedInComboBox = true;
            foreach(string item in ComboBoxCategories.Items)
            {
                if(item == ComboBoxCategories.Text)
                {
                    badTypedInComboBox = false;
                    break;
                }
            }
            if (badTypedInComboBox)
            {
                MessageBox.Show("Not a valid category.");
                return;
            }
            // Checks that Entered name does not exist as a category name
            foreach(string table in SqliteDataAccess.GetAllTables(true))
            {
                if(TextBoxEntryName.Text == table)
                {
                    MessageBox.Show("Entry Name: " + table + "\nAlready exists as a category name. Please select another.");
                    return;
                }
            }
            // Checks that entered Entry name does not already exist in category
            foreach (string entry in SqliteDataAccess.GetAllTableEntries(ComboBoxCategories.Text))
            {
                if (TextBoxEntryName.Text == entry)
                {
                    MessageBox.Show("Entry Name: " + entry + "\nIs already used in this category. Please select another.");
                    return;
                }
            }
            editor.GetNewEntryValues(CheckBoxAddFile.Checked, ComboBoxCategories.Text,TextBoxEntryName.Text);
            this.Close();
        }
        // ###################################################################################################

        // Reactive Editor Form upon NewEntryForm closing
        private void NewEntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            editor.Enabled = true;
        }
    }
}
