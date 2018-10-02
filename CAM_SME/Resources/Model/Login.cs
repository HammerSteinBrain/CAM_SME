using SQLite;

namespace LoginSQLite{
    public class Login{
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(25)]
        public string usuario { get; set; }

        [MaxLength(15)]
        public string senha { get; set; }

    }
}