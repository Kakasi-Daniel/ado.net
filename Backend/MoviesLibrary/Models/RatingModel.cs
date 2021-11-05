using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.Models
{
    [Table("Ratings")]
    public class RatingModel : BaseModel
    {
        public int MovieId { get; set; }
        public int Rating { get; set; }
    }
}
