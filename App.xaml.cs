using NomenDeutsch.Data;

namespace NomenDeutsch
{
    public partial class App : Application
    {
        static NounNeuDatabase database = null!;
        static string dbPath = string.Empty;

        public static NounNeuDatabase Database
        {
            get
            {
                database ??= new NounNeuDatabase(dbPath);
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            dbPath = Path.Combine(FileSystem.AppDataDirectory, "noun.db");
            CopyDatabaseIfNeeded("noun.db").Wait();
        }

        static async Task CopyDatabaseIfNeeded(string dbName)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

            if (!File.Exists(dbPath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(dbName);
                using var newStream = File.Create(dbPath);
                await stream.CopyToAsync(newStream);
            }
        }
    }
}