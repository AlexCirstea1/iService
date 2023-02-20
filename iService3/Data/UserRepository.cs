using iService3.Models;
using SQLite;

namespace iService3.Data
{
    public class UserRepository
    {
        private string dbPath;
        private SQLiteConnection conn;

        public UserRepository(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public void Init()
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<User>();
        }

        public List<User> GetAllUsers()
        {
            Init();
            return conn.Table<User>().ToList();
        }

        public void Add(User user)
        {
            conn = new SQLiteConnection(dbPath);
            conn.Insert(user);
        }

        public void Delete(int id)
        {
            conn = new SQLiteConnection(dbPath);
            conn.Delete(new User { Id = id });
        }
    }
}