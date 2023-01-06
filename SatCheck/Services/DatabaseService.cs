using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using SatCheck.Models;
using System.Collections.ObjectModel;

namespace SatCheck.Services
{
    public class DatabaseService

    {
        readonly SQLite.SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Komponenty>().Wait();
        }

        public Task<List<Komponenty>> GetTaskAsync()
        {
            return _database.Table<Komponenty>().OrderBy(x => x.Id).ToListAsync();

        }

        







        public Task<int> SaveTaskAsync(Komponenty taskModel)
        {
            return _database.InsertAsync(taskModel);

        }

        public Task<int> UpdateTaskAsync(Komponenty taskModel)
        {
            return _database.UpdateAsync(taskModel);

        }

        public Task<int> DeleteTaskAsync(Komponenty taskModel)
        {
            return _database.DeleteAsync(taskModel);

        }



    }
}
