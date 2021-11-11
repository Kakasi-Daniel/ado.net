using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.Models
{
    [Table("Movies")]
    public class MovieModel : BaseModel
    {
 
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
