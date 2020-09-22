using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Models
{
    class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Ignore]
        public int Classement { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
