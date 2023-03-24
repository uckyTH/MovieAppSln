using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MovieApp.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public String Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        public String ImgPath { get; set; }

    }
}
