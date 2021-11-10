using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.DTOs
{
    public class RoleOut
    {
        [Column(Order = 1)]
        public int Id { get; set; }
        [Column(Order = 2)]
        public string Name { get; set; }
        [Column(Order = 3)]
        public int ActorId { get; set; }
        [Column(Order = 5)]
        public int MovieId { get; set; }
    }
}
