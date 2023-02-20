using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iService3.Models
{
    [Table("user")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
