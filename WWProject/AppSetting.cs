using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WWProject
{
    internal class AppSetting
    {
        public static void ChangeConnectionString(string databaseName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //MessageBox.Show(config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString);
            config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString = @"Data Source=.\Databases\" + databaseName + @".db;Version=3;";
            config.ConnectionStrings.ConnectionStrings["Default"].ProviderName = "System.Data.sqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            

        }
    }
}
