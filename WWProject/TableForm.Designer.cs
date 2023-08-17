namespace WWProject
{
    partial class TableForm
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
            this.PanelContent = new System.Windows.Forms.Panel();
            this.LabelCategoryName = new System.Windows.Forms.Label();
            this.PanelName = new System.Windows.Forms.Panel();
            this.LabelTableName = new System.Windows.Forms.Label();
            this.ButtonSubmit = new System.Windows.Forms.Button();
            this.ButtonColumnAdd = new System.Windows.Forms.Button();
            this.ButtonColumnRemove = new System.Windows.Forms.Button();
            this.PanelSubmit = new System.Windows.Forms.Panel();
            this.PanelHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.PanelContent.SuspendLayout();
            this.PanelName.SuspendLayout();
            this.PanelSubmit.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(211)))), ((int)(((byte)(195)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PanelSubmit, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.PanelContent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.PanelName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PanelHeader, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.201915F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.25975F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.53833F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 565);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PanelContent
            // 
            this.PanelContent.AutoScroll = true;
            this.PanelContent.Controls.Add(this.LabelCategoryName);
            this.PanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContent.Location = new System.Drawing.Point(0, 89);
            this.PanelContent.Margin = new System.Windows.Forms.Padding(0);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(397, 424);
            this.PanelContent.TabIndex = 0;
            // 
            // LabelCategoryName
            // 
            this.LabelCategoryName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelCategoryName.AutoSize = true;
            this.LabelCategoryName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.LabelCategoryName.Location = new System.Drawing.Point(130, -61);
            this.LabelCategoryName.Name = "LabelCategoryName";
            this.LabelCategoryName.Size = new System.Drawing.Size(151, 25);
            this.LabelCategoryName.TabIndex = 1;
            this.LabelCategoryName.Text = "Category Name";
            // 
            // PanelName
            // 
            this.PanelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(110)))), ((int)(((byte)(121)))));
            this.PanelName.Controls.Add(this.LabelTableName);
            this.PanelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelName.Location = new System.Drawing.Point(0, 0);
            this.PanelName.Margin = new System.Windows.Forms.Padding(0);
            this.PanelName.Name = "PanelName";
            this.PanelName.Size = new System.Drawing.Size(397, 26);
            this.PanelName.TabIndex = 2;
            // 
            // LabelTableName
            // 
            this.LabelTableName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelTableName.AutoSize = true;
            this.LabelTableName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTableName.Location = new System.Drawing.Point(150, 0);
            this.LabelTableName.Name = "LabelTableName";
            this.LabelTableName.Size = new System.Drawing.Size(115, 25);
            this.LabelTableName.TabIndex = 0;
            this.LabelTableName.Text = "Table Name";
            // 
            // ButtonSubmit
            // 
            this.ButtonSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSubmit.Location = new System.Drawing.Point(137, 5);
            this.ButtonSubmit.Name = "ButtonSubmit";
            this.ButtonSubmit.Size = new System.Drawing.Size(114, 36);
            this.ButtonSubmit.TabIndex = 0;
            this.ButtonSubmit.Text = "Submit";
            this.ButtonSubmit.UseVisualStyleBackColor = true;
            this.ButtonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // ButtonColumnAdd
            // 
            this.ButtonColumnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonColumnAdd.Location = new System.Drawing.Point(30, 14);
            this.ButtonColumnAdd.Name = "ButtonColumnAdd";
            this.ButtonColumnAdd.Size = new System.Drawing.Size(88, 26);
            this.ButtonColumnAdd.TabIndex = 1;
            this.ButtonColumnAdd.Text = "+ Column";
            this.ButtonColumnAdd.UseVisualStyleBackColor = true;
            this.ButtonColumnAdd.Click += new System.EventHandler(this.ButtonColumnAdd_Click);
            // 
            // ButtonColumnRemove
            // 
            this.ButtonColumnRemove.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonColumnRemove.Location = new System.Drawing.Point(285, 14);
            this.ButtonColumnRemove.Name = "ButtonColumnRemove";
            this.ButtonColumnRemove.Size = new System.Drawing.Size(88, 26);
            this.ButtonColumnRemove.TabIndex = 2;
            this.ButtonColumnRemove.Text = "- Column";
            this.ButtonColumnRemove.UseVisualStyleBackColor = true;
            this.ButtonColumnRemove.Click += new System.EventHandler(this.ButtonColumnRemove_Click);
            // 
            // PanelSubmit
            // 
            this.PanelSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(177)))), ((int)(((byte)(152)))));
            this.PanelSubmit.Controls.Add(this.ButtonColumnRemove);
            this.PanelSubmit.Controls.Add(this.ButtonColumnAdd);
            this.PanelSubmit.Controls.Add(this.ButtonSubmit);
            this.PanelSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSubmit.Location = new System.Drawing.Point(0, 513);
            this.PanelSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.PanelSubmit.Name = "PanelSubmit";
            this.PanelSubmit.Size = new System.Drawing.Size(397, 52);
            this.PanelSubmit.TabIndex = 1;
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(177)))), ((int)(((byte)(152)))));
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Location = new System.Drawing.Point(0, 26);
            this.PanelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(397, 63);
            this.PanelHeader.TabIndex = 3;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(413, 604);
            this.MinimumSize = new System.Drawing.Size(413, 604);
            this.Name = "TableForm";
            this.Text = "TableForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.PanelContent.ResumeLayout(false);
            this.PanelContent.PerformLayout();
            this.PanelName.ResumeLayout(false);
            this.PanelName.PerformLayout();
            this.PanelSubmit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel PanelContent;
        private System.Windows.Forms.Panel PanelName;
        private System.Windows.Forms.Label LabelTableName;
        private System.Windows.Forms.Label LabelCategoryName;
        private System.Windows.Forms.Panel PanelSubmit;
        private System.Windows.Forms.Button ButtonColumnRemove;
        private System.Windows.Forms.Button ButtonColumnAdd;
        private System.Windows.Forms.Button ButtonSubmit;
        private System.Windows.Forms.Panel PanelHeader;
    }
}