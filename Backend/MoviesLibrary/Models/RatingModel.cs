using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }

    }
}
