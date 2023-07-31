namespace WWProject
{
    partial class StartUp
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TextboxNewDB = new System.Windows.Forms.TextBox();
            this.ButtonDeleteDatabase = new System.Windows.Forms.Button();
            this.ButtonNewDatabase = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.LabelDatabases = new System.Windows.Forms.Label();
            this.ListViewDatabases = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.56813F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.21138F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.78862F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 399F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(383, 523);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LabelTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 41);
            this.panel1.TabIndex = 0;
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.Location = new System.Drawing.Point(94, 6);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LabelTitle.Size = new System.Drawing.Size(175, 31);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "World Writer";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TextboxNewDB);
            this.panel2.Controls.Add(this.ButtonDeleteDatabase);
            this.panel2.Controls.Add(this.ButtonNewDatabase);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 70);
            this.panel2.TabIndex = 1;
            // 
            // TextboxNewDB
            // 
            this.TextboxNewDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxNewDB.Location = new System.Drawing.Point(143, 8);
            this.TextboxNewDB.Name = "TextboxNewDB";
            this.TextboxNewDB.Size = new System.Drawing.Size(225, 26);
            this.TextboxNewDB.TabIndex = 2;
            // 
            // ButtonDeleteDatabase
            // 
            this.ButtonDeleteDatabase.Enabled = false;
            this.ButtonDeleteDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDeleteDatabase.Location = new System.Drawing.Point(9, 40);
            this.ButtonDeleteDatabase.Name = "ButtonDeleteDatabase";
            this.ButtonDeleteDatabase.Size = new System.Drawing.Size(128, 26);
            this.ButtonDeleteDatabase.TabIndex = 1;
            this.ButtonDeleteDatabase.Text = "Delete Database";
            this.ButtonDeleteDatabase.UseVisualStyleBackColor = true;
            this.ButtonDeleteDatabase.Click += new System.EventHandler(this.ButtonDeleteDatabase_Click);
            // 
            // ButtonNewDatabase
            // 
            this.ButtonNewDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNewDatabase.Location = new System.Drawing.Point(9, 8);
            this.ButtonNewDatabase.Name = "ButtonNewDatabase";
            this.ButtonNewDatabase.Size = new System.Drawing.Size(128, 26);
            this.ButtonNewDatabase.TabIndex = 0;
            this.ButtonNewDatabase.Text = "New Database";
            this.ButtonNewDatabase.UseVisualStyleBackColor = true;
            this.ButtonNewDatabase.Click += new System.EventHandler(this.ButtonNewDatabase_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ButtonOpen);
            this.panel3.Controls.Add(this.LabelDatabases);
            this.panel3.Controls.Add(this.ListViewDatabases);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 126);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(377, 394);
            this.panel3.TabIndex = 2;
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpen.Location = new System.Drawing.Point(151, 361);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(75, 30);
            this.ButtonOpen.TabIndex = 1;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // LabelDatabases
            // 
            this.LabelDatabases.AutoSize = true;
            this.LabelDatabases.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDatabases.Location = new System.Drawing.Point(147, 11);
            this.LabelDatabases.Name = "LabelDatabases";
            this.LabelDatabases.Size = new System.Drawing.Size(87, 20);
            this.LabelDatabases.TabIndex = 0;
            this.LabelDatabases.Text = "Databases";
            // 
            // ListViewDatabases
            // 
            this.ListViewDatabases.HideSelection = false;
            this.ListViewDatabases.Location = new System.Drawing.Point(68, 34);
            this.ListViewDatabases.MultiSelect = false;
            this.ListViewDatabases.Name = "ListViewDatabases";
            this.ListViewDatabases.Size = new System.Drawing.Size(244, 321);
            this.ListViewDatabases.TabIndex = 0;
            this.ListViewDatabases.UseCompatibleStateImageBehavior = false;
            this.ListViewDatabases.View = System.Windows.Forms.View.Tile;
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 523);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StartUp";
            this.Text = "StartUp";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LabelDatabases;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView ListViewDatabases;
        private System.Windows.Forms.Button ButtonDeleteDatabase;
        private System.Windows.Forms.Button ButtonNewDatabase;
        private System.Windows.Forms.TextBox TextboxNewDB;
        private System.Windows.Forms.Button ButtonOpen;
    }
}