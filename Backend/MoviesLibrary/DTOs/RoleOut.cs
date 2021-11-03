using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.DTOs
{
    public class RoleOut
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }
    }
}
