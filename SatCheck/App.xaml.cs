using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignColors;
using MaterialDesignThemes;
using SatCheck.Services;
using System.IO;


namespace SatCheck
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        static DatabaseService database;
        
        public static DatabaseService Database
        {
            get
            {
                if (database == null)
                {
                    string db = "satData.db";
                    string fullPath;
                    fullPath = System.IO.Path.GetFullPath(db);
                                        
                    database = new DatabaseService(fullPath);
                }
                return database;
            }
        }
    }
}
