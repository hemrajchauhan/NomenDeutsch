using SQLite;
using NomenDeutsch.Models;

namespace NomenDeutsch.Data
{
    public class NounNeuDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NounNeuDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<NounNeu>().Wait();
        }

        public Task<List<NounNeu>> GetNounsAsync()
        {
            return database.Table<NounNeu>().ToListAsync();
        }
    }
}