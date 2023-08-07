using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWProject
{
    // Helper class for user inputs
    internal class UserInputHelper
    {

        public static bool YesNoMessage(string message, string caption)
        {
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            // If Yes, delete selected database and file directory
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public static bool YesNoMessage()
        {
            string message = "Are you sure you want to continue?";
            string caption = "User Responce";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            // If Yes, delete selected database and file directory
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        // Checks If user input is made of standard characters.
        // ADD TO A SEPERATE CLASS
        public static bool CheckUserInput(string userInput)
        {
            if (userInput.All(char.IsLetterOrDigit))
            {
                return true;
            }
            else return false;
        }
        // ###################################################################################################
    }
}
