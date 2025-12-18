using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorChecker.Models
{
    public class Door
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }

        public Door()
        {
            Name = string.Empty;
        }
    }
}
