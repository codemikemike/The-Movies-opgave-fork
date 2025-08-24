using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movie.Models
{
    internal class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public MovieGenre Genre { get; set; }
    }
}