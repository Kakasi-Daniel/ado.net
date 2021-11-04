using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
