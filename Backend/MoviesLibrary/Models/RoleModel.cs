using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLibrary.Models
{
    [Table("Roles")]
    public class RoleModel : BaseModel
    {
        public string Name { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }
    }
}
