using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XamarinProyectoFinal.Models;

namespace XamarinProyectoFinal.Database
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_app.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Mood>().Wait();
            _database.CreateTableAsync<Reminder>().Wait();
            _database.CreateTableAsync<Resource>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }

        // CRUD para Mood
        public Task<List<Mood>> GetMoodsAsync()
        {
            return _database.Table<Mood>().ToListAsync();
        }

        public Task<int> SaveMoodAsync(Mood mood)
        {
            if (mood.Id != 0)
            {
                return _database.UpdateAsync(mood);
            }
            else
            {
                return _database.InsertAsync(mood);
            }
        }

        public Task<int> DeleteMoodAsync(Mood mood)
        {
            return _database.DeleteAsync(mood);
        }

        // CRUD para Reminder
        public Task<List<Reminder>> GetRemindersAsync()
        {
            return _database.Table<Reminder>().ToListAsync();
        }

        public Task<int> SaveReminderAsync(Reminder reminder)
        {
            if (reminder.Id != 0)
            {
                return _database.UpdateAsync(reminder);
            }
            else
            {
                return _database.InsertAsync(reminder);
            }
        }

        public Task<int> DeleteReminderAsync(Reminder reminder)
        {
            return _database.DeleteAsync(reminder);
        }

        // CRUD para Resource
        public Task<List<Resource>> GetResourcesAsync()
        {
            return _database.Table<Resource>().ToListAsync();
        }

        public Task<int> SaveResourceAsync(Resource resource)
        {
            if (resource.Id != 0)
            {
                return _database.UpdateAsync(resource);
            }
            else
            {
                return _database.InsertAsync(resource);
            }
        }

        public Task<int> DeleteResourceAsync(Resource resource)
        {
            return _database.DeleteAsync(resource);
        }

        // CRUD para User
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            return _database.Table<User>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }
    }
}
