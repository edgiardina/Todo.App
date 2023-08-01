using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Data
{
    public class TodoDatabase
    {
        public SQLiteAsyncConnection Catalog { get; set; }
        private readonly string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSqliteDb.db3");

        public TodoDatabase()
        {
            Catalog = new SQLiteAsyncConnection(databasePath);
            Catalog.CreateTableAsync<TodoItem>().Wait();
            Catalog.CreateTableAsync<TodoList>().Wait();
        }
    }
}
