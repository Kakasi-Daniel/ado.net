using System.ComponentModel.DataAnnotations;

namespace MoviesLibrary.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
