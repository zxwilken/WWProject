using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace WWProject
{
    internal class DataDisplayHelper
    {

        // Displays a entry's data
        public static Dictionary<string, RichTextBox> DynamDisplayEntries(List<string> columnNames, List<string> columnData,Panel panel)
        {
            Dictionary<string,RichTextBox> entryData = new Dictionary<string,RichTextBox>();
            Font newFont = new Font("Segoe UI", 12);
            int labelX = 50;
            int textBoxX = 150;
            int contentY = 40;


            for (int i = 3; i < columnNames.Count; i++)
            {
                Label label = new Label();
                label.Font = newFont; 
                label.Text = columnNames[i];
                label.Size = new Size(100, 50);
                label.Location = new Point(labelX, contentY);

                RichTextBox richText = new RichTextBox();
                richText.Font = newFont;
                if(columnData.Count() <= i || columnData[i] == "")
                    richText.Text = "";
                else richText.Text = columnData[i];
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

                entryData.Add(label.Text, richText);

                panel.Controls.Add(label);
                panel.Controls.Add(richText);

                contentY = richText.Bottom + 35;
            }

            panel.Show();
            return entryData;
        }


        // creates a basic textbox to use 
        public static TextBox CreateTextBox(string tableName = "") { 
            TextBox textBox = new TextBox();
            textBox.Font = new Font("Segoe UI", 12);
            textBox.Text = tableName;
            textBox.MinimumSize = new Size(160,0);
            textBox.MaximumSize = new Size(160, 28);
            textBox.Anchor = AnchorStyles.None;
            return textBox;
        }


    }
}
