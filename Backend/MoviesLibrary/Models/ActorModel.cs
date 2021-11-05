using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.Models
{
    [Table("Actors")]
    public class ActorModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
    }
}
