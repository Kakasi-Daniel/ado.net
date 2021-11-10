using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.DTOs
{
    public class RoleOutWithNames
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        
    }
}
