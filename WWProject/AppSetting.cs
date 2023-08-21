using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Updates App.config connectionString to the one for user chosen database
namespace WWProject
{
    internal class AppSetting
    {
        // Changes the 'Default' ConnectionString in the config file to the user chosen database. Called in StartUp.
        public static void ChangeConnectionString(string databaseName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //MessageBox.Show(config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString);
            config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString = @"Data Source=.\Databases\" + databaseName + @".db;Version=3;";
            config.ConnectionStrings.ConnectionStrings["Default"].ProviderName = "System.Data.sqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            //MessageBox.Show(config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString);

        }

        // method that will clear connectionstring value
        public static void ClearConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString = "";
            config.ConnectionStrings.ConnectionStrings["Default"].ProviderName = "System.Data.sqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show(config.ConnectionStrings.ConnectionStrings["Default"].ConnectionString);
        }
    }
}
