﻿namespace WWProject
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PanelTopLeft = new System.Windows.Forms.Panel();
            this.LabelCategories = new System.Windows.Forms.Label();
            this.PanelCategoryTables = new System.Windows.Forms.Panel();
            this.TextboxSearch = new System.Windows.Forms.TextBox();
            this.ListViewEntries = new System.Windows.Forms.ListView();
            this.ComboBoxCategories = new System.Windows.Forms.ComboBox();
            this.PanelTextBox = new System.Windows.Forms.Panel();
            this.RichTextBoxMain = new System.Windows.Forms.RichTextBox();
            this.PanelTopMid = new System.Windows.Forms.Panel();
            this.LabelEntryName = new System.Windows.Forms.Label();
            this.PanelTopRight = new System.Windows.Forms.Panel();
            this.LabelDatabase = new System.Windows.Forms.Label();
            this.PanelDatabase = new System.Windows.Forms.Panel();
            this.PanelBottomMid = new System.Windows.Forms.Panel();
            this.ButtonAddTextFile = new System.Windows.Forms.Button();
            this.ButtonDeleteTextFile = new System.Windows.Forms.Button();
            this.ButtonSaveTextFile = new System.Windows.Forms.Button();
            this.PanelBottomRight = new System.Windows.Forms.Panel();
            this.ButtonDeleteDBEntry = new System.Windows.Forms.Button();
            this.ButtonNewDBEntry = new System.Windows.Forms.Button();
            this.ButtonSaveDBEntry = new System.Windows.Forms.Button();
            this.PanelBottomLeft = new System.Windows.Forms.Panel();
            this.ButtonEditTable = new System.Windows.Forms.Button();
            this.ButtonNewCategory = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.PanelTopLeft.SuspendLayout();
            this.PanelCategoryTables.SuspendLayout();
            this.PanelTextBox.SuspendLayout();
            this.PanelTopMid.SuspendLayout();
            this.PanelTopRight.SuspendLayout();
            this.PanelBottomMid.SuspendLayout();
            this.PanelBottomRight.SuspendLayout();
            this.PanelBottomLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.37255F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.62745F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 455F));
            this.tableLayoutPanel1.Controls.Add(this.PanelTopLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PanelCategoryTables, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PanelTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.PanelTopMid, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.PanelTopRight, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.PanelDatabase, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.PanelBottomMid, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.PanelBottomRight, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.PanelBottomLeft, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.1136F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.88641F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1501, 617);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PanelTopLeft
            // 
            this.PanelTopLeft.Controls.Add(this.LabelCategories);
            this.PanelTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTopLeft.Location = new System.Drawing.Point(3, 3);
            this.PanelTopLeft.Name = "PanelTopLeft";
            this.PanelTopLeft.Size = new System.Drawing.Size(217, 76);
            this.PanelTopLeft.TabIndex = 0;
            // 
            // LabelCategories
            // 
            this.LabelCategories.AutoSize = true;
            this.LabelCategories.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCategories.Location = new System.Drawing.Point(57, 46);
            this.LabelCategories.Name = "LabelCategories";
            this.LabelCategories.Size = new System.Drawing.Size(106, 25);
            this.LabelCategories.TabIndex = 0;
            this.LabelCategories.Text = "Categories";
            // 
            // PanelCategoryTables
            // 
            this.PanelCategoryTables.Controls.Add(this.TextboxSearch);
            this.PanelCategoryTables.Controls.Add(this.ListViewEntries);
            this.PanelCategoryTables.Controls.Add(this.ComboBoxCategories);
            this.PanelCategoryTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelCategoryTables.Location = new System.Drawing.Point(3, 85);
            this.PanelCategoryTables.Name = "PanelCategoryTables";
            this.PanelCategoryTables.Size = new System.Drawing.Size(217, 493);
            this.PanelCategoryTables.TabIndex = 1;
            // 
            // TextboxSearch
            // 
            this.TextboxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TextboxSearch.Location = new System.Drawing.Point(12, 18);
            this.TextboxSearch.Name = "TextboxSearch";
            this.TextboxSearch.Size = new System.Drawing.Size(194, 26);
            this.TextboxSearch.TabIndex = 2;
            this.TextboxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxSearch_KeyPress);
            // 
            // ListViewEntries
            // 
            this.ListViewEntries.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListViewEntries.HideSelection = false;
            this.ListViewEntries.Location = new System.Drawing.Point(9, 87);
            this.ListViewEntries.MultiSelect = false;
            this.ListViewEntries.Name = "ListViewEntries";
            this.ListViewEntries.Size = new System.Drawing.Size(197, 403);
            this.ListViewEntries.TabIndex = 1;
            this.ListViewEntries.UseCompatibleStateImageBehavior = false;
            this.ListViewEntries.View = System.Windows.Forms.View.List;
            this.ListViewEntries.DoubleClick += new System.EventHandler(this.ListViewEntries_DoubleClick);
            // 
            // ComboBoxCategories
            // 
            this.ComboBoxCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxCategories.FormattingEnabled = true;
            this.ComboBoxCategories.Location = new System.Drawing.Point(9, 50);
            this.ComboBoxCategories.Name = "ComboBoxCategories";
            this.ComboBoxCategories.Size = new System.Drawing.Size(197, 28);
            this.ComboBoxCategories.TabIndex = 0;
            this.ComboBoxCategories.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCategories_SelectedIndexChanged);
            this.ComboBoxCategories.TextChanged += new System.EventHandler(this.ComboBoxCategories_TextChanged);
            // 
            // PanelTextBox
            // 
            this.PanelTextBox.Controls.Add(this.RichTextBoxMain);
            this.PanelTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTextBox.Location = new System.Drawing.Point(226, 85);
            this.PanelTextBox.Name = "PanelTextBox";
            this.PanelTextBox.Size = new System.Drawing.Size(816, 493);
            this.PanelTextBox.TabIndex = 2;
            // 
            // RichTextBoxMain
            // 
            this.RichTextBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBoxMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBoxMain.Location = new System.Drawing.Point(0, 0);
            this.RichTextBoxMain.Name = "RichTextBoxMain";
            this.RichTextBoxMain.Size = new System.Drawing.Size(816, 493);
            this.RichTextBoxMain.TabIndex = 0;
            this.RichTextBoxMain.Text = "";
            // 
            // PanelTopMid
            // 
            this.PanelTopMid.Controls.Add(this.LabelEntryName);
            this.PanelTopMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTopMid.Location = new System.Drawing.Point(226, 3);
            this.PanelTopMid.Name = "PanelTopMid";
            this.PanelTopMid.Size = new System.Drawing.Size(816, 76);
            this.PanelTopMid.TabIndex = 3;
            // 
            // LabelEntryName
            // 
            this.LabelEntryName.AutoSize = true;
            this.LabelEntryName.Font = new System.Drawing.Font("Segoe UI", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelEntryName.Location = new System.Drawing.Point(377, 36);
            this.LabelEntryName.Name = "LabelEntryName";
            this.LabelEntryName.Size = new System.Drawing.Size(73, 37);
            this.LabelEntryName.TabIndex = 1;
            this.LabelEntryName.Text = "        ";
            // 
            // PanelTopRight
            // 
            this.PanelTopRight.Controls.Add(this.LabelDatabase);
            this.PanelTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTopRight.Location = new System.Drawing.Point(1048, 3);
            this.PanelTopRight.Name = "PanelTopRight";
            this.PanelTopRight.Size = new System.Drawing.Size(450, 76);
            this.PanelTopRight.TabIndex = 4;
            // 
            // LabelDatabase
            // 
            this.LabelDatabase.AutoSize = true;
            this.LabelDatabase.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDatabase.Location = new System.Drawing.Point(192, 48);
            this.LabelDatabase.Name = "LabelDatabase";
            this.LabelDatabase.Size = new System.Drawing.Size(93, 25);
            this.LabelDatabase.TabIndex = 1;
            this.LabelDatabase.Text = "Database";
            // 
            // PanelDatabase
            // 
            this.PanelDatabase.AutoSize = true;
            this.PanelDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelDatabase.Location = new System.Drawing.Point(1048, 85);
            this.PanelDatabase.Name = "PanelDatabase";
            this.PanelDatabase.Size = new System.Drawing.Size(0, 0);
            this.PanelDatabase.TabIndex = 20;
            // 
            // PanelBottomMid
            // 
            this.PanelBottomMid.Controls.Add(this.ButtonAddTextFile);
            this.PanelBottomMid.Controls.Add(this.ButtonDeleteTextFile);
            this.PanelBottomMid.Controls.Add(this.ButtonSaveTextFile);
            this.PanelBottomMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBottomMid.Location = new System.Drawing.Point(226, 584);
            this.PanelBottomMid.Name = "PanelBottomMid";
            this.PanelBottomMid.Size = new System.Drawing.Size(816, 30);
            this.PanelBottomMid.TabIndex = 6;
            // 
            // ButtonAddTextFile
            // 
            this.ButtonAddTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAddTextFile.Location = new System.Drawing.Point(191, 0);
            this.ButtonAddTextFile.Name = "ButtonAddTextFile";
            this.ButtonAddTextFile.Size = new System.Drawing.Size(110, 30);
            this.ButtonAddTextFile.TabIndex = 3;
            this.ButtonAddTextFile.Text = "Add File";
            this.ButtonAddTextFile.UseVisualStyleBackColor = true;
            this.ButtonAddTextFile.Click += new System.EventHandler(this.ButtonAddTextFile_Click);
            // 
            // ButtonDeleteTextFile
            // 
            this.ButtonDeleteTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDeleteTextFile.Location = new System.Drawing.Point(414, 0);
            this.ButtonDeleteTextFile.Name = "ButtonDeleteTextFile";
            this.ButtonDeleteTextFile.Size = new System.Drawing.Size(75, 30);
            this.ButtonDeleteTextFile.TabIndex = 2;
            this.ButtonDeleteTextFile.Text = "Delete";
            this.ButtonDeleteTextFile.UseVisualStyleBackColor = true;
            this.ButtonDeleteTextFile.Click += new System.EventHandler(this.ButtonDeleteTextFile_Click);
            // 
            // ButtonSaveTextFile
            // 
            this.ButtonSaveTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSaveTextFile.Location = new System.Drawing.Point(564, 0);
            this.ButtonSaveTextFile.Name = "ButtonSaveTextFile";
            this.ButtonSaveTextFile.Size = new System.Drawing.Size(75, 30);
            this.ButtonSaveTextFile.TabIndex = 0;
            this.ButtonSaveTextFile.Text = "Save";
            this.ButtonSaveTextFile.UseVisualStyleBackColor = true;
            this.ButtonSaveTextFile.Click += new System.EventHandler(this.ButtonSaveTextFile_Click);
            // 
            // PanelBottomRight
            // 
            this.PanelBottomRight.Controls.Add(this.ButtonDeleteDBEntry);
            this.PanelBottomRight.Controls.Add(this.ButtonNewDBEntry);
            this.PanelBottomRight.Controls.Add(this.ButtonSaveDBEntry);
            this.PanelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBottomRight.Location = new System.Drawing.Point(1048, 584);
            this.PanelBottomRight.Name = "PanelBottomRight";
            this.PanelBottomRight.Size = new System.Drawing.Size(450, 30);
            this.PanelBottomRight.TabIndex = 7;
            // 
            // ButtonDeleteDBEntry
            // 
            this.ButtonDeleteDBEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDeleteDBEntry.Location = new System.Drawing.Point(210, 0);
            this.ButtonDeleteDBEntry.Name = "ButtonDeleteDBEntry";
            this.ButtonDeleteDBEntry.Size = new System.Drawing.Size(75, 30);
            this.ButtonDeleteDBEntry.TabIndex = 4;
            this.ButtonDeleteDBEntry.Text = "Delete";
            this.ButtonDeleteDBEntry.UseVisualStyleBackColor = true;
            this.ButtonDeleteDBEntry.Click += new System.EventHandler(this.ButtonDeleteDBEntry_Click);
            // 
            // ButtonNewDBEntry
            // 
            this.ButtonNewDBEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNewDBEntry.Location = new System.Drawing.Point(33, 0);
            this.ButtonNewDBEntry.Name = "ButtonNewDBEntry";
            this.ButtonNewDBEntry.Size = new System.Drawing.Size(80, 30);
            this.ButtonNewDBEntry.TabIndex = 1;
            this.ButtonNewDBEntry.Text = "New Entry";
            this.ButtonNewDBEntry.UseVisualStyleBackColor = true;
            this.ButtonNewDBEntry.Click += new System.EventHandler(this.ButtonNewDBEntry_Click);
            // 
            // ButtonSaveDBEntry
            // 
            this.ButtonSaveDBEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSaveDBEntry.Location = new System.Drawing.Point(346, 0);
            this.ButtonSaveDBEntry.Name = "ButtonSaveDBEntry";
            this.ButtonSaveDBEntry.Size = new System.Drawing.Size(75, 30);
            this.ButtonSaveDBEntry.TabIndex = 3;
            this.ButtonSaveDBEntry.Text = "Save";
            this.ButtonSaveDBEntry.UseVisualStyleBackColor = true;
            this.ButtonSaveDBEntry.Click += new System.EventHandler(this.ButtonSaveDBEntry_Click);
            // 
            // PanelBottomLeft
            // 
            this.PanelBottomLeft.Controls.Add(this.ButtonEditTable);
            this.PanelBottomLeft.Controls.Add(this.ButtonNewCategory);
            this.PanelBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBottomLeft.Location = new System.Drawing.Point(3, 584);
            this.PanelBottomLeft.Name = "PanelBottomLeft";
            this.PanelBottomLeft.Size = new System.Drawing.Size(217, 30);
            this.PanelBottomLeft.TabIndex = 8;
            // 
            // ButtonEditTable
            // 
            this.ButtonEditTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonEditTable.Location = new System.Drawing.Point(128, 0);
            this.ButtonEditTable.Name = "ButtonEditTable";
            this.ButtonEditTable.Size = new System.Drawing.Size(87, 30);
            this.ButtonEditTable.TabIndex = 2;
            this.ButtonEditTable.Text = "Edit Table";
            this.ButtonEditTable.UseVisualStyleBackColor = true;
            this.ButtonEditTable.Click += new System.EventHandler(this.ButtonEditTable_Click);
            // 
            // ButtonNewCategory
            // 
            this.ButtonNewCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNewCategory.Location = new System.Drawing.Point(3, 0);
            this.ButtonNewCategory.Name = "ButtonNewCategory";
            this.ButtonNewCategory.Size = new System.Drawing.Size(110, 30);
            this.ButtonNewCategory.TabIndex = 2;
            this.ButtonNewCategory.Text = "New Category";
            this.ButtonNewCategory.UseVisualStyleBackColor = true;
            this.ButtonNewCategory.Click += new System.EventHandler(this.ButtonNewCategory_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1501, 617);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Editor";
            this.Text = "Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Editor_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.PanelTopLeft.ResumeLayout(false);
            this.PanelTopLeft.PerformLayout();
            this.PanelCategoryTables.ResumeLayout(false);
            this.PanelCategoryTables.PerformLayout();
            this.PanelTextBox.ResumeLayout(false);
            this.PanelTopMid.ResumeLayout(false);
            this.PanelTopMid.PerformLayout();
            this.PanelTopRight.ResumeLayout(false);
            this.PanelTopRight.PerformLayout();
            this.PanelBottomMid.ResumeLayout(false);
            this.PanelBottomRight.ResumeLayout(false);
            this.PanelBottomLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel PanelTopLeft;
        private System.Windows.Forms.Label LabelCategories;
        private System.Windows.Forms.Panel PanelCategoryTables;
        private System.Windows.Forms.ListView ListViewEntries;
        private System.Windows.Forms.Panel PanelTextBox;
        private System.Windows.Forms.RichTextBox RichTextBoxMain;
        private System.Windows.Forms.Panel PanelTopMid;
        private System.Windows.Forms.Label LabelEntryName;
        private System.Windows.Forms.Panel PanelTopRight;
        private System.Windows.Forms.Label LabelDatabase;
        private System.Windows.Forms.Panel PanelDatabase;
        private System.Windows.Forms.Panel PanelBottomMid;
        private System.Windows.Forms.Button ButtonDeleteTextFile;
        private System.Windows.Forms.Button ButtonNewDBEntry;
        private System.Windows.Forms.Button ButtonSaveTextFile;
        private System.Windows.Forms.Panel PanelBottomRight;
        private System.Windows.Forms.Button ButtonDeleteDBEntry;
        private System.Windows.Forms.Button ButtonSaveDBEntry;
        private System.Windows.Forms.Panel PanelBottomLeft;
        private System.Windows.Forms.Button ButtonNewCategory;
        private System.Windows.Forms.Button ButtonAddTextFile;
        private System.Windows.Forms.Button ButtonEditTable;
        public System.Windows.Forms.ComboBox ComboBoxCategories;
        private System.Windows.Forms.TextBox TextboxSearch;
    }
}

