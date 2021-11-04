using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Models
{
    [Table("Ratings")]
    public class RatingModel : BaseModel
    {
        public int MovieId { get; set; }
        public int Rating { get; set; }

    }
}
